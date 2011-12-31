using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace FarenDotNet.Loader
{
	public class GameLoaderFactory
	{
		public bool IsLoadable(string path)
		{
			var file = Path.Combine(path, "Config");
			var result = File.Exists(file);
			return result;
		}

		public GameLoader GetLoader(string path)
		{
			return new GameLoader(path);
		}
	}
}
