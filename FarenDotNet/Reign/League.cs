using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paraiba.Utility;

namespace FarenDotNet.Reign
{
	/// <summary>
	/// 同盟ターンを記録するクラス
	/// 値の代入により自動的にテーブルサイズを広げる。
	/// 負の値を入れないように。
	/// </summary>
	[Serializable]
	public class League
	{
		// ----- ----- ----- Field ----- ----- -----
		int[] _table;
		int _max;
		public const int INF = 99;

		// ----- ----- ----- Ctor ----- ----- -----
		public League() : this(0) { }
		public League(int max)
		{
			_max = max;
			_table = new int[max * (max + 1) / 2];
		}

		// ----- ----- ----- Property ----- ----- -----
		public int this[int x, int y]
		{
			// 0 <= x < y < _max で
			get
			{
				if (x > y) return this[y, x];
				if (x < 0 || x == y || y >= _max)
					return 0;
				return _table[y * (y - 1) / 2 + x];
			}
			set
			{
				if (x > y) this[y, x] = value;
				if (value < 0 || x < 0 || x == y) return;
				if (y >= _max)
				{
					 // 拡張
					_max = y + 1;
					int[] neo = new int[y * (y + 1) / 2];
					for (int i = 0; i < _table.Length; i++)
						neo[i] = _table[i];
					_table = neo;
				}
				_table[y * (y - 1) / 2 + x] = value;
			}
		}

		public IEnumerable<Tuple<int, int>> GetLeagues(int areaNo)
		{
			for (int i = 0; i < areaNo; i++)
				yield return Tuple.Create(i, _table[areaNo * (areaNo - 1) / 2 + i]);
			for (int i = areaNo + 1; i < _max; i++)
				yield return Tuple.Create(i, _table[i * (i - 1) / 2 + areaNo]);
		}

		// ----- ----- ----- Method ----- ----- -----
		public void DeclementAll()
		{
			for (int i = 0; i < _table.Length; i++)
			{
				if (_table[i] > 0 && _table[i] != INF)
					_table[i]--;
			}
		}
	}
}
