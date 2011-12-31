using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace FarenDotNet.BasicData
{
	[DebuggerDisplay("{Name}")]
	public class AreaData
	{
		int _no;
		// ----- ----- ----- プロパティ ----- ----- -----
		public int No { get { return _no; } }
		public string Name { get; set; }
		public int AreaType { get; set; }
		public int DefaultIncome { get; set; }

		//最大数
		public int NumCity { get; set; }
		public int NumWall { get; set; }
		public int NumRoad { get; set; }

		public int NeutralPower { get; set; }

		// 初期の壁の完成度（中立）
		public int DefautlWallRate { get; set; }

		public Point FlagLocation { get; set; }
		public int[] Adjacent { get; set; }
		public string MusicFileName { get; set; }

		public AreaData(int no)
		{
			_no = no;
		}
	}
}
