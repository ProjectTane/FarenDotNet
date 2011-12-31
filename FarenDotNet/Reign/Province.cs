using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using Paraiba;
using System.Diagnostics.Contracts;
using Paraiba.Drawing.Animations.Surfaces;
using Paraiba.Drawing.Surfaces;
using Paraiba.Core;
using Paraiba.Utility;
using FarenDotNet.BasicData;

namespace FarenDotNet.Reign
{
	/// <summary>
	/// それぞれの勢力
	/// </summary>
	[DebuggerDisplay("No.{_no} {_name}")]
	public class Province
	{
		// ----- ----- ----- Const ----- ----- -----
		const int MAX_MONEY = 999;
		const int MIN_MONEY = 0;

		// ----- ----- ----- Field ----- ----- -----
		int _no;
		string _name;
		int _money;
		IList<Surface> _flags;
		UnitData _master;

		// ----- ----- ----- Peroperty ----- ----- -----
		public bool IsNeutral { get; set; }

		/// <summary>
		/// ユーザが動かすかどうか。
		/// 動かす場合trueで。
		/// </summary>
		public bool IsPlayer { get; set; }

		public int No { get { return _no; } }
		public string Name { get { return _name; } }
		public UnitData Master { get { return _master; } }

		public int Money
		{
			get { return _money; }
			set { _money = XMath.Center(value, MIN_MONEY, MAX_MONEY); }
		}

		public IList<Surface> FlagImage
		{
			get { return _flags; }
		}

		// ----- ----- ----- Method ----- ----- -----
		private Province(int no, string name, IList<Surface> flags, UnitData master, bool isNeutral)
		{
			Contract.Requires(no >= 0, "no");
			Contract.Requires(name != null, "name");
			Contract.Requires(flags != null, "flags");

			_no = no;
			_name = name;
			_master = master;
			_flags = flags;
			IsNeutral = isNeutral;
		}

		/// <summary>
		/// マスター有りの勢力
		/// </summary>
		/// <param name="no">勢力No</param>
		/// <param name="name">勢力名</param>
		/// <param name="master">マスター</param>
		public Province(int no, string name, UnitData master)
			: this(no, name, master.Images.Flag, master, false)
		{
		}

		/// <summary>
		/// マスターの存在しない勢力
		/// </summary>
		/// <param name="no">勢力No</param>
		/// <param name="name">勢力名</param>
		/// <param name="flags">旗のイメージ</param>
		public Province(int no, string name, IList<Surface> flags)
			: this(no, name, flags, null, true)
		{
		}
	}
}
