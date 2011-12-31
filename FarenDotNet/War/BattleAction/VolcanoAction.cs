using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paraiba.Geometry;
using FarenDotNet.BasicData;
using FarenDotNet.War.BattleAction.ScreenEffect;
using FarenDotNet.War.Scope;

namespace FarenDotNet.War.BattleAction
{
	public class VolcanoAction : ChipTargetActionWithCoroutine
	{
		private readonly AttackType _attackType;
		private readonly ICost _cost;
		private readonly int _power;
		private readonly IScope _scope;
		private readonly IScreenEffect _screenEffect;

		public VolcanoAction(IScope scope, ICost cost, IScreenEffect screenEffect, int power, AttackType attackType)
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

			var lf = WarGlobal.Landforms;
			var landforms = new[,] {
			    { lf[152], lf[143], lf[143], lf[143], lf[143], lf[153], null, null, null, null, null, },
			    { lf[159], lf[172], lf[163], lf[163], lf[163], lf[172], lf[158], null, null, null, null, },
			    { lf[159], lf[179], lf[172], lf[163], lf[163], lf[173], lf[178], lf[158], null, null, null, },
			    { lf[159], lf[179], lf[179], lf[172], lf[163], lf[173], lf[178], lf[178], lf[158], null, null, },
			    { lf[159], lf[179], lf[179], lf[179], lf[141], lf[141], lf[178], lf[178], lf[178], lf[158], null, },
			    { lf[144], lf[164], lf[164], lf[164], lf[141], lf[141], lf[141], lf[165], lf[165], lf[165], lf[145], },
			    { null, lf[156], lf[176], lf[176], lf[176], lf[141], lf[141], lf[177], lf[177], lf[177], lf[157], },
			    { null, null, lf[156], lf[176], lf[176], lf[174], lf[162], lf[175], lf[177], lf[177], lf[157], },
			    { null, null, null, lf[156], lf[176], lf[174], lf[162], lf[162], lf[175], lf[177], lf[157], },
			    { null, null, null, null, lf[156], lf[174], lf[162], lf[162], lf[162], lf[175], lf[157], },
			    { null, null, null, null, null, lf[154], lf[142], lf[142], lf[142], lf[142], lf[155], },
			};
			var heights = new[,] {
				{ 4, 4, 4, 4, 4, 4, 0, 0, 0, 0, 0, },
				{ 4, 5, 5, 5, 5, 5, 4, 0, 0, 0, 0, },
				{ 4, 5, 6, 6, 6, 6, 5, 4, 0, 0, 0, },
				{ 4, 5, 6, 7, 7, 7, 6, 5, 4, 0, 0, },
				{ 4, 5, 6, 7, 8, 8, 7, 6, 5, 4, 0, },
				{ 4, 5, 6, 7, 8, 8, 8, 7, 6, 5, 4, },
				{ 0, 4, 5, 6, 7, 8, 8, 7, 6, 5, 4, },
				{ 0, 0, 4, 5, 6, 7, 7, 7, 6, 5, 4, },
				{ 0, 0, 0, 4, 5, 6, 6, 6, 6, 5, 4, },
				{ 0, 0, 0, 0, 4, 5, 5, 5, 5, 5, 4, },
				{ 0, 0, 0, 0, 0, 4, 4, 4, 4, 4, 4, },
			};

			// 地形書き換え処理
			var map = args.Situation.Map;
			var sx = center.X - 5;
			var sy = center.Y - 5;

			foreach (var p in validPoints)
			{
				var taker = args.Situation.Map[p].Unit;
				AdditionalEffect cond;
				var damage = BattleActionUtil.GetMagicDamage(doer, taker, _power, _attackType, out cond);
				BattleActionUtil.RunAttackRoutine(args, doer, taker, damage, cond, doCoroutine);
			}

			foreach (var p in areaPoints)
			{
				// マップの更新
				map[p] = new Land(heights[p.Y - sy, p.X - sx], landforms[p.Y - sy, p.X - sx], null);
			}
			yield return 0;		// ダメージ表示

			finished();
		}
	}
}
