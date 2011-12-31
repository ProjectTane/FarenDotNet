using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Diagnostics.Contracts;
using FarenDotNet.Reign;
using Paraiba.Drawing;
using FarenDotNet.BasicData;
using System;
using FarenDotNet.Reign.UI;
using Paraiba.Linq;

namespace FarenDotNet.Loader
{
	public class ReignLoader
	{
		private readonly string _gamePath;
		private readonly string _rootPath;

		public ReignLoader(string gamepath, string rootpath)
		{
			Contract.Requires(gamepath != null, "gamepath");
			Contract.Requires(rootpath != null, "rootpath");

			_gamePath = gamepath;
			_rootPath = rootpath;
		}

		public ReignData Load()
		{
			return new ReignData(
				LoadWorldMap(),
				LoadAreas(),
				LoadCallable());
		}

		private IList<AreaData> LoadAreas()
		{
			var filepath = Path.Combine(_gamePath, Path.Combine("Data", "AreaData"));
			return new AreaDataLoader().Load(filepath);
		}

		private FieldMap LoadWorldMap()
		{
			var picDir = Path.Combine(_gamePath, "Picture");
			var mapImage = Global.BmpManager.GetSurface(Path.Combine(picDir, "FieldMap.bmp"));
			var flagImages = Global.BmpManager.GetSurfaces(Path.Combine(picDir, "Flag0.bmp"), 32, 32,
				path => BitmapUtil.Load(path, Point.Empty)); // TODO: 透過色は左上座標の値で良いのか調査
			var convImg = Global.BmpManager.GetSurface(Path.Combine(_rootPath,
				Path.Combine("Picture", "Conversation.bmp")));

			return new FieldMap(mapImage, flagImages, convImg);
		}

		private IList<Callable> LoadCallable()
		{
			var table = new SettingFileReader(
				Path.Combine(_gamePath, Path.Combine("Data", "CallAble")));

			var calls = new List<Callable>();
			int index = 0;
			while (table.HasKey("Call" + index) && table.HasKey("Init" + index))
			{
				var c = new Callable(index, table["Call" + index], table["Init" + index]);
				calls.Add(c);
				index++;
			}

			return calls;
		}
	}
}
