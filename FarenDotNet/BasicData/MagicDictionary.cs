using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarenDotNet.BasicData
{
	/// <summary>
	/// 取り合えず単純なSingletonで
	/// </summary>
	public class MagicDictionary
	{
		MagicData[,][] table;

		public MagicDictionary(MagicData[,][] magicTable)
		{
			this.table = magicTable;
		}

		public IList<MagicData> this[AttackType element, byte level]
		{
			get
			{
				return table[ElementToInt(element), level];
			}
		}

		private int ElementToInt(AttackType element)
		{
			switch (element)
			{
				case AttackType.火: return 0;
				case AttackType.水: return 1;
				case AttackType.風: return 2;
				case AttackType.土: return 3;
				case AttackType.光: return 4;
				case AttackType.闇: return 5;
				default:
					throw new ArgumentOutOfRangeException(
						"element",
						"属性は火～闇の範囲で指定してください");
			}
		}
	}
}
