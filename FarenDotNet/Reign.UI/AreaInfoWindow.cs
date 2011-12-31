using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics.Contracts;

namespace FarenDotNet.Reign.UI
{
	public partial class AreaInfoWindow : Form
	{
		Area _area;
		HashSet<Unit> _selectedUnit = new HashSet<Unit>();
		ReignManager _rManager;

		public AreaInfoWindow()
		{
			InitializeComponent();
		}

		public void SetReignManager(ReignManager rm) { _rManager = rm; }

		public void SetArea(Area area, bool detail)
		{
			_area = area; // イベント用
			area.Units.Sort();
			if (area.Province.IsNeutral)
				this.Text = area.Name;
			else
				this.Text = String.Format("{0}（{1}領）", area.Name, area.Province.Name);
			this.ResetUnit();
			this.SuspendLayout();
			{
				// サイズの記憶
				var flowSize = flp_Units.Size;
				// 詳細ウィンドウを表示するかどうか
				if (detail)
				{
					this.SetDetail(area);
					this.detail.Visible = true;
					this.ClientSize = new Size(flowSize.Width + this.detail.Width, flowSize.Height);
				}
				else
				{
					this.detail.Visible = false;
					this.ClientSize = flowSize;
				}
			}
			this.ResumeLayout();
			this.Invalidate();
		}

		public IEnumerable<Unit> SelectedUnit
		{
			get
			{
				return flp_Units.Controls
					.OfType<UnitImageControl>()
					.Where(u => u.Selected)
					.Select(uCtrl => uCtrl.Unit);
			}
		}

		public void ResetUnit ()
		{
			this.flp_Units.Controls.Clear();
			_selectedUnit.Clear();
			foreach (var u in _area.Units)
			{
				var uCtrl = new UnitImageControl { Unit = u, CanSelect = true };
				if (u.Acted)
					uCtrl.Text = "Finish";
				uCtrl.Click += this.UnitClick;
				this.flp_Units.Controls.Add(uCtrl);
			}
		}

		private void AreaInfoWindow_FormClosing(object sender, FormClosingEventArgs e)
		{
			Contract.Requires(this.Owner != null);
			if (this.Visible && e.CloseReason == CloseReason.UserClosing)
			{
				e.Cancel = true;
				this.Visible = false;
				_area = null;
			}
		}

		private void SetDetail(Area area)
		{
			var city = new { num = area.NumCity, max = area.MaxCity };
			var wall = new { num = area.NumWall, max = area.MaxWall };
			var road = new { num = area.NumRoad, max = area.MaxRoad };

			int numSp = area.Province.IsNeutral ? 0
				: area.Units.Count(u => u.Species == area.Province.Master.Species);

			lbl_vIncome.Text = area.Income.ToString();
			lbl_vAccess.Text = road.num == road.max ? "○" : "×";
			lbl_vCity.Text = String.Format("{0,2}/{1,3}", city.num, city.max);
			lbl_vWall.Text = String.Format("{0,2}/{1,3}", wall.num, wall.max);
			lbl_vRoad.Text = String.Format("{0,2}/{1,3}", road.num, road.max);
			lbl_Species.Text = String.Format("同種族　{0,2}人", numSp);

			cbx_Action.SelectedIndex = (int)area.WaitAction;
		}

		private void cbx_Action_SelectedIndexChanged(object sender, EventArgs e)
		{
			_area.WaitAction = (Area.WaitUnitAction)cbx_Action.SelectedIndex;
		}

		// ユニットをクリックしたとき
		private void UnitClick (object sender, EventArgs e)
		{
			var uCtrl = sender as UnitImageControl;
			if (uCtrl == null)
				return;
			uCtrl.Selected = !uCtrl.Selected;
			this.Invalidate();
		}

		/// <summary>
		/// 移動ボタンをクリックしたときの処理
		/// </summary>
		private void btn_Move_Click (object sender, EventArgs e)
		{
			if (_area.Adjacent
				.Where(a => a.Province == _area.Province)
				.Count() == 0)
			{
				MessageBox.Show("移動できるエリアがありません");
				return;
			}
			// 移動先の決定
			Area selectArea = null;
			using (var cand = new MoveAreaCandidateWindow())
			{
				cand.SetArea(_area);
				cand.ShowDialog();
				var dResult = cand.DialogResult;
				if (dResult != DialogResult.OK)
					return;
				selectArea = cand.Result;
			}
			// 移動の開始
			using (var move = new MoveUnitWindow())
			{
				move.SetAreas(_area, selectArea);
				move.ShowDialog();
			}
			this.ResetUnit();
		}

		/// <summary>
		/// 雇用ボタンを押したとき
		/// </summary>
		private void btn_Employ_Click (object sender, EventArgs e)
		{
			if (_area.Units.Count == 20)
			{
				MessageBox.Show("ユニットを雇う余裕がありません");
				return;
			}
			Debug.Assert(_area.Units.Count < 20);
			var employer = this.SelectedUnit
				.Where(u => !u.Acted)
				.ToList();

			if (employer.Count == 0)
				return;
			using (var employ = new EmployWindow())
			{
				employ.SetUnit(employer, _area, _rManager);
				employ.ShowDialog();
				if (employ.DialogResult == DialogResult.OK)
					this.ResetUnit();
			}
		}

		/// <summary>
		/// 解雇ボタンを押したとき
		/// </summary>
		private void btn_Chop_Click (object sender, EventArgs e)
		{
			var uCtrls = flp_Units.Controls
				.OfType<UnitImageControl>()
				.Where(c => c.Selected);

			int count = 0;
			foreach (var uCtrl in uCtrls)
			{
				var unit = uCtrl.Unit;
				if (unit.IsMaster)
				{
					MessageBox.Show("マスターは解雇できません", "警告",
						MessageBoxButtons.OK,
						MessageBoxIcon.Warning);
					continue;
				}

				string msg = String.Format("{0}を解雇しますがよろしいですか？", unit.Name);
				var result = MessageBox.Show(msg, "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
				if (result == DialogResult.Yes)
				{
					_area.Units.Remove(uCtrl.Unit);
					count++;
				}
			}
			if (count > 0)
				this.ResetUnit();
		}
	}
}
