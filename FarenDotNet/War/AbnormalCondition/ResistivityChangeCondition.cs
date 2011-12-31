using System;
using FarenDotNet.BasicData;

namespace FarenDotNet.War.AbnormalCondition
{
	public class ResistivityChangeCondition : IAbnormalCondition
	{
		private readonly ResistivityType _newRegistivityType;
		private readonly AttackType _targetAttackType;

		public ResistivityChangeCondition(AttackType targetAttackType, ResistivityType newRegistivityType)
		{
			_targetAttackType = targetAttackType;
			_newRegistivityType = newRegistivityType;
		}

		#region IAbnormalCondition メンバ

		public string Name
		{
			get { return "耐性変化"; }
		}

		public string ID
		{
			get { return "ファーレントゥーガ.耐性変化:" + _targetAttackType; }
		}

		/// <summary>
		/// ユニットに状態異常が追加される直前に呼び出される。
		/// </summary>
		/// <param name="situation"></param>
		/// <param name="unit"></param>
		/// <param name="oldCond"></param>
		public bool AddingConditonTable(Situation situation, WarUnit unit, IAbnormalCondition oldCond)
		{
			return true;
		}

		public void AddedConditonTable(Situation situation, WarUnit unit)
		{
			unit.ReviseStatus += ReviseStatus;
		}

		public void RemovedConditonTable(Situation situation, WarUnit unit)
		{
			unit.ReviseStatus -= ReviseStatus;
		}

		public IAbnormalCondition DeepClone()
		{
			throw new NotImplementedException();
		}

		private void ReviseStatus(Status status)
		{
			status.Resistivity[_targetAttackType] = _newRegistivityType;
		}

		#endregion
	}
}