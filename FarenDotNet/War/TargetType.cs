using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarenDotNet.War
{
	public enum TargetType
	{
		NONE = 0x01,		// 敵も味方も存在しない場所
		FRIEND = 0x02,		// 敵が存在する場所
		ENEMY = 0x04,		// 味方が存在する場所
	}
}
