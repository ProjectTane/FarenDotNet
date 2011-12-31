using System;
using System.Collections.Generic;
using Paraiba;
using Paraiba.Core;
using Paraiba.Geometry;
using FarenDotNet.BasicData;
using FarenDotNet.War.BattleAction.ScreenEffect;
using FarenDotNet.War.Scope;

namespace FarenDotNet.War.BattleAction
{
	/// <summary>
	/// 技による戦闘行為を表します。
	/// </summary>
	public class SkillAction : ChipTargetActionWithCoroutine
	{
		private readonly AttackDependency _atkDependency;
		private readonly AttackType _attackType;
		private readonly ICost _cost;
		private readonly DefenseDependency _defDependency;
		private readonly int _maxTimes;
		private readonly int _power;
		private readonly IScope _scope;
		private readonly IScreenEffect _screenEffect;

		public SkillAction(IScope scope, ICost cost, int power, int maxTimes, AttackType attackType,
		                         AttackDependency atkDependency, DefenseDependency defDependency, IScreenEffect screenEffect)
		{
			_scope = scope;
			_cost = cost;
			_power = power;
			_maxTimes = Math.Max(1, maxTimes);
			_attackType = attackType;
			_atkDependency = atkDependency;
			_defDependency = defDependency;
			_screenEffect = screenEffect;
		}

		/// <summary>
		/// スコープ（マップチップの対象範囲）を表現するクラス
		/// </summary>
		protected override IScope Scope
		{
			get { return _scope; }
		}

		/// <summary>
		///  行為が発動可能かどうかチェックする
		/// </summary>
		/// <param name="args"></param>
		/// <param name="doer">行為者</param>
		/// <returns>
		/// 発動可能かどうか
		/// </returns>
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

			IEnumerable<Point2> areaPoints;
			var validPoints = _scope.GetValidAreaScope(args.Situation, doer, center, out areaPoints);
			var times = XMath.Center(doer.SkillTimes, 1, _maxTimes);

			_screenEffect.SetScreenEffect(args, doer, center, areaPoints, validPoints, times, (times_, maxTimes_) => {
				foreach (var p in validPoints)
				{
					var taker = args.Situation.Map[p].Unit;
					AdditionalEffect cond;
					var value = BattleActionUtil.GetSkillValue(doer, taker, _power, _attackType,
						_atkDependency, _defDependency, out cond);
					var action = times_ < maxTimes_ ? doCoroutine.GetNOP() : doCoroutine;

					if (_attackType != AttackType.体力回復 && _attackType != AttackType.魔力回復)
					{
						BattleActionUtil.RunAttackRoutine(args, doer, taker, value, cond, action);
					}
					else
					{
						if (_attackType == AttackType.体力回復)
							taker.HealHP(args.Situation, doer, value);
						else
							taker.HealMP(args.Situation, doer, value);
						// 回復値の表示
						args.Model.SetHealAnimationOnMap(value, doer.Location, action);
					}
				}
			});
			yield return 0; // 最後のダメージ表示

			finished();
		}
	}
}