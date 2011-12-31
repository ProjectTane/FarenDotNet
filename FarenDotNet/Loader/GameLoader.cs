using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Diagnostics.Contracts;
using FarenDotNet.BasicData;

namespace FarenDotNet.Loader
{
	public class GameLoader
	{
		// ----- ----- ----- Field ----- ----- -----
		private readonly string _gamePath;
		private GameData _gameData;

		// ----- ----- ----- ctor ----- ----- -----
		public GameLoader(string path)
		{
			_gamePath = path;
		}

		// ----- ----- ----- Property ----- ----- -----
		public GameData GameData
		{
			get { return _gameData; }
		}

		public string GameDirPath
		{
			get { return _gamePath; }
		}

		// ----- ----- ----- Method ----- ----- -----

		/// <summary>
		/// ゲームのタイトル等を読み込む。
		/// </summary>
		public GameData LoadGame()
		{
			if (_gameData != null)
				return _gameData;

			const string fileName = "Config";
			string filePath = Path.Combine(_gamePath, fileName);
			var table = new SettingFileReader(filePath);

			try
			{
				return _gameData = new GameData(table["Name"][0]);
			} catch (IndexOutOfRangeException e)
			{
				throw new DataFormatException("タイトル名が指定されていません", filePath, e);
			} catch (NullReferenceException e)
			{
				throw new DataFormatException("タイトルがありません", filePath, e);
			}
		}

		/// <summary>
		/// 複数あるシナリオデータを読み込む。
		/// </summary>
		public void LoadScenarios()
		{
			if (_gameData.Scenarios != null)
				return;

			var list = new List<ScenarioData>();
			var loader = new ScenarioLoader();

			string dataPath = Path.Combine(_gamePath, "Data");
			string filePath;
			int num = 1;
			// Scenario1 ファイルから順に調べる
			while (File.Exists(filePath = Path.Combine(dataPath, "Scenario" + num++)))
			{
				var sc = loader.Load(filePath);
				Debug.Assert(sc != null);
				list.Add(sc);
			}
			_gameData.Scenarios = list;
		}

		/// <summary>
		/// マスターデータを読み込む
		/// </summary>
		public void LoadMasterData()
		{
			if (_gameData.MastersData != null)
				return;

			var loader = new MasterDataLoader();
			var filepath = Path.Combine(_gamePath, Path.Combine("Data", "MasterData"));
			_gameData.MastersData = loader.Load(filepath);
		}

		public void LoadUnits()
		{
			if (_gameData.Units != null)
				return;
			var dirs = GameData.MastersData.CharaDirs.Select(dir => Path.Combine(_gamePath, dir));

			var list = UnitDataLoader.LoadList(dirs.ToList());
			_gameData.Units = list;
		}
	}
}