using FarenDotNet.War.Phase;

namespace FarenDotNet.War.AbnormalCondition
{
	public class PoisonCondition : IAbnormalCondition
	{
		private WarUnit _unit;

		#region IAbnormalCondition Members

		public string Name
		{
			get { return "毒"; }
		}

		public string ID
		{
			get { return "ファーレントゥーガ.毒"; }
		}

		/// <summary>
		/// ユニットに状態異常が追加される直前に呼び出される。
		/// </summary>
		/// <param name="situation"></param>
		/// <param name="unit"></param>
		public bool AddingConditonTable(Situation situation, WarUnit unit, IAbnormalCondition oldCond)
		{
			return false;
		}

		public void AddedConditonTable(Situation situation, WarUnit unit)
		{
			_unit = unit;

			Phases.TurnPhase.Exit += PoisonDamage;
		}

		public void RemovedConditonTable(Situation situation, WarUnit unit)
		{
			Phases.TurnPhase.Exit -= PoisonDamage;
		}

		#endregion

		private void PoisonDamage(Situation situation)
		{
			_unit.DamageHP(situation, null, (int)(_unit.OriginalStatus.MaxHP * 0.1));
		}
	}
}