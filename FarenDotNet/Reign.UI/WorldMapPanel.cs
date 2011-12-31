using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics.Contracts;
using Paraiba.Drawing;
using Paraiba.Drawing.Animations;
using Paraiba.Drawing.Animations.Surfaces;
using Paraiba.Drawing.Surfaces;
using Paraiba.Linq;
using Paraiba.TaskList;
using Paraiba.Utility;
using Paraiba.Windows.Forms;

namespace FarenDotNet.Reign.UI
{
	public partial class WorldMapPanel : UserControl
	{
		// ----- ----- ----- CONST ----- ----- -----

		// ----- ----- ----- COLOR ----- ----- -----
		private readonly AnimationManager<IAnimation> _animations;
		private readonly WorldMap _reignMap;
		private readonly Color LINE_BACK = Color.Black;
		private readonly Color LINE_FORE = Color.Red;
		private readonly Color TEXT_BACK = Color.Black;
		private readonly Color TEXT_FORE = Color.White;

		// ----- ----- ----- Field ----- ----- -----
		private bool _isDrawAreaName = true;
		private bool _isDrawLines = true;

		/// <summary>左上の座標から旗の根本までのずれ</summary>
		private Size OFFSET = new Size(6, 32);

		// ----- ----- ----- ctor ----- ----- -----
		public WorldMapPanel(WorldMap map)
		{
			Contract.Requires(map != null);
			_reignMap = map;
			_animations = new AnimationManager<IAnimation>();

			Initialize();

			InitializeComponent();

			InitializeOtherComponent();
		}

		// ----- ----- ----- Property ----- ----- -----

		/// <summary>エリア間のラインを引くかどうか</summary>
		public bool IsDrawLines
		{
			get { return _isDrawLines; }
			set { _isDrawLines = value; }
		}

		// ----- ----- ----- Event ----- ----- -----

		public event EventHandler<AreaClickEventArgs> AreaClick;

		// ----- ----- ----- Method ----- ----- -----

		private void Initialize()
		{
			Contract.Requires(_reignMap.MapImage != null);
			Size = _reignMap.MapImage.Size;
			DoubleBuffered = true;
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.Opaque, false);

			Global.MainLoop.TickEvents.Add(0, ElapseEvent);
		}

		private void InitializeOtherComponent()
		{
			SuspendLayout();
			foreach (var area in _reignMap.Areas)
			{
				Func<IList<Surface>, AnimationSurface> createAnimation = flagImages_ =>
					new EndlessRepeatAnimation(
						new XFrameAnimation(flagImages_, index_ => Global.Random.Next(400, 600))	// TODO: リテラルの移動
					);
				var control = new AnimationSurfaceControl(createAnimation(area.Province.FlagImage)) {
					Location = area.Location,
					Size = new Size(32, 32),
				};
				// 地域の支配者が変化した場合、描画する旗も変更する
				area.ProvinceChanged += province_ =>
					control.Surface = createAnimation(province_.FlagImage);

				var area_ = area; // クロージャーで記憶するため
				control.MouseClick += (sender_, e_) => {
					if (AreaClick != null)
					{
						AreaClick(sender_, new AreaClickEventArgs(area_));
					}
				};
				_animations.Add(control);
				Controls.Add(control);
			}
			ResumeLayout(false);
		}

		private void ElapseEvent(TaskArgs<int> taskArgs, int time)
		{
			_animations.Elapse(time);
		}

		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);

			Size = _reignMap.MapImage.Size;
		}

		#region 描画部分

		protected override void OnPaint(PaintEventArgs e)
		{
			var g = e.Graphics;
			g.SmoothingMode = SmoothingMode.AntiAlias;

			// 地図描画
			g.DrawSurface(_reignMap.MapImage, 0, 0);

			// エリア間の線描画
			if (_isDrawLines)
				DrawLines(g);
			// 都市名描画
			if (_isDrawAreaName)
				DrawAreaName(g);

			base.OnPaint(e);
		}

		// エリア間のラインを引く
		private void DrawLines(Graphics g)
		{
			Contract.Requires(!((IEnumerable<Area>) _reignMap.Areas).IsNullOrEmpty());

			var offset = OFFSET + new Size(0, -3);

			using (var penFore = new Pen(LINE_FORE, 3))
			using (var penBack = new Pen(LINE_BACK, 4))
			{
				var lines = from src in _reignMap.Areas
				from dst in src.Adjacent
				where src.No < dst.No
				select new { Start = src.Location, End = dst.Location };

				foreach (var line in lines)
				{
					g.DrawLine(penBack, line.Start + offset, line.End + offset);
					g.DrawLine(penFore, line.Start + offset, line.End + offset);
				}
			}
		}

		// エリア名の描画
		private void DrawAreaName(Graphics g)
		{
			var font = new Font("MS UI Gothic", 16, FontStyle.Bold | FontStyle.Italic);
			var format = new StringFormat { Alignment = StringAlignment.Center };

			using (var back = new Pen(TEXT_BACK, 2))
			using (var fore = new SolidBrush(TEXT_FORE))
			{
				foreach (var area in _reignMap.Areas)
				{
					using (var path = new GraphicsPath())
					{
						path.AddString(
							area.Name,
							font.FontFamily, (int)font.Style, font.Size,
							area.Location + OFFSET,
							format);
						g.DrawPath(back, path);
						g.FillPath(fore, path);
					}
				}
			} // end using
		}

		#endregion

		// ----- ----- ----- Inner Class ----- ----- -----

		#region Nested type: AreaClickEventArgs

		[DebuggerDisplay("{Area}")]
		public class AreaClickEventArgs : EventArgs
		{
			public readonly Area Area;

			public AreaClickEventArgs(Area area)
			{
				Area = area;
			}
		}

		#endregion
	}
}