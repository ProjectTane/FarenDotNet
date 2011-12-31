using Paraiba.Drawing.Surfaces;

namespace FarenDotNet.BasicData
{
	public class Landform
	{
		public Landform(Surface image, LandformInfo landformInfo)
		{
			Image = image;
			Info = landformInfo;
		}

		public Surface Image { get; private set; }

		public LandformInfo Info { get; private set; }
	}
}