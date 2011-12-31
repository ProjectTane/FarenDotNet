using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FarenDotNet.FileData
{
	public class Scenario
	{
		// ----- ----- ----- ----- ----- Field ----- ----- ----- ----- -----
		public readonly string Title;
		public readonly string Explanation;
		public readonly int StartTurn;
		public readonly List<int> AreaOwners;
		public readonly List<Provinces> SelectableProvinces;
		public readonly List<Scenario.League> Leagues;
		public readonly List<Scenario.Locate> Locates; 


		// ----- ----- ----- ----- ----- Method ----- ----- ----- ----- -----
		public Scenario(
			string title,
			string exp,
			int turn,
			List<int> owner,
			List<Scenario.Provinces> provs,
			List<Scenario.League> leagues,
			List<Scenario.Locate> locates)
		{
			this.Title = title;
			this.Explanation = exp;
			this.StartTurn = turn;
			this.AreaOwners = owner;
			this.SelectableProvinces = provs;
			this.Leagues = leagues;
			this.Locates = locates;
		}


		// ----- ----- ----- ----- ----- InnerClass ----- ----- ----- ----- -----
		public class Provinces
		{
			public readonly int ProvinceNo;
			public readonly string Name;
			public readonly string Difficulty;
			public readonly string Explanation;
			public Provinces(int no, string name, string diff, string exp)
			{
				this.ProvinceNo = no;
				this.Name = name;
				this.Difficulty = diff;
				this.Explanation = exp;
			}
		}

		[DebuggerDisplay("League No.{ProvNoA} No.{ProvNoB} Turn:{Turn}")]
		public class League
		{
			public readonly int ProvNoA;
			public readonly int ProvNoB;
			public readonly int Turn;
			public League(int a, int b, int turn)
			{
				this.ProvNoA = a;
				this.ProvNoB = b;
				this.Turn = turn;
			}
		}

		[DebuggerDisplay("Locate {UnitID} to Area{AreaNo}")]
		public class Locate
		{
			public readonly int AreaNo;
			public readonly string UnitID;
			public readonly int Level;
			public Locate(int area, string unit, int level)
			{
				this.AreaNo = area;
				this.UnitID = unit;
				this.Level = level;
			}
		}
	}
}
