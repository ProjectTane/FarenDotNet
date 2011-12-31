using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using Paraiba.Drawing;
using FarenDotNet.BasicData;
using System.Diagnostics.Contracts;

namespace FarenDotNet.War.UI
{
	public partial class UnitInfoWindow : Form
	{
		// ---------- ---------- static処理 ---------- ----------

		// Singleton パターンで利用するインスタンス
		private static UnitInfoWindow instance;

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
				instance = new UnitInfoWindow();
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

		private UnitInfoWindow()
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
					Invalidate();
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
				g.DrawString(String.Format("{0}系", Unit.Species),
					font, brush, x, y);
				g.DrawString(
					String.Format("Rank {0}  Exp {1}", Unit.Rank, Unit.Exp),
					font, brush, x + 90, y);
				y += yStep;
			}

			{
				x = margin;
				y += 10;
				g.DrawString(
					String.Format("HP:     {0,3}/{1,3}", Unit.Status.HP, Unit.OriginalStatus.MaxHP),
					font, brush, x, y);
				y += yStep;
				g.DrawString(
					String.Format("MP:     {0,3}/{1,3}", Unit.Status.MP, Unit.OriginalStatus.MaxMP),
					font, brush, x, y);
				y += yStep;
				g.DrawString(
					String.Format("攻撃:   {0,3}/{1,3}", Unit.Status.Atk, Unit.OriginalStatus.Atk),
					font, brush, x, y);
				y += yStep;
				g.DrawString(
					String.Format("防御:   {0,3}/{1,3}", Unit.Status.Def, Unit.OriginalStatus.Def),
					font, brush, x, y);
				y += yStep;
				g.DrawString(
					String.Format("技量:   {0,3}/{1,3}", Unit.Status.Tec, Unit.OriginalStatus.Tec),
					font, brush, x, y);
				y += yStep;
				g.DrawString(
					String.Format("速さ:   {0,3}/{1,3}", Unit.Status.Agi, Unit.OriginalStatus.Agi),
					font, brush, x, y);
				y += yStep;
				g.DrawString(
					String.Format("魔力:   {0,3}/{1,3}", Unit.Status.Mag, Unit.OriginalStatus.Mag),
					font, brush, x, y);
				y += yStep;
				g.DrawString(
					String.Format("抵抗:   {0,3}/{1,3}", Unit.Status.Res, Unit.OriginalStatus.Res),
					font, brush, x, y);
				y += yStep;
				g.DrawString(
					String.Format("HP再生: {0,3}/{1,3}", Unit.Status.HpAutoHeal, Unit.OriginalStatus.HpAutoHeal),
					font, brush, x, y);
				y += yStep;
				g.DrawString(
					String.Format("MP再生: {0,3}/{1,3}", Unit.Status.MpAutoHeal, Unit.OriginalStatus.MpAutoHeal),
					font, brush, x, y);
				y += yStep + 5;
			}

			// 以下魔法レベル描画
			int widthStep = 16;
			var attributeTypes = new[] { AttackType.火, AttackType.水, AttackType.風, AttackType.土, AttackType.光, AttackType.闇 };
			for (int i = 0; i < attributeTypes.Length; i++)
			{
				g.DrawString(String.Format("{0}", attributeTypes[i]), font, brush, x + i * widthStep, y);
			}
			y += yStep;
			for (int i = 0; i < attributeTypes.Length; i++)
			{
				g.DrawString(String.Format("{0}", MagicLevelToChar(Unit.MagicLevels[attributeTypes[i]])),
					font, brush, x + i * widthStep + 3, y);
			}

			x = 120; y = 50;
			g.DrawString(String.Format("移動タイプ: {0}タイプ", Unit.MoveType), font, brush, x, y);
			y += yStep;
			g.DrawString(String.Format("移動力: {0}", Unit.Status.Mobility), font, brush, x, y);

			y += yStep + 5;
			g.DrawString(String.Format("攻撃Ｘ{0}", Unit.DefaultAttacks.Count), font, brush, x, y);

			y += yStep;
			g.DrawString(String.Format("{0}X{1}", Unit.Commands[3].Command.Name, Unit.DefaultAttacks.Count), font, brush, x, y);

			y += yStep + 10;

			var res = Unit.Resistivity;
			var attackTypes = AttackTypes.Attack;
			int count = 0;
			for (int i = 0; i < attackTypes.Count; i++)
			{
				var type = attackTypes[i];
				var resType = res[type];
				switch (resType)
				{
					case ResistivityType.強い:
						g.DrawString(String.Format("{0}に強い", type), font, brush, x, y + yStep * count++);
						break;
					case ResistivityType.弱い:
						g.DrawString(String.Format("{0}に弱い", type), font, brush, x, y + yStep * count++);
						break;
					case ResistivityType.吸収:
						g.DrawString(String.Format("{0}を吸収", type), font, brush, x, y + yStep * count++);
						break;
				}
			}

			// 毎回クライアントサイズを計算して適用
			// this.ClientSize = new Size(x + xStep, y + yStep * 2 + margin);
		}

		#region D&D処理用

		private void UnitInfoMiniWindow_MouseDown(object sender, MouseEventArgs e)
		{
			// D&D処理用
			if (e.Button == MouseButtons.Left)
			{
				this.isDragging = true;
				this.Opacity = 0.5;
				this.dragStart = e.Location;
			}
		}

		private void UnitInfoMiniWindow_MouseUp(object sender, MouseEventArgs e)
		{
			// D&D処理用
			isDragging = false;
			this.Opacity = 1.0;
			this.Invalidate();
		}

		private void UnitInfoMiniWindow_MouseMove(object sender, MouseEventArgs e)
		{
			// D&D処理用
			if (isDragging)
			{
				this.Left += e.X - dragStart.X;
				this.Top += e.Y - dragStart.Y;
			}
		}

		#endregion

		private char MagicLevelToChar(byte b)
		{
			char c = 'X';
			switch (b)
			{
				case 0: 
					c = 'X';
					break;
				case 1:
					c = 'C';
					break;
				case 2:
					c = 'D';
					break;
				case 3:
					c = 'B';
					break;
				case 4:
					c = 'A';
					break;
				case 5:
					c = 'S';
					break;
				default:
					Debug.Assert(true, "魔法レベルおかしいよ");
					break;
			}
			return c;
		}
	}
}
