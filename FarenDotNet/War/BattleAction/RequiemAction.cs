using System;
using System.Collections.Generic;
using System.Linq;
using Paraiba.Geometry;
using FarenDotNet.BasicData;
using FarenDotNet.War.BattleAction.ScreenEffect;
using FarenDotNet.War.Scope;

namespace FarenDotNet.War.BattleAction
{
	public class RequiemAction : ChipTargetActionWithCoroutine
	{
		private readonly AttackType _attackType;
		private readonly ICost _cost;
		private readonly int _power;
		private readonly IScope _scope;
		private readonly IScreenEffect _screenEffect;

		public RequiemAction(IScope scope, ICost cost, IScreenEffect screenEffect, int power, AttackType attackType)
		{
			_scope = scope;
			_cost = cost;
			_screenEffect = screenEffect;
			_power = power;
			_attackType = attackType;
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
		/// <param name="args">アクションの引数</param>
		/// <param name="doer">行為者</param>
		/// <returns>
		/// 発動可能かどうか
		/// </returns>
		public override bool CanBoot(ActionArguments args, WarUnit doer)
		{
			return _cost.CanExpend(doer) &&
				args.Situation.Units.Alive.Any(unit_ => unit_.IsUndead);
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

			foreach (var p in validPoints)
			{
				var taker = args.Situation.Map[p].Unit;
				AdditionalEffect cond;
				var damage = BattleActionUtil.GetMagicDamage(doer, taker, _power, _attackType, out cond);
				BattleActionUtil.RunAttackRoutine(args, doer, taker, damage, cond, doCoroutine);
			}
			yield return 0; // ダメージ表示

			finished();
		}
	}
}