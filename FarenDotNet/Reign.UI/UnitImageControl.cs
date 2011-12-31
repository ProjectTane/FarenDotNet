using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Paraiba.Drawing;
using Paraiba.Drawing.Surfaces;
using System.Drawing.Drawing2D;
using FarenDotNet.Properties;

namespace FarenDotNet.Reign.UI
{
	public partial class UnitImageControl : UserControl
	{
		bool _canSelect;
		bool _selected;

		public Unit Unit
		{
			get { return base.Tag as Unit; }
			set { base.Tag = value as Unit; }
		}

		private new object Tag
		{
			get { return base.Tag; }
			set { base.Tag = value; }
		}

		public new bool CanSelect
		{
			get { return _canSelect; }
			set { _canSelect = value; }
		}
		public bool Selected
		{
			get { return _selected; }
			set
			{
				if (_canSelect)
				{
					_selected = value;
					this.Invalidate();
				}
			}
		}

		public UnitImageControl(Unit unit) : this() { base.Tag = unit; }
		public UnitImageControl()
		{
			this.Size = new Size(32, 32);
			this.Margin = new Padding(4);
			DoubleBuffered = true;
			SetStyle(ControlStyles.UserPaint, true);
			SetStyle(ControlStyles.Opaque, false);
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			//base.OnPaint(e);
			Unit unit = this.Tag as Unit;
			if (unit == null) return;

			var g = e.Graphics;
			g.SmoothingMode = SmoothingMode.AntiAlias;
			Surface img = unit.Image;
			if (this.CanSelect && this.Selected)
				g.Clear(Color.FromArgb(127, Color.Red));
			g.DrawSurface(img, 0, 0);

			// 文字の描画
			if (this.Text != null && this.Text.Length > 0)
			{
				switch (this.Text)
				{
				case "Finish":
					{
						var txt = Resources.u_finish;
						g.DrawImage(txt, 0, 0);
						break;
					}
				case "Moved":
					{
						var txt = Resources.u_moved;
						g.DrawImage(txt, 0, 0);
						break;
					}
				default:
					using (var back = new Pen(Color.Black, 2))
					using (var fore = new SolidBrush(Color.White))
					using (var font = new Font("MS UI Gothic", 10))
					using (var path = new GraphicsPath())
					{
						path.AddString(
							this.Text,
							font.FontFamily, (int)font.Style, font.Size,
							new PointF(16, 16),
							new StringFormat {
								Alignment = StringAlignment.Center,
								LineAlignment = StringAlignment.Center
							});
						g.DrawPath(back, path);
						g.FillPath(fore, path);
					}
					break;
				}
			}
		}
	}
}
