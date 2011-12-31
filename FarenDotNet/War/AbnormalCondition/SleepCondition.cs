using System;
using Paraiba.Core;
using FarenDotNet.War.Phase;

namespace FarenDotNet.War.AbnormalCondition
{
	public class SleepCondition : IAbnormalCondition
	{
		private WarUnit _unit;

		#region IAbnormalCondition Members

		public string Name
		{
			get { return "眠り"; }
		}

		public string ID
		{
			get { return "ファーレントゥーガ.眠り"; }
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
			_unit = unit;

			// 睡眠中は行動不可能
			unit.ReviseStatus += ReviseStatus;

			// 攻撃を受けたときの復帰処理を追加する
			unit.DamageEvent += WakeUpCheck;

			// ターン開始時に状態異常が自動回復するかの判定処理を追加する
			Phases.TurnPhase.Start += AutoRecoveryCheck;
		}

		public void RemovedConditonTable(Situation situation, WarUnit unit)
		{
			// 追加したデリゲートは全て削除する
			unit.ReviseStatus -= ReviseStatus;
			unit.DamageEvent -= WakeUpCheck;
			Phases.TurnPhase.Start -= AutoRecoveryCheck;
		}

		#endregion

		private static void ReviseStatus(Status status)
		{
			status.IsFreezing = true;
		}

		private void WakeUpCheck(Situation situation, WarUnit unit, WarUnit doer, int value)
		{
			// 攻撃者がいた場合のみ、復帰処理を行う
			if (doer != null && WarGlobal.Random.NextUnfairBool(1.0 / 3))
			{
				unit.Conditions.Remove(((IAbnormalCondition)this).ID, situation);
			}
		}

		private void AutoRecoveryCheck(Situation situation)
		{
			if (WarGlobal.Random.NextUnfairBool(1.0 / 3))
			{
				_unit.Conditions.Remove(((IAbnormalCondition)this).ID, situation);
			}
		}
	}
}