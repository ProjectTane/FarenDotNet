using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace FarenDotNet.BasicData
{
	// 攻撃クラス
	[DebuggerDisplay("{Attribute} : {Power}")]
	public struct DefaultAttack
	{
		public readonly int Power;
		public readonly AttackType Type;
		public DefaultAttack(int power, AttackType attr)
		{
			Debug.Assert(0 <= power && power <= 9999,
				"攻撃力(" + power + ")は0から9999の間で指定してください。");
			Debug.Assert(AttackType.なし <= attr && attr <= AttackType.神聖,
				"攻撃(" + attr + ")は物理～神聖の間で指定してください。");
			this.Power = power;
			this.Type = attr;
		}
	}
}
