using System.Collections.Generic;

namespace FarenDotNet.BasicData
{
	/// <summary>攻撃属性</summary>
	public enum AttackType : byte
	{
		// 順序を変更しないこと、値は変更はNG
		なし,
		物理,
		毒,
		石化,
		麻痺,
		眠り,
		幻想,
		死,
		吸収,
		神聖,
		火,
		水,
		風,
		土,
		光,
		闇,
		体力回復,
		魔力回復
	}

	public static class AttackTypes
	{
		public static IList<AttackType> Magic = new[] {
			AttackType.火, AttackType.水, AttackType.風, AttackType.土, AttackType.光, AttackType.闇
		};

		public static IList<AttackType> Attack = new[] {
			AttackType.物理, AttackType.毒, AttackType.石化, AttackType.麻痺, AttackType.眠り,
			AttackType.幻想, AttackType.死, AttackType.吸収, AttackType.神聖,
			AttackType.火, AttackType.水, AttackType.風, AttackType.土, AttackType.光, AttackType.闇
		};

		public static IList<AttackType> Heal = new[] {
			AttackType.体力回復, AttackType.魔力回復,
		};

		public static IList<AttackType> AbnormalCondition = new[] {
			AttackType.毒, AttackType.石化, AttackType.麻痺, AttackType.眠り, AttackType.幻想, AttackType.死,
		};
	}
}