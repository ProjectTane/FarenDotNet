using System.Collections.Generic;
using System.Linq;
using FarenDotNet.BasicData;

namespace FarenDotNet.War
{
	// http://drillroman.sakura.ne.jp/srcResult.txt
	public class WarSide
	{
		public WarSide(bool isPlayer)
		{
			IsPlayer = isPlayer;
		}

		/// <summary>
		/// この軍の味方の軍を設定する。
		/// </summary>
		public IList<WarSide> Friends { get; set; }

		/// <summary>
		/// 軍の勢力
		/// </summary>
		public double Force { get; private set; }

		/// <summary>
		/// 軍の士気
		/// </summary>
		public double Brave { get; private set; }

		/// <summary>
		/// プレイヤー側の軍隊かどうか
		/// </summary>
		public bool IsPlayer { get; private set; }

		/// <summary>
		/// 指定した勢力がこの勢力の敵対関係にあるかどうか判定する。
		/// </summary>
		/// <param name="target">判定したい勢力</param>
		/// <returns>敵対関係にあればtrue、そうでなければfalse</returns>
		public bool IsOpponent(WarSide target)
		{
			return target != this && (Friends == null || !Friends.Contains(target));
		}

		public void UpdateForceAndBrave(IEnumerable<WarUnit> units)
		{
			//各キャラの戦力
			//    経験地 + 20 * ( ランク / 10 + 1 ) * ( HP * 0.8 / MAXHP + 0.2 )
			//さらにそのキャラの状態による補正
			//    BChr.Health & 16 > 0 の場合 戦力は 30%だけ加算
			//    BChr.Health & 2 > 0 の場合 50%だけ加算
			//    BChr.Health & (4 + 8) > 0 の場合、75%だけ加算
			// でも変更します
			Force = units.Where(unit => unit.Side == this)
				.Sum(unit =>
					(0.2 + 0.8 * unit.Status.HP / unit.Status.MaxHP) * unit.Rank.GetCoefficient() * 20 + unit.Exp
				);	// TODO: ( ランク / 10 + 1 ) == unit.Rank.GetCoefficient() ?

			//TODO: Calc Brave
			Brave = Force;
		}
	}
}