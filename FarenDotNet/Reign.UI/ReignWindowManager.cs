using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics.Contracts;
using Paraiba.Core;

namespace FarenDotNet.Reign.UI
{
	public class ReignWindowManager : IDisposable
	{
		// ----- ----- ----- Field ----- ----- -----
		ReignManager _reinManager;
		Action _finishEvent;

		IEnumerator<Phase> _coroutine;
		IEnumerator<int> _script;

		// コンポーネント
		public readonly ReignWindow Main;
		Form _owner;
		MasterWindow _masterWindow;
		AreaInfoWindow _areaInfoWindow;

		// ----- ----- ----- Property ----- ----- -----
		public MasterWindow MasterWindow { get { return _masterWindow; } }
		public AreaInfoWindow AreaInfoWindow { get { return _areaInfoWindow; } }


		// ----- ----- ----- Method ----- ----- -----
		public ReignWindowManager(ReignManager manager, Form owner)
		{
			_owner = owner;
			_reinManager = manager;
			Main = this.CreateReignWindow();
			var result = MessageBox.Show("ゲームを開始します", "確認", MessageBoxButtons.OK);
			StartGame();
		}

		#region 進行用メソッド群

		private void StartGame ()
		{
			Contract.Requires(_coroutine == null);
			_coroutine = _reinManager.Start().GetEnumerator();
			NextStep();
		}

		private void NextStep()
		{
			Contract.Requires(_coroutine != null);
			this.Main.AreaClick -= this.ShowAreaInfoWindowEvent;
			if (!_coroutine.MoveNext())
				return;
			this.DieCheck();
			switch (_coroutine.Current)
			{
			case Phase.TurnStart:
				StartTurn();
				break;
			case Phase.PlayerTurn:
				MasterTurn();
				break;
			case Phase.Win:
				string winner = _reinManager.LiveProvs.Select(p => p.Name).JoinString("と");
				MessageBox.Show(winner + "が大陸を制覇しました。");
				goto case Phase.GameOver;
			case Phase.GameOver:
				new GameOver { Owner = Main }.Show();
				break;
			}
		}

		private void DieCheck()
		{
			var died = _reinManager.ProvinceDieCheck();
			foreach (var prov in died)
				if (prov.Master != null)
					MessageBox.Show(String.Format(
						"{0}が死亡しました。\r\n" +
						"{0}の領土は中立地帯になります",
						prov.Master.Name));
			died.Clear();
		}

		private void StartTurn ()
		{
			Main.StartTurn(_reinManager.Turn);
			Main.SetName("イベント");
			_finishEvent = NextStep;
			this.StartEvent();
		}

		private void MasterTurn ()
		{
			_finishEvent = delegate {
				var areas = _reinManager.WorldMap.Areas;
				var prov = _reinManager.ActingProv;
				Main.SetName(prov.Name);

				if (prov.IsPlayer)
				{
					// マスターのいるエリア
					var masterArea = areas.Single(
						a => a.Province == prov && a.Units.Any(u => u.ID == prov.Master.ID));
					ShowAreaInfoWindow(masterArea, true);
					ShowMasterWindow(prov, areas);
					Main.AreaFocus(masterArea);
					this.Main.AreaClick += this.ShowAreaInfoWindowEvent;
				}
				else
					NextStep();
			};
			this.StartEvent();
		}

		#endregion

		#region イベント進行用

		private void StartEvent()
		{
			var scripter = new Scripter(Main, _reinManager, this);
			_script = _reinManager.GetScript(scripter).GetEnumerator();
			this.ContinueEvent();
		}

		public void ContinueEvent()
		{
			if (_script.MoveNext())
				Main.SetEvent(this.ContinueEvent, _script.Current);
			else
				FinishEvent();
		}

		private void FinishEvent()
		{
			_script.Dispose();
			_script = null;
			var next = _finishEvent;
			_finishEvent = null;
			this.DieCheck();
			next();
		}

		#endregion

		#region ウィンドウ

		private ReignWindow CreateReignWindow()
		{
			var w = new ReignWindow(this._reinManager);
			w.WindowState = FormWindowState.Maximized;
			w.Show(_owner);
			return w;
		}

		public void ShowAreaInfoWindowEvent (object sender, WorldMapPanel.AreaClickEventArgs e)
		{
			this.ShowAreaInfoWindow(e.Area, e.Area.Province == _reinManager.ActingProv);
		}

		private void ShowMasterWindow(Province prov, IEnumerable<Area> areas)
		{
			Func<MasterWindow> create = delegate
			{
				var w = new MasterWindow {
					Owner = Main,
					ReignManager = _reinManager,
					RWManager = this
				};
				w.ClosingCallback += (sender, e) => {
					try {
						if (_areaInfoWindow != null)
							_areaInfoWindow.Visible = false;
					} catch (ObjectDisposedException) { };
					this.NextStep();
				};
				return w;
			};
			if (_masterWindow == null)
				_masterWindow = create();
			try
			{
				_masterWindow.Show();
			}catch (ObjectDisposedException)
			{
				_masterWindow = create();
				_masterWindow.Show();
			}
			_masterWindow.SetData(prov, areas);
		}

		private void ShowAreaInfoWindow(Area area, bool detail)
		{
			Func<AreaInfoWindow> create = delegate {
				var w = new AreaInfoWindow();
				w.Owner = Main;
				w.SetReignManager(_reinManager);
				return w;
			};
			if (_areaInfoWindow == null)
				_areaInfoWindow = create();
			try
			{
				_areaInfoWindow.Visible = true;
			} catch (ObjectDisposedException)
			{
				_areaInfoWindow = create();
				_areaInfoWindow.Show();
			}
			_areaInfoWindow.SetArea(area, detail);
		}

		#endregion

		#region IDisposable メンバ

		public void Dispose()
		{
			if (_coroutine != null)
			{
				_coroutine.Dispose();
				_coroutine = null;
			}
			if (_script != null)
			{
				_script.Dispose();
				_script = null;
			}
		}

		#endregion
	}
}
