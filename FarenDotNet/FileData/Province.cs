using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paraiba.Drawing.Surfaces;

namespace FarenDotNet.FileData
{
	public class Province
	{
		public readonly int No;
		public readonly string Name;
		public readonly string Language;

		public Surface BigEmblem { get; set; }
		public Surface[] SmallEmblem { get; set; }

		public Province(int no, string name, string lang)
		{
			this.No = no;
			this.Name = name;
			this.Language = lang;
		}
	}
}
