using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics.Contracts;
using Paraiba.Collections.Generic;
using Paraiba.Core;
using System.Linq.Expressions;

namespace FarenDotNet.Reign.UI
{
	public class Scripter
	{
		TextWindow _window0 = new TextWindow { Location = TextWindowLocation.Top };
		TextWindow _window1 = new TextWindow { Location = TextWindowLocation.Bottom };

		TextWindow _lastWindow = null;
		ReignManager _rManager;
		ReignWindowManager _rwManager;

		public readonly int LOOP_LIMIT = 1000;

		public Scripter(Form owner, ReignManager rManager, ReignWindowManager rwManager)
		{
			Contract.Requires(owner != null, "owner");
			Contract.Requires(rManager != null, "rManager");
			Contract.Requires(rwManager != null, "rwManager");

			_window0.Owner = owner;
			_window1.Owner = owner;
			_rManager = rManager;
			_rwManager = rwManager;
			_lastWindow = _window0;

			#region インデクサの初期化

			// ----- ----- ----- ----- ----- エリアNoから取得 ----- ----- ----- ----- -----
			AreaButaiKz = new IndexGetter<int>(GetAreaByNo(area => area.Units.Count));
			AreaCity = new Indexer<int>(
				GetAreaByNo(area => area.NumCity),
				SetAreaByNo((area, num) => area.NumCity100 = num * 100));
			AreaRoad = new Indexer<int>(
				GetAreaByNo(area => area.NumRoad),
				SetAreaByNo((area, num) => area.NumRoad100 = num * 100));
			AreaWall = new Indexer<int>(
				GetAreaByNo(area => area.NumWall),
				SetAreaByNo((area, num) => area.NumWall100 = num * 100));
			AreaKuni = new IndexGetter<int>(GetAreaByNo(area => area.Province.No));

			// ----- ----- ----- ----- ----- 勢力Noから取得 ----- ----- ----- ----- -----
			Ley = new Indexer<int>(
				GetProvByNo(p => p.Money),
				SetProvByNo((p, money) => p.Money += money));

			KuniFlag = new IndexGetter<int>(provNo => {
				foreach (var area in _rManager.WorldMap.Areas)
					if (area.Province.No == provNo)
						return 1;
				return 0;
			});

			KuniPlayer = new IndexGetter<int>(
				GetProvByNo(p => p == _rManager.ActingProv && p.IsPlayer ? 1 : 0));

			this.BaseArea = new IndexGetter<int>(provNo => {
				// マスターの存在エリアNo 
				// 実際はマスターがいれば良いらしい
				foreach (var area in _rManager.WorldMap.Areas)
					if (area.Province.No == provNo)
						foreach (var unit in area.Units)
							if (unit.IsMaster)
								return area.No;
				return 0;
			});

			this.AreaKz = new IndexGetter<int>(provNo => {
				int count = 0;
				foreach (var area in _rManager.WorldMap.Areas)
					if (area.Province.No == provNo)
						count++;
				return count;
			});

			// ----- ----- ----- ----- ----- 勢力Noから取得 ----- ----- ----- ----- -----
			// 固有ユニットがいる勢力No
			this.HeroKuni = new IndexGetter<int>(unitNo => {
				var unit = _rManager.UnitsData[unitNo - 1];
				if (!unit.IsUnique || unit.IsMaster)
					return 0;
				string unitID = unit.ID;
				// FOR文で
				foreach (var area in _rManager.WorldMap.Areas)
					foreach (var u in area.Units)
						if (u.ID == unitID)
							return area.Province.No;
				return 0;
			});

			// 固有ユニットがいるエリアNo
			this.HeroFlag = new IndexGetter<int>(unitNo => {
				var unit = _rManager.UnitsData[unitNo - 1];
				if (!unit.IsUnique || unit.IsMaster)
					return 0;
				string unitID = unit.ID;
				// LINQで
				var area = _rManager.WorldMap.Areas
					.FirstOrDefault(a =>
						a.Units.Any(u => u.ID == unitID));
				return area == null ? 0 : area.No;
			});


			// ----- ----- ----- ----- ----- その他 ----- ----- ----- ----- -----
			Flag = new Indexer<int>(
				index => { _rManager.Flag.Extend(index+1); return _rManager.Flag[index] ? 1 : 0; },
				(index, val) => { _rManager.Flag.Extend(index+1); _rManager.Flag[index] = val != 0; });

			// HACK: そのまま代入してもいいんだけど、どうしようか
			this.League = new Indexer<int, int>(
				(a, b) => _rManager.League[a, b],
				(a, b, v) => _rManager.League[a, b] = v);

			MostWin = new IndexGetter<int>(num => {
				var unitID = _rManager.WorldMap.Areas
					.SelectMany(a => a.Units) // 全てのフィールド上のユニット
					.OrderByDescending(u => u.ObliterateCount) // 撃墜数で降順
					.Skip(num).First() // index番目を取り出す
					.ID;
				var units = _rManager.UnitsData;
				for (int i = 0; i < units.Count; i++)
					if (units[i].ID == unitID)
						return i + 1;
				return 0;
			});

			WinKz = new IndexGetter<int>(unitNo => {
				if (unitNo <= 0 || _rManager.UnitsData.Count < unitNo) return 0;
				var unitID = _rManager.UnitsData[unitNo - 1].ID;
				var unit = _rManager.WorldMap.Areas
					.SelectMany(a => a.Units)
					.FirstOrDefault(u => u.ID == unitID);
				return unit == null ? 0 : unit.ObliterateCount;
			});


			// ----- ----- ----- ----- ----- ホントは関数だけど ----- ----- ----- ----- -----
			YesNo = new IndexGetter<string>(str => {
				if (MessageBox.Show(str, "イベント", MessageBoxButtons.YesNo) == DialogResult.Yes)
					return 1;
				return 0;
			});

			Random = new IndexGetter<int>(Global.Random.Next);

			// ----- ----- ----- ----- ----- インデクサ 初期化終了 ----- ----- ----- ----- -----
			#endregion
		}

		// ----- ----- ----- ----- ----- スクリプト用 インデクサ ----- ----- ----- ----- -----

		#region インデクサ宣言

		// ----- ----- エリアNoから取得 ----- -----
		public readonly IndexGetter<int> AreaButaiKz;
		public readonly Indexer<int> AreaCity;
		public readonly Indexer<int> AreaWall;
		public readonly Indexer<int> AreaRoad;
		public readonly IndexGetter<int> AreaKuni;

		// ----- ----- 勢力Noから取得 ----- -----
		public readonly Indexer<int> Ley;
		public readonly IndexGetter<int> KuniFlag;
		public readonly IndexGetter<int> KuniPlayer;
		public readonly IndexGetter<int> BaseArea;
		public readonly IndexGetter<int> AreaKz;

		// ----- ----- ユニットNoから取得（ユニーク&非マスターのみ） ----- -----
		public readonly IndexGetter<int> HeroKuni;
		public readonly IndexGetter<int> HeroFlag;

		// ----- ----- その他 ----- -----
		public readonly Indexer<int> Flag;
		public readonly Indexer<int, int> League;
		public readonly IndexGetter<int> MostWin;
		public readonly IndexGetter<int> WinKz;

		/// <summary>始まってからの経過ターン</summary>
		public int StartTurn { get { return _rManager.Turn - _rManager.StartTurn; } }
		public int Turn { get { return _rManager.Turn; } }
		public int Scenario { get { return _rManager.ScenarioNo; } }

		// ----- ----- 本来は関数 ----- -----
		public readonly IndexGetter<string> YesNo;
		public readonly IndexGetter<int> Random;

		#endregion

		// ----- ----- ----- ----- ----- 公開関数 ----- ----- ----- ----- -----
		public void ShowMessage(string msg)
		{
			MessageBox.Show(msg);
		}
		
		// ----- ----- ----- ----- ----- スクリプト用 関数 ----- ----- ----- ----- -----

		#region スクリプト ウィンドウ系

		public void Window(int b)
		{
			_lastWindow = b == 0 ? _window0 : _window1;
			_lastWindow.Visible = true;
			_lastWindow.TopLevel = true;
			_lastWindow.Clear();
			_lastWindow.Focus();
		}

		public void Conversation(int no)
		{
			var unit = _rManager.UnitsData[no - 1];
			_lastWindow.Conversation(unit.Name, unit.Images.Face, _rManager.WorldMap.ConversationImage);
		}

		public void FaceOut(int no)
		{
			var unit = _rManager.UnitsData[no - 1];

		}

		public void Size(int w, int h) { _lastWindow.SetSize(w, h); }

		public void Print(string message) { _lastWindow.Print(message); }

		public void PrintKz(int num) { _lastWindow.Print(num.ToString()); }

		public void WindowOff()
		{
			_window0.Visible = false;
			_window1.Visible = false;
		}

		public void Clear() { _lastWindow.Clear(); }

		#endregion

		#region データ操作系

		public void CharSet(int areaNo, int unitNo)
		{
			var area = _rManager.WorldMap.Areas.SingleOrDefault(a => a.No == areaNo);
			if (area == null || area.Units.Count == 20)
				return;
			Debug.Assert(area.Units.Count < 20);
			var unit = _rManager.UnitsData[unitNo - 1];
			area.Units.Add(new Unit(unit));
		}

		public void CharDelete(int unitNo)
		{
			var unitID = _rManager.UnitsData[unitNo - 1].ID;
			foreach (var area in _rManager.WorldMap.Areas)
			{
				var unit = area.Units.FirstOrDefault(u => u.ID == unitID);
				if (unit != null)
				{
					area.Units.Remove(unit);
					return;
				}
			}
		}

		#endregion

		public void AreaFocus(int areaNo)
		{
			_rwManager.Main.AreaFocus(areaNo);
		}

		public void Music(string fileName)
		{
			// TODO : 音楽を鳴らせてください。
		}

		// ----- ----- ----- ----- ----- スクリプト用 関数 終わり ----- ----- ----- ----- -----

		/// <summary>
		/// 「エリアNo→ エリア、エリア→何か」の右側だけを指定しておく高階関数
		/// カリー化に近い？
		/// </summary>
		private Func<int, T> GetAreaByNo<T>(Func<Area, T> func)
		{
			return areaNo => {
				var area = _rManager.WorldMap.Areas.SingleOrDefault(a => a.No == areaNo);
				return area == null ? default(T) : func(area);
			};
		}

		/// <summary>
		/// 「エリアNo→ エリア、エリア→int」の右側だけを指定しておく高階関数
		/// T型でうまくいかなかった
		/// </summary>
		private Action<int, int> SetAreaByNo(Action<Area, int> func)
		{
			return (areaNo, val) => {
				var area = _rManager.WorldMap.Areas.SingleOrDefault(a => a.No == areaNo);
				func(area, val);
			};
		}

		/// <summary>
		/// 「勢力No→ 勢力、勢力→何か」の右側だけを指定しておく高階関数
		/// </summary>
		private Func<int, T> GetProvByNo<T>(Func<Province, T> func)
		{
			return provNo => {
				var prov = _rManager.LiveProvs.SingleOrDefault(p => p.No == provNo);
				return prov == null ? default(T) : func(prov);
			};
		}

		/// <summary>
		/// 「勢力No→ 勢力、勢力→int」の右側だけを指定しておく高階関数
		/// </summary>
		private Action<int, int> SetProvByNo(Action<Province, int> action)
		{
			return (provNo, val) => {
				var prov = _rManager.LiveProvs.SingleOrDefault(p => p.No == provNo);
				if (prov != null)
					action(prov, val);
			};
		}


		#region 内部クラス

		public class Indexer<T>
		{
			readonly Func<T, int> _get;
			readonly Action<T, int> _set;

			public Indexer(Func<T, int> get, Action<T, int> set)
			{
				Contract.Requires(get != null, "get");
				Contract.Requires(set != null, "set");
				_get = get;
				_set = set;
			}

			public int this[T index]
			{
				get { return _get(index); }
				set { _set(index, value); }
			}
		}

		public class Indexer<T1, T2>
		{
			readonly Func<T1, T2, int> _get;
			readonly Action<T1, T2, int> _set;

			public Indexer(Func<T1, T2, int> get, Action<T1, T2, int> set)
			{
				Contract.Requires(get != null, "get");
				Contract.Requires(set != null, "set");
				_get = get;
				_set = set;
			}

			public int this[T1 t1, T2 t2]
			{
				get { return _get(t1, t2); }
				set { _set(t1, t2, value); }
			}
		}

		public class IndexGetter<T>
		{
			readonly Func<T, int> _get;

			public IndexGetter(Func<T, int> get)
			{
				Contract.Requires(get != null, "get");
				_get = get;
			}

			public int this[T index]
			{
				get { return _get(index); }
			}
		}

		#endregion
	}
}
