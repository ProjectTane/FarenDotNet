using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using FarenDotNet.Loader;
using FarenDotNet.BasicData;

namespace FarenDotNet.BasicData
{
	[DebuggerDisplay("{_title}")]
	public class GameData
	{
		// ----- ----- ----- フィールド ----- ----- -----
		string _title;

		// ----- ----- ----- プロパティ ----- ----- -----

		public string Title { get { return this._title; } }
		/// <summary>

		/// マスターおよび陣営のデータ
		/// IDとは1つずれるので注意すること
		/// </summary>
		public MasterData MastersData { get; set; }
		public IList<ScenarioData> Scenarios { get; set; }
		public IList<UnitData> Units { get; set; }
		public ReignLoader ReignLoader { get; set; }

		// ----- ----- ----- メソッド ----- ----- -----
		public GameData(string title)
		{
			Debug.Assert(title != null);

			this._title = title;
		}

		public override string ToString()
		{
			return this._title;
		}
	}
}
