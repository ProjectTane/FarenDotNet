using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics.Contracts;
using FarenDotNet.War;
using Paraiba.Utility;

namespace FarenDotNet.Reign.UI
{
	public partial class PrepareWarWindow : Form
	{
		public Area TargetArea { get; set; }
		public IList<Area> AdjArea { get; set; }
		public ReignManager RManager { get; set; }
		public ReignWindowManager RWManager { get; set; }

		List<Unit> _battler = new List<Unit>();
		List<FlowLayoutPanel> _flps = new List<FlowLayoutPanel>();

		public PrepareWarWindow()
		{
			InitializeComponent();
		}

		private void PrepareWarWindow_Load(object sender, EventArgs e)
		{
			// ページの作成
			_tabControl.TabPages.Clear();
			foreach (var area in AdjArea)
			{
				var tab = new TabPage(area.Name) {
					Margin = new Padding(0),
					Padding = new Padding(0),
				};
				var flp = new FlowLayoutPanel {
					Tag = area,
					Dock = DockStyle.Fill,
					BackColor = Color.Navy,
					Margin = new Padding(0),
					Padding = new Padding(0),
				};
				foreach (var unit in area.Units) {
					var uCtrl = new UnitImageControl(unit);
					uCtrl.Click += this.UnitClick;
					flp.Controls.Add(uCtrl);
				}
				_flps.Add(flp);
				tab.Controls.Add(flp);
				_tabControl.TabPages.Add(tab);
			}
		}

		private void UnitClick(object sender, EventArgs e)
		{
			var uCtrl = sender as UnitImageControl;
			if (uCtrl == null) return;

			var unit = uCtrl.Unit;
			if (_battler.Contains(uCtrl.Unit))
			{
				// 右から左へ
				_battler.Remove(unit);
				flp_Battler.Controls.Remove(uCtrl);
				foreach (var flp in _flps)
				{
					Area area = flp.Tag as Area;
					if (area.Units.Contains(unit))
					{
						flp.Controls.Add(uCtrl);
						break;
					}
				}
			}
			else if (_battler.Count < 20)
			{
				// 左から右へ
				var tab = _tabControl.SelectedTab;
				var flp = tab.Controls.OfType<FlowLayoutPanel>().Single();
				flp.Controls.Remove(uCtrl);
				flp_Battler.Controls.Add(uCtrl);
				_battler.Add(unit);
			}
		}

		private void btn_Finish_Click(object sender, EventArgs e)
		{
			if (_battler.Count == 0)
			{
				MessageBox.Show("部隊が一つも選択されていません");
				return;
			}
			// 呼び出しようのデータ作成
			var area = TargetArea;
			var conquerList = new List<Tuple<int, IList<Unit>, Area>>();
			for (int i = 0; i < area.Adjacent.Count; i++)
			{
				var adj = area.Adjacent[i];
				var list = adj.Units
					.Where(u => _battler.Contains(u))
					.ToList();
				if (list.Count > 0)
					conquerList.Add(new Tuple<int, IList<Unit>, Area>(i, list, adj));
			}


			var owner = this.Owner;
			this.Close();
			RWManager.AreaInfoWindow.Visible = false;
			RWManager.MasterWindow.Visible = false;
			owner.Visible = false;

			//TODO: 戦闘終了時に実行するデリゲートの作成。今は全体を終了させている
			var phaseCreator = new PhaseCreator(area, AdjArea[0].Province, delegate {
				
				owner.Visible = true;
				RWManager.MasterWindow.Visible = true;
				RWManager.AreaInfoWindow.Visible = true;
				RWManager.AreaInfoWindow.ResetUnit();
			});
			new War.WarInitializer(RManager.UnitsData, area, conquerList, phaseCreator, RManager.GamePath).Initialize();
		}

		private void btn_Cancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void btn_AllBattler_Click(object sender, EventArgs e)
		{
			var tab = _tabControl.SelectedTab;
			var flp = tab.Controls.OfType<FlowLayoutPanel>().Single();
			var uCtrls = flp.Controls.OfType<UnitImageControl>().ToArray();

			foreach (var uCtrl in uCtrls)
			{
				if (_battler.Count == 20)
					break;
				flp_Battler.Controls.Add(uCtrl);
				flp.Controls.Remove(uCtrl);
				_battler.Add(uCtrl.Unit);
			}
		}
	}
}
