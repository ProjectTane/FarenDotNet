using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Paraiba.Core;
using FarenDotNet.War.BattleAction;
using FarenDotNet.War.BattleCommand;
using Paraiba.Linq;

namespace FarenDotNet.War.UI
{
	public partial class CommandWindow : DockingWindow
	{
		private readonly IDictionary<IBattleCommand, Button> buttons;
		private readonly WarPresentationModel model;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		/// <param name="model">ウインドウを仲介するクラス</param>
		public CommandWindow(WarPresentationModel model)
		{
			this.model = model;
			InitializeComponent();
			// 全てのボタンを作らないようにキャッシュを利用
			buttons = new Dictionary<IBattleCommand, Button>();

			Situation.ActiveUnitChanged += unit => {
				if (unit.Side.IsPlayer)
				{
					Text = unit.Name;
					TabText = unit.Name;
					ResetCommandButtons();
					Enabled = true;
				}
				else
				{
					Enabled = false;
				}
			};
		}

		private WarUnit ActiveUnit
		{
			get { return model.Situation.ActiveUnit; }
		}

		private Situation Situation
		{
			get { return model.Situation; }
		}

		private void CommandWindow_Load(object sender, EventArgs e)
		{
		}

		private Button GetButton(IBattleCommand cmd)
		{
			Button btn;
			if (!buttons.TryGetValue(cmd, out btn))
			{
				// クリックしたときに実行されるコマンド
				// 作成されるボタン
				btn = new Button {
					Margin = new Padding(0),
					Size = new Size(48, 48),
					Image = cmd.Image,
					Text = cmd.Image == null ? cmd.Name : "",
				};
				buttons[cmd] = btn;
				// マウスオーバーで表示される文字
				toolTip.SetToolTip(btn, cmd.Description);
				// クリック時のイベントを設定
				btn.Click += delegate {
					flowLayoutPanel.Enabled = false;
					var doer = Situation.ActiveUnit;

					// キャンセル時の処理はGUIの実装に依存するためここに記述
					Action finished = () => {
						doer.ChangeCommandState(cmd);
						ResetCommandButtons();
						flowLayoutPanel.Enabled = true;
					};
					model.CancelCommandStack.Push(() => {
						ResetCommandButtons();
						flowLayoutPanel.Enabled = true;
						return true; // キャンセル処理の完了
					});
					var arc = new ActionArguments(Situation, model);
					cmd.Execute(arc, doer, finished);
				};
			}
			return btn;
		}

		/// <summary>
		/// コマンドウィンドウにボタンを再配置する
		/// </summary>
		public void ResetCommandButtons()
		{
			var control = flowLayoutPanel.Controls;
			// 一旦ボタンを全て空にして
			control.Clear();
			// 使用可能なものを追加する
			ActiveUnit.Commands.ForEach(cmdState => {
				var btn = GetButton(cmdState.Command);
				btn.Enabled = cmdState.Enable;
				control.Add(btn);
			});
		}
	}
}