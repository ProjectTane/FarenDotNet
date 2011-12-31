using System;
using System.Collections.Generic;
using Paraiba.Geometry;
using FarenDotNet.BasicData;
using FarenDotNet.War.BattleAction.ScreenEffect;
using FarenDotNet.War.Scope;

namespace FarenDotNet.War.BattleAction
{
	public class WallCrashAction : ChipTargetActionWithCoroutine
	{
		private readonly AttackType _attackType;
		private readonly ICost _cost;
		private readonly int _power;
		private readonly IScope _scope;
		private readonly IScreenEffect _screenEffect;

		public WallCrashAction(IScope scope, ICost cost, IScreenEffect screenEffect, int power, AttackType attackType)
		{
			_scope = scope;
			_cost = cost;
			_screenEffect = screenEffect;
			_power = power;
			_attackType = attackType;
		}

		protected override IScope Scope
		{
			get { return _scope; }
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
			yield return 0; // エフェクト表示

			// 対象内の壁を破壊
			var map = args.Situation.Map;
			var enableDamageAnimation = false;
			foreach (var p in areaPoints)
			{
				var land = map[p];
				if (land.Construct != null && land.Construct.Info.Name == "壁")
				{
					map[p] = new Land(land.Height, land.Landform, null);
					if (land.Unit != null && _attackType != AttackType.なし)
					{
						var taker = land.Unit;
						AdditionalEffect cond;
						var damage = BattleActionUtil.GetMagicDamage(doer, taker, _power, _attackType, out cond);
						BattleActionUtil.RunAttackRoutine(args, doer, taker, damage, cond, doCoroutine);
						enableDamageAnimation = true;
					}
				}
			}
			if (enableDamageAnimation)
				yield return 0;		// ダメージ表示

			finished();
		}

		public override bool CanBoot(ActionArguments args, WarUnit doer)
		{
			return _cost.CanExpend(doer);
		}
	}
}