using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows.Forms;
using FarenDotNet.NewGame;
using FarenDotNet.NewGame.UI;
using FarenDotNet.Reign;
using FarenDotNet.Reign.UI;

namespace FarenDotNet
{
	public partial class MainWindow : Form
	{
		// ----- ----- ----- フィールド ----- ----- -----

		// ----- ----- ----- メソッド ----- ----- -----
		public MainWindow()
		{
			InitializeComponent();
		}

		// ----- ----- ----- イベント用メソッド ----- ----- -----

		private void btn_WorldMap_Click(object sender, EventArgs e)
		{
			// ユーザ選択の代わりに適当なデータを与えゲームを始める。
			var root = new Root();
			{
				root.LoadGames();
				root.GameIndex = 0;
				var loader = root.Pairs[root.GameIndex].Loader;
				{
					loader.LoadGame();
					loader.LoadScenarios();
					loader.LoadMasterData();
					loader.LoadUnits();
				}
				root.ScenarioIndex = 1;
				root.SelectedMastersNo.Add(1);
			}
			ShowReignWindow(root);
		}

		private void btn_NewGame_Click(object sender, EventArgs e)
		{
			Root root;
			using (var window = new NewGameWindow())
			{
				window.ShowDialog();
				if (window.DialogResult != DialogResult.OK)
					return;
				root = window.Root;
			}
			ShowReignWindow(root);
		}

		/// <summary>
		/// 戦略ウィンドウの表示
		/// </summary>
		private void ShowReignWindow(Root root)
		{
			var creator = new Reign.NewGame(root);
			var manager = creator.Create();
			var uiManager = new ReignWindowManager(manager, this);
		}

		private void btn_War_Click(object sender, EventArgs e)
		{
			War.Program.Main2();
		}

		private void btn_Load_Click(object sender, EventArgs e)
		{
			if (_openFileDialog.ShowDialog() == DialogResult.OK)
			{
				Save save;
				using (var fs = new FileStream(
					_openFileDialog.FileName,
					FileMode.Open,
					FileAccess.Read))
				{
					var bf = new BinaryFormatter();
					save = bf.Deserialize(fs) as Save;
				}
				if (save == null)
				{
					MessageBox.Show("ファイルフォーマットが違います");
					return;
				}
				var load = new Load(save, this);
				load.Create();
			}
		}
	}
}