using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Paraiba.Drawing.Surfaces;
using FarenDotNet.BasicData;
using System.Diagnostics.Contracts;

namespace FarenDotNet.Reign
{
	[DebuggerDisplay("{Areas.Count} Areas.")]
	public class WorldMap
	{
		// ----- ----- ----- Field ----- ----- -----
		public readonly IList<Area> Areas;
		readonly BasicData.FieldMap _data;

		// ----- ----- ----- Property ----- ----- -----
		public Surface MapImage { get { return _data.MapImage; } }
		public Surface ConversationImage { get { return _data.ConversationImage; } }


		// ----- ----- ----- Method ----- ----- -----
		public WorldMap(IList<Area> areas, BasicData.FieldMap map)
		{
			Contract.Requires(areas != null, "areas");
			Contract.Requires(map != null, "map");

			Areas = areas;
			_data = map;
		}

	}
}
