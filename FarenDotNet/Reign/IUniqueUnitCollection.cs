using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarenDotNet.Reign
{
	public interface IUniqueUnitCollection
	{
		Unit this[string id] { get; set; }
	}
}
