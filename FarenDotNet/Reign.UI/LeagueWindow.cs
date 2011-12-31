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
	public partial class LeagueWindow : Form
	{
		ReignManager _rManager;

		public Province SelectedProvince { get; private set; }

		public LeagueWindow()
		{
			InitializeComponent();
			this.DialogResult = DialogResult.Cancel;
			_provInfoControl.ListView.DoubleClick += this.SelectProv;
		}

		public void SetManager(ReignManager manager)
		{
			_rManager = manager;
			_provInfoControl.SetInfos(manager.GetProvinceInfos());
		}

		private void SelectProv(object sender, EventArgs e)
		{
			var prov = _provInfoControl.SelectedInfo.Province;
			var myProv = _rManager.ActingProv;

			// 自分との同盟は行わない || 延長できるのは6ターン未満
			if (prov == myProv || _rManager.League[prov.No, myProv.No] >= 6)
				return;
			this.SelectedProvince = prov;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}
	}
}
