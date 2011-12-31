using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using Paraiba.Geometry;
using FarenDotNet.BasicData;
using FarenDotNet.Reign;
using FarenDotNet.War.BattleAction.ScreenEffect;
using FarenDotNet.War.Scope;

namespace FarenDotNet.War.BattleAction
{
	public class SummonAction : ChipTargetActionWithCoroutine
	{
		private readonly ICost _cost;
		private readonly IScope _scope;
		private readonly UnitData _unit;
		private readonly IScreenEffect _screenEffect;

		public SummonAction(IScope scope, ICost cost, UnitData unit, IScreenEffect screenEffect)
		{
			_scope = scope;
			_cost = cost;
			_unit = unit;
			_screenEffect = screenEffect;
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

			foreach (var p in validPoints)
			{
				Debug.Assert(args.Situation.Map[p].Unit == null);
				var unit = WarGlobal.UnitBuilder.Create(new Unit(_unit), doer.Side, null);
				args.Situation.Units.AddUnit(unit);
				args.Situation.Map.Deploy(unit, p);
			}

			finished();
		}

		public override bool CanBoot(ActionArguments args, WarUnit doer)
		{
			return _cost.CanExpend(doer);
		}
	}
}