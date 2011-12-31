using System.Collections.Generic;
using Paraiba.Core;
using FarenDotNet.War.Phase;

namespace FarenDotNet.War.AbnormalCondition
{
	public class ParalysisCondition : IAbnormalCondition
	{
		private WarUnit _unit;

		#region IAbnormalCondition Members

		public string Name
		{
			get { return "麻痺"; }
		}

		public string ID
		{
			get { return "ファーレントゥーガ.麻痺"; }
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

			// 麻痺時のコマンドテーブル変化を適用する
			unit.ReviseCommands += CommandEnableTableRevise;

			// ターン開始時に状態異常が自動回復するかの判定処理を追加する
			Phases.TurnPhase.Start += AutoRecoveryCheck;
		}

		public void RemovedConditonTable(Situation situation, WarUnit unit)
		{
			// 追加したデリゲートは全て削除する
			unit.ReviseCommands -= CommandEnableTableRevise;
			Phases.TurnPhase.Start -= AutoRecoveryCheck;
		}

		#endregion

		private void AutoRecoveryCheck(Situation situation)
		{
			if (WarGlobal.Random.NextUnfairBool(1.0 / 5))
			{
				_unit.Conditions.Remove(((IAbnormalCondition)this).ID, situation);
			}
		}

		private static void CommandEnableTableRevise(List<CommandInfo> commandStates)
		{
			var count = commandStates.Count;
			for (int i = 0; i < count; i++)
			{
				var name = commandStates[i].Command.Name;
				if (name != "待機" && name != "魔法" && name != "退却")
				{
					// HACK: より良い見分け方を
					commandStates[i] = new CommandInfo(commandStates[i].Command, false);
				}
			}
		}
	}
}