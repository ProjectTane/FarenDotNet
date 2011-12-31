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
	public partial class GameOver : Form
	{
		public GameOver()
		{
			InitializeComponent();
		}

		private void _label_Click(object sender, EventArgs e)
		{
			var owner = this.Owner;
			if (owner != null)
				owner.Close();
		}
	}
}
