using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarenDotNet.War.BattleAction;

namespace FarenDotNet.War.UI
{
	public class WarMagicData
	{
		public string Name { get; private set; }
		public string Description { get; private set; }

		public int Range { get; private set; }
		public int Area { get; private set; }
		public int ExpandMP { get; private set; }

		public IBattleAction Action { get; private set; }

		public WarMagicData(string name, string description, IBattleAction action, int expandMP, int area, int range)
		{
			this.Name = name;
			this.Description = description;
			this.Action = action;
			this.ExpandMP = expandMP;
			this.Area = area;
			this.Range = range;
		}
	}
}
