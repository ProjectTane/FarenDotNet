using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Paraiba.Drawing;
using Paraiba.Text;
using Paraiba.Utility;

namespace FarenDotNet
{
	public static class Global
	{
		static Global()
		{
			BmpManager = new SurfaceResourceManager(1024 * 1024 * 4,
				BitmapUtil.Load,
				(bmp, w, h) => bmp.SplitToBitmaps(w, h));

			ChipSize = new Size(32, 32);
			Encoding = XEncoding.SJIS;
			MainLoop = new MainLoop(30);
			Random = new Random();
		}

		public static SurfaceResourceManager BmpManager { get; private set; }

		public static Encoding Encoding { get; private set; }
		public static MainLoop MainLoop { get; private set; }
		public static Random Random { get; private set; }

		public static Size ChipSize { get; private set; }

		public static string InstallDir { get; set; }
		public static string ScenarioDir { get; set; }
		public static IList<string> CharDirs { get; set; }
	}
}