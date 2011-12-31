using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using FarenDotNet.BasicData;
using FarenDotNet.Reign.UI;
using Paraiba.Utility;

namespace FarenDotNet.Reign
{
	/// <summary>
	/// 内政・戦略画面全体を統括するクラス
	/// </summary>
	public class ReignManager
	{
		// ----- ----- ----- ----- ----- FIELD ----- ----- ----- ----- -----
		WorldMap _worldMap;
		IList<Province> _liveProvs;
		IList<Province> _provinces;
		int _turn;
		League _league = new League();

		Province _actingProvince = null;
		Queue<Province> _turnQueue = new Queue<Province>();
		Phase _turnPhase;

		// ----- ----- ----- ----- ----- PUBLIC FIELD ----- ----- ----- ----- -----
		public readonly List<bool> Flag = new List<bool>();
		public readonly List<Province> DiedProvCache = new List<Province>();
		public readonly int StartTurn;
		public readonly int ScenarioNo;
		public readonly bool WatchingMode = false;

		// ----- ----- ----- ----- ----- PROPERTY ----- ----- ----- ----- -----
		public WorldMap WorldMap { get { return _worldMap; } }
		public int Level { get; set; }

		public int Turn { get { return _turn; } }
		public IList<Province> LiveProvs { get { return _liveProvs; } }
		public Province ActingProv { get { return _actingProvince; } }
		public League League { get { return _league; } }
		public Queue<Province> TurnQueue { get { return _turnQueue; } }

		public IUniqueUnitCollection UniqueUnits { get; set; }
		public IList<UnitData> UnitsData { get; set; }
		public Func<Scripter, IEnumerable<int>> Script { get; set; }
		public IList<Callable> Callable { get; set; }
		public string GamePath { get; set; }
		public IList<Province> AllProvince { get { return _provinces; } }

		// ----- ----- ----- ----- ----- METHOD ----- ----- ----- ----- -----
		public ReignManager(WorldMap map, IList<Province> provinces, int level, int startTurn, int scenarioNo)
		{
			Contract.Requires(map != null, "map");
			Contract.Requires(provinces != null, "provinces");
			Contract.Requires(1 <= level && level <= 5, "level");
			Contract.Requires(startTurn > 0, "turn");

			_worldMap = map;
			_liveProvs = provinces;
			Level = level;
			_turn = startTurn;
			StartTurn = startTurn;
			ScenarioNo = scenarioNo;

			_provinces = new Province[provinces.Count];
			for (int i = 0; i < _provinces.Count; i++)
				_provinces[i] = provinces[i];

			// 参加してないマスターを排除
			this.ProvinceDieCheck();
			this.DiedProvCache.Clear();
		}

		/// <summary>
		/// ゲームを開始する
		/// コルーチンとして動作
		/// </summary>
		public IEnumerable<Phase> Start()
		{
			while (true) {
				switch (this._turnPhase)
				{
				case Phase.TurnStart:
					this.ProvinceDieCheck();
					TurnStartEvent();

					_turnQueue.Clear();
					foreach (var p in _liveProvs)
						_turnQueue.Enqueue(p);
					yield return Phase.TurnStart;
					this._turnPhase = Phase.PlayerTurn;
					
					goto case Phase.PlayerTurn;

				case Phase.PlayerTurn:
					if (_turnQueue.Count == 0)
					{
						this._turnPhase = Phase.TurnEnd;
						goto case Phase.TurnEnd;
					}

					this.ProvinceDieCheck();
					_actingProvince = _turnQueue.Dequeue();
					yield return Phase.PlayerTurn;
					_actingProvince = null;

					this._turnPhase = Phase.PlayerTurn;
					goto case Phase.PlayerTurn;

				case Phase.TurnEnd:
					TurnEndEvent();
					if (this.WinGame())
						yield return Phase.Win;

					if (!WatchingMode && !_liveProvs.Any(p => p.IsPlayer))
						yield return Phase.GameOver;

					this._turnPhase = Phase.TurnStart;
					goto case Phase.TurnStart;
				}
			}
		}

		public IEnumerable<int> GetScript(Scripter scripter)
		{
			//return SampleEventScript.Run(scripter);
			//return EventScript.Run(scripter);
			return this.Script(scripter);
		}

		public IList<ProvinceInfo> GetProvinceInfos()
		{
			var provs =
				from area in _worldMap.Areas
				group area by area.Province into prov
				where prov.Key.No > 0
				orderby prov.Key.No
				select new { p = prov.Key, a = prov.ToList() };

			return provs
				.Where(i => i.a.Count > 0)
				.Select(i => new ProvinceInfo(i.p, i.a, this))
				.ToList();
		}

		public List<Province> ProvinceDieCheck()
		{
			var died = new List<Province>();
			foreach (var prov in _liveProvs)
			{
				if (prov.IsNeutral) continue;

				var existMaster = _worldMap.Areas
					.Where(a => a.Province == prov)
					.Any(a => a.Units
						.Any(u => u.IsMaster));

				if (existMaster == false)
				{
					died.Add(prov);
					var newtral = _provinces.First(p => p.IsNeutral);
					var areas = _worldMap.Areas.Where(a => a.Province == prov);
					foreach (var a in areas)
					{
						a.Province = newtral;
						this.FillNeutral(a);
					}
				}
			}

			foreach (var prov in died)
				_liveProvs.Remove(prov);

			this.DiedProvCache.AddRange(died);
			return this.DiedProvCache;
		}

		// ----- ----- ----- ----- ----- PRIVATE METHOD ----- ----- ----- ----- -----

		private void TurnStartEvent()
		{
			// 資金の増加
			foreach (var prov in _liveProvs)
			{
				int income = 0;
				int cost = 0;
				foreach (var a in _worldMap.Areas)
				{
					if (a.Province != prov)
						continue;
					income += a.Income;
					cost += a.Units.Where(u => u.IsUnique).Sum(u => u.Cost);
				}
				prov.Money += (income - cost);
			}
			// 再行動可能に
			foreach (var a in _worldMap.Areas)
				foreach (var u in a.Units)
					u.Acted = false;
		}

		private void TurnEndEvent()
		{
			// ターン数増加
			_turn++;
			// 建設など
			const int AUTO_WALL = 8;
			const int CITY_INC = 24;
			const int ROAD_INC = 40;
			const int WALL_INC = 96;
			foreach (var a in _worldMap.Areas)
			{
				var unacted = a.Units.Where(u => !u.Acted);
				switch (a.WaitAction)
				{
				case Area.WaitUnitAction.Train:
					foreach (var u in unacted)
					{
						u.Exp += (byte)(8 - (int)u.Rank + Global.Random.Next(12 - Level * 2));
					}
					break;
				case Area.WaitUnitAction.Search:
					break;
				case Area.WaitUnitAction.CityDevelop:
					a.NumCity100 += unacted.Count() * CITY_INC;
					break;
				case Area.WaitUnitAction.RoadConst:
					a.NumRoad100 += unacted.Count() * ROAD_INC;
					break;
				case Area.WaitUnitAction.WallConst:
					a.NumWall100 += unacted.Count() * WALL_INC;
					break;
				}
				// 自動建築
				if (a.NumWall < a.MaxWall)
					a.NumWall100 += a.Units.Count * AUTO_WALL;
			}
			// 同盟ターンの現象
			_league.DeclementAll();
		}

		private bool WinGame()
		{
			if (_liveProvs.Count == 0)
				return false;
			var hash = new HashSet<Province>();
			var stack = new Stack<Province>();
			stack.Push(_liveProvs[0]);
			while (stack.Count > 0)
			{
				var prov = stack.Pop();
				hash.Add(prov);
				foreach (var p in _liveProvs)
				{
					if (hash.Contains(p)) continue;
					if (_league[p.No, prov.No] == League.INF)
					{
						hash.Add(p);
						stack.Push(p);
					}
				}
			} // stack.Count == 0
			return _liveProvs.All(p => hash.Contains(p));
		}

		/// <summary>
		/// 指定したエリアを中立で埋める
		/// </summary>
		private void FillNeutral(Area area)
		{
			area.SetRandomNeutrals(Level, Callable, UnitsData);
		}

		// ----- ----- ----- ----- ----- INTERNAL METHOD ----- ----- ----- ----- -----
		internal void SetData(int turn, int level, League league)
		{
			_turn = turn;
			Level = level;
			_league = league;
			this._turnPhase = Phase.PlayerTurn;
		}
	}
}
