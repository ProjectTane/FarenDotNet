using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Drawing;

namespace FarenDotNet.BasicData
{
	/// <summary>
	/// ゲームデータ上でのユニットの情報を持つクラス
	/// </summary>
	[DebuggerDisplay("{Name}")]
	public class UnitData
	{
		// ----- ----- ----- プロパティ ----- ----- -----
		public string Name { get; set; }
		public string ID { get; set; }
		/// <summary>種族</summary>
		public Species Species { get; set; }
		public UnitImageResources Images { get; set; }
		/// <summary>人材ユニットかどうか</summary>
		public bool IsUnique { get; set; }
		/// <summary>マスターかどうか</summary>
		public bool IsMaster { get; set; }

		// ----- ----- 1ページ目 基本 ----- -----
		/// <summary>
		/// 人材の場合は維持費
		/// 一般ユニットの場合は雇用費
		/// </summary>
		public int Cost { get; set; }
		public int Exp { get; set; }
		/// <summary>移動タイプ</summary>d
		public MoveType MoveType { get; set; }
		/// <summary>移動量</summary>
		public int Mobility { get; set; }
		/// <summary>アンデッド属性を持つか</summary>
		public bool IsUndead { get; set; }
		/// <summary>ビーストテイマーかどうか</summary>
		public bool IsTamer { get; set; }
		public int HelpID { get; set; }

		// ----- ----- 2ページ目 能力１ ----- -----
		public int HP { get; set; }
		public int MP { get; set; }
		public int Atk { get; set; }
		public int Def { get; set; }
		public int Tec { get; set; }
		public int Agi { get; set; }
		public int Mag { get; set; }
		public int Res { get; set; }

		// ----- ----- 3ページ目 能力２ ----- -----
		/// <summary>士気</summary>
		public int Morale { get; set; }
		/// <summary>勇猛</summary>
		public int Courage { get; set; }
		public int HpHeal { get; set; }
		public int MpHeal { get; set; }
		public int SkillIndex { get; set; } // HACK
		public int SkillTimes { get; set; }
		public int ClassChangeNo { get; set; } // HACK
		public UnitData ClassChange { get; set; }

		// ----- ----- 4ページ目 攻撃 ----- -----
		[DebuggerDisplay("{AttackCount}回攻撃")]
		public IList<DefaultAttack> Attacks { get; set; }

		// ----- ----- 5ページ目 魔法 ----- -----
		public IList<byte> MagicLevel { get; set; } // HACK : これはむしろ魔法をもってた方が楽なのかも？

		// ----- ----- 6ページ目 特性 ----- -----
		/// <summary>各属性抵抗</summary>
		public Resistivity Resistivity { get; set; } // HACK

		// ----- ----- 7ページ目 召集 ----- -----
		/// <summary>召集可能ユニット</summary>
		public IList<int> Summon { get; set; } // HACK

		// ----- ----- 8ページ目 人材 ----- -----
		/// <summary>自動的に放浪</summary>
		public bool IsVagrant { get; set; }
		/// <summary>特殊ユニットとして扱う</summary>
		public bool IsSpecial { get; set; }
		/// <summary>放浪開始ターン</summary>
		public int EntryTurn { get; set; }
		/// <summary>放浪終了ターン</summary>
		public int LimitTurn { get; set; }

		// ----- ----- ----- 表示用メソッド ----- ----- -----
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private int AttackCount
		{
			get { return this.Attacks.Count(attack => attack.Type != AttackType.なし); }
		}
	}
}
