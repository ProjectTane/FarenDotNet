using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarenDotNet.BasicData
{
	/// <summary>移動タイプ</summary>
	public enum MoveType : byte
	{	// 本家のindexに影響しているため順番を入れ替えないこと
		草原,
		鈍足,
		海上,
		砂漠,
		沼地,
		山地,
		森林,
		雪原,
		騎馬,
		飛行
	}

}
