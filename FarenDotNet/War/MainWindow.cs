#if !DOCUMENT
using System;
using System.Windows.Forms;
using Paraiba.Geometry;
using FarenDotNet.War.UI;
using WeifenLuo.WinFormsUI.Docking;
#endif

namespace FarenDotNet.War
{
	public partial class MainWindow : Form
	{
#if !DOCUMENT
		protected DockContent mainDock;
#endif

		public MainWindow()
		{
			InitializeComponent();
		}

		public void ShowMapWindow(Situation situation, WarPresentationModel model)
		{
			// �Ƃ肠�����S�ẴE�B���h�E��\������
			// MapWindow �C���X�^���X�̐���
			var mapWindow = new MapWindow(model);
			mainDock = mapWindow;
			mainDock.AllowEndUserDocking = false;
			mainDock.Show(dockPanel, DockState.Document);

			var commandWindow = new CommandWindow(model);
			commandWindow.Show(mapWindow.Pane, DockAlignment.Top, 0.12);

			var mapChipInfoWindow = new MapChipInfoWindow(model);
			mapChipInfoWindow.Show(commandWindow.Pane, DockAlignment.Right, 0.5);

			// UnitMiniInfoWindow�\���p
			Action<Point2> showUnitMiniInfoWindow = delegate(Point2 p) {
				var unit = model.WarMap[p].Unit;
				if (unit != null)
				{
					MiniUnitInfoWindow.ShowWindow(this, unit);
				}
				else
				{
					MiniUnitInfoWindow.HideWindow();
				}
			};

			// MapChip�N���b�N�ɂ��UnitInfoWindow�̕\���̃f���Q�[�g��ǉ�
			model.SelectMapChipEvent += p => {
				var unit = model.WarMap[p].Unit;
				if (unit != null && model.Scope == null)
					UnitInfoWindow.ShowWindow(this, unit);
				else
					UnitInfoWindow.HideWindow();
			};

			model.CursorChipPointChangedEvent += showUnitMiniInfoWindow;
			model.CursorEnterEvent += showUnitMiniInfoWindow;
			model.CursorLeaveEvent += delegate {
				if (MiniUnitInfoWindow.IsInitialized && !MiniUnitInfoWindow.IsOnCursor)
					MiniUnitInfoWindow.HideWindow();
			};
		}

		private void MainWindow_Load(object sender, EventArgs e)
		{
		}

		public void SetTurn(int turn)
		{
			labelTurn.Text = turn + "�^�[����";
			labelTurn.Invalidate();
		}
	}
}