using System;
using System.Collections.Generic;
using Paraiba.Geometry;
using FarenDotNet.BasicData;
using FarenDotNet.War.BattleAction.ScreenEffect;
using FarenDotNet.War.Scope;

namespace FarenDotNet.War.BattleAction
{
	/// <summary>
	/// 魔法を表します。
	/// </summary>
	public class MagicAction : ChipTargetActionWithCoroutine
	{
		private readonly AttackType _attackType;
		private readonly ICost _cost;
		private readonly int _power;
		private readonly IScope _scope;
		private readonly IScreenEffect _screenEffect;

		public MagicAction(IScope scope, ICost cost, int power, AttackType attackType, IScreenEffect screenEffect)
		{
			_scope = scope;
			_cost = cost;
			_power = power;
			_attackType = attackType;
			_screenEffect = screenEffect;
		}

		protected override IScope Scope
		{
			get { return _scope; }
		}

		public override bool CanBoot(ActionArguments args, WarUnit doer)
		{
			return _cost.CanExpend(doer);
		}

		protected override IEnumerable<int> GetCoroutine(ActionArguments args, WarUnit doer, Point2 center, Action finished, Action doCoroutine)
		{
			// 行為のキャンセルの不許可
			args.Model.CancelCommandStack.Clear();
			// コストの消費
			_cost.Expend(args.Situation, doer);

			// 攻撃アニメーションとその後の処理を予約する
			IEnumerable<Point2> areaPoints;
			var validPoints = _scope.GetValidAreaScope(args.Situation, doer, center, out areaPoints);
			_screenEffect.SetScreenEffect(args, doer, center, areaPoints, validPoints, doCoroutine);
			yield return 0;		// エフェクト表示

			foreach (var p in validPoints)
			{
				var taker = args.Situation.Map[p].Unit;
				if (!AttackTypes.Heal.Contains(_attackType))
				{
					AdditionalEffect cond;
					var damage = BattleActionUtil.GetMagicDamage(doer, taker, _power, _attackType, out cond);
					BattleActionUtil.RunAttackRoutine(args, doer, taker, damage, cond, doCoroutine);
				}
				else
				{
					var value = BattleActionUtil.GetMagicHeal(doer, taker, _power);
					if (_attackType == AttackType.体力回復)
						taker.HealHP(args.Situation, doer, value);
					else
						taker.HealMP(args.Situation, doer, value);
					// 回復値の表示
					args.Model.SetHealAnimationOnMap(value, doer.Location, doCoroutine);
				}
			}
			yield return 0;		// ダメージ表示

			finished();
		}
	}
}