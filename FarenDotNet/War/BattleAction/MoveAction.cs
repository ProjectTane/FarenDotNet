using System;
using System.Collections.Generic;
using Paraiba.Geometry;
using FarenDotNet.War.Scope;

namespace FarenDotNet.War.BattleAction
{
	public class MoveAction : ChipTargetActionWithCoroutine
	{
		private readonly MoveScope _scope;

		public MoveAction()
		{
			_scope = new MoveScope();
		}

		protected override IScope Scope
		{
			get { return _scope; }
		}

		public override bool CanBoot(ActionArguments args, WarUnit doer)
		{
			return true;
		}

		public override void Boot(ActionArguments args, WarUnit doer, Action finished)
		{
			var situation = args.Situation;
			var movableArea = situation.Map.MoveCalculator.CalcMovableArea(situation.Map, doer, doer.Location);
			_scope.CurrentMovableArea = movableArea;
			base.Boot(args, doer, finished);
		}

		protected override IEnumerable<int> GetCoroutine(ActionArguments args, WarUnit doer, Point2 center, Action finished, Action doCoroutine)
		{
			// キャンセル処理のために記憶する
			var oldLocation = doer.Location;
			var resetCommandState = doer.SaveCommandState();

			// 移動アニメーション用に経路を生成する
			var movePoints = new Stack<Point2>();
			var movePoint = _scope.CurrentMovableArea.Find(p => p.Point == center);
			while (movePoint != null)
			{
				movePoints.Push(movePoint.Point);
				movePoint = movePoint.From;
			}

			// アニメーション中は、マップ上のユニット表示をオフにする
			doer.Visible = false;
			// 移動アニメーションのセット
			args.Model.SetContinuouslyMovingAnimationOnMap(doer.ChipImage, movePoints, 150, 20, doCoroutine);
			yield return 0;		// 移動アニメーションの表示

			// アニメーション後に元に戻す
			doer.Visible = true;
			args.Situation.Map.MoveUnit(doer, center);

			// アクション終了の通知
			finished();

			// 移動前に戻すキャンセル処理の追加
			args.Model.CancelCommandStack.Push(
				() => {
					args.Situation.Map.MoveUnit(doer, oldLocation);
					resetCommandState();
					return false;	// キャンセル処理の続行
				});
		}

		public override void BootAI(ActionArguments args, WarUnit doer, Action finished)
		{
			var situation = args.Situation;
			var movableArea = situation.Map.MoveCalculator.CalcMovableArea(situation.Map, doer, doer.Location);
			_scope.CurrentMovableArea = movableArea;
		}
	}

	public class MoveScope : IScope
	{
		private List<MovePoint> currentMovableArea;
		private List<Point2> currentMovablePoints;

		public List<MovePoint> CurrentMovableArea
		{
			get { return currentMovableArea; }
			set
			{
				currentMovableArea = value;
				currentMovablePoints = currentMovableArea.ConvertAll(mp => mp.Point);
			}
		}

		#region IScope Members

		public bool ExistChoice
		{
			get { return true; }
		}

		public IEnumerable<Point2> GetRangeScope(Situation situation, WarUnit doer)
		{
			return currentMovablePoints;
		}

		public IList<Point2> GetValidRangeScope(Situation situation, WarUnit doer, out IEnumerable<Point2> rangeScope)
		{
			rangeScope = currentMovablePoints;
			return currentMovablePoints;
		}

		public IEnumerable<Point2> GetAreaScope(Situation situation, WarUnit doer, Point2 center)
		{
			return new[] { center };
		}

		public IEnumerable<Point2> GetValidAreaScope(Situation situation, WarUnit doer, Point2 center, out IEnumerable<Point2> areaScope)
		{
			areaScope = ((IScope)this).GetAreaScope(situation, doer, center);
			return areaScope;
		}

		#endregion
	}
}