using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarenDotNet.BasicData;
using System.Windows.Forms;
using System.Diagnostics.Contracts;

namespace FarenDotNet.Reign
{
	public class ProvinceInfo
	{
		// ----- ----- ----- ----- ----- FIELD ----- ----- ----- ----- -----
		Province _prov;
		Area _area; // 本拠地
		int _nArea;
		int _income;
		int _nUnique;
		int _power;
		int _league;

		// ----- ----- ----- ----- ----- PROPERTY ----- ----- ----- ----- -----
		public string Name { get { return _prov.Name; } }
		public Province Province { get { return _prov; } }
		public Area Area { get { return _area; } }
		public int NumAreas { get { return _nArea; } }
		public int Income { get { return _income; } }
		public int NumUnique { get { return _nUnique; } }
		public int Power { get { return _power; } }
		public int League { get { return _league; } }

		// ----- ----- ----- ----- ----- CTOR ----- ----- ----- ----- -----
		public ProvinceInfo(Province prov, IList<Area> areas, ReignManager manager)
		{
			Contract.Requires(prov != null, "prov");
			Contract.Requires(areas != null, "areas");

			_prov = prov;
			foreach (var area in areas)
			{
				_nArea++;
				_income += area.Income;
				foreach (var unit in area.Units)
				{
					if (unit.IsMaster)
						_area = area;
					else if (unit.IsUnique)
						_nUnique++;
					_power += unit.Potential;
				}
			}
			_league = manager.League[_prov.No, manager.ActingProv.No];
		}

		public ListViewItem ToListViewItem()
		{
			return new ListViewItem(new string[]{
				_prov.Name,
				_area.Name,
				_nArea.ToString(),
				_prov.Money.ToString(),
				_income.ToString(),
				_income.ToString(),
				_power.ToString(),
				_league == Reign.League.INF ? "∞" : _league.ToString()
			});
		}
	}
}
