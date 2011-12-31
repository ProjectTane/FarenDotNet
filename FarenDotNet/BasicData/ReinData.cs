using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarenDotNet.BasicData;
using FarenDotNet.Reign;

namespace FarenDotNet.BasicData
{
	public class ReignData
	{
		public readonly FieldMap WorldMap;
		public readonly IList<AreaData> Areas;
		public readonly IList<Callable> Callables;

		public ReignData (FieldMap map, IList<AreaData> areas, IList<Callable> callables)
		{
			this.WorldMap = map;
			this.Areas = areas;
			this.Callables = callables;
		}
	}
}
