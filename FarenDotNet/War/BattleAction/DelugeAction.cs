using System;
using System.Collections.Generic;
using System.Linq;
using Paraiba.Geometry;
using FarenDotNet.BasicData;
using FarenDotNet.War.BattleAction.ScreenEffect;
using FarenDotNet.War.Scope;

namespace FarenDotNet.War.BattleAction
{
	public class DelugeAction : ChipTargetActionWithCoroutine
	{
		private const int TARGET_HEIGHT = 2;
		private static readonly IList<string> brinkNames = new [] { "浅瀬", "海", "深海" };
		private readonly ICost _cost;
		private readonly IScope _scope;
		private readonly IScreenEffect _screenEffect;
		private readonly Landform _brinkLandform;
		private readonly int _power;
		private readonly AttackType _attackType;

		public DelugeAction(IScope scope, ICost cost, IScreenEffect screenEffect, int power, AttackType attackType, Landform waterLandform)
		{
			_scope = scope;
			_cost = cost;
			_screenEffect = screenEffect;
			_brinkLandform = waterLandform;
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
			yield return 0;		// エフェクト表示

			// 洪水処理
			var map = args.Situation.Map;
			var width = map.Width;
			var height = map.Height;
			var enableDamageAnimation = false;
			for (int x = 0; x < width; x++)
			{
				for (int y = 0; y < height; y++)
				{
					var land = map[x, y];
					if (land.Height <= TARGET_HEIGHT)
					{
						var taker = land.Unit;
						if (taker != null && _attackType != AttackType.なし)
						{
							AdditionalEffect cond;
							var damage = BattleActionUtil.GetMagicDamage(doer, taker, _power, _attackType, out cond);
							BattleActionUtil.RunAttackRoutine(args, doer, taker, damage, cond, doCoroutine);
							enableDamageAnimation = true;
						}

						// マップの更新
						if (brinkNames.Contains(land.Info.Name))
						{
							map[x, y] = new Land(land.Height - TARGET_HEIGHT, land.Landform, null);
						}
						else
						{
							map[x, y] = new Land(land.Height - TARGET_HEIGHT, _brinkLandform, null);
						}
					}
				}
			}
			if (enableDamageAnimation)
				yield return 0;		// ダメージ表示

			finished();
		}

		public override bool CanBoot(ActionArguments args, WarUnit doer)
		{
			return _cost.CanExpend(doer) &&
				args.Situation.Map.Any(land_ => brinkNames.Contains(land_.Info.Name));
		}
	}
}