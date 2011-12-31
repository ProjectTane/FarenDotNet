using System;
using System.Drawing;
using Paraiba.Drawing.Surfaces;
using FarenDotNet.War.BattleAction;

namespace FarenDotNet.War.BattleCommand
{
	public interface IBattleCommand
	{
		/// <summary>ボタン名</summary>
		string Name { get; }

		/// <summary>ボタンアイコン</summary>
		Image Image { get; }

		/// <summary>詳細</summary>
		string Description { get; }

		/// <summary>
		/// コマンドを使用可能かどうか調べる
		/// </summary>
		/// <param name="args">アクションの引数</param>
		/// <param name="doer">行為者</param>
		/// <returns>使用可能かどうか</returns>
		bool CanExecute(ActionArguments args, WarUnit doer);

		/// <summary>
		/// コマンドを使用する
		/// </summary>
		/// <param name="args">アクションの引数</param>
		/// <param name="doer">行為者</param>
		/// <param name="finished">アクション完了時に呼び出されるデリゲート</param>
		void Execute(ActionArguments args, WarUnit doer, Action finished);
	}
}