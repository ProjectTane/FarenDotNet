using System.Collections.Generic;
using System.Linq;

namespace FarenDotNet.War
{
	public class WarUnitCollection
	{
		public WarUnitCollection()
		{
			WarUnits = new List<WarUnit>();
		}

		public WarUnitCollection(IList<WarUnit> warUnits)
		{
			WarUnits = warUnits;
		}

		public IList<WarUnit> WarUnits { get; private set; }

		public IEnumerable<WarUnit> Alive
		{
			get { return WarUnits.Where(unit => unit.Alive); }
		}

		public void AddUnit(WarUnit unit)
		{
			WarUnits.Add(unit);
		}
	}
}