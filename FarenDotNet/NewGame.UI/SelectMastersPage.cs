using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using FarenDotNet.BasicData;
using System.Collections;
using System.Diagnostics.Contracts;

namespace FarenDotNet.NewGame.UI
{
	public partial class SelectMastersPage : Page
	{
		Root _root;

		public SelectMastersPage(Root root)
		{
			_root = root;
			InitializeComponent();
		}

		private void ButtonReset()
		{
			var loader = _root.Pairs[_root.GameIndex].Loader;
			loader.LoadMasterData();
			loader.LoadUnits();

			var scMaster = _root.SelectedScenario.Masters;
			var masData = _root.SelectedGame.MastersData.Masters;
			var units = _root.SelectedGame.Units;

			var masters = from s in scMaster
						  where s.TakeOff == false
						  join d in masData on s.No equals d.No
						  join u in units on d.ID equals u.ID
						  select new {Sc = s, Data = d, Unit = u };

			var flowControl = this.flowLayoutPanel.Controls;

			this.SuspendLayout();
			{
				flowControl.Clear();

				foreach (var m in masters) {
					var item = new CheckBox {
						Text = m.Data.Name,
						Tag = m.Sc,
						Image = m.Unit.Images.Face.GetImage(),
						Appearance = Appearance.Button,
						TextImageRelation = TextImageRelation.ImageAboveText,
						TextAlign = ContentAlignment.MiddleCenter,
						AutoSize = true,
					};
					item.MouseEnter += this.OnMasterMouseEnter;
					item.CheckedChanged += this.MasterCheckChanged;
					flowControl.Add(item);
				}
				this.CheckFinish();
			}
			this.ResumeLayout();
		}

		private void CheckFinish()
		{
			int max = Math.Min(_root.N_Player, MasterCheckBoxes.Count());
			int num = this.MasterCheckBoxes.Count(x => x.Checked);

			if (num < max) {
				this.CanNext = false;
				foreach (var x in MasterCheckBoxes)
					x.Enabled = true;
				_root.SelectedMastersNo.Clear();
			}
			else {
				this.CanNext = true;
				var masters = _root.SelectedGame.MastersData.Masters;
				var selectedNo = _root.SelectedMastersNo;
				foreach (var x in MasterCheckBoxes) {
					if (x.Checked) {
						x.Enabled = true;
						var scMaster = (ScenarioData.Master)x.Tag;
						selectedNo.Add(scMaster.No);
					}
					else
						x.Enabled = false;
				}
			}
		}

		private IEnumerable<CheckBox> MasterCheckBoxes
		{
			get
			{
				foreach (var x in this.flowLayoutPanel.Controls) {
					var cBox = x as CheckBox;
					if (cBox == null)
						continue;
					yield return cBox;
				}
			}
		}
				

		// ----- ----- ----- イベント系 ----- ----- -----

		/// <summary>
		/// このページが開かれた時
		/// </summary>
		protected internal override void OnPageOpen(EventArgs e)
		{
			base.OnPageOpen(e);
			//Console.WriteLine("OPEN");
			this.ButtonReset();
			this.lbl_MasterName.Text = "";
			this.lbl_Difficulty.Text = "";
			this.lbl_Explanation.Text = "";
		}

		/// <summary>
		/// マスターのチェックボックスにマウスが入った時
		/// </summary>
		private void OnMasterMouseEnter(object sender, EventArgs e)
		{
			var master = ((Control)sender).Tag as ScenarioData.Master;
			this.lbl_MasterName.Text = master.DisplayName;
			this.lbl_Difficulty.Text = master.Difficulty;
			this.lbl_Explanation.Text = master.Explanation;
		}

		private void MasterCheckChanged(object sender, EventArgs e)
		{
			CheckFinish();
		}
	}
}
