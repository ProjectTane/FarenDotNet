using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using FarenDotNet.BasicData;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Drawing;
using Paraiba.Drawing.Surfaces;

namespace FarenDotNet.Reign
{
	[DebuggerDisplay("{Name}")]
	public class Unit : IComparable<Unit>
	{
		// ----- ----- ----- フィールド ----- ----- -----
		UnitData _data;
		UnitRank _rank;
		int _exp = 0;
		int _obliterateCount = 0; // 撃破数
		// ----- ----- ----- プロパティ ----- ----- -----
		/// <summary>ユニットのランクE～Sまで</summary>
		public UnitRank Rank { get { return _rank; } }
		/// <summary>ユニットの獲得経験値</summary>
		public int Exp {
			get { return _exp; }
			set {
				_exp = value;
				if (_exp < 100) return;
				switch (_rank)
				{
				case UnitRank.S:
					_exp = 100;
					break;
				case UnitRank.A:
					if (_data.ClassChange != null)
					{
						_data = _data.ClassChange;
						_rank = UnitRank.E;
						_exp -= 100;
						break;
					}
					goto default;
				default:
					_rank++;
					_exp -= 100;
					break;
				}
			}
		}
		/// <summary>撃破数</summary>
		public int ObliterateCount {
			get { return _obliterateCount; }
			set { _obliterateCount = value; }
		}

		// UnitDataの中身をパススルー
		// 作りかけ
		public string Name { get { return _data.Name; } }
		public string ID { get { return _data.ID; } } // 後で消すこと。
		public int HP { get { return _data.HP; } }
		public int MP { get { return _data.MP; } }
		public int Atk { get { return Rank.GetStatus(_data.Atk); } }
		public int Def { get { return Rank.GetStatus(_data.Def); } }
		public int Tec { get { return Rank.GetStatus(_data.Tec); } }
		public int Agi { get { return Rank.GetStatus(_data.Agi); } }
		public int Mag { get { return Rank.GetStatus(_data.Mag); } }
		public int Res { get { return Rank.GetStatus(_data.Res); } }
		public int Mobility { get { return _data.Mobility; } }
		public int HpHeal { get { return _data.HpHeal; } }
		public int MpHeal { get { return _data.MpHeal; } }

		/// <summary>戦力</summary>
		public int Potential { get { return _data.Exp * (25 + Rank.GetStatus(75)) / 100; } }

		public Species Species { get { return _data.Species; } }

		public IList<DefaultAttack> Attacks { get { return _data.Attacks; } }

		public MoveType MoveType { get { return _data.MoveType; } }

		public IList<byte> MagicLevel { get { return _data.MagicLevel; } }

		public Resistivity Resistivity { get { return _data.Resistivity; } }

		public int SkillIndex { get { return _data.SkillIndex; } }
		public int SkillTimes { get { return _data.SkillTimes; } }

		public UnitImageResources Images { get { return _data.Images; } }
		public Surface Image { get { return _data.Images.Icon; } }
		public bool IsUnique { get { return _data.IsUnique; } }
		public bool IsMaster { get { return _data.IsMaster; } }
		public int Cost { get { return _data.Cost; } }
		public IList<int> Summon { get { return _data.Summon; } }
		/// <summary>アンデッド属性を持つか</summary>
		public bool IsUndead { get { return _data.IsUndead; } }

		/// <summary>行動済み</summary>
		public bool Acted { get; set; }

		// ----- ----- ----- メソッド ----- ----- -----

		public Unit(UnitData data) : this(data, UnitRank.E) { }
		public Unit(UnitData data, UnitRank rank)
		{
			Contract.Requires(data != null, "data");
			_data = data;
			_rank = rank;
		}

		// セーブ用
		internal Unit(UnitData data, UnitRank rank, int exp, int obCount)
		{
			_data = data;
			_rank = rank;
			_exp = exp;
			_obliterateCount = obCount;
		}

		#region IComparable<Unit> メンバ

		public int CompareTo (Unit u)
		{
			return u.ValueForCompareTo() - this.ValueForCompareTo();
		}

		// 前にいるほど大きい値を返す
		private int ValueForCompareTo ()
		{
			return this.Potential
				+ (_data.IsMaster ? 1 * 1000 * 1000 * 1000 : 0)
				+ (_data.IsUnique ? 1 * 1000 * 1000 : 0);
		}

		#endregion

		public override string ToString() { return Name; }
	}
}
