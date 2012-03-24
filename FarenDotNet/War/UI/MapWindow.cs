using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Paraiba.Diagnostics;
using Paraiba.Drawing;
using Paraiba.Drawing.Animations.Surfaces;
using Paraiba.Drawing.Animations.Surfaces.Sprites;
using Paraiba.Drawing.Surfaces;
using Paraiba.Core;
using Paraiba.Geometry;
using Paraiba.Linq;
using Paraiba.TaskList;
using Paraiba.Windows.Forms;

namespace FarenDotNet.War.UI
{
	public partial class MapWindow : DockingWindow
	{
		public const int ATTACK_EFFECT_TIME = 200;
		public const int DAMAGE_EFFECT_TIME = 200;
		private static readonly Font _damageFont = new Font("ＭＳ ゴシック", 18, FontStyle.Bold);

		private readonly WarPresentationModel _model;

		/// <summary>
		/// マップチップが１行変わる度にずれるX座標のピクセル
		/// </summary>
		private readonly int _offsetX;

		/// <summary>
		/// マップチップのサイズ
		/// </summary>
		private Size _chipPixelSize;

		private DrawableControl _mapPanel;

		public MapWindow(WarPresentationModel model)
		{
			Contract.Requires(model != null);

			model.MapWindow = this;

			// フィールドの初期化
			_model = model;
			_chipPixelSize = WarGlobal.ChipSize;
			_offsetX = -_chipPixelSize.Width / 2;

			InitializeComponent();
			InitializeMapPanel();

			// イベント等の追加
			Global.MainLoop.TickEvents.Add(0, ElapseEvent);
			Closed += (sender_, e_) =>
				Global.MainLoop.TickEvents.Remove(ElapseEvent);

			model.ScopeChangedEvent += scope => {
				Action<Point2> func = p => _mapPanel.Invalidate(false);
				if (scope != null)
					model.CursorChipPointChangedEvent += func;
				else
					model.CursorChipPointChangedEvent -= func;
				// スコープが変化したので再描画
				_mapPanel.Invalidate(false);
			};
			WarMap.Changed += _ => _mapPanel.Invalidate(false);

			model.Situation.ActiveUnitChanged += unit =>
				CenteringByChipPoint(unit.Location);
		}

		private Point2 CursorChipPoint
		{
			get { return _model.CursorChipPoint; }
			set { _model.CursorChipPoint = value; }
		}

		private WarMap WarMap
		{
			get { return _model.Situation.Map; }
		}

		private PrintableScope Scope
		{
			get { return _model.Scope; }
		}

		private void InitializeMapPanel()
		{
			SuspendLayout();

			// マップパネルの生成
			_mapPanel = new DrawableControl {
				Size = new Size(WarMap.Width * _chipPixelSize.Width - _chipPixelSize.Width / 2,
					WarMap.Height * _chipPixelSize.Height),
			};
			_mapPanel.DoubleClick += MapPanel_DoubleClick;
			_mapPanel.MouseMove += MapPanel_MouseMove;
			_mapPanel.MouseClick += MapPanel_MouseClick;
			_mapPanel.MouseEnter += MapPanel_MouseEnter;
			_mapPanel.MouseLeave += MapPanel_MouseLeave;
			_mapPanel.Paint += MapPanel_Paint;

			_scrollablePanel.Panel = _mapPanel;

			ResumeLayout(false);
		}

		private void CenteringByChipPoint(Point2 p)
		{
			p = ChipPoint2PixelCenterPoint(p);
			_scrollablePanel.Centering(p);
		}

		private Point2 ChipPoint2PixelPoint(Point2 p)
		{
			return new Point2(
				p.X * _chipPixelSize.Width + (p.Y + 1) * _offsetX,
				p.Y * _chipPixelSize.Height);
		}

		private Point2 ChipPoint2PixelCenterPoint(Point2 p)
		{
			return ChipPoint2PixelPoint(p)
				+ new Vector2(_chipPixelSize.Width / 2, _chipPixelSize.Height / 2);
		}

		private Point2 PixelPoint2ChipPoint(Point2 p)
		{
			int y = p.Y / _chipPixelSize.Height;
			int x = (p.X - (y + 1) * _offsetX) / _chipPixelSize.Width;
			return new Point2(x, y);
		}

		private void ElapseEvent(TaskArgs<int> taskArgs, int time)
		{
			var requiredRefresh = false;
			requiredRefresh |= _model.ChipAnimations.Elapse(time);
			requiredRefresh |= _model.ScreenAnimations.Elapse(time);
			if (requiredRefresh)
				_mapPanel.Invalidate(false);

			var unit = _model.Situation.ActiveUnit;
			if (unit != null)
				_mapPanel.Invalidate(new Rectangle(ChipPoint2PixelPoint(unit.Location), WarGlobal.ChipSize));
		}

		/// <summary>
		/// 指定したマップを描画する
		/// </summary>
		/// <param name="e">Paint イベントのデータ</param>
		/// <param name="map">描画するマップ</param>
		public void DrawMap(PaintEventArgs e, WarMap map)
		{
			// 描画範囲の計算
			var g = e.Graphics;
			var rect = e.ClipRectangle;

			int y = rect.Top / _chipPixelSize.Height;
			int endy = (rect.Bottom - 1) / _chipPixelSize.Height;

			for (; y <= endy; y++)
			{
				int gap = _offsetX * (y + 1);
				int x = (rect.Left - gap) / _chipPixelSize.Width;
				int endx = (rect.Right - 1 - gap) / _chipPixelSize.Width;
				for (; x <= endx; x++)
				{
					var mapchip = map[x, y];
					var p = ChipPoint2PixelPoint(new Point2(x, y));
					// 地形描画
					g.DrawSurface(mapchip.Image, p);
					// 枠描画
					//g.DrawRectangle(Pens.Red, new Rectangle(p, _chipPixelSize));

					// ユニット描画
					var unit = mapchip.Unit;
					if (unit != null && unit.Visible)
					{
						g.DrawSurface(unit.ChipImage, p);
						var r1 = new Rectangle(p + new Vector2(2, 2), new Size(6, 6));
						var r2 = new Rectangle(p + new Vector2(2, 2), new Size(6, 6));
						if (unit.Side.IsPlayer)
							g.FillEllipse(Brushes.Blue, r1);
						else
							g.FillEllipse(Brushes.Red, r1);
						g.DrawEllipse(Pens.White, r2);
					}
				}
			}

			// 現在行動しているユニットがわかるように表示
			if (_model.Situation.ActiveUnit != null)
			{
				var alpha = Global.MainLoop.FrameCount * Global.MainLoop.Fps / 5 % 256;
				if (alpha > 128) alpha = 256 - alpha;
				g.FillRectangle(new SolidBrush(Color.FromArgb(alpha, 255, 255, 0)),
					new Rectangle(ChipPoint2PixelPoint(_model.Situation.ActiveUnit.Location), _chipPixelSize));
			}
		}

		/// <summary>
		/// 初期配置候補を描画する
		/// </summary>
		/// <param name="g">描画を行う Graphics</param>
		public void DrawDeployCandidate(Graphics g)
		{
			using (var pen = new Pen(Color.Red, 2))
			{
				foreach (var p in WarMap.InitDeployCandidate)
				{
					var q = ChipPoint2PixelPoint(p);
					g.DrawRectangle(pen, new Rectangle(q, _chipPixelSize));
				}
			}
		}

		/// <summary>
		/// 指定した色を使用して範囲を描画する
		/// </summary>
		/// <param name="g">描画を行う Graphics</param>
		/// <param name="chips">MapChip座標系での範囲内位置のシーケンス</param>
		/// <param name="color">範囲の描画で使用する色</param>
		public void DrawScope(Graphics g, IEnumerable<Point2> chips, Color color)
		{
			using (var brush = new SolidBrush(color))
			{
				foreach (var p in chips)
				{
					var q = ChipPoint2PixelPoint(p);
					g.FillRectangle(brush, q.X, q.Y, _chipPixelSize.Width, _chipPixelSize.Height);
				}
			}
		}

		/// <summary>
		/// マップ上に対するエフェクトを描画する
		/// </summary>
		/// <param name="g">描画を行う Graphics</param>
		public void DrawMapAnimation(Graphics g)
		{
			Func<Point2, Size, Point2> converter = (p_, size_) => p_.GetCenterToTopLeft(size_);

			foreach (var effect in _model.ChipAnimations)
			{
				effect.Draw(g, converter);
			}
		}

		/// <summary>
		/// ウィンドウ全体に対するエフェクトを描画する
		/// </summary>
		/// <param name="e">Paint イベントのデータ</param>
		public void DrawScreenAnimation(PaintEventArgs e)
		{
			var location = e.ClipRectangle.Location;
			var size = e.ClipRectangle.Size;
			var g = e.Graphics;

			foreach (var effect in _model.ScreenAnimations)
			{
				var p = new Point2(location.X + size.Width / 2, location.Y + size.Height / 2);
				effect.Draw(g, p.GetCenterToTopLeft(effect.Size));
			}
		}

		private void MapWindow_Load(object sender, EventArgs e)
		{
		}

		#region アニメーション配置用メソッド

		/// <summary>
		/// 画面上で指定した複数のサーフェイスを指定した一定時間ごとに切り替わるアニメーションを作成します。
		/// </summary>
		/// <param name="surfaces"></param>
		/// <param name="interval"></param>
		/// <returns></returns>
		public AnimationSurface CreateFrameAnimationOnScreen(IList<Surface> surfaces, float interval)
		{
			return new FrameAnimation(surfaces, interval);
		}

		/// <summary>
		/// マップチップ上で指定した文字を表示するアニメーションを作成します。
		/// </summary>
		/// <param name="str"></param>
		/// <param name="point"></param>
		/// <param name="color"></param>
		/// <param name="totalTime"></param>
		public AnimationSprite CreateStringAnimationOnMap(string str, Point2 point, Color color, float totalTime)
		{
			point = ChipPoint2PixelPoint(point)
				.GetLeftToCenter(_chipPixelSize);

			var strSurface = new OutlinedStringSurface(str, _damageFont, new SolidBrush(color), Pens.Black);
			var endPoint = point.GetTopToBottom(_chipPixelSize)
				.GetBottomToTop(strSurface.Size);

			// 文字列が等速度に移動するアニメーション
			return new UniformMotionSprite(
				new CustomCoordSurface(strSurface, (p_, size_) => p_.GetTopToCenter(size_)),
				point, endPoint, totalTime);
		}

		/// <summary>
		/// マップチップ上で指定した複数のサーフェイスを一定時間ごとに切り替わるアニメーションを作成します。
		/// </summary>
		/// <param name="surfaces"></param>
		/// <param name="chipPoint"></param>
		/// <param name="interval"></param>
		public AnimationSprite CreateFrameAnimationOnMap(IList<Surface> surfaces, Point2 chipPoint, float interval)
		{
			return new UnmovingSprite(
				new FrameAnimation(surfaces, interval),
				ChipPoint2PixelCenterPoint(chipPoint)
				);
		}

		/// <summary>
		/// マップチップ上で指定した複数のサーフェイスを一定時間ごとに切り替わるアニメーションを作成します。
		/// </summary>
		/// <param name="surfaces"></param>
		/// <param name="chips"></param>
		/// <param name="interval"></param>
		public AnimationSprite CreateFrameAnimationOnMap(IList<Surface> surfaces, IEnumerable<Point2> chips, float interval)
		{
			var chipPoint = chips.First();
			var anime = new UnmovingSprite(
				new FrameAnimation(surfaces, interval),
				ChipPoint2PixelCenterPoint(chipPoint)
				);
			var points = chips.Skip(1)
				.Select(p => ChipPoint2PixelCenterPoint(p));
			return new CopyAnimationSprite(anime, points);
		}

		/// <summary>
		/// マップチップ上で指定したサーフェイスが指定した地点間で等速直線移動するアニメーション郡を作成します。
		/// </summary>
		/// <param name="surface"></param>
		/// <param name="points"></param>
		/// <param name="unitTotalTime"></param>
		public IList<AnimationSprite> CreateContinuouslyMovingAnimationOnMap(
			Surface surface, IEnumerable<Point2> points, float unitTotalTime)
		{
			return points.Zip2Chain()
				.Select(
				t => (AnimationSprite)new UniformMotionSprite(
					surface,
					ChipPoint2PixelCenterPoint(t.Item1),
					ChipPoint2PixelCenterPoint(t.Item2),
					unitTotalTime))
				.ToList();
		}

		/// <summary>
		/// マップチップ上で指定した開始位置から指定した終了位置まで指定した速度で等速直線移動するアニメーションを作成します。
		/// なお、サーフェイスは指定したサーフェイス郡の中から移動方向に合わせて適切に選択されます。
		/// </summary>
		/// <param name="surfaces"></param>
		/// <param name="from"></param>
		/// <param name="to"></param>
		/// <param name="speed"></param>
		public AnimationSprite CreateDirectedUniformMotionAnimationOnMap(
			IList<Surface> surfaces, Point2 from, Point2 to, float speed)
		{
			from = ChipPoint2PixelCenterPoint(from);
			to = ChipPoint2PixelCenterPoint(to);
			var angle = ((to - from).Angle + Math.PI / 2);
			int index = (int)Math.Round(angle / (2 * Math.PI) * surfaces.Count);
			return new UniformMotionSprite(surfaces[(index + surfaces.Count * 10) % surfaces.Count], from, to, speed, true);
		}

		#endregion

		#region MapPanelのGUIイベント

		private void MapPanel_Paint(object sender, PaintEventArgs e)
		{
			using (new Benchmark())
			{
				// マップの描画
				//   ユニットおよび建物の描画を含みます
				if (WarMap != null)
				{
					DrawMap(e, WarMap);

					//初期配置候補の描画
					if (WarMap.InitDeployCandidate != null)
						DrawDeployCandidate(e.Graphics);
				}

				// スコープの描画
				if (Scope != null)
				{
					DrawScope(e.Graphics, Scope.RangeChips, Color.FromArgb(64, 0, 0, 255));
					DrawScope(e.Graphics, Scope.ValidRangeChips, Color.FromArgb(64, 0, 255, 0));
					if (Scope.ValidRangeChips.Contains(CursorChipPoint))
						DrawScope(e.Graphics, Scope.GetAreaChips(CursorChipPoint), Color.FromArgb(64, 255, 0, 0));
				}

				// アニメーションの描画
				DrawMapAnimation(e.Graphics);
				DrawScreenAnimation(e);
			}
			DrawMapAnimation(e.Graphics);
		}

		private void MapPanel_DoubleClick(object sender, EventArgs e)
		{
			CenteringByChipPoint(CursorChipPoint);
		}

		private void MapPanel_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				_model.InvokeSelectMapChipEvent();
			}
			else if (e.Button == MouseButtons.Right)
			{
				//HACK: 以下の2つのイベントは、呼び出しの是非や呼び出し順番において考慮の余地あり
				_model.InvokeCancelMapChipEvent();
				_model.InvokeCancelEvent();
			}
		}

		private void MapPanel_MouseMove(object sender, MouseEventArgs e)
		{
			CursorChipPoint = PixelPoint2ChipPoint(e.Location);
		}

		private void MapPanel_MouseEnter(object sender, EventArgs e)
		{
			_model.InvokeCursorEnterEvent();
		}

		private void MapPanel_MouseLeave(object sender, EventArgs e)
		{
			_model.InvokeCursorLeaveEvent();
		}

		#endregion
	}
}