using System;
using FarenDotNet.War.Phase;

namespace FarenDotNet.War.AbnormalCondition
{
	public class SummoningCondition : IAbnormalCondition
	{
		private readonly WarUnit _summonUnit;

		public SummoningCondition(WarUnit summonUnit)
		{
			_summonUnit = summonUnit;
		}

		#region IAbnormalCondition Members

		public string Name
		{
			get { return "召喚中"; }
		}

		public string ID
		{
			get { return "ファーレントゥーガ.召喚中"; }
		}

		/// <summary>
		/// ユニットに状態異常が追加される直前に呼び出される。
		/// </summary>
		/// <param name="situation"></param>
		/// <param name="unit"></param>
		/// <param name="oldCond"></param>
		public bool AddingConditonTable(Situation situation, WarUnit unit, IAbnormalCondition oldCond)
		{
			return false;
		}

		public void AddedConditonTable(Situation situation, WarUnit unit)
		{
			_summonUnit.DiedEvent += SummonUnitDied;
			unit.ReviseStatus += ReviseStatus;
		}

		public void RemovedConditonTable(Situation situation, WarUnit unit)
		{
			_summonUnit.DiedEvent -= SummonUnitDied;
			unit.ReviseStatus -= ReviseStatus;
		}

		#endregion

		private void SummonUnitDied(Situation situation, WarUnit unit, WarUnit doer)
		{
			unit.Conditions.Remove(ID, situation);
		}

		private static void ReviseStatus(Status status)
		{
			status.IsFreezing = true;
		}
	}
}