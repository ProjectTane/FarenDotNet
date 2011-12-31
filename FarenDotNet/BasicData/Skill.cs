using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarenDotNet.BasicData
{
	public enum ScopeType
	{
		Arrow = 1,	// 弓矢系。範囲１限定。複数回攻撃可能。画像は３２＊２５６。（８枚）
		Breath,		// ブレス系。範囲２限定。複数回攻撃可能。画像は９６＊２２４。（２１枚）
		Heal,		// 範囲内の対象マスにのみアニメーションする。
		// 唯一、射程０、範囲０で全体特技として使用可能なタイプ。
		Magic,		// 魔法系。一番使いやすいタイプ。
		WallBreak,	// 城壁破壊系。攻撃側で城壁に隣接していないと使用することすら出来ない。
		Unknown,	// 使用不可能。使うとバグります。
		SixWay,		// ６方向系。範囲は自分のマスも含むことに注意。
		BlackDragonSword,	// 黒竜剣系。範囲２限定。唯一９６平方のアニメーションが使用可能。
	}

	public enum SkillType
	{
		Othre,
		Heal,
		Attack,
	}

	public enum AttackDependency
	{
		None,
		Attack,
		Magic,
	}

	public enum DefenseDependency
	{
		None,
		Defense,
		Resistivity,
	}

	public class Skill
	{
		internal string name;							// 名前
		internal ScopeType scopeType;					// 画像の表示形式
		internal SkillType skillType;					// その他 / 回復 / 攻撃
		internal int mpCost;						// MP消費量
		internal int range;								// 射程
		internal int area;								// 範囲
		internal int power;								// 威力
		internal AttackDependency attackDependency;		// 攻撃に依存する能力
		internal DefenseDependency defenseDependency;	// 防御に依存する能力
		internal AttackType attackType;					// 属性
		internal string soundName;						// 効果音のファイル名
		internal int sideSize;							// 画像の横幅
		internal int imageCount;						// 画像数

		public string SoundName
		{
			get { return soundName; }
		}

		public int SideSize
		{
			get { return sideSize; }
		}

		public int ImageCount
		{
			get { return imageCount; }
		}


		public string Name
		{
			get { return name; }
		}

		public ScopeType ScopeType
		{
			get { return scopeType; }
		}

		public SkillType SkillType
		{
			get { return skillType; }
		}

		public int Range
		{
			get { return this.range; }
		}
		public int Area
		{
			get { return this.area; }
		}
		public int Power
		{
			get { return this.power; }
		}
		public int MPCost
		{
			get { return this.mpCost; }
		}
		public AttackDependency AtkDependency
		{
			get { return this.attackDependency; }
		}
		public DefenseDependency DefDependency
		{
			get { return this.defenseDependency; }
		}
		public AttackType AtkType
		{
			get { return this.attackType; }
		}
		public bool IsHitAbsolute
		{
			get { return this.defenseDependency != DefenseDependency.Defense; }
		}
	}
}
