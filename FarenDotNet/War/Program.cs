using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Paraiba.Drawing.Surfaces;
using Paraiba.Core;
using Paraiba.Geometry;
using Paraiba.TaskList;
using Paraiba.Utility;
using FarenDotNet.BasicData;
using FarenDotNet.Loader;
using FarenDotNet.Reign;
using FarenDotNet.War.BattleAction;
using FarenDotNet.War.BattleCommand;
using FarenDotNet.War.Loader;
using FarenDotNet.War.Phase;
using FarenDotNet.War.Scope;
using FarenDotNet.War.UI;
using Microsoft.Win32;
using System.IO;

namespace FarenDotNet.War
{
	internal static class Program
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		public static void Main2()
		{
			// アプリケーションの初期設定
			//Application.EnableVisualStyles();
			//Application.SetCompatibleTextRenderingDefault(false);

			var scenarioDir = GetScenarioDir();
			Global.ScenarioDir = scenarioDir;
            var unitDataes = UnitDataLoader.LoadList(new[] {
				Path.Combine(scenarioDir, "DefChar"),
				Path.Combine(scenarioDir, "Element")
            });
			var conquerArea1 = new Area(new AreaData(23));
			var conquerArea2 = new Area(new AreaData(25));
			var conquerList = new List<Tuple<int, IList<Unit>, Area>>();
			conquerList.Add(new Tuple<int, IList<Unit>, Area>(2, new Unit[] { new Unit(unitDataes[0]), new Unit(unitDataes[50]) }, conquerArea1));
			conquerList.Add(new Tuple<int, IList<Unit>, Area>(1, new Unit[] { new Unit(unitDataes[60]), new Unit(unitDataes[55]) }, conquerArea2));

			var battleAreaData = new AreaData(24);
			var battleArea = new Area(battleAreaData);
			battleArea.Units.Add(new Unit(unitDataes[10]));
			battleArea.Units.Add(new Unit(unitDataes[70]));
			battleArea.Units.Add(new Unit(unitDataes[30]));
			battleArea.Units.Add(new Unit(unitDataes[80]));

			var freMaster = new UnitData();
			var eneMaster = new UnitData();

			//コンパイルエラーがでるので、コメント中
			//Province friPro = new Province(10, "TestFriendProvince", fresurface);
			//Province enePro = new Province(11, "TestEnemyProvince", enesurface);
			//area.Province = enePro;


			var phaseCreator = new AutoDeployPhaseCreator(battleArea, null, () => Environment.Exit(0));
			new WarInitializer(unitDataes, battleArea, conquerList, phaseCreator, scenarioDir).Initialize();
		}

		private static string GetScenarioDir()
		{
			var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\AtAt\Faren");
			if (key != null)
				return Path.Combine((key.GetValue("Directory") as string), "Default");

			return Path.Combine(Directory.GetCurrentDirectory(), "Default");
		}
	}
}