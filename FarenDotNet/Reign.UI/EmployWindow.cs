using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FarenDotNet.BasicData;
using System.Diagnostics.Contracts;

namespace FarenDotNet.Reign.UI
{
	public partial class EmployWindow : Form
	{
		IEnumerator<Unit> _unitList;
		List<UnitData> _employable;
		IList<UnitData> _areaSummon;
		Area _area;
		ReignManager _rManager;

		// ----- ----- ----- ----- ----- PUBLIC ----- ----- ----- ----- -----
		public EmployWindow()
		{
			InitializeComponent();
		}

		public void SetUnit(IEnumerable<Unit> unit, Area area, ReignManager rManager)
		{
			_unitList = unit.GetEnumerator();
			_area = area;
			_rManager = rManager;

			SetCount();
			SetUnit();
			this.DialogResult = DialogResult.Cancel;
		}

		// ----- ----- ----- ----- ----- PROTECTED ----- ----- ----- ----- -----
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing)
				{
					if (components != null)
						components.Dispose();
					if (_unitList != null)
						_unitList.Dispose();
				}
			} finally
			{
				base.Dispose(disposing);
			}
		}

		// ----- ----- ----- ----- ----- PRIVATE ----- ----- ----- ----- -----
		/// <summary>
		/// ラベルに雇用可能数を張る
		/// </summary>
		private void SetCount()
		{
			int last = 20 - _area.Units.Count;
			lbl_Num.Text = "雇用可能部隊数：" + last;
		}

		/// <summary>
		/// ListBoxにユニットを突っ込む
		/// </summary>
		private void SetUnit()
		{
			if (_unitList.MoveNext() == false)
			{
				this.Close(); // 最低一つはあるものを呼んでいるため、1回目は発生しないはず。
				return;
			}
			var unit = _unitList.Current;

			// 雇用可能ユニットの設定
			_employable = new List<UnitData>();
			foreach (var no in unit.Summon)
			{
				if (no == 0) continue;
				var u = _rManager.UnitsData[no - 1];
				_employable.Add(u);
			}
			// エリア雇用可能の設定
			if (_areaSummon == null)
			{
				var callable = _rManager.Callable.Single(c => c.No == _area.AreaType).Call;
				_areaSummon = new List<UnitData>(callable.Count);
				foreach (var id in callable)
				{
					var uData = _rManager.UnitsData.Single(u => u.ID == id);
					_areaSummon.Add(uData);
				}
			}
			_listBox.Items.Clear();
			foreach (var u in _employable)
				_listBox.Items.Add(GetText(u));
			foreach (var u in _areaSummon)
				_listBox.Items.Add(GetText(u));
		}

		/// <summary>
		/// ListBoxに張るテキストを得る関数
		/// </summary>
		private string GetText(UnitData u)
		{
			return
				(u.Species == _area.Province.Master.Species ? "（同）" : "")
				+ u.Name
				+ " （" + u.Cost + "Ley）";
		}

		/// <summary>
		/// 雇用ボタンを押したときの処理
		/// </summary>
		private void btn_Employ_Click(object sender, EventArgs e)
		{
			int index = _listBox.SelectedIndex;
			if (index == -1) return;

			var unit = index < _employable.Count ?
				new Unit(_employable[index]) :
				new Unit(_areaSummon[index - _employable.Count]);

			if (unit.Cost > _area.Province.Money)
			{
				MessageBox.Show("お金が足りていません");
				return;
			}
			_area.Province.Money -= unit.Cost;

			_unitList.Current.Acted = true;
			unit.Acted = true;
			_area.Units.Add(unit);
			this.DialogResult = DialogResult.OK;

			if (_area.Units.Count == 20)
			{
				this.Close();
			}
			else
			{
				SetCount();
				SetUnit();
			}
		}

		/// <summary>
		/// キャンセルボタンを押したときの処理
		/// </summary>
		private void btn_Cancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
