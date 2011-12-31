using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using Paraiba.Drawing;

namespace FarenDotNet.War.UI
{
	/// <summary>
	/// ゲーム中ユニット上にマウスポインタが移動した時に、
	/// ポップアップされるそのユニットの情報が描画されているウィンドウです。
	/// </summary>
	/// <remarks>ファーレン依存</remarks>
	public partial class MiniUnitInfoWindow : Form
	{
		// ---------- ---------- static処理 ---------- ----------

		// Singleton パターンで利用するインスタンス
		private static MiniUnitInfoWindow instance;

		public static bool IsInitialized
		{
			get { return instance != null && !instance.IsDisposed; }
		}

		public static bool IsOnCursor
		{
			get { return instance.Bounds.Contains(Cursor.Position); }
		}

		public static void ShowWindow(IWin32Window window, WarUnit unit)
		{
			Contract.Requires(window != null, "window");
			Contract.Requires(unit != null, "unit");

			if (!IsInitialized)
			{
				instance = new MiniUnitInfoWindow();
			}
			if (!instance.Visible)
			{
				instance.Show(window);
			}
			instance.Unit = unit;
		}

		public static void ShowWindow(IWin32Window window, WarUnit unit, int x, int y)
		{
			Contract.Requires(window != null, "window");
			Contract.Requires(unit != null, "unit");

			ShowWindow(window, unit);
			instance.Left = x;
			instance.Top = y;
		}

		public static void ShowWindow(IWin32Window window, WarUnit unit, Point location)
		{
			Contract.Requires(window != null, "window");
			Contract.Requires(unit != null, "unit");

			ShowWindow(window, unit);
			instance.Location = location;
		}

		public static void HideWindow()
		{
			if (IsInitialized)
			{
				instance.Hide();
			}
		}

		// ---------- ---------- 非static処理 ---------- ----------

		// 描画対象のユニットを指定
		private WarUnit _unit;

		// ドラッグ移動をするための変数
		private bool isDragging = false;
		private Point dragStart;

		protected override bool ShowWithoutActivation
		{
			get
			{
				return true;
			}
		}

		private MiniUnitInfoWindow()
		{
			InitializeComponent();
		}

		private WarUnit Unit
		{
			get { return _unit; }
			set
			{
				Debug.Assert(value != null);
				if (_unit != value)
				{
					_unit = value;
					this.Invalidate();
				}
			}
		}

		// これ以上高速化したい場合はOnPaintをOverrideすること。
		private void UnitInfoWindow_Paint(object sender, PaintEventArgs e)
		{
			// ドラッグ移動中は見栄えのため描画しない
			if (isDragging || Unit == null)
			{
				return;
			}

			//HACK: デザイン改良
			// 固定位置に変更します。
			var g = e.Graphics;
			const int margin = 4;
			int x = margin, y = margin;
			g.DrawSurface(Unit.ChipImage, x, y);

			var font = new Font("ＭＳ ゴシック", 10);
			var brush = Brushes.White;
			int yStep = 16; // Fontサイズを見て調節すべし

			{
				x += Unit.ChipImage.Width + margin;
				g.DrawString(Unit.Name, font, brush, x, y);
				y += yStep + 3;
				g.DrawString(
					String.Format("HP:{0}/{1}", Unit.Status.HP, Unit.OriginalStatus.MaxHP),
					font, brush, x, y);
				g.DrawString(
					String.Format("MP:{0}/{1}", Unit.Status.MP, Unit.OriginalStatus.MaxMP),
					font, brush, x + 80, y);
				y += yStep;
			}

			{
				int xStep = 64;
				x = margin;
				g.DrawString(
					String.Format("攻撃:{0:000}", Unit.Status.Atk),
					font, brush, x, y);
				g.DrawString(
					String.Format("防御:{0:000}", Unit.Status.Def),
					font, brush, x, y + yStep);
				x += xStep;
				g.DrawString(
					String.Format("技量:{0:000}", Unit.Status.Tec),
					font, brush, x, y);
				g.DrawString(
					String.Format("速さ:{0:000}", Unit.Status.Agi),
					font, brush, x, y + yStep);
				x += xStep;
				g.DrawString(
					String.Format("魔力:{0:000}", Unit.Status.Mag),
					font, brush, x, y);
				g.DrawString(
					String.Format("抵抗:{0:000}", Unit.Status.Res),
					font, brush, x, y + yStep);
				x += xStep;
			}

			// 毎回クライアントサイズを計算して適用
			this.ClientSize = new Size(x, y + yStep * 2 + margin);
		}

		private void MiniUnitInfoWindow_Deactivate(object sender, EventArgs e)
		{
			HideWindow();
		}

		#region D&D処理用

		private void MiniUnitInfoWindow_MouseUp(object sender, MouseEventArgs e)
		{
			// D&D処理用
			isDragging = false;
			this.Opacity = 1.0;
			this.Invalidate();
		}

		private void MiniUnitInfoWindow_MouseDown(object sender, MouseEventArgs e)
		{
			// D&D処理用
			if (e.Button == MouseButtons.Left) {
				this.isDragging = true;
				this.Opacity = 0.5;
				this.dragStart = e.Location;
			}
		}

		private void MiniUnitInfoWindow_MouseMove(object sender, MouseEventArgs e)
		{
			// D&D処理用
			if (isDragging) {
				this.Left += e.X - dragStart.X;
				this.Top += e.Y - dragStart.Y;
			}
		}

		#endregion
	}
}
