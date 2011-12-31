namespace FarenDotNet.War.BattleAction
{
	/// <summary>
	/// 戦闘時の行為による消費を表現するインタフェース
	/// BattleAction で利用される
	/// </summary>
	public interface ICost
	{
		/// <summary>
		/// 指定した行為者が消費可能かチェックする
		/// </summary>
		/// <param name="doer"></param>
		/// <returns></returns>
		bool CanExpend(WarUnit doer);

		/// <summary>
		/// 指定した行為者が実際に消費する
		/// </summary>
		/// <param name="situation"></param>
		/// <param name="doer"></param>
		void Expend(Situation situation, WarUnit doer);
	}

	/// <summary>
	/// MP消費を表現するクラス
	/// </summary>
	public class MPCost : ICost
	{
		private readonly int _value;

		public MPCost(int value)
		{
			_value = value;
		}

		#region ICost Members

		public bool CanExpend(WarUnit doer)
		{
			return doer.Status.MP >= _value;
		}

		public void Expend(Situation situation, WarUnit doer)
		{
			doer.ExpendMP(situation, doer, _value);
		}

		#endregion
	}
}