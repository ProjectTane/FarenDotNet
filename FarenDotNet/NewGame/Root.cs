using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using FarenDotNet.BasicData;
using FarenDotNet.Loader;
using Microsoft.Win32;


namespace FarenDotNet.NewGame
{

	public class Root
	{
		#region struct DataPair
		public struct GameLoaderDataPair
		{
			public readonly GameLoader Loader;
			public readonly GameData Data;
			public GameLoaderDataPair(GameLoader loader, GameData data)
			{
				Contract.Requires(loader != null, "loader");
				Contract.Requires(data != null, "data");
				this.Loader = loader;
				this.Data = data;
			}
		}
		#endregion

		// ----- ----- ----- Field ----- ----- -----
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		private IList<GameLoaderDataPair> _pairs;

		// UserSelectData;
		private int _gameIndex = -1;
		private int _scenarioIndex = -1;
		private int _level = 2;
		private int _nPlayer = 1;
		private HashSet<int> _selectedMastersNo = new HashSet<int>();
		// ----- ----- ----- Property ----- ----- -----

		public IList<GameLoaderDataPair> Pairs
		{
			get { return _pairs; }
		}

		public int GameIndex
		{
			get { return _gameIndex; }
			set
			{
				Contract.Requires(
					-1 <= value && value < _pairs.Count,
					"値は範囲内でなくてはなりません");
				if (_gameIndex == value)
					return;
				_gameIndex = value;
				if (_gameIndex == -1)
					Global.ScenarioDir = null;
				else
					Global.ScenarioDir = this._pairs[_gameIndex].Loader.GameDirPath;
				_scenarioIndex = -1;
			}
		}

		public GameData SelectedGame
		{
			get
			{
				if (_gameIndex == -1)
					return null;
				return _pairs[_gameIndex].Data;
			}
		}

		public int ScenarioIndex
		{
			get { return _scenarioIndex; }
			set
			{
				Contract.Requires(
					-1 <= value && value < SelectedGame.Scenarios.Count,
					"値は範囲内でなくてはなりません");
				_scenarioIndex = value;
			}
		}

		public ScenarioData SelectedScenario
		{
			get
			{
				if (_scenarioIndex == -1)
					return null;
				return SelectedGame.Scenarios[_scenarioIndex];
			}
		}

		/// <summary>
		/// ゲームレベル
		/// </summary>
		public int Level
		{
			get { return _level; }
			set
			{
				Contract.Requires(0 < value && value <= 5, "value");
				_level = value;
			}
		}

		/// <summary>
		/// プレイヤー数
		/// </summary>
		public int N_Player
		{
			get { return _nPlayer; }
			set {
				Contract.Requires(0 <= value && value < 5, "value");
				_nPlayer = value;
			}
		}

		/// <summary>
		/// プレイヤーが使用するマスターNo.
		/// </summary>
		public HashSet<int> SelectedMastersNo
		{
			get { return _selectedMastersNo; }
		}

		public string RootDir { get; private set; }

		// ----- ----- ----- Method ----- ----- -----

		public int LoadGames()
		{
			// HACK : 二重ロードを防ぐか否か。
			if (_pairs != null)
				return -1;
			_pairs = new List<GameLoaderDataPair>();
			var factory = new GameLoaderFactory();
			var searchDir = GetSearchDirectories();
			var loaders = from search in searchDir
						  from dir in Directory.GetDirectories(search)
						  where factory.IsLoadable(dir)
						  select factory.GetLoader(dir);

			int counter = 0;
			foreach (var loader in loaders) {
				counter++;
				_pairs.Add(new GameLoaderDataPair(loader, loader.LoadGame()));
			}

			return counter;
		}

		private IList<String> GetSearchDirectories()
		{
			// インストールしたディレクトリ
			string installDir = null;
			var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\AtAt\Faren");
			if (key != null)
				installDir = key.GetValue("Directory") as string;
			Global.InstallDir = installDir;

			// カレントディレクトリを追加
			string currentDir = Directory.GetCurrentDirectory();

			if (installDir == null)
			{
				this.RootDir = currentDir;
				return new[] { currentDir };
			}
			else
			{
				this.RootDir = installDir;
				return new[] { installDir, currentDir };
			}
		}
	}
}
