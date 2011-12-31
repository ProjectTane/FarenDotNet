using System;
using WeifenLuo.WinFormsUI.Docking;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace FarenDotNet.Reign.UI
{
	public partial class ReignWindow : DockContent
	{
		// ----- ----- ----- FIELD ----- ----- -----
		private readonly WorldMapPanel _map;
		private ReignManager _reinManager;
		Action _event;

		// ----- ----- ----- CTOR ----- ----- -----
		public ReignWindow(ReignManager manager)
		{
			_reinManager = manager;
			InitializeComponent();
			_map = new WorldMapPanel(manager.WorldMap);
			_scrollablePanel.Panel = _map;

			// EventScript用
			_map.Click += this.ClickEvent;
			_timer.Tick += this.ClickEvent;
		}

		// ----- ----- ----- METHOD ----- ----- -----
		public void StartTurn(int turn)
		{
			_topPanel.Turn = turn;
			_topPanel.Invalidate();
		}

		public void SetName(string name)
		{
			_topPanel.MasterName = name;
			_topPanel.Invalidate();
		}

		public void SetEvent(Action action, int msec)
		{
			_event = action;
			if (msec > 0)
			{
				_timer.Interval = msec;
				_timer.Start();
			}
		}

		public void AreaFocus(int areaNo)
		{
			var area = _reinManager.WorldMap.Areas.SingleOrDefault(a => a.No == areaNo);
			AreaFocus(area);
		}
		public void AreaFocus (Area area) { _scrollablePanel.Centering(area.Location); }

		// ----- ----- ----- EVENT ----- ----- -----
		/// <summary>
		/// エリアをクリックしたときの処理
		/// </summary>
		public event EventHandler<WorldMapPanel.AreaClickEventArgs> AreaClick
		{
			add { _map.AreaClick += value; }
			remove { _map.AreaClick -= value; }
		}

		/// <summary>
		/// マップのどこかをクリックしたとき
		/// </summary>
		private void ClickEvent(object sender, EventArgs e)
		{
			_timer.Stop();
			var ev = _event;
			_event = null;
			if (ev == null)
				return;
			ev();
		}
	}
}