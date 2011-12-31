using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarenDotNet.BasicData;
using System.Diagnostics.Contracts;

namespace FarenDotNet.Reign
{
	public class UniqueUnitCollection : IUniqueUnitCollection
	{
		// ----- ----- ----- Field ----- ----- -----
		IList<UnitData> _units; 
		Func<string, IList<UnitData>, Unit> _func;
		Dictionary<string, Unit> _dictionary = new Dictionary<string,Unit>();

		// ----- ----- ----- Property ----- ----- -----
		public Unit this[string id]
		{
			get {
				Unit unit;
				if (_dictionary.TryGetValue(id, out unit))
					return unit;
				return _dictionary[id] = _func(id, _units);
			}
			set
			{
				Unit unit;
				if (_dictionary.TryGetValue(id, out unit)) {
					if (value != unit)
						throw new WrongUnitException(value);
				}
				else {
					if (value.ID == id)
						_dictionary[id] = value;
					else
						throw new WrongUnitException(value);
				}
			}
		}


		// ----- ----- ----- Method ----- ----- -----
		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="units">ユニットのソースデータ</param>
		/// <param name="func">ソースとIDからユニットデータを作成する関数</param>
		public UniqueUnitCollection (IList<UnitData> units, Func<string, IList<UnitData>, Unit> func)
		{
			Contract.Requires(units != null, "units");
			Contract.Requires(func != null, "func");

			_units = units;
			_func = func;
		}

		// ----- ----- ----- InnerClass ----- ----- -----
		public class WrongUnitException : Exception
		{
			public readonly Unit Unit;

			public WrongUnitException (Unit unit)
			{
				this.Unit = unit;
			}
		}
	}
}
