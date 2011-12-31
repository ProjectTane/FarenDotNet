using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Paraiba.Geometry;
using FarenDotNet.War.Scope;
using FarenDotNet.War.UI;

namespace FarenDotNet.War.BattleAction
{
	public abstract class ChipTargetAction : IBattleAction
	{
		/// <summary>
		/// スコープ（マップチップの対象範囲）を表現するクラス
		/// </summary>
		protected abstract IScope Scope { get; }

		#region IBattleAction Members

		public abstract bool CanBoot(ActionArguments args, WarUnit doer);

		public virtual void Boot(ActionArguments args, WarUnit doer, Action finished)
		{
			var warAdapter = args.Model;

			IEnumerable<Point2> rangePoints;
			var validRangePoints = Scope.GetValidRangeScope(args.Situation, doer, out rangePoints).ToArray();
			if (validRangePoints.Length <= 0)
			{
				// HACK: メッセージボックス表示ルーチンの記述場所
				MessageBox.Show("射程内に有効な対象が存在しません.");
				args.Model.InvokeCancelEvent();
				return;
			}

			// スコープの位置に選択肢があれば、ユーザーに選択させる
			if (Scope.ExistChoice)
			{
				var pScope = new PrintableScope(Scope, rangePoints.ToArray(), validRangePoints, args.Situation, doer);

				// スコープを選択した際のイベント
				Action<Point2> selectMapChip = null;

				// スコープ選択状態を解除するデリゲート
				Action resetSelectScopeMode = () => {
					warAdapter.Scope = null;
					warAdapter.SelectMapChipEvent -= selectMapChip;
				};

				// スコープを選択した際のイベントの定義
				selectMapChip = p => {
					if (warAdapter.Scope.ValidRangeChips.Contains(p))
					{
						// スコープ選択状態を解除
						resetSelectScopeMode();
						Execute(args, doer, p, finished);
					}
				};

				// スコープの表示
				warAdapter.Scope = pScope;
				// スコープを選択した際のイベントを追加
				warAdapter.SelectMapChipEvent += selectMapChip;
				// キャンセルをした際のイベントを追加
				warAdapter.CancelCommandStack.Push(delegate {
					resetSelectScopeMode();
					return false; // 次のキャンセル処理へ続く
				});
			}
			else
			{
				Execute(args, doer, validRangePoints[0], finished);
			}
		}

		public virtual void BootAI(ActionArguments args, WarUnit doer, Action finished)
		{
			//わざと何も処理してません

			//何か処理したければオーバーライドしてください
		}

		#endregion

		public abstract void Execute(ActionArguments args, WarUnit doer, Point2 center, Action finished);
	}

	public abstract class ChipTargetActionWithCoroutine : ChipTargetAction
	{
		public override void Execute(ActionArguments args, WarUnit doer, Point2 center, Action finished)
		{
			IEnumerator<int> enumerator = null;
			enumerator = GetCoroutine(args, doer, center, finished,
				() => enumerator.MoveNext()).GetEnumerator();
			enumerator.MoveNext();
		}

		protected abstract IEnumerable<int> GetCoroutine(ActionArguments args, WarUnit doer, Point2 center, Action finished, Action doCoroutine);
	}
}