using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FarenDotNet.Reign.UI
{
	public partial class TopPanel : UserControl
	{
		const string TURN_FORMAT = "第 {0} ターン";

		public TopPanel()
		{
			InitializeComponent();
		}

		public string MasterName
		{
			set {
				lbl_Name.Text = value;
				this.Invalidate();
			}
		}

		public int Turn
		{
			set
			{
				lbl_Turn.Text = String.Format(TURN_FORMAT, value);
				this.Invalidate();
			}
		}
	}
}
