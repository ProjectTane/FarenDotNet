using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using FarenDotNet.BasicData;

namespace FarenDotNet.BasicData
{
	public class ScenarioData
	{
		// ----- ----- ----- Field ----- ----- -----
		private string _title;
		private string _exp;
		private int _turn;
		private IList<int> _area;
		private IList<ScenarioData.Master> _masters;
		private IList<ScenarioData.League> _leagues;
		private IList<ScenarioData.Locate> _locates;

		// ----- ----- ----- Property ----- ----- -----
		public string Title { get { return _title; } }
		public string Explanation { get { return _exp; } }
		public int StartTurn { get { return _turn; } }
		public IList<int> AreaOwner { get { return _area; } }
		public IList<ScenarioData.Master> Masters { get { return _masters; } }
		public IList<ScenarioData.League> Leagues { get { return _leagues; } }
		public IList<ScenarioData.Locate> Locates { get { return _locates; } }

		// ----- ----- ----- Method ----- ----- -----
		public ScenarioData(
			string title,
			string explanation,
			int turn,
			IList<ScenarioData.Master> masters,
			IList<int> area,
			IList<ScenarioData.League> leagues,
			IList<ScenarioData.Locate> locates)
		{
			_title = title;
			_exp = explanation;
			_turn = turn;
			_area = area;
			_masters = masters;
			_leagues = leagues;
			_locates = locates;
		}

		public override string ToString() { return _title; }

		// ----- ----- ----- Inner Struct & Class ----- ----- -----
		[DebuggerDisplay("Turn {Turn}  No.{ProvNoA}-No.{ProvNoB}")]
		public struct League
		{
			public readonly int ProvNoA;
			public readonly int ProvNoB;
			public readonly int Turn;
			/// <summary>
			/// マスター間の同盟を表すクラス。
			/// </summary>
			/// <param name="noA">マスターNo</param>
			/// <param name="noB">マスターNo</param>
			/// <param name="turn">同盟持続ターン数</param>
			public League(int noA, int noB, int turn)
			{
				this.ProvNoA = noA;
				this.ProvNoB = noB;
				this.Turn = turn;
			}
		}

		public struct Locate
		{
			public readonly string UnitID;
			public readonly int AreaNo;
			public readonly int Rank;

			/// <summary>
			/// ユニットの初期配置を表すクラス。
			/// </summary>
			/// <param name="unit">配置するユニット</param>
			/// <param name="area">配置するエリア番号</param>
			/// <param name="rank">配置時の強さ</param>
			public Locate(string unit, int area, int rank)
			{
				this.UnitID = unit;
				this.AreaNo = area;
				this.Rank = rank;
			}
		}

		[DebuggerDisplay("{ToString()}")]
		public class Master
		{
			/* * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * 
			 * このクラスはプレイヤーのマスター選択のためだけに存在しており、
			 * このクラスは内政部分に一切の影響を与えません。
			 * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * * */

			public readonly int No;
			public readonly string DisplayName;
			public readonly string Difficulty;
			public readonly string Explanation;

			/// <summary>マスターとして選択できるかどうか</summary>
			public readonly bool TakeOff;

			/// <param name="no">マスター番号</param>
			/// <param name="name">マスターの表示名</param>
			/// <param name="diff">難易度</param>
			/// <param name="exp">説明文</param>
			/// <param name="takeoff"></param>
			public Master(int no, string name, string diff, string exp, bool takeoff)
			{
				Contract.Requires(name != null, "name");
				Contract.Requires(diff != null, "diff");
				Contract.Requires(exp != null, "exp");

				No = no;
				DisplayName = name;
				Difficulty = diff;
				Explanation = exp;
				TakeOff = takeoff;
			}

			// [DebuggerDisplay]で呼び出す。
			public override string ToString()
			{
				if (TakeOff)
					return '(' + DisplayName + ')';
				return DisplayName;
			}
		}
	}
}
