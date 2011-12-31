using System;
using System.Diagnostics.Contracts;
using FarenDotNet.BasicData;

namespace FarenDotNet.War.AbnormalCondition
{
	public class AttackCountChangeCondition : IAbnormalCondition
	{
		/// <summary>
		/// 攻撃回数を変化させる値 +1は1回分追加　-1は1回分減少
		/// </summary>
		private int _changeAttackCount;

		public AttackCountChangeCondition(int changeAttackCount)
		{
			Contract.Requires(changeAttackCount != 0, "changeAttackCount");

			_changeAttackCount = changeAttackCount;
		}

		#region IAbnormalCondition メンバ

		public string Name
		{
			get { return "攻撃回数変化"; }
		}

		public string ID
		{
			get { return "ファーレントゥーガ.攻撃回数変化"; }
		}

		public bool AddingConditonTable(Situation situation, WarUnit unit, IAbnormalCondition oldCond)
		{
			Contract.Requires(oldCond.GetType() == GetType());

			var cond = (AttackCountChangeCondition)oldCond;
			var newCount = cond._changeAttackCount + _changeAttackCount;
			// 変化量が0ならば削除、そうでなければ値を更新
			if (newCount == 0)
				unit.Conditions.Remove(ID, situation);
			else
				cond._changeAttackCount = _changeAttackCount;
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

		private void ReviseStatus(Status status)
		{
			var attacks = status.DefaultAttacks;

			if (_changeAttackCount == 1)		// 攻撃回数増加
			{
				// 攻撃回数が0の場合は物理100の攻撃を適当に追加
				if (attacks.Count == 0)
					attacks.Add(new DefaultAttack(100, AttackType.物理));
				else
					attacks.Add(attacks[0]);	// 1番目の攻撃を一番最後に追加する
			}
			else if (_changeAttackCount == -1)	// 攻撃回数減少
			{
				var count = attacks.Count;
				if (count != 0)
					attacks.RemoveAt(count - 1);
			}
		}
	}
}