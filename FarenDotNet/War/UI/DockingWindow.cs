using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarenDotNet.War.UI
{
	public class DockingWindow :
#if DOCUMENT
		Form
#else
		WeifenLuo.WinFormsUI.Docking.DockContent
#endif
	{
	}
}
