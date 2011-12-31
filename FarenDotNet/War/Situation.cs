using System;
using System.Collections.Generic;
using FarenDotNet.War.Phase;

namespace FarenDotNet.War
{
	public class Situation
	{
		private readonly WarMap _map;
		private readonly PhaseManager _phaseManager;
		private readonly WarUnitCollection _units;
		private WarUnit _activeUnit;
		private IList<WarSide> _sides;
		private TurnManager _turnManager;

		private int turnCount;

		public Situation(WarMap warMap, IList<WarSide> troops, WarUnitCollection units)
		{
			_map = warMap;
			Sides = troops;
			_units = units;
			_phaseManager = new PhaseManager();
			_turnManager = new TurnManager();
		}

		public TurnManager TurnManager
		{
			get { return _turnManager; }
			set { _turnManager = value; }
		}

		public PhaseManager PhaseManager
		{
			get { return _phaseManager; }
		}

		public WarUnitCollection Units
		{
			get { return _units; }
		}

		/// <summary>戦闘に参加している全勢力のリスト</summary>
		public IList<WarSide> Sides
		{
			get { return _sides; }
			set
			{
				var old = _sides;
				if (old != value)
				{
					_sides = value;
					if (SidesChanged != null)
						SidesChanged(value);
				}
			}
		}

		/// <summary>戦闘のマップ</summary>
		public WarMap Map
		{
			get { return _map; }
		}

		/// <summary>現在行動可能なユニット</summary>
		public WarUnit ActiveUnit
		{
			get { return _activeUnit; }
			set
			{
				var old = _activeUnit;
				if (old != value)
				{
					_activeUnit = value;
					if (ActiveUnitChanged != null)
						ActiveUnitChanged(value);
				}
			}
		}

		public int TurnCount
		{
			get { return turnCount; }
			set
			{
				var old = turnCount;
				if (old != value)
				{
					turnCount = value;
					if (TurnCountChanged != null)
						TurnCountChanged(value);
				}
			}
		}

		public event Action<IList<WarSide>> SidesChanged;
		public event Action<WarUnit> ActiveUnitChanged;
		public event Action<int> TurnCountChanged;
	}
}