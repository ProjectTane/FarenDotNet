using System;
using System.Collections.Generic;
using Paraiba.Drawing.Surfaces;
using FarenDotNet.BasicData;

namespace FarenDotNet.War
{
	public class LandformCollection
	{
		public LandformCollection(IList<Tuple<Surface, LandformInfo>> landforms)
		{
			Landforms = landforms;
		}

		public IList<Tuple<Surface, LandformInfo>> Landforms { get; private set; }
	}
}