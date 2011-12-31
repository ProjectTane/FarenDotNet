using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace FarenDotNet.BasicData
{
	[DebuggerDisplay("No.{No}, Call={Call.Count}, Init={Init.Count}")]
	public class Callable
	{
		/// <summary>
		/// エリアタイプNo
		/// </summary>
		public readonly int No;

		/// <summary>
		/// マスター用：雇用可能ユニット
		/// </summary>
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public readonly IList<string> Call;

		/// <summary>
		/// 中立軍用：雇用可能ユニット
		/// </summary>
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public readonly IList<string> Init;

		public Callable (int no, IList<string> call, IList<string> init)
		{
			Contract.Requires(call != null, "call");
			Contract.Requires(init != null, "init");

			this.No = no;
			this.Call = call;
			this.Init = init;
		}
	}
}
