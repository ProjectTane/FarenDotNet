using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Paraiba.Drawing;
using Paraiba.Geometry;
using WeifenLuo.WinFormsUI.Docking;

namespace FarenDotNet.War.UI
{
	public partial class DeploymentWindow : DockContent
	{
		//以降少しずつ修正するので、消すなよ
		private readonly List<List<WarUnit>> _deployingUnitsList;
		private readonly Action _finishDelegate;
		private readonly WarPresentationModel _model;
		private readonly WarMap _warMap;
		private bool[] _allocated;
		private List<WarUnit> _deployingUnits;
		private int _index;
		private int _selectedIndex;

		public DeploymentWindow(List<List<WarUnit>> deployingUnitsList, WarPresentationModel model, WarMap warMap, Action finishDelegate)
		{
			InitializeComponent();
			_deployingUnitsList = deployingUnitsList;
			_index = 0;
			_model = model;
			_warMap = warMap;
			_finishDelegate = finishDelegate;
			_model.SelectMapChipEvent += Deploy;
			_model.CancelMapChipEvent += Undeploy;

			initializeDeployment();
		}

		private void initializeDeployment()
		{
			_deployingUnits = _deployingUnitsList[_index];
			_allocated = new bool[_deployingUnits.Count];
			_selectedIndex = 0;
		}

		private void Undeploy(Point2 focusPoint)
		{
			var unit = _warMap[focusPoint].Unit;

			// 配置済みのユニットがあれば戻す
			int index;
			if (unit != null && (index = _deployingUnits.IndexOf(unit)) >= 0)
			{
				_warMap.UnDeploy(unit, focusPoint);
				_allocated[index] = false;

				Invalidate();
			}
		}

		private void Deploy(Point2 focusPoint)
		{
			//注目座標が初期配置候補の中にない場合はreturn
			if (!_warMap.InitDeployCandidate.Contains(focusPoint))
				return;

			Debug.Assert(_allocated[_selectedIndex] == false, "配置済みのユニットを選択しています。");

			var chip = _warMap[focusPoint];

			// 配置済みのユニットがあれば戻す
			int index;
			if (chip.Unit != null && (index = _deployingUnits.IndexOf(chip.Unit)) >= 0)
			{
				//TODO: リファクタリングでメソッド化
				_warMap.UnDeploy(chip.Unit, focusPoint);
				_allocated[index] = false;
			}

			//選択されているユニットを配置
			_warMap.Deploy(_deployingUnits[_selectedIndex], focusPoint);
			_allocated[_selectedIndex] = true;

			// ウィンドウの更新
			Invalidate();

			// 未配置キャラを探索する
			index = Array.IndexOf(_allocated, false);

			if (index < 0) //今考えている配置可能ユニット達が全て配置されている
			{
				if (_index + 1 >= _deployingUnitsList.Count) //全ての攻め込み方向で配置を完了した
				{
					// 全てのキャラが配置されている
					DialogResult result = MessageBox.Show("これでいいですか？", "キャラ配置", MessageBoxButtons.YesNo);
					if (result == DialogResult.Yes)
					{
						// 初期配置可能候補をnullにすることで、初期配置が描画されなくなる
						_warMap.InitDeployCandidate = null;

						// 初期配置でのマウスイベントを削除
						_model.SelectMapChipEvent -= Deploy;
						_model.CancelMapChipEvent -= Undeploy;
						Close();

						//初期配置がおわり、戦闘を開始する。
						_finishDelegate();
					}
					else
					{
						//最後に配置したキャラを戻す
						_warMap.UnDeploy(chip.Unit, focusPoint);
						_allocated[_selectedIndex] = false;

						// ウィンドウの更新
						Invalidate();
					}
				}
				else
				{
					initializeDeployment();
					_index++;
					_deployingUnits = _deployingUnitsList[_index];
					_warMap.InitDeployCandidate = _warMap.InitDeployCandidateList[_index];
					Invalidate();
				}
			}
			else
			{
				// 未配置キャラを選択する
				_selectedIndex = index;
			}
		}

		private void InitCharaArrange_Load(object sender, EventArgs e)
		{
			// フロート（非ドッキング）ウィンドウのサイズを設定
			FloatPane.FloatWindow.Size = new Size(100, 500);
			//DoubleBufferはプロパティで設定済み
		}

		private void DeploymentWindow_Paint(object sender, PaintEventArgs e)
		{
			var g = e.Graphics;
			var units = _deployingUnits;
			int chipSize = units[0].ChipImage.Height;
			for (int i = 0; i < units.Count; i++)
			{
				if (!_allocated[i])
					// 描画イメージの大きさを指定することでスケーリング回避 
					g.DrawSurface(units[i].ChipImage, 0, i * chipSize);
			}
			g.DrawRectangle(Pens.Red, 0, _selectedIndex * chipSize, chipSize, chipSize);
		}

		private void DeploymentWindow_Resize(object sender, EventArgs e)
		{
			TabText = Size + ClientSize.ToString();
		}

		private void DeploymentWindow_MouseDown(object sender, MouseEventArgs e)
		{
			//TODO: フォーカス問題
			int index = e.Location.Y / 32;
			if (e.X / 32 == 0 && index < _allocated.Length)
				_selectedIndex = index;
			Invalidate();
		}

		private void DeploymentWindow_MouseMove(object sender, MouseEventArgs e)
		{
			if (e.X / 32 == 0 && e.Y / 32 < _allocated.Length && !_allocated[e.Y / 32])
				MiniUnitInfoWindow.ShowWindow(this, _deployingUnits[e.Y / 32]);
		}
	}
}