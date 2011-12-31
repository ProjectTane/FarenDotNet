using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using Paraiba.Linq;
using Paraiba.Utility;
using FarenDotNet.BasicData;
using FarenDotNet.Loader;
using FarenDotNet.NewGame;

namespace FarenDotNet.Reign
{
	public class NewGame
	{
		// ----- ----- ----- Field ----- ----- -----
		Root _root;
		ReignLoader _loader;
		ReignData _data;

		// ----- ----- ----- Method ----- ----- -----
		public NewGame(Root root)
		{
			Contract.Requires(root != null, "root");
			_root = root;

			var path = root.Pairs[root.GameIndex].Loader.GameDirPath;
			_loader = new ReignLoader(path, root.RootDir);
			_data = _loader.Load();
		}

		/// <summary>
		/// マネージャ作成
		/// </summary>
		public ReignManager Create()
		{
			var uniques = new UniqueUnitCollection(
				_root.SelectedGame.Units,
				(id, units) => new Unit(units.Single(u => u.ID == id)));
			var provinces = CreateProvinces(uniques);
			var areas = CreateAreas(provinces);

			var map = new WorldMap(areas, _data.WorldMap);
			int level = _root.Level;
			int turn = _root.SelectedScenario.StartTurn;

			var gPath = _root.Pairs[_root.GameIndex].Loader.GameDirPath;

			var script = new ScriptLoader().Load(
				Path.Combine(gPath, Path.Combine("Data", "EventScript")),
				_root.SelectedGame.Units);

			var manager = new ReignManager(map, provinces, level, turn, _root.ScenarioIndex + 1) {
				UnitsData = _root.SelectedGame.Units,
				Script = script,
				Callable = _data.Callables,
				GamePath = gPath,
			};
			SetLeague(manager.League);
			return manager;
		}

		private void SetLeague(League league)
		{
			var leagueData = _root.SelectedScenario.Leagues;
			foreach (var l in leagueData)
			{
				league[l.ProvNoA, l.ProvNoB] = l.Turn;
			}
		}

		/// <summary>
		/// 全ての勢力を作成
		/// </summary>
		private List<Province> CreateProvinces(IUniqueUnitCollection uniques)
		{
			var list = new List<Province>();
			var masters = _root.SelectedGame.MastersData.Masters;
			var units = _root.SelectedGame.Units;
			var player = _root.SelectedMastersNo;


			list.Add(new Province(0, "", _data.WorldMap.NewtralFlagImage));

			var provs =
				from d in masters
				join u in units on d.ID equals u.ID
				let isPlayer = player.Contains(d.No)
				select new { No = d.No, Name = d.Name, Unit = u, IsPlayer = isPlayer};

			foreach (var p in provs)
			{
				var po = new Province(p.No, p.Name, p.Unit) {
					IsPlayer = p.IsPlayer,
					Money = 50
				};
				list.Add(po);
			}
			return list;
		}

		/// <summary>
		/// エリア作成関数
		/// </summary>
		private List<Area> CreateAreas(IList<Province> provinces)
		{
			Contract.Requires(!((IEnumerable<Province>) provinces).IsNullOrEmpty());
			var owners = _root.SelectedScenario.AreaOwner;

			// エリア作成
			var tmp = _data.Areas.Select(a => new { name = a.Name, power = a.NeutralPower }).ToList();

			var areaList = new List<Area>();
			foreach (var data in _data.Areas)
				areaList.Add(CreateArea(data, provinces));

			// 隣接エリアの設定
			foreach (var area in areaList) {
				var adj = from no in area.Data.Adjacent
						  where no > 0
						  join a in areaList on no equals a.No
						  select a;
				area.Adjacent.AddRange(adj);
			}

			// 残りの雇用
			SetUnits(areaList);

			return areaList;
		}

		private Area CreateArea(AreaData data, IList<Province> provinces)
		{
			Area area = new Area(data);
			// 勢力の設定
			var provNo = _root.SelectedScenario.AreaOwner[data.No - 1];
			switch (provNo)
			{
			case -1:
				break;
			case 0:
				// 中立
				area.Province = provinces.Single(p => p.No == provNo);
				break;
			default:
				// 通常
				area.Province = provinces.Single(p => p.No == provNo);
				break;
			}
			// 固有ユニットのセット
			var locs = _root.SelectedScenario.Locates;
			var units = _root.SelectedGame.Units;
			var locates = from l in locs
						  where l.AreaNo == area.No
						  join unit in units on l.UnitID equals unit.ID
						  let rank = (UnitRank)l.Rank
						  select new Unit(unit, rank);
			area.Units.AddRange(locates);
			while (area.Units.Count > 20) area.Units.RemoveAt(area.Units.Count - 1);
			// 街、道路、城壁の値をセット
			switch (provNo)
			{
			case -1:
			case 0:
				area.NumCity100 = area.MaxCity * 10;
				area.NumRoad100 = area.MaxRoad * 10;
				area.NumWall100 = area.MaxWall * data.DefautlWallRate;
				break;
			default:
				area.NumCity100 = area.MaxCity / 6 * 100;
				area.NumRoad100 = area.MaxRoad / 6 * 100;
				area.NumWall100 = area.MaxWall * (data.DefautlWallRate + 40);
				// マスターのいる拠点だったばあい
				if (area.Units.Any(u => u.ID == area.Province.Master.ID))
				{
					area.NumCity100 *= 2;
					area.NumRoad100 *= 2;
					area.NumWall100 = area.MaxWall * 100;
				}
				break;
			}

			return area;
		}

		private void SetUnits (IEnumerable<Area> areas)
		{
			var level = _root.Level;
			double defMoney = Math.Pow(2, 0.5 * level) * 200; // たぶんこれくらい。
			var units = _root.SelectedGame.Units;

			foreach (var area in areas)
			{
				if (area.Province == null) // イベント用ダミー
					continue;

				int money = 0;
				IEnumerable<UnitData> callable;

				if (area.Province.IsNeutral)
				{
					area.SetRandomNeutrals(_root.Level, _data.Callables, units);
				}
				else
				{
					// お金は適当に1割で乱数掛ける
					if (area.Units.Any(u => u.ID == area.Province.Master.ID))
					{
						money = Global.Random.Next(450,550);
					}
					else if (area.Adjacent.Any(a => a.Province != area.Province))
					{
						money = Global.Random.Next(270,330);
					}
					else
					{
						money = Global.Random.Next(90, 110);
					}
					var callNo = area.Province.Master.Summon;
					callable = callNo.Where(n => n > 0).Select(no => units[no - 1]);
					area.SetRandomUnits(money, callable);
				}
			}
		}

	}
}
