using System.Drawing;
using Paraiba.Drawing.Surfaces;
using Paraiba.Utility;
using System.Collections.Generic;

namespace FarenDotNet.BasicData
{
	/// <summary>
	/// ユニットのもつImageのデータをまとめて持つ構造体
	/// </summary>
	public struct UnitImageResources
	{
		/// <summary>
		/// 顔グラフィック
		/// マスターおよび固有ユニットの一部がもつ
		/// </summary>
		public readonly Surface Face;

		/// <summary>
		/// 旗のグラフィック
		/// マスターのみがもつ
		/// </summary>
		public readonly IList<Surface> Flag;

		/// <summary>
		/// アイコン
		/// 全ユニットがもつ
		/// </summary>
		public readonly Surface Icon;

		public UnitImageResources(Surface icon, Surface face, IList<Surface> flag)
		{
			Icon = icon;
			Face = face;
			Flag = flag;
		}
	}
}