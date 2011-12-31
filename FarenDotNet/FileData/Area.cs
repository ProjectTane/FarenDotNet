using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Drawing;

namespace FarenDotNet.FileData
{
	[DebuggerDisplay("Area{No} {Name}")]
	public class Area
	{
		public int No { get; private set; }
		public string Name { get; private set; }
		public int AreaType { get; set; }
		public int DefaultIncome { get; set; }

		//最大数
		public int NumCity { get; set; }
		public int NumWall { get; set; }
		public int NumRoad { get; set; }

		public int NeutralPower { get; set; }

		// 初期の壁の完成度
		public int DefautlWallRate { get; set; }

		public Point Location { get; set; }
		public int[] Adjacent { get; set; }
		public string MusicFileName { get; set; }

		public Area(int no, string name)
		{
			this.No = no;
			this.Name = name;
		}
	}
}
