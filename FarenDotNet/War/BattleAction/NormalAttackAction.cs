using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Paraiba.Core;
using Paraiba.Geometry;
using FarenDotNet.BasicData;
using FarenDotNet.War.Scope;
using Paraiba.Linq;

namespace FarenDotNet.War.BattleAction
{
	public class NormalAttackAction : ChipTargetActionWithCoroutine
	{
		private readonly IScope _scope;

		public NormalAttackAction()
		{
			_scope = new DefaultScope(1, 1, TargetType.ENEMY);
		}

		protected override IScope Scope
		{
			get { return _scope; }
		}

		public override bool CanBoot(ActionArguments args, WarUnit doer)
		{
			return true;
		}

		protected override IEnumerable<int> GetCoroutine(ActionArguments args, WarUnit doer, Point2 center, Action finished, Action doCoroutine)
		{
			// 行為のキャンセルの不許可
			args.Model.CancelCommandStack.Clear();

			Debug.Assert(args.Situation.Map[center].Unit != null, "選択位置にユニットが存在しません.");
			var taker = args.Situation.Map[center].Unit;

			var doerAttack = doer.Status.DefaultAttacks.Select(atk => new { Doer = doer, Taker = taker, atk.Type, atk.Power });
			var takerAttack = taker.Status.DefaultAttacks.Select(atk => new { Doer = taker, Taker = doer, atk.Type, atk.Power });
			var source = doerAttack.AlternatelyConcat(takerAttack)
				.Where(info => info.Type != AttackType.なし);

			foreach (var info in source)
			{
				// ローカル変数にも代入しておく（ごっちゃにならないように）
				doer = info.Doer;
				taker = info.Taker;
				// アニメーション中は、マップ上のユニット表示をオフにする
				doer.Visible = false;
				// 攻撃アニメーションとその後の処理を予約する
				args.Model.SetContinuouslyMovingAnimationOnMap(
					doer.ChipImage, new[] { doer.Location, taker.Location, doer.Location },
					args.Model.ATTACK_EFFECT_TIME, 0, doCoroutine);
				yield return 0;		// エフェクト表示

				doer.Visible = true;

				AdditionalEffect cond;
				var damage = BattleActionUtil.GetDamage(doer, taker, info.Power, info.Type, out cond);
				BattleActionUtil.RunAttackRoutine(args, doer, taker, damage, cond, doCoroutine);
				yield return 0;		// ダメージ表示

				// どちらかが死亡したら処理を中断
				if (!doer.Alive || !taker.Alive)
					break;
			}

			finished();
		}
	}
}