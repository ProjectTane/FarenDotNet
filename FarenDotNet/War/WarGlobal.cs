using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Paraiba.Drawing;
using Paraiba.Utility;
using FarenDotNet.BasicData;

namespace FarenDotNet.War
{
	// Global から WarGlobal にデータをコピーすべし
	public static class WarGlobal
	{
		public static readonly SurfaceResourceManager BmpManager =
			new SurfaceResourceManager(1024 * 1024 * 4,
				BitmapUtil.Load,
				(bmp, w, h) => bmp.SplitToBitmaps(w, h));

		public static Encoding Encoding = Encoding.GetEncoding("shift_jis");

		/// <summary>
		/// このプログラム共通の乱数ジェネレータ
		/// </summary>
		public static Random Random = new Random();

		public static IList<Landform> Landforms { get; set; }
		public static IList<Construct> Constructs { get; set; }
		public static IList<WarSkill> Skills { get; set; }
		public static WarUnitBuilder UnitBuilder { get; set; }

		public static Size ChipSize = new Size(32, 32);
	}
}