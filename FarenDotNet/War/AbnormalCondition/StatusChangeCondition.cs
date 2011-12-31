using System;
using System.Diagnostics.Contracts;

namespace FarenDotNet.War.AbnormalCondition
{
	public class StatusChangeCondition : IAbnormalCondition
	{
		private int _changeStatusValue;
		private readonly StatusKey _targetStatus;

		public StatusChangeCondition(StatusKey targetStatus, int changeStatusValue)
		{
			Contract.Requires(changeStatusValue != 0, "changeStatusValue");

			_targetStatus = targetStatus;
			_changeStatusValue = changeStatusValue;
		}

		#region IAbnormalCondition メンバ

		public string Name
		{
			get { return "ステータス変化"; }
		}

		public string ID
		{
			get { return "ファーレントゥーガ.ステータス変化:" + _targetStatus; }
		}

		/// <summary>
		/// ユニットに状態異常が追加される直前に呼び出される。
		/// </summary>
		/// <param name="situation"></param>
		/// <param name="unit"></param>
		/// <param name="oldCond"></param>
		public bool AddingConditonTable(Situation situation, WarUnit unit, IAbnormalCondition oldCond)
		{
			Contract.Requires(oldCond.GetType() == GetType());

			var cond = (StatusChangeCondition)oldCond;
			// 効果がお互いに反対であった場合は状態異常を消す
			if (_changeStatusValue * cond._changeStatusValue < 0)
			{
				unit.Conditions.Remove(ID, situation);
			}
			// そうでない場合は、効果が大きいほうで上書きする
			else if (Math.Abs(_changeStatusValue) > Math.Abs(cond._changeStatusValue))
			{
				cond._changeStatusValue = _changeStatusValue;
			}

			return false;
		}

		public void AddedConditonTable(Situation situation, WarUnit unit)
		{
			unit.ReviseStatus += ReviseStatus;
		}

		public void RemovedConditonTable(Situation situation, WarUnit unit)
		{
			unit.ReviseStatus -= ReviseStatus;
		}

		#endregion

		private void ReviseStatus(Status arg)
		{
			arg.Params[_targetStatus] += _changeStatusValue;
		}
	}
}