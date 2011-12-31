using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarenDotNet.War;

namespace FarenDotNet.War.BattleAction
{
	public interface IBattleAction
	{
		/// <summary>
		/// 行為が発動可能かどうかチェックする
		/// </summary>
		/// <param name="args">アクションの引数</param>
		/// <param name="doer">行為者</param>
		/// <returns>発動可能かどうか</returns>
		bool CanBoot(ActionArguments args, WarUnit doer);

		/// <summary>
		/// 行為を発動する
		/// </summary>
		/// <param name="args">アクションの引数</param>
		/// <param name="doer">行為者</param>
		/// <param name="finished">アクション完了時に呼び出されるデリゲート</param>
		/// <returns></returns>
		void Boot(ActionArguments args, WarUnit doer, Action finished);

		/// <summary>
		/// AI用のBootメソッド
		/// </summary>
		/// <param name="args">アクションの引数</param>
		/// <param name="doer">行為者</param>
		/// <param name="finished">アクション完了時に呼び出されるデリゲート</param>
		/// <returns></returns>
		void BootAI(ActionArguments args, WarUnit doer, Action finished);

	}
}
