using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using Paraiba;
using System.Diagnostics.Contracts;
using Paraiba.Drawing.Animations.Surfaces;
using Paraiba.Drawing.Surfaces;
using Paraiba.Core;
using FarenDotNet.BasicData;

namespace FarenDotNet.Reign
{
	[DebuggerDisplay("No.{No} {Name}")]
	public class Area
	{
		// ----- ----- ----- CONST ----- ----- -----
		private const int MAX_UNIT = 20;

		// ----- ----- ----- Field ----- ----- -----
		private readonly AreaData _data;
		private readonly List<Unit> _units = new List<Unit>();
		private readonly List<Area> _adjacent = new List<Area>();

		// 100分の1として扱う
		private int _nCity;
		private int _nRoad;
		private int _nWall;

		private int _defIncome;
		private Province _province;

		// ----- ----- ----- Event ----- ----- -----
		public Action<Province> ProvinceChanged;

		// ----- ----- ----- Property ----- ----- -----
		// 不変データ
		public AreaData Data { get { return _data; } }
		public int No { get { return _data.No; } }
		public string Name { get { return _data.Name; } }

		public int MaxCity { get { return _data.NumCity; } }
		public int MaxRoad { get { return _data.NumRoad; } }
		public int MaxWall { get { return _data.NumWall; } }

		public Point Location { get { return _data.FlagLocation; } }
		public List<Area> Adjacent { get { return _adjacent; } }
		public int AreaType { get { return _data.AreaType; } }

		// 可変データ
		public Province Province
		{
			get { return _province; }
			set
			{
				Contract.Requires(value != null, "value");
				if (_province != value)
				{
					_province = value;
					ProvinceChanged.InvokeIfNotNull(value);
				}
			}
		}

		public List<Unit> Units { get { return _units; } }
		public WaitUnitAction WaitAction { get; set; }

		public int NumCity { get { return _nCity / 100; } }
		public int NumRoad { get { return _nRoad / 100; } }
		public int NumWall { get { return _nWall / 100; } }

		public int NumCity100
		{
			get { return _nCity; }
			set { _nCity = XMath.Center(value, 0, MaxCity * 100); }
		}
		public int NumRoad100
		{
			get { return _nRoad; }
			set { _nRoad = XMath.Center(value, 0, MaxRoad * 100); }
		}
		public int NumWall100
		{
			get { return _nWall; }
			set { _nWall = XMath.Center(value, 0, MaxWall * 100); }
		}

		public int Income
		{
			get { return _defIncome + (_nRoad == MaxRoad ? 2 : 1) * _nCity / 400; }
			set { /* 今のところ何もしない */ }
		}

		// ----- ----- ----- Method ----- ----- -----
		public Area(AreaData data)
		{
			Contract.Requires(data != null, "data");
			_data = data;
			_defIncome = Data.DefaultIncome;
		}

		public void SetRandomNeutrals(int level, IList<Callable> callable, IList<UnitData> units)
		{
			Contract.Requires(callable != null, "callable");
			Contract.Requires(units != null, "units");

			double defMoney = Math.Pow(2, 0.5 * level) * 200; 
			var rand = Global.Random;
			var money = (int)(defMoney * this.Data.NeutralPower * (rand.NextDouble() + 0.5) * 0.01);
			var callNames = callable.Single(c => c.No == this.AreaType).Init;
			var callUnit =
				from id in callNames
				join unit in units on id equals unit.ID
				select unit;
			this.SetRandomUnits(money, callUnit);
		}

		public void SetRandomUnits(int money, IEnumerable<UnitData> calls)
		{
			// 値段による重み付け乱数により雇用することにします（仮）
			// 初期化
			var units = calls.ToArray();
			Array.Sort(units, (u1, u2) => u1.Cost - u2.Cost);
			var costs = new int[units.Length];
			int sum = 0;
			for (int i = 0; i < units.Length; i++)
			{
				sum += units[i].Cost;
				costs[i] = sum;
			}
			int max = units.Length - 1;
			while (_units.Count < 20)
			{
				while (max >= 0 && units[max].Cost > money) max--; // 雇えないユニットを外す
				if (max < 0) break;

				int select = Global.Random.Next(costs[max]);
				int start = 0, end = max;
				while (start < end)
				{
					int middle = (start + end) / 2;
					if (costs[middle] < select)
						start = middle + 1;
					else
						end = middle;
				}
				// チェック
				Debug.Assert(start == end);
				Debug.Assert(select <= costs[start]);
				Debug.Assert(start == 0 || costs[start - 1] < select);
				Debug.Assert(units[start].Cost <= money);
				// 雇用
				var selected = units[start];
				_units.Add(new Unit(selected));
				money -= selected.Cost;
			}
			_units.Sort();
		}

		// ----- ----- ----- Inner ----- ----- -----
		public enum WaitUnitAction
		{
			Train, Search, CityDevelop, RoadConst, WallConst
		}
	}
}
