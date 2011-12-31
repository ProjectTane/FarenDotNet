using System;
using System.Collections.Generic;
using System.Linq;
using Paraiba.Collections.Generic;
using Paraiba.Utility;
using FarenDotNet.BasicData;
using FarenDotNet.Reign;
using FarenDotNet.War.Loader;
using FarenDotNet.War.Phase;
using FarenDotNet.War.UI;

namespace FarenDotNet.War
{
	public class WarInitializer
	{
		private readonly Area _area;
		private readonly IList<Tuple<int, IList<Unit>, Area>> _conquerInfo;
		private readonly string _gamePath;
		private readonly PhaseCreator _phaseCreator;
		private readonly IList<UnitData> _units;

		public WarInitializer(IList<UnitData> unitDatas,
		                      Area area, IList<Tuple<int, IList<Unit>, Area>> conquerInfo,
		                      PhaseCreator phaseCreator, string gamepath)
		{
			_units = unitDatas;
			_area = area;
			_conquerInfo = conquerInfo;
			_conquerInfo.OrderBy(conquer => conquer.Item1);
			_phaseCreator = phaseCreator;
			_gamePath = gamepath;
		}

		public void Initialize()
		{
			// 戦争用データのロード
			var areaFileName = string.Format("BMap_NO{0}", _area.No);
			// TODO: 地形の読み込みは一回だけで良い
			var landformLoader = new LandformLoader(_gamePath);
			landformLoader.Load();
			var warMapLoader = new WarMapLoader(_gamePath);
			var warMap = warMapLoader.Load(areaFileName, 100, 200, 300,
				_conquerInfo.SelectToList(conquer => conquer.Item1));

			// キャラクタのロード
			var friends = new WarSide(true);
			var enemies = new WarSide(false);
			var units = new WarUnitCollection();

			//初期配置の時に必要になるので用意
			var friendUnits = new List<List<WarUnit>>();
			var enemyUnits = new List<WarUnit>();

			WarGlobal.UnitBuilder = new WarUnitBuilder(_gamePath, _units);

			// 味方軍
			foreach (var conquer in _conquerInfo)
			{
				friendUnits.Add(new List<WarUnit>());
				foreach (var unit in conquer.Item2)
				{
					var warUnit = WarGlobal.UnitBuilder.Create(unit, friends, conquer.Item3);
					units.AddUnit(warUnit);
					friendUnits.Last().Add(warUnit);
				}
			}

			//敵軍
			foreach (var unit in _area.Units)
			{
				var warUnit = WarGlobal.UnitBuilder.Create(unit, enemies, _area);
				units.AddUnit(warUnit);
				enemyUnits.Add(warUnit);
			}

			// クラスの初期化
			var situation = new Situation(warMap, new[] { friends, enemies }, units);
			var model = new WarPresentationModel(situation);

			// ウィンドウの表示
			var mainWindow = new MainWindow();
			mainWindow.ShowMapWindow(situation, model);
			mainWindow.Show();

			//PhaseManager関連
			_phaseCreator.CreatePhase(model, friendUnits, enemyUnits, mainWindow);

			// 配置フェーズの開始
			situation.PhaseManager.StartPhase(situation, Phases.DeployUnitPhase);
		}
	}
}