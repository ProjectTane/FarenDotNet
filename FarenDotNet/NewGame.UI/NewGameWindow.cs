using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FarenDotNet.NewGame.UI
{
	public partial class NewGameWindow : Form
	{
		// ----- ----- ----- Field ----- ----- -----
		private Root _root; 

		// ----- ----- ----- Property ----- ----- -----
		public Root Root { get { return _root; } }


		// ----- ----- ----- Method ----- ----- -----


		public NewGameWindow()
		{
			InitializeComponent();
			Initialize();
			this.DialogResult = DialogResult.Cancel;
		}

		private void Initialize()
		{
			_root = new Root();

			this.pageBinder.Pages = new Page[] {
				new SelectScenarioPage(_root),
				new SelectNumberPage(_root),
				new SelectMastersPage(_root)
			};
		}

		private void pageBinder_CancelClick(object sender, EventArgs e)
		{
			this.Close();
		}

		private void pageBinder_FinishClick(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
