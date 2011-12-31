using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FarenDotNet.Reign.UI
{
	public partial class MasterWindow : Form
	{
		public event EventHandler ClosingCallback;
		public ReignManager ReignManager { get; set; }
		public ReignWindowManager RWManager { get; set; }
		// ----- ----- ----- ----- ----- CTOR ----- ----- ----- ----- -----
		public MasterWindow()
		{
			InitializeComponent();
		}

		// ----- ----- ----- ----- ----- PUBLIC ----- ----- ----- ----- -----

		public void SetData(Province prov, IEnumerable<Area> areas)
		{
			// タイトル
			var master = prov.Master;
			if (master == null)
				return;
			var ownAreas = areas.Where(a => a.Province == prov);
			var income = ownAreas.Sum(a => a.Income);
			var cost = ownAreas.SelectMany(a => a.Units).Where(u => u.IsUnique).Sum(u => u.Cost);

			this.Text = master.Name;
			lbl_Name.Text = prov.Name;
			lbl_vCost.Text = cost.ToString();
			lbl_vIncome.Text = income.ToString();
			lbl_vMoney.Text = prov.Money.ToString();
			pbx_Face.Image = master.Images.Face.GetImage();
			pbx_Flag.Image = master.Images.Flag[0].GetImage();
		}

		// ----- ----- ----- ----- ----- EVENT ----- ----- ----- ----- -----
		/// <summary>
		/// ウィンドウを閉じるとき
		/// </summary>
		private void SelectActionWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			Contract.Requires(this.Owner != null);
			if (this.Visible && e.CloseReason == CloseReason.UserClosing)
			{
				this.Visible = false;
				e.Cancel = true;
				var tmp = ClosingCallback;
				if (tmp != null)
					tmp(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// 戦争ボタン
		/// </summary>
		private void btn_War_Click(object sender, EventArgs e)
		{
			var main = this.Owner as ReignWindow;
			if (main == null) return;

			var warWindow = CreateWarAreaSelectWindow();
			EventHandler<WorldMapPanel.AreaClickEventArgs> selectArea = (sender_, e_) => {
				var area = e_.Area;
				var myProv = ReignManager.ActingProv;
				if (area.Province == myProv)
					return;
				var adj = area.Adjacent
					.Where(a => a.Province == myProv)
					.ToList();

				if (adj.Count == 0) return;
				if (ReignManager.League[area.Province.No, myProv.No] > 0)
				{
					MessageBox.Show("同盟国に攻め込むことはできません。");
					return;
				}

				warWindow.Close();
				var window = new PrepareWarWindow {
					Owner = this.Owner,
					TargetArea = area,
					AdjArea = adj,
					RManager = ReignManager,
					RWManager = RWManager,
				};
				window.Show();
			};

			main.AreaClick -= RWManager.ShowAreaInfoWindowEvent;
			main.AreaClick += selectArea;
			warWindow.FormClosing += (sender_, e_) => {
				main.AreaClick -= selectArea;
				main.AreaClick += RWManager.ShowAreaInfoWindowEvent;
			};
			warWindow.Show();
		}

		/// <summary>
		/// 同盟ボタン
		/// </summary>
		private void btn_League_Click(object sender, EventArgs e)
		{
			Province prov;
			using (var league = new LeagueWindow())
			{
				league.SetManager(ReignManager);
				league.ShowDialog();
				if (league.DialogResult != DialogResult.OK)
					return;
				prov = league.SelectedProvince;
			}
			var myProv = ReignManager.ActingProv;
			var leagueTurn = ReignManager.League[prov.No, myProv.No];

			if (leagueTurn == 0)
			{
				// 新規の同盟
				var cost = 12;
				var result = MessageBox.Show(
					(prov.Name + "と\r\n" +
					"同盟締結するには" + cost + "必要です。\r\n" +
					"６ターンの同盟締結を行いますか？"),
					"確認",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question);
				if (result != DialogResult.Yes) return;
				if (cost > myProv.Money)
				{
					MessageBox.Show("お金が足りません");
					return;
				}
				// 分からんので、全部拒否で！
				MessageBox.Show("同盟を拒否されました。");
			}
			else
			{
				// 同盟の延長
				var cost = 12;
				int turn = 1;
				var result = MessageBox.Show(
					(prov.Name + "と\r\n" +
					"同盟締結するには" + cost + "必要です。\r\n" +
					turn + "ターンの同盟期限の延長を行いますか？"),
					"確認",
					MessageBoxButtons.YesNo,
					MessageBoxIcon.Question);
				if (result != DialogResult.Yes) return;
				if (cost > myProv.Money)
				{
					MessageBox.Show("お金が足りません");
					return;
				}
				// 全部許可
				myProv.Money -= cost;
				MessageBox.Show(prov.Name + "と同盟締結の延長を行いました。");
			}
			return;
		}

		/// <summary>
		/// 保存ボタン
		/// </summary>
		private void btn_Save_Click(object sender, EventArgs e)
		{
			if (_saveFileDialog.ShowDialog() == DialogResult.OK)
			{
				var save = new Save(this.ReignManager);
				using (FileStream stream = new FileStream(
					_saveFileDialog.FileName,
					FileMode.Create,
					FileAccess.Write))
				{
					var bf = new BinaryFormatter();
					bf.Serialize(stream, save);
				}
			}
		}

		/// <summary>
		/// 終了ボタン
		/// </summary>
		private void btn_Finish_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		/// <summary>
		/// 戦争先を指定するウィンドウの作成
		/// </summary>
		private Form CreateWarAreaSelectWindow()
		{
			var label = new Label {
				Text = "移動先を選択してください。",
				Location = new Point(5, 5),
				AutoSize = true
			};
			var button = new Button {
				Text = "中止",
				Dock = DockStyle.None,
				Location = new Point(60, 20),
			};

			var form = new Form {
				Owner = this.Owner,
				FormBorderStyle = FormBorderStyle.FixedSingle,
				ShowInTaskbar = false,
				ShowIcon = false,
				MaximizeBox = false,
				MinimizeBox = false,
				Size = new Size(155, 85),
			};
			form.Controls.Add(label);
			form.Controls.Add(button);

			button.Click += (sender, e) => form.Close();
			return form;
		}
	}
}
