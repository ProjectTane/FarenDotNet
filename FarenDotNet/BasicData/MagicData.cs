using System;
using System.Diagnostics;

namespace FarenDotNet.BasicData
{
	/// <summary>
	/// 魔法の種類
	/// </summary>
	public enum MagicType
	{
		// 順序依存（AtAt氏が定義した整数値との整合性のため）
		召喚,
		回復,
		攻撃,
		付与,
	}

	public struct MagicCore
	{
		/// <summary>範囲</summary>
		public readonly byte Area;

		/// <summary>コスト</summary>
		public readonly byte MPCost;

		/// <summary>威力</summary>
		public readonly byte Power;

		/// <summary>射程</summary>
		public readonly byte Range;

		/// <param name="cost">消費コスト</param>
		/// <param name="range">射程</param>
		/// <param name="area">範囲</param>
		/// <param name="power">威力</param>
		public MagicCore(byte cost, byte range, byte area, byte power)
		{
			Debug.Assert(0 <= cost && cost <= 200,
				String.Format("消費コスト({0})は0から200の範囲で指定してください。", cost));

			MPCost = cost;
			Range = range;
			Area = area;
			Power = power;
		}
	}

	/// <summary>
	/// 魔法クラス
	/// 一つの名前に対して一つ存在
	/// </summary>
	public class MagicData
	{
		public MagicData(string name, MagicType type, MagicCore[] cores, string desc,
		                 AttackType element, string se, string anime)
		{
			Debug.Assert(AttackTypes.Magic.Contains(element),
				"属性({0})は「火水風土光闇」の中から選んでください。");

			Name = name;
			Type = type;
			Cores = cores;
			Description = desc;
			Element = element;
			SEFileName = se;
			AnimationFileName = anime;
		}

		/// <summary>名前</summary>
		public string Name { get; private set; }

		/// <summary>行動タイプ</summary>
		public MagicType Type { get; private set; }

		/// <summary>威力やコストなどの値</summary>
		public MagicCore[] Cores { get; private set; }

		/// <summary>魔法の説明</summary>
		public string Description { get; private set; }

		/// <summary>魔法属性</summary>
		public AttackType Element { get; private set; }

		/// <summary>エフェクト音のファイル名</summary>
		public string SEFileName { get; private set; }

		/// <summary>アニメーショングラフィックのファイル名</summary>
		public string AnimationFileName { get; private set; }

		public MagicData Clone(int coreLevel)
		{
			var cores = new MagicCore[coreLevel];
			for (int i = 0; i < coreLevel; i++)
				cores[i] = Cores[i];
			return new MagicData(Name, Type, cores, Description,
				Element, SEFileName, AnimationFileName);
		}
	}
}