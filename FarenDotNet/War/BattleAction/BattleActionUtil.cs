using System;
using Paraiba;
using System.Diagnostics.Contracts;
using FarenDotNet.BasicData;
using FarenDotNet.War.AbnormalCondition;

namespace FarenDotNet.War.BattleAction
{
	public static class BattleActionUtil
	{
		public static int GetDamage(WarUnit doer, WarUnit taker, int power, AttackType type, out AdditionalEffect effect)
		{
			Contract.Requires(AttackTypes.Attack.Contains(type));

			// 状態異常の結果を初期化
			effect = AdditionalEffect.なし;

			if (!IsHit(doer, taker))
				return 0;

			// Attack=(攻撃力+30+random(8))*攻撃値/80
			var atk = (doer.Status.Atk + 30 + WarGlobal.Random.Next(8)) * power / 80;
			// Defense＝防御力+30
			var def = taker.Status.Def + 30;

			// 状態異常の判定
			var resType = taker.Resistivity[type];
			if (AttackTypes.AbnormalCondition.Contains(type))
			{
				if (resType == ResistivityType.弱い ||
					(resType == ResistivityType.普通 && IsHit(doer, taker)))
				{
					effect = type - AttackType.毒 + AdditionalEffect.毒;
				}
				else if (resType == ResistivityType.強い ||
					resType == ResistivityType.吸収)
				{
					// ダメージの処理は通常攻撃扱い
					resType = taker.Resistivity[AttackType.物理];
				}
			}
			else if (type == AttackType.吸収)
			{
				// 吸収属性の吸収はミス
				if (resType == ResistivityType.吸収)
					return 0;
				// 吸収属性があれば、ここでマイナスに反転。
				effect = AdditionalEffect.吸収;
			}

			// 相手に「強い」属性が有る場合はここでDefense*2、「弱い」の場合はAttack*2。
			if (resType == ResistivityType.弱い)
				atk *= 2;
			else if (resType == ResistivityType.強い)
				def *= 2;

			// ダメージ期待値=Attack^0.7*Attack/(Defense+10+random(10))*(20+random(5))/35
			var point = (int)(Math.Pow(atk, 0.7) * atk / (def + 10 + WarGlobal.Random.Next(10)) * (20 + WarGlobal.Random.Next(5)) / 35);
			// ダメージが1以下なら1、999以上なら999。
			point = XMath.Center(point, 1, 999);

			return point;
		}

		public static bool IsHit(WarUnit doer, WarUnit taker)
		{
			// 命中値 = 攻撃側技量 * 地形修正 + 20
			var atk = doer.Status.Tec * doer.LandRev + 20;
			// 回避値 = 防御側技量 * 地形修正 + 20
			var def = taker.Status.Agi * taker.LandRev + 20;
			// 真の命中率 = 命中 / 回避 * 0.8
			var prob = atk * 80 / def;

			return prob > WarGlobal.Random.NextDouble();
		}

		public static int GetMagicDamage(WarUnit doer, WarUnit taker, int power, AttackType type, out AdditionalEffect effect)
		{
			// Attack＝((魔力*0.8＋Random(8)＋60)*(魔法威力+30))/40
			var atk = (doer.Status.Mag * 0.8 + WarGlobal.Random.Next(8) + 60) * (power + 30) / 40;
			// Defense=抵抗*0.8+70
			var def = taker.Status.Res * 0.8 + 70;

			// 状態異常の結果を初期化
			effect = AdditionalEffect.なし;
			// 状態異常の判定
			var resType = taker.Resistivity[type];
			if (AttackTypes.AbnormalCondition.Contains(type))
			{
				if (resType == ResistivityType.弱い ||
					(resType == ResistivityType.普通 && IsMagicConditionHit(doer, taker)))
				{
					effect = type - AttackType.毒 + AdditionalEffect.毒;
				}
				else if (resType == ResistivityType.強い ||
					resType == ResistivityType.吸収)
				{
					// ダメージの処理は通常攻撃扱い
					resType = taker.Resistivity[AttackType.物理];
				}
			}
			else if (type == AttackType.吸収)
			{
				// 吸収属性の吸収はミス
				if (resType == ResistivityType.吸収)
					return 0;
				// 吸収属性があれば、ここでマイナスに反転。
				effect = AdditionalEffect.吸収;
			}

			// 相手に弱い属性が有ればAttack*2、強い属性ならばDefense*2。
			if (resType == ResistivityType.弱い)
				atk *= 2;
			else if (resType == ResistivityType.強い)
				def *= 2;

			// ダメージ期待値=(Attack^0.4)*(Attack/(Defense+random(10))+.6)*(30+random(5))/35
			var point = (int)(Math.Pow(atk, 0.4) * (atk / (def + WarGlobal.Random.Next(10)) + 0.6) * (30 + WarGlobal.Random.Next(5)) / 35);
			// ダメージが1以下なら1、999以上なら999。
			point = XMath.Center(point, 1, 999);

			return point;
		}

		public static int GetMagicHeal(WarUnit doer, WarUnit taker, int power)
		{
			// Attack＝((魔力*0.8＋Random(8)＋60)*(魔法威力+30))/40
			var atk = (doer.Status.Mag * 0.8 + WarGlobal.Random.Next(8) + 60) * (power + 30) / 40;
			// MP回復、HP回復の技は常にDefenseは30
			var def = 30;

			// ダメージ期待値=(Attack^0.4)*(Attack/(Defense+random(10))+.6)*(30+random(5))/35
			var point = (int)(Math.Pow(atk, 0.4) * (atk / (def + WarGlobal.Random.Next(10)) + 0.6) * (30 + WarGlobal.Random.Next(5)) / 35);
			// ダメージが1以下なら1、999以上なら999。
			point = XMath.Center(point, 1, 999);

			return point;
		}

		public static bool IsMagicConditionHit(WarUnit doer, WarUnit taker)
		{
			// 命中値 = 攻撃側魔力 * 地形修正
			var atk = doer.Status.Mag * doer.LandRev;
			// 回避値 = 防御側抵抗 * 地形修正 + 20
			var def = taker.Status.Res * taker.LandRev + 20;
			// 魔法命中率=命中/回避*80
			var prob = atk * 80 / def;

			return prob + 0.1 > WarGlobal.Random.NextDouble() * 1.1;
		}

		public static int GetSkillValue(WarUnit doer, WarUnit taker, int power, AttackType type, AttackDependency atkDep, DefenseDependency defDep, out AdditionalEffect effect)
		{
			// 状態異常の結果を初期化
			effect = AdditionalEffect.なし;

			// 防御ステータスの決定
			int defStatus = 0;
			if (defDep == DefenseDependency.Defense)
			{
				if (!IsHit(taker, doer))
					return 0;
				defStatus = taker.Status.Def;
			}
			else if (defDep == DefenseDependency.Resistivity)
			{
				defStatus = taker.Status.Res;
			}

			// 攻撃ステータスの決定
			int atkStatus = 0;
			if (atkDep == AttackDependency.Attack)
				atkStatus = (int)(doer.Status.Atk * 0.8);
			else if (atkDep == AttackDependency.Magic)
				atkStatus = doer.Status.Mag;

			// Attack＝（Attack+random(8)+30)*技威力/100
			var atk = (atkStatus + WarGlobal.Random.Next(8) + 30) * power / 100;
			// Defense=Defense+30
			var def = defStatus + 30;

			if (AttackTypes.Attack.Contains(type))
			{
				// 状態異常の判定
				var resType = taker.Resistivity[type];
				if (AttackTypes.AbnormalCondition.Contains(type))
				{
					if (resType == ResistivityType.弱い ||
						(resType == ResistivityType.普通 && IsMagicConditionHit(doer, taker)))
					{
						effect = type - AttackType.毒 + AdditionalEffect.毒;
					}
					else if (resType == ResistivityType.強い ||
						resType == ResistivityType.吸収)
					{
						// ダメージの処理は通常攻撃扱い
						resType = taker.Resistivity[AttackType.物理];
					}
				}
				else if (type == AttackType.吸収)
				{
					// 吸収属性の吸収はミス
					if (resType == ResistivityType.吸収)
						return 0;
					// 吸収属性があれば、ここでマイナスに反転。
					effect = AdditionalEffect.吸収;
				}

				// ここで弱い属性が有る場合、Attackは二倍。強い属性があった場合、Defenseは３倍。
				if (resType == ResistivityType.弱い)
					atk *= 2;
				else if (resType == ResistivityType.強い)
					def *= 3;
			}

			//Attack^0.7*(Attack/(Defense+10+random(10)+0.5)*(20+random(5))/35
			var point = (int)(Math.Pow(atk, 0.7) * atk / (def + 10 + WarGlobal.Random.Next(10) + 0.5) * (20 + WarGlobal.Random.Next(5)) / 35);
			// ダメージが1以下なら1、999以上なら999。
			point = XMath.Center(point, 1, 999);

			return point;
		}

		public static void RunAttackRoutine(ActionArguments args, WarUnit doer, WarUnit taker,
		                                    int value, AdditionalEffect effect, Action finishAnimation)
		{
			switch (effect)
			{
			case AdditionalEffect.なし:
				break;
			case AdditionalEffect.毒:
				taker.Conditions.Add(new PoisonCondition(), args.Situation);
				break;
			case AdditionalEffect.石化:
				taker.Conditions.Add(new PetrifactionCondition(), args.Situation);
				break;
			case AdditionalEffect.麻痺:
				taker.Conditions.Add(new ParalysisCondition(), args.Situation);
				break;
			case AdditionalEffect.眠り:
				taker.Conditions.Add(new SleepCondition(), args.Situation);
				break;
			case AdditionalEffect.幻想:
				// TODO: 実装
				break;
			case AdditionalEffect.死:
				taker.Die(args.Situation, doer);
				break;
			case AdditionalEffect.吸収:
				// 回復値の表示
				doer.HealHP(args.Situation, doer, value);
				args.Model.SetHealAnimationOnMap(value, doer.Location, null);
				break;
			default:
				throw new ArgumentOutOfRangeException("effect");
			}

			// ダメージの表示要請
			taker.DamageHP(args.Situation, doer, value);
			args.Model.SetDamageAnimationOnMap(value, taker.Location, finishAnimation);
		}
	}
}