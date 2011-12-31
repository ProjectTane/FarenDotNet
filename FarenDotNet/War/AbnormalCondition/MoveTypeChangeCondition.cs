using System;
using FarenDotNet.BasicData;

namespace FarenDotNet.War.AbnormalCondition
{
	public class MoveTypeChangeCondition : IAbnormalCondition
	{
		private readonly MoveType _newMoveType;

		public MoveTypeChangeCondition(MoveType newMoveType)
		{
			_newMoveType = newMoveType;
		}

		#region IAbnormalCondition メンバ

		public string Name
		{
			get { return "移動タイプ変化"; }
		}

		public string ID
		{
			get { return "ファーレントゥーガ.移動タイプ変化:" + _newMoveType; }
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

		#endregion

		private void ReviseStatus(Status status)
		{
			status.MoveType = _newMoveType;
		}
	}
}