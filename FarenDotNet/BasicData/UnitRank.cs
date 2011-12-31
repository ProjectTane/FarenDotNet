using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarenDotNet.BasicData
{
	/// <summary>
	/// ユニットのランクを示すクラス
	/// E～Aで変化し、最上級クラスはその上にSがある
	/// </summary>
	public enum UnitRank : byte
	{
		E, D, C, B, A, S
	}

	public static class UnitRankExtensionMethod
	{
		public static int GetStatus(this UnitRank rank, int value)
		{
			switch (rank)
			{
			case UnitRank.E:
				return value;

			case UnitRank.D:
				return value * 13 / 12;

			case UnitRank.C:
				return value * 14 / 12;

			case UnitRank.B:
				return value * 15 / 12;

			case UnitRank.A:
				return value * 16 / 12;

			case UnitRank.S:
				return value * 20 / 12;
			}
			throw new Exception("到達不可能");
		}

		public static double GetCoefficient(this UnitRank rank)
		{
			switch (rank)
			{
			case UnitRank.E:
				return 0;

			case UnitRank.D:
				return 13.0 / 12;

			case UnitRank.C:
				return 14.0 / 12;

			case UnitRank.B:
				return 15.0 / 12;

			case UnitRank.A:
				return 16.0 / 12;

			case UnitRank.S:
				return 20.0 / 12;
			}
			throw new Exception("到達不可能");
		}
	}
}
