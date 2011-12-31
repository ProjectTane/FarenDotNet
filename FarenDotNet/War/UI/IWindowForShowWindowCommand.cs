using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarenDotNet.War.BattleAction;

namespace FarenDotNet.War.UI
{
	// HACK: 名称変更
	/// <summary>
	/// ShowWindowCommandにより表示されるWindowが実装すべきインターフェース
	/// </summary>
	public interface IWindowForShowWindowCommand
	{
		/// <summary>
		/// ウィンドウを表示する
		/// </summary>
		/// <param name="model"></param>
		/// <param name="doer"></param>
		/// <param name="finished"></param>
		void Show(WarPresentationModel model, WarUnit doer, Action finished);
	}
}
