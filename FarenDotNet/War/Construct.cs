using Paraiba.Drawing.Surfaces;
using FarenDotNet.BasicData;

namespace FarenDotNet.War
{
	public class Construct
	{
		public Construct(Surface image, LandformInfo landformInfo)
		{
			Image = image;
			Info = landformInfo;
		}

		public Surface Image { get; private set; }
		public LandformInfo Info { get; private set; }
	}
}