using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using FarenDotNet.BasicData;
using FarenDotNet.Loader;
using FarenDotNet.War.BattleAction;
using FarenDotNet.War.Loader;

namespace FarenDotNet.War.UI
{
	public partial class MagicSelectWindow : DockingWindow
	{
		private static readonly AttackType[] magicAttributes =
			{
				AttackType.火, AttackType.水, AttackType.風,
				AttackType.土, AttackType.光, AttackType.闇,
			};

		// IList<IList<WarMagicData>>:	名前が異なる魔法（リスト）のリスト
		// IList<WarMagicData>:		同名でレベル違いの魔法リスト
		// _magicCommands[属性, 魔法レベル]
		private readonly IList<IList<WarMagicData>>[,] _magicCommands;
		private WarPresentationModel _model;
		private WarMagicData _pointingMagic;

		public MagicSelectWindow(IList<IList<WarMagicData>>[,] magicCommands)
		{
			_magicCommands = magicCommands;

			InitializeComponent();
		}

		private WarMagicData PointingMagic
		{
			get { return _pointingMagic; }
			set
			{
				if (_pointingMagic != value)
				{
					_pointingMagic = value;
					SetDetail(value);
				}
			}
		}

		private void SetDetail(WarMagicData command)
		{
			if (command != null)
			{
				labelLine1.Text = String.Format("{0} 消費MP：{1}", command.Name, command.ExpandMP);
				labelLine2.Text = command.Description;
				labelLine3.Text = String.Format("射程：{0} / 範囲：{1}", command.Range, command.Area);
			}
			else
			{
				labelLine1.Text = "";
				labelLine2.Text = "";
				labelLine3.Text = "";
			}
		}

		/// <summary>
		/// このウインドウを表示する
		/// </summary>
		/// <param name="model"></param>
		/// <param name="doer"></param>
		/// <param name="finished"></param>
		/// <param name="index">魔法の属性を示すインデックス値</param>
		public void Show(WarPresentationModel model, WarUnit doer, Action finished, int index)
		{
			Debug.Assert(0 <= index && index <= 5, "予期せぬindex値を受け取りました: " + index);

			_model = model;
			var situation = model.Situation;

			// ボタンの全消去
			flowButtonPanel.Controls.Clear();

			// ボタンの追加
			var magicDatasList = _magicCommands[index, doer.MagicLevels[magicAttributes[index]]];
			for (int j = 0; j < magicDatasList.Count; j++)
			{
				var magicDatas = magicDatasList[j];
				for (int i = 0; i < magicDatas.Count; i++)
				{
					var magicData = magicDatas[i];
					var arc = new ActionArguments(situation, model);

					var button = new Button {
						Enabled = magicData.Action.CanBoot(arc, doer),
						Text = magicData.Name + (i + 1),
					};
					button.Click += (sender_, e_) => {
						Visible = false;
						model.CancelCommandStack.Push(delegate {
							Visible = true;
							return true; // キャンセル処理の完了
						});
						magicData.Action.Boot(arc, doer, finished);
					};
					button.MouseEnter += delegate { PointingMagic = magicData; };
					button.MouseLeave += delegate { PointingMagic = null; };
					flowButtonPanel.Controls.Add(button);
				}
			}

			SetDetail(null);
			Text = doer.Name;
			Visible = true;
		}

		private void MagicSelectWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			e.Cancel = true;
			_model.InvokeCancelEvent();
		}

		private void MagicSelectWindow_Load(object sender, EventArgs e)
		{
		}

		private void MagicSelectWindow_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Close();
		}

		private void flowButtonPanel_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
				Close();
		}
	}
}