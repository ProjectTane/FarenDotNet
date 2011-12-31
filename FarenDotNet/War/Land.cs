using System;
using Paraiba.Drawing.Surfaces;
using FarenDotNet.BasicData;

namespace FarenDotNet.War
{
	public class Land
	{
		private readonly Construct _construct;
		private readonly Landform _landform;

		public Land(int height, Landform landform, Construct construct)
		{
			Height = Math.Max(height, 0);
			_landform = landform;
			_construct = construct;

			if (construct != null)
			{
				Image = new LayerSurface(new[] { _landform.Image, _construct.Image });
				Info = construct.Info;
			}
			else
			{
				Image = landform.Image;
				Info = landform.Info;
			}
		}

		public int Height { get; private set; }

		public WarUnit Unit { get; internal set; }

		// 壊された場合などは、nullが入る
		public Construct Construct
		{
			get { return _construct; }
		}

		public Landform Landform
		{
			get { return _landform; }
		}

		public Surface Image { get; private set; }

		public Surface OriginalImage
		{
			get { return _landform.Image; }
		}

		public LandformInfo Info { get; private set; }

		public LandformInfo OriginalLandformInfo
		{
			get { return _landform.Info; }
		}
	}
}