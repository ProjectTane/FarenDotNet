using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paraiba.Drawing.Surfaces;

namespace FarenDotNet.FileData
{
	public class Unit
	{
		public readonly string Name;
		public readonly string ID;

		public Surface Face { get; set; }
		public Surface Icon { get; set; }

		public Unit(string name, string id)
		{
			this.Name = name;
			this.ID = id;
		}
	}
}
