using System;
using System.Drawing;
using System.Windows.Forms;
using Paraiba.Drawing;
using Paraiba.Geometry;

namespace FarenDotNet.War.UI
{
	public partial class MapChipInfoWindow : DockingWindow
	{
		private readonly WarPresentationModel _model;

		public MapChipInfoWindow(WarPresentationModel model)
		{
			InitializeComponent();
			_model = model;
			model.CursorChipPointChangedEvent += (p => Invalidate(true));
		}

		private WarUnit ActiveUnit
		{
			get { return _model.ActiveUnit; }
		}

		private Point2 CursorChipPoint
		{
			get { return _model.CursorChipPoint; }
		}

		private WarMap WarMap
		{
			get { return _model.WarMap; }
		}

		private void MapChipInfoWindow_Load(object sender, EventArgs e)
		{
		}

		private void MapChipInfoWindow_Paint(object sender, PaintEventArgs e)
		{
			var g = e.Graphics;
			var land = WarMap[CursorChipPoint];

			// 地形描画
			g.DrawSurface(land.Image, 0, 0);
			if (land.Construct != null)
				g.DrawSurface(land.Construct.Image, 0, 0);

			// 地形名表示
			var font = new Font("ＭＳゴシック", 7);
			var info = land.Info;
			g.DrawString(info.Name, font, Brushes.Black, 32, 0);

			if (ActiveUnit != null)
			{
				g.DrawString("必要移動量: " + info.RequiredMobility[ActiveUnit.MoveType], font, Brushes.Black, 32, 9);
				g.DrawString("技術修正量: " + info.Revision[ActiveUnit.MoveType], font, Brushes.Black, 32, 20);
			}

			#region Debug

			int i = 0;
			g.DrawString("現在の座標: " + CursorChipPoint, font, Brushes.Black, 0, 40 + 10 * i++);
			foreach (WarSide troop in _model.Troops)
			{
				g.DrawString("Force: " + troop.Force, font, Brushes.Black, 0, 40 + 10 * i++);
			}

			#endregion
		}
	}
}