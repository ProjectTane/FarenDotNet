using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FarenDotNet.BasicData;

namespace FarenDotNet.War.UI
{
	public partial class ElementSelectWindow : DockingWindow, IWindowForShowWindowCommand
	{
		private readonly Button[] _buttonTable;

		private readonly AttackType[] _magicAttributes =
			{
				AttackType.火, AttackType.水, AttackType.風,
				AttackType.土, AttackType.光, AttackType.闇,
			};

		private WarUnit _doer;
		private Action _finished;
		private MagicSelectWindow _magicSelectWindow;
		private WarPresentationModel _model;

		public ElementSelectWindow(IList<IList<WarMagicData>>[,] magicCommands)
		{
			InitializeComponent();

			_buttonTable = new[] {
				btn_Fire,
				btn_Water,
				btn_Wind,
				btn_Earth,
				btn_Light,
				btn_Dark
			};
			for (int i = 0; i < _buttonTable.Length; i++)
			{
				int index = i;
				_buttonTable[i].Click += (sender_, e_) => {
					if (_magicSelectWindow == null)
						_magicSelectWindow = new MagicSelectWindow(magicCommands);
					Visible = false;
					_model.CancelCommandStack.Push(delegate {
						Visible = true;
						_magicSelectWindow.Visible = false;
						return true; // キャンセル処理の完了
					});
					_magicSelectWindow.Show(_model, _doer, _finished, index);
				};
			}
		}

		#region IWindowForShowWindowCommand Members

		public void Show(WarPresentationModel model, WarUnit doer, Action finished)
		{
			// ボタンクリックイベントのためにフィールドを設定する
			_model = model;
			_doer = doer;
			_finished = finished;

			for (int i = 0; i < _buttonTable.Length; i++)
			{
				_buttonTable[i].Enabled = doer.MagicLevels[_magicAttributes[i]] > 0;
			}

			// キャンセル処理の登録
			model.CancelCommandStack.Push(delegate {
				Visible = false;
				return false; // 次のキャンセル処理へ続く
			});

			// ウィンドウの表示
			Visible = true;
		}

		#endregion

		private void ElementTableWindow_Load(object sender, EventArgs e)
		{
		}

		private void ElementTableWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (Visible)
			{
				e.Cancel = true;
				_model.InvokeCancelEvent();
			}
		}

		private void ElementSelectWindow_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Close();
		}
	}
}