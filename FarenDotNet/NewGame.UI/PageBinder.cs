using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics.Contracts;

namespace FarenDotNet.NewGame.UI
{
	public partial class PageBinder : UserControl
	{
		// ----- ----- ----- Field ----- ----- -----
		private Page[] _pages;
		private int _index;

		// ----- ----- ----- Property ----- ----- -----
		public Page[] Pages
		{
			get { return _pages; }
			set
			{
				if (value == null | _pages != null)
					return;
				_pages = value;
				this.MovePage();
			}
		}

		// ----- ----- ----- Event ----- ----- -----
		/// <summary>キャンセルボタンを押したときの処理</summary>
		[Category("Action")]
		[Description("キャンセルボタンがクリックされたときに発生します。")]
		public event EventHandler CancelClick
		{
			add { this.btn_Cancel.Click += value; }
			remove { this.btn_Cancel.Click -= value; }
		}

		/// <summary>完了ボタンを押したときの処理</summary>
		[Category("Action")]
		[Description("完了ボタンがクリックされたときに発生します。")]
		public event EventHandler FinishClick;

		// ----- ----- ----- Method ----- ----- -----
		public PageBinder()
		{
			InitializeComponent();
		}

		private void UpdateButtons()
		{
			// 戻るボタン
			this.btn_Back.Enabled = _index > 0;
			// 次へボタン
			this.btn_Next.Enabled = _pages[_index].CanNext;
			this.btn_Next.Text = _pages.Length - _index == 1 ?
				"完了(&F)" : "次へ(&N)";
		}

		/// <summary>
		/// 現在のページを変更する。
		/// 変更先のページ番号は_indexに入れておくこと。
		/// </summary>
		private void MovePage()
		{
			var page = _pages[_index];
			page.Dock = DockStyle.Fill;
			page.Binder = this;

			this.SuspendLayout();
			{
				this.panel.Controls.Clear();
				this.panel.Controls.Add(page);
				this.UpdateButtons();
			}
			this.ResumeLayout();
		}

		/// <summary>
		/// 次へいく条件が変化したら呼ばれる。
		/// </summary>
		public void UpdateNext()
		{
			UpdateButtons();
		}

		/// <summary>
		/// 戻るボタンクリック
		/// </summary>
		private void btn_Back_Click(object sender, EventArgs e)
		{
			Contract.Requires(0 < _index && _index < _pages.Length, "Indexが範囲を超えています");
			_pages[_index].OnGoBack(EventArgs.Empty);
			_index--;
			this.MovePage();
		}

		/// <summary>
		/// 進むボタンクリック
		/// </summary>
		private void btn_Next_Click(object sender, EventArgs e)
		{
			Contract.Requires(0 <= _index && _index < _pages.Length, "Indexが範囲を超えています");
			if (_pages.Length - _index == 1) {
				// 最終ページ
				var fEvent = this.FinishClick;
				if (fEvent != null)
					fEvent(sender, e);
			}
			else {
				_index++;
				this.MovePage();
				_pages[_index].OnPageOpen(EventArgs.Empty);
			}
		}
	}
}
