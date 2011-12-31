﻿using System;
using System.Collections.Generic;
using Paraiba.Geometry;
using FarenDotNet.War.AbnormalCondition;
using FarenDotNet.War.BattleAction.ScreenEffect;
using FarenDotNet.War.Scope;

namespace FarenDotNet.War.BattleAction
{
	public class GiveAbnormalConditionAction : ChipTargetActionWithCoroutine
	{
		private readonly IAbnormalCondition _cond;
		private readonly ICost _cost;
		private readonly IScope _scope;
		private readonly IScreenEffect _screenEffect;

		public GiveAbnormalConditionAction(IScope scope, ICost cost, IScreenEffect screenEffect, IAbnormalCondition cond)
		{
			_scope = scope;
			_cost = cost;
			_screenEffect = screenEffect;
			_cond = cond;
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

			foreach (var p in validPoints)
			{
				var taker = args.Situation.Map[p].Unit;
				taker.Conditions.Add(_cond, args.Situation);
			}
			finished();
		}

		public override bool CanBoot(ActionArguments args, WarUnit doer)
		{
			return _cost.CanExpend(doer);
		}
	}
}