using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarenDotNet.War.BattleAction;
using Paraiba.Utility;
using Paraiba.Geometry;
using FarenDotNet.War.Scope;
using FarenDotNet.BasicData;
using FarenDotNet.War.UI;

namespace FarenDotNet.War.AI
{
	public class AI
	{
		public IEnumerator<Tuple<ChipTargetAction, Point2>> GetActionWithCoroutine(Situation situation)
		{
			WarUnit doer = situation.ActiveUnit;
			IEnumerable<Point2> enumPoint;
			ChipTargetAction bestBattleAction = null;
			Point2 bestp = doer.Location;
			IScope scope;
			int bestrank = 0;


			#region 死にそうな敵がいる所を見つけるアルゴリズム
			{
				scope = new StraightScope(1, TargetType.ENEMY);
				IEnumerable<Point2> around = scope.GetValidRangeScope(situation, doer, out enumPoint);
				foreach (Point2 q in around)
				{
					WarUnit unit = situation.Map[q].Unit;
					if (unit != null)
					{
						bestrank += unit.Status.HP;
					}
				}
				if (bestrank == 0) bestrank = 500;
			}
			MoveScope movescope = new MoveScope();
			movescope.CurrentMovableArea = situation.Map.MoveCalculator.CalcMovableArea(situation.Map, doer, bestp);

			foreach (Point2 p in movescope.GetValidRangeScope(situation, doer, out enumPoint))
			{
				scope = new StraightScope(1, TargetType.ENEMY);
				IEnumerable<Point2> around = scope.GetValidRangeScope(situation, doer, out enumPoint);
				int rank = 0;
				foreach (Point2 q in around)
				{
					WarUnit unit = situation.Map[q].Unit;
					if (unit != null)
					{
						rank += unit.Status.HP;
					}
				}
				if (rank == 0) rank = 500;
				if (rank < bestrank)
				{
					bestrank = rank;
					bestp = p;
				}
			}
			#endregion

			#region 行動範囲内に敵がいなかったら、一番近くの敵に移動する

			if (bestp == doer.Location)
			{
				double min = Double.MaxValue;
				Vector2 diff = new Vector2();
				foreach (WarUnit unit in situation.Units.WarUnits)
				{
					if (situation.Sides[0].IsPlayer && unit.Side == situation.Sides[0])
					{
						Vector2 bufdiff = unit.Location - doer.Location;
						double r = bufdiff.X * bufdiff.X + bufdiff.Y * bufdiff.Y;
						if (r < min)
						{
							min = r;
							diff = bufdiff;
						}

					}
				}
				for (double d = 1.0; d > 0; d -= 0.1)
				{
					Point2 arrive = new Point2((int)(doer.Location.X + diff.X * d), (int)(doer.Location.Y + diff.Y * d));
					if (situation.Map.IsValidPoint(arrive) && situation.Map[arrive].Unit == null && situation.Map[arrive].Construct == null)
					{
						if (movescope.GetValidRangeScope(situation, doer, out enumPoint).Contains(arrive))
						{
							bestp = arrive;
							break;
						}
					}
				}
			}
			#endregion

			if (bestp != doer.Location)
				yield return new Tuple<ChipTargetAction, Point2>(new MoveAction(), bestp);

			bestrank = Int32.MaxValue;
			bestBattleAction = null;

			scope = new StraightScope(1, TargetType.ENEMY);
			IEnumerable<Point2> validRange = scope.GetValidRangeScope(situation, doer, out enumPoint);
			if (validRange.Count() != 0)
			{
				bestBattleAction = new NormalAttackAction();
				foreach (Point2 p in scope.GetValidRangeScope(situation, doer, out enumPoint))
				{
					WarUnit unit = situation.Map[p].Unit;
					if (unit != null && unit.Status.HP < bestrank)
					{
						bestrank = unit.Status.HP;
						bestp = p;
					}
				}
			}

			//TODO: 魔法等の検討
			
			if(bestBattleAction != null)
				yield return new Tuple<ChipTargetAction, Point2>(bestBattleAction, bestp);

			yield break;

			#region 参考にするために一応保持
			//int bestrank = 0;
			//{
			//    doer.DefaultAttacks[0].
			//    List<Point2> validRangePoints = doer.AdditionalEffect.ReadyAI(doer, mapControl, out areaPoints);
			//    if(validRangePoints != null)
			//    {
			//        foreach(Point2 rangePoint in validRangePoints)
			//        {
			//            List<Point2> targets = new List<Point2>();
			//            foreach(Point2 q in areaPoints)
			//            {
			//                targets.Add(rangePoint + q);
			//            }
			//            int rank = doer.AdditionalEffect.BootAI(mapControl, doer, targets);
			//            if(bestrank < rank)
			//            {
			//                bestrank = rank;
			//                bestp = rangePoint;
			//                bestBattleAction = doer.AdditionalEffect;
			//            }
			//        }
			//    }
			//}
			//foreach(Magic magic_ in doer.GetMagicList(0))
			//{
			//    IDefaultBattleAction battleAction = magic_.BattleAction;
			//    List<Point2> validRangePoints = battleAction.ReadyAI(doer, mapControl, out areaPoints);
			//    if(validRangePoints != null)
			//    {
			//        foreach(Point2 rangePoint in validRangePoints)
			//        {
			//            List<Point2> targets = new List<Point2>();
			//            foreach(Point2 q in areaPoints)
			//            {
			//                targets.Add(rangePoint + q);
			//            }
			//            int rank = battleAction.BootAI(mapControl, doer, targets);
			//            if(bestrank < rank)
			//            {
			//                bestrank = rank;
			//                bestp = rangePoint;
			//                bestBattleAction = battleAction;
			//            }
			//        }
			//    }
			//}

			//if(bestrank > 0)
			//{
			//    bestBattleAction.ReadyAI(doer, mapControl, out areaPoints);
			//    if(areaPoints == null)
			//    {
			//        FinishAct();
			//    }
			//    else
			//    {
			//        List<Point2> targets = new List<Point2>();
			//        foreach(Point2 q in areaPoints)
			//        {
			//            targets.Add(bestp + q);
			//        }
			//        bestBattleAction.Boot(mapControl, doer, targets, actionManager);
			//    }
			//}
			//else
			//{
			//    FinishAct();
			//}
			#endregion

		}
	}
}
