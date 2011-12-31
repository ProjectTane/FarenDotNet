using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Paraiba.Drawing.Surfaces;
using Paraiba.Utility;
using System.Diagnostics.Contracts;

namespace FarenDotNet.BasicData
{
	public class FieldMap
	{
		public readonly Surface MapImage;
		public readonly IList<Surface> NewtralFlagImage;
		public readonly Surface ConversationImage;

		public FieldMap(Surface mapImg, IList<Surface> flgImg, Surface convImg)
		{
			Contract.Requires(mapImg != null, "mapImg");
			Contract.Requires(flgImg != null, "flgImg");
			Contract.Requires(convImg != null, "convImg");

			MapImage = mapImg;
			NewtralFlagImage = flgImg;
			ConversationImage = convImg;
		}
	}
}
