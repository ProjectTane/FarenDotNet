using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.Drawing;
using System.Diagnostics;

namespace FarenDotNet.BasicData
{
	public class MasterData
	{
		// ----- ----- ----- Property ----- ----- -----
		public IList<string> CharaDirs { get; set; }
		public MusicData Music { get; set; }
		public IList<Revival> Revivals { get; set; }
		public IList<Inherit> Inherits { get; set; }
		public IList<Master> Masters { get; set; }

		// ----- ----- ----- Method ----- ----- -----
		public MasterData(
			IList<string> dirs,
			MusicData music,
			IList<Revival> revival,
			IList<Inherit> inherit,
			IList<Master> masters)
		{
			CharaDirs = dirs;
			Music = music;
			Revivals = revival;
			Inherits = inherit;
			Masters = masters;
		}



		// ----- ----- ----- Inner struct & class ----- ----- -----
		[DebuggerDisplay("No.{_no} {_name}")]
		public class Master
		{
			// ----- ----- ----- Field ----- ----- -----
			int _no;
			string _id;
			string _name;
			int _prudence;
			Color _color;
			string _reignMusic;
			string _battleMusic;
			string _masterBattleMusic;
			bool _ghost;

			// ----- ----- ----- Property ----- ----- -----
			public int No { get { return _no; } }
			public string ID { get { return _id; } }
			public string Name { get { return _name; } }

			/// <summary>慎重さ</summary>
			public int Prudence { get { return _prudence; } }
			public Color Color { get { return _color; } }

			public string ReignMusic { get { return _reignMusic; } }
			public string BattleMusic { get { return _battleMusic; } }
			public string MasterBattleMusic { get { return _masterBattleMusic; } }
			
			public bool Ghost { get { return _ghost; } }

			// ----- ----- ----- Method ----- ----- -----
			public Master(
				int no,
				string id, string name,
				int prd, Color color,
				string reign, string battle, string master,
				bool ghost)
			{
				_no = no;
				_id = id;
				_name = name;
				_prudence = prd;
				_color = color;
				_reignMusic = reign;
				_battleMusic = battle;
				_masterBattleMusic = master;
				_ghost = ghost;
			}
		}

		[DebuggerDisplay("No.{MasterNo} -> No.{NextMasterNo}")]
		public class Revival
		{
			public readonly int MasterNo;
			public readonly int NextMasterNo;
			public readonly int StartTurn;
			public readonly string Message;

			public Revival(int master, int next, int turn, string message)
			{
				this.MasterNo = master;
				this.NextMasterNo = next;
				this.StartTurn = turn;
				this.Message = message;
			}
		}

		[DebuggerDisplay("No.{MasterNo} -> No.{NextMasterNo} width {RequireUnitID}")]
		public class Inherit
		{
			public readonly int MasterNo;
			public readonly int NextMasterNo;
			public readonly string RequireUnitID;
			public readonly int StartTurn;
			public readonly string Message;

			public Inherit(
				int master, string unit, int next, int turn, string msg)
			{
				this.MasterNo = master;
				this.RequireUnitID = unit;
				this.NextMasterNo = next;
				this.StartTurn = turn;
				this.Message = msg;
			}
		}

		public struct MusicData
		{
			public readonly string Event;
			public readonly string Desicive;

			public MusicData(string eventMusic, string desiciveMusic)
			{
				Contract.Requires(eventMusic != null, "eventMusic");
				Contract.Requires(desiciveMusic != null, "desiciveMusic");

				Event = eventMusic;
				Desicive = desiciveMusic;
			}
		}
	}
}
