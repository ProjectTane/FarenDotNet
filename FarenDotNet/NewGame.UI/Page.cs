using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics.Contracts;

namespace FarenDotNet.NewGame.UI
{
	public partial class Page : UserControl
	{
		// ----- ----- ----- Field ----- ----- -----
		private bool _canNext;

		// ----- ----- ----- Event ----- ----- -----

		// ----- ----- ----- Property ----- ----- -----
		public PageBinder Binder { get; set; }

		public bool CanNext
		{
			get { return _canNext; }
			protected set
			{
				if (_canNext != value) {
					// 変更してから通知すること。
					_canNext = value;
					if (this.Binder != null)
						this.Binder.UpdateNext();
				}
			}
		}

		// ----- ----- ----- Method ----- ----- -----
		public Page()
		{
			InitializeComponent();
		}

		protected internal virtual void OnGoBack(EventArgs e)
		{

		}

		protected internal virtual void OnPageOpen(EventArgs e)
		{

		}
	}
}
