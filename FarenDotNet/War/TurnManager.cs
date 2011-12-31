using System.Collections.Generic;
using System.Linq;

namespace FarenDotNet.War
{
	public class TurnManager
	{
		private const int MAX_LOOP = 5;
		private const int NEEDED_ACTIVITY = 500;
		private readonly Queue<WarUnit> _waitingUnits;
		private int _loopCount;
		private int _trun;

		public TurnManager()
		{
			_waitingUnits = new Queue<WarUnit>();
		}

		public int Turn
		{
			get { return _trun; }
			set { _trun = value; }
		}

		public WarUnit GetInitiativeUnit(Situation situation)
		{
			do
			{
				// 行動可能な生存者を取得する
				while (_waitingUnits.Count != 0)
				{
					var unit = _waitingUnits.Dequeue();
					if (unit.Alive && unit.Status.IsFreezing == false)
					{
						// イニシアチブを取ったので、行動値を減らす
						unit.AddActivity(situation, -NEEDED_ACTIVITY);
						return unit;
					}
				}
				// 行動可能な生存者の行動値を増やす
				var freeUnits = situation.Units.Alive
					.Where(u => !u.Status.IsFreezing);
				foreach (var unit in freeUnits)
				{
					unit.AddActivity(situation, (int)(unit.Status.Agi * 0.8 + 54.5));
					// 
					if (unit.Status.Activity > NEEDED_ACTIVITY)
						_waitingUnits.Enqueue(unit);
				}
				// ターン処理を進める
			} while (++_loopCount <= MAX_LOOP);
			_loopCount = 0;
			return null;
		}
	}
}