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
	public partial class MoveUnitWindow : Form
	{
		const string MOVED = "Moved";
		const string ACTED = "Finish";

		Area _left, _right;

		public MoveUnitWindow ()
		{
			InitializeComponent();
		}

		public void SetAreas (Area left, Area right)
		{
			_left = left; _right = right;

			// ユニット配置
			foreach (var u in left.Units)
			{
				var ctrl = new UnitImageControl { Unit = u };
				if (u.Acted)
					ctrl.Text = ACTED;
				else
					ctrl.Click += this.MoveLtR;
				flp_Left.Controls.Add(ctrl);
			}
			foreach (var u in right.Units)
			{
				var ctrl = new UnitImageControl { Unit = u };
				if (u.Acted)
					ctrl.Text = ACTED;
				else
					ctrl.Click += this.MoveRtL;
				flp_Right.Controls.Add(ctrl);
			}
		}

		private void UnitMove (UnitImageControl uCtrl, bool l2r)
		{
			if (uCtrl.Text == ACTED) return;
			var clicks = new EventHandler[] { this.MoveLtR, this.MoveRtL };
			var flows = new[] { flp_Left.Controls, flp_Right.Controls };
			int src = l2r ? 0 : 1;
			int dst = src ^ 1;

			if (flows[dst].Count == 20) return;
			Debug.Assert(flows[dst].Count < 20);

			uCtrl.Click -= clicks[src];
			uCtrl.Click += clicks[dst];
			flows[src].Remove(uCtrl);
			flows[dst].Add(uCtrl);

			if (uCtrl.Text == MOVED)
				uCtrl.Text = "";
			else
				uCtrl.Text = MOVED;
		}

		#region EVENT

		/// <summary>
		/// ユニットを左から右に移動
		/// </summary>
		private void MoveLtR (object sender, EventArgs e)
		{
			var uCtrl = sender as UnitImageControl;
			if (uCtrl == null) return;
			UnitMove(uCtrl, true);
		}

		/// <summary>
		/// ユニットを右から左に移動
		/// </summary>
		private void MoveRtL (object sender, EventArgs e)
		{
			var uCtrl = sender as UnitImageControl;
			if (uCtrl == null) return;
			UnitMove(uCtrl, false);
		}

		/// <summary>
		/// 全てのユニットを左から右へ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btn_LtR_Click (object sender, EventArgs e)
		{
			var list = flp_Left.Controls.OfType<UnitImageControl>().ToList();
			foreach (var uCtrl in list)
				UnitMove(uCtrl, true);
		}

		/// <summary>
		/// 全てのユニットを右から左へ
		/// </summary>
		private void btn_RtL_Click (object sender, EventArgs e)
		{
			var list = flp_Right.Controls.OfType<UnitImageControl>().ToList();
			foreach (var uCtrl in list)
				UnitMove(uCtrl, false);
		}

		private void btn_Cancel_Click (object sender, EventArgs e)
		{
			this.Close();
		}

		#endregion

		private void btn_Finish_Click (object sender, EventArgs e)
		{
			if (_left == null || _right == null)
				return;
			this.DialogResult = DialogResult.OK;

			_left.Units.Clear();
			foreach (UnitImageControl uCtrl in flp_Left.Controls)
			{
				var unit = uCtrl.Unit;
				if (uCtrl.Text == MOVED)
					unit.Acted = true;
				_left.Units.Add(unit);
			}
			_right.Units.Clear();
			foreach (UnitImageControl uCtrl in flp_Right.Controls)
			{
				var unit = uCtrl.Unit;
				if (uCtrl.Text == MOVED)
					unit.Acted = true;
				_right.Units.Add(unit);
			}
		}
	}
}
