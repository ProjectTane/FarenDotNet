using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarenDotNet.Loader;
using Paraiba.IO;
using System.IO;

namespace FarenDotNet.FileData.AtAtLoader
{
	public partial class Loader
	{
		// ----- ----- ----- ----- ----- STATIC ----- ----- ----- ----- -----
		const string CONFIG = "Config";

		public bool IsLoadable(string path)
		{
			return File.Exists(Path.Combine(path, CONFIG));
		}

		// ----- ----- ----- ----- ----- 非STATIC ----- ----- ----- ----- -----

		public GameData Load(string path)
		{
			var table = new SettingFileReader(Path.Combine(path, CONFIG));
			string title = table["Name"][0];

			var game = new GameData(title, path);

			game.Scenarios = LoadScenarios(path);

			return game;
		}
	}
}
