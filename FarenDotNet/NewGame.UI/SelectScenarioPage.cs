using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics.Contracts;

namespace FarenDotNet.NewGame.UI
{
	public partial class SelectScenarioPage : Page
	{
		Root _root;

		public SelectScenarioPage(Root root)
		{
			_root = root;
			InitializeComponent();
		}

		/// <summary>
		/// 最初の描画のとき呼び出される。
		/// </summary>
		private void SelectScenarioPage_Load(object sender, EventArgs e)
		{
			_root.LoadGames();
			foreach (var pair in _root.Pairs) {
				this.lbx_SelectGame.Items.Add(pair.Data);
			}
		}

		/// <summary>
		/// ゲームタイトルを選んだときに発生するイベント
		/// </summary>
		private void lbx_SelectGame_SelectedIndexChanged(object sender, EventArgs e)
		{
			int index = lbx_SelectGame.SelectedIndex;
			_root.GameIndex = index;
			// 何も選択していない
			if (index == -1) return;

			var pair = _root.Pairs[index];
			pair.Loader.LoadScenarios();

			this.SuspendLayout();
			{
				this.CanNext = false;
				this.lbx_SelectScenario.Items.Clear();
				foreach (var sc in pair.Data.Scenarios)
					this.lbx_SelectScenario.Items.Add(sc);
			}
			this.ResumeLayout();
		}

		/// <summary>
		/// シナリオを選択したときに発生するイベント
		/// </summary>
		private void lbx_SelectScenario_SelectedIndexChanged(object sender, EventArgs e)
		{
			int index = lbx_SelectScenario.SelectedIndex;
			_root.ScenarioIndex = index;
			if (index == -1) {
				this.tbx_Explanation.Clear();
				return;
			}
			this.tbx_Explanation.Text = _root.SelectedScenario.Explanation;
			this.CanNext = true;
		}
	}
}
