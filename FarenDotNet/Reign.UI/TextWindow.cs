using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics.Contracts;
using Paraiba.Drawing;
using Paraiba.Drawing.Surfaces;

namespace FarenDotNet.Reign.UI
{
	public partial class TextWindow : Form
	{
		StringFormat _format = LEFT;
		Brush _txtBrush = Brushes.White;
		int _x = 0;
		int _y = 0;

		Bitmap _canvas = null;
		Surface _convImg = null;

		public new TextWindowLocation Location { get; set; }

		/// <summary>基本サイズ </summary>
		static readonly Size SIZE = new Size(490, 110);
		//文字描画の時に利用
		static readonly Font FONT = new Font("MS UI Gothic", 12);
		static readonly StringFormat CENTER = new StringFormat { Alignment = StringAlignment.Center };
		static readonly StringFormat LEFT = new StringFormat { Alignment = StringAlignment.Near };
		static readonly StringFormat RIGHT = new StringFormat { Alignment = StringAlignment.Far };

		// ----- ----- ----- CTOR ----- ----- -----
		public TextWindow()
		{
			InitializeComponent();
		}

		// ----- ----- ----- Event ----- ----- -----
		private void TextWindow_Load(object sender, EventArgs e)
		{
			this.SetSize(SIZE);
		}

		private void TextWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
				e.Cancel = true;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			//base.OnPaint(e);
			var g = e.Graphics;
			g.DrawImage(_canvas, 0, 0);
			g.DrawRectangle(Pens.Gray, 0, 0, _canvas.Width - 1, _canvas.Height - 1);
		}

		// イベントじゃないけど、イベントに近いと思ったので
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (components != null)
						components.Dispose();
					if (_canvas != null)
						_canvas.Dispose();
				}
			} finally { base.Dispose(disposing); }
		}

		// ----- ----- ----- 公開メソッド ----- ----- -----
		public void Conversation(string name, Surface face, Surface convImg)
		{
			_convImg = convImg;
			this.FaceOut(name, face);
			this.Clear();
		}

		public void FaceOut(string name, Surface face)
		{
			this.SetSize(SIZE);
			using (var g = Graphics.FromImage(_canvas))
			{
				g.Clear(Color.Navy);
				g.DrawString(name, FONT, Brushes.White, new PointF(3, 0));
				g.DrawSurface(face, new Point(0, SIZE.Height - face.Height));
			};
			this.Invalidate();
		}

		public void Clear()
		{
			if (_convImg == null)
			{
				using (var g = Graphics.FromImage(_canvas))
				{
					g.Clear(Color.Navy);
				}
				_txtBrush = Brushes.White;
				_format = CENTER;
				_x = _canvas.Width / 2;
				_y = 16;
			}
			else
			{
				var conv = _convImg;
				int x = SIZE.Width - conv.Width;
				int y = SIZE.Height - conv.Height;
				using (var g = Graphics.FromImage(_canvas))
				{
					g.DrawSurface(conv, new Point(x, y));
				};
				_txtBrush = Brushes.Black;
				_format = LEFT;
				_x = x + 32;
				_y = y + 16;
				this.Invalidate();
			}
		}

		public void SetSize(int width, int height)
		{
			this.SetSize(new Size(width, height));
			_x = width / 2;
		}

		public void Print(string txt)
		{
			using (var g = Graphics.FromImage(_canvas))
			{
				g.DrawString(txt, FONT, _txtBrush, _x, _y, _format);
			}
			_y += 20;
			this.Invalidate();
		}

		// ----- ----- ----- 非公開メソッド ----- ----- -----
		private void SetSize(Size size)
		{
			if (_canvas == null)
			{
				_canvas = new Bitmap(size.Width, size.Height);
			}
			else if (_canvas.Size == size)
			{
				return;
			}
			else
			{
				var old = _canvas;
				_canvas = new Bitmap(size.Width, size.Height);
				old.Dispose();
			}
			this.ClientSize = size;
			using (var g = Graphics.FromImage(_canvas))
			{
				g.Clear(Color.Navy);
			}

			// 位置変更
			this.Left = Owner.Left + (Owner.Width - this.Width) / 2;
			switch (this.Location)
			{
			case TextWindowLocation.Top:
				this.Top = Owner.Top + 200;
				break;
			case TextWindowLocation.Center:
				this.Top = Owner.Top + (Owner.Height - this.Height) / 2;
				break;
			case TextWindowLocation.Bottom:
				this.Top = Owner.Bottom - 180 - this.Height;
				break;
			}
		}
	}

	public enum TextWindowLocation
	{
		Top, Center, Bottom
	}
}
