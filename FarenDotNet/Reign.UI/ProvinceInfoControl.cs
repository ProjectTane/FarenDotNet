using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace FarenDotNet.Reign.UI
{
	public partial class ProvinceInfoControl : UserControl
	{
		IList<ProvinceInfo> _infos;

		public ProvinceInfoControl()
		{
			InitializeComponent();
		}

		public void SetInfos(IList<ProvinceInfo> infos)
		{
			_infos = infos;
			this.ListView.Items.Clear();
			foreach (var info in infos)
				this.ListView.Items.Add(info.ToListViewItem());
		}

		public ProvinceInfo SelectedInfo
		{
			get
			{
				var ixs = ListView.SelectedIndices;
				if (ixs.Count == 0 || ixs[0] == -1)
					return null;
				return _infos[ixs[0]];
			}
		}

	}
}
