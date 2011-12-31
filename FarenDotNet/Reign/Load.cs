using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarenDotNet.NewGame;
using FarenDotNet.Reign.UI;
using System.Windows.Forms;

namespace FarenDotNet.Reign
{
	public class Load
	{
		Save _save;
		Form _owner;

		public Load(Save save, Form owner)
		{
			_save = save;
			_owner = owner;
		}

		public void Create()
		{
			// Root の作成
			Root root = new Root();
			{
				root.LoadGames();
				var pair = root.Pairs.Single(p => p.Loader.GameDirPath == _save.GamePath);

				int index;
				for (index = 0; index < root.Pairs.Count; index++)
					if (root.Pairs[index].Loader.GameDirPath == _save.GamePath)
						break;

				root.GameIndex = index;

				var loader = pair.Loader;
				{
					loader.LoadGame();
					loader.LoadScenarios();
					loader.LoadMasterData();
					loader.LoadUnits();
				}
				root.ScenarioIndex = _save.ScenarioNo - 1;
			}
			var creator = new NewGame(root);
			var manager = creator.Create();
			_save.OverrideData(manager);

			var uiManager = new ReignWindowManager(manager, _owner);
		}
	}
}
