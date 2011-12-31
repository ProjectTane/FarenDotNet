using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FarenDotNet.Reign.UI
{
	public partial class MoveAreaCandidateWindow : Form
	{
		public Area Result { get; private set; }

		public MoveAreaCandidateWindow ()
		{
			InitializeComponent();
			this.DialogResult = DialogResult.Cancel;
		}

		public void SetArea (Area area)
		{
			var adjs =
				from a in area.Adjacent
				where a.Province == area.Province
				select a;
			this.SuspendLayout();
			{
				int btm = 35;
				foreach (var a in adjs)
				{
					btm += 23;
					var button = new Button {
						Text = a.Name,
						Dock = DockStyle.Top,
						Tag = a
					};
					button.Click += this.ButtonClick;
					this.Controls.Add(button);
				}
				this.Controls.Remove(_label);
				this.Controls.Add(_label);
				this.ClientSize = new Size(this.ClientSize.Width, btm);
			}
			this.ResumeLayout();
		}

		private void ButtonClick (object sender, EventArgs e)
		{
			var btn = sender as Button;
			if (btn == null) return;
			var area = btn.Tag as Area;
			if (area == null) return;

			this.DialogResult = DialogResult.OK;
			this.Result = area;
			this.Close();
		}

		private void btn_Cancel_Click (object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
