using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace FarenDotNet.BasicData
{
	public enum ResistivityType
	{
		普通,
		弱い,
		強い,
		吸収,
	}

	public class Resistivity : ICloneable
	{
		const int SIZE = (int)AttackType.闇 - (int)AttackType.物理 + 1;

		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		private byte[] _resist;

		public ResistivityType this[AttackType attr]
		{
			get {
				return (ResistivityType)_resist[(int)attr - (int)AttackType.物理];
			}
			set
			{
				_resist[(int)attr - (int)AttackType.物理] = (byte)value;
			}
		}

		public Resistivity()
		{
			_resist = new byte[SIZE];
		}

		public Resistivity(byte[] array)
		{
			Debug.Assert(array.Length == SIZE);
			_resist = array;
		}

		public Resistivity Clone()
		{
			return new Resistivity((byte[])_resist.Clone());
		}

		#region ICloneable メンバ

		object ICloneable.Clone()
		{
			return Clone();
		}

		#endregion
	}
}
