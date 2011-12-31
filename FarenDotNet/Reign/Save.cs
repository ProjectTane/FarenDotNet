using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Paraiba.Collections.Generic;
using Paraiba.Core;
using FarenDotNet.BasicData;

namespace FarenDotNet.Reign
{
	[Serializable]
	public class Save
	{
		// ReignMasterからのロード
		Queue<int> _turnQueue; // 行動中のもつっこむこと
		List<int> _liveProv;
		int _turn;
		int _level;
		int _scenario;
		League _league;

		string _gamepath;

		List<Save.Area> _areas;
		List<Save.Province> _provs;

		public string GamePath { get { return _gamepath; } }
		public int ScenarioNo { get { return _scenario; } }

		public Save(ReignManager manager)
		{
			_turnQueue = new Queue<int>(manager.TurnQueue.Select(p => p.No));
			_liveProv = new List<int>(manager.LiveProvs.Select(p => p.No));
			_turn = manager.Turn;
			_level = manager.Level;
			_scenario = manager.ScenarioNo;
			_league = manager.League;
			_gamepath = manager.GamePath;

			_areas = new List<Save.Area>();
			foreach (var a in manager.WorldMap.Areas)
			{
				_areas.Extend(a.No + 1);
				_areas[a.No] = new Save.Area(a);
			}

			_provs = new List<Save.Province>();
			foreach (var p in manager.AllProvince)
			{
				_provs.Extend(p.No + 1);
				_provs[p.No] = new Save.Province(p);
			}
		}

		public void OverrideData(ReignManager manager)
		{
			// 基本データのセット
			manager.SetData(_turn, _level, _league);
			manager.DiedProvCache.Clear();

			// 勢力データの上書き
			foreach (var p in manager.AllProvince)
				_provs[p.No].OverrideData(p);

			// エリアデータの上書き
			foreach (var a in manager.WorldMap.Areas)
				_areas[a.No].OverrideData(a, manager.AllProvince, manager.UnitsData);

			// TurnQueue&LiveProvの上書き
			var provs = manager.AllProvince;
			var queue = manager.TurnQueue;
			queue.Clear();
			foreach (var i in _turnQueue)
				queue.Enqueue(provs.Single(p => p.No == i));

			var live = manager.LiveProvs;
			live.Clear();
			foreach (var i in _liveProv)
				live.Add(provs.Single(p => p.No == i));
		}

		#region 内部クラス

		[Serializable]
		[DebuggerDisplay("S-Area /{_nCity}/{_nWall}/{_nRoad}/ Prov.{_provNo} {_units.Length}")]
		public class Area
		{
			Save.Unit[] _units;
			int _nCity, _nWall, _nRoad;
			int _provNo;

			public Area(Reign.Area area)
			{
				_provNo = area.Province.No;
				_nCity = area.NumCity100;
				_nRoad = area.NumRoad100;
				_nWall = area.NumWall100;

				_units = new Save.Unit[area.Units.Count];
				for (int i = 0; i < _units.Length; i++)
					_units[i] = new Save.Unit(area.Units[i]);
			}

			public void OverrideData(Reign.Area area, IList<Reign.Province> provs, IList<UnitData> data)
			{
				var prov = provs.Single(p => p.No == _provNo);
				area.Province = prov;

				area.NumCity100 = _nCity;
				area.NumRoad100 = _nRoad;
				area.NumWall100 = _nWall;

				area.Units.Clear();
				area.Units.AddRange(_units.Select(u => u.ToUnit(data)));
			}
		}

		[Serializable]
		[DebuggerDisplay("S-Prov Money={_money} Player={_isPlayer}")]
		public class Province
		{
			int _money;
			bool _isPlayer;

			public Province(Reign.Province prov)
			{
				_money = prov.Money;
				_isPlayer = prov.IsPlayer;
			}

			public void OverrideData(Reign.Province prov)
			{
				prov.Money = _money;
				prov.IsPlayer = _isPlayer;
			}
		}

		/// <summary>
		/// このクラスはReign.Unitをコピーして復元するクラスです
		/// </summary>
		[Serializable]
		[DebuggerDisplay("S-Unit {_id}")]
		public class Unit
		{
			string _id;
			UnitRank _rank;
			int _exp;
			int _obCount; // 撃破数
			bool _acted;

			public Unit(Reign.Unit unit)
			{
				_id = unit.ID;
				_rank = unit.Rank;
				_exp = unit.Exp;
				_obCount = unit.ObliterateCount;
				_acted = unit.Acted;
			}

			public Reign.Unit ToUnit(IList<UnitData> data)
			{
				var uData = data.Single(u => u.ID == _id);

				return new Reign.Unit(uData, _rank, _exp, _obCount) {
					Acted = _acted
				};
			}
		}

		#endregion
	}
}
