using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Paraiba.Core;
using Paraiba.Geometry;

namespace FarenDotNet.War
{
	public class WarMap : IEnumerable<Land>
	{
		private readonly Land _dummyChip;
		private readonly Land[,] _map;
		private readonly int _height, _width;

		public WarMap(Land[,] map, Land dummyChip, MoveCalculator moveCalc, List<List<Point2>> initDeployCandidateList)
		{
			_map = map;
			_dummyChip = dummyChip;
			_height = _map.GetLength(0);
			_width = _map.GetLength(1);
			MoveCalculator = moveCalc;
			InitDeployCandidateList = initDeployCandidateList;
			InitDeployCandidate = InitDeployCandidateList[0];
		}

		public List<Point2> InitDeployCandidate { get; set; }
		public List<List<Point2>> InitDeployCandidateList { get; set; }

		/// <summary>
		/// このマップで使用される移動力の計算のロジック
		/// </summary>
		public MoveCalculator MoveCalculator { get; private set; }

		/// <summary>
		/// チップ単位のマップ全域の高さ
		/// </summary>
		public int Height
		{
			get { return _height; }
		}

		/// <summary>
		/// チップ単位のマップ全域の幅
		/// </summary>
		public int Width
		{
			get { return _width; }
		}

		public Land this[int x, int y]
		{
			get
			{
				// ゲーム内の座標から配列の添え字に変換する
				x -= (y + 1) / 2;
				// HACK: 余分に配列を確保するか、この方法でいくか
				if (0 <= y && y < _height && 0 <= x && x < _width)
					return _map[x, y];
				return _dummyChip;
			}
			set
			{
				x -= (y + 1) / 2;
				// HACK: 余分に配列を確保するか、この方法でいくか
				if (0 <= y && y < _height && 0 <= x && x < _width)
				{
					if (_map[x, y] != value)
					{
						var old = _map[x, y];
						if (old != null)
							value.Unit = old.Unit;
						_map[x, y] = value;
						// マップの内容変化を告知
						Changed.InvokeIfNotNull(this);
					}
				}
			}
		}

		public Land this[Point2 p]
		{
			get { return this[p.X, p.Y]; }
			set { this[p.X, p.Y] = value; }
		}

		public IEnumerable<Point2> ValidPoints
		{
			get
			{
				for (int y = 0; y < Height; y++)
				{
					for (int x = 0; x < Width; x++)
					{
						yield return new Point2(x + (y + 1) / 2, y);
					}
				}
			}
		}

		public event Action<WarMap> Changed = null;

		/// <summary>
		/// 指定した座標が有効（ユニットが配置可能）な座標かどうか調べる
		/// </summary>
		/// <param name="x">有効かどうかチェックするX座標</param>
		/// <param name="y">有効かどうかチェックするY座標</param>
		/// <returns>有効かどうか</returns>
		public bool IsValidPoint(int x, int y)
		{
			// ゲーム内の座標から配列の添え字に変換する
			x -= (y + 1) / 2;
			if (!(0 <= y && y < _height))
				return false;
			if ((y & 1) == 0)
			{
				if (!(1 <= x && x < _width))
					return false;
			}
			else
			{
				if (!(0 <= x && x < _width - 1))
					return false;
			}
			return true;
		}

		/// <summary>
		/// 指定した座標が有効（ユニットが配置可能）な座標かどうか調べる
		/// </summary>
		/// <param name="p">有効かどうかチェックする座標</param>
		/// <returns>有効かどうか</returns>
		public bool IsValidPoint(Point2 p)
		{
			return IsValidPoint(p.X, p.Y);
		}

		/// <summary>
		/// 未配置のユニットを指定したマップ上の指定した位置に配置する。
		/// ゲーム初期のユニットを配置する時に使用する。
		/// </summary>
		/// <param name="unit"></param>
		/// <param name="location">配置する座標。座標はMapChip座標系で表わされている。</param>
		public void Deploy(WarUnit unit, Point2 location)
		{
			unit.Map = this;
			unit.Location = location;
			this[location].Unit = unit;

			// マップの内容変化を告知
			Changed.InvokeIfNotNull(this);
		}

		public void UnDeploy(WarUnit unit, Point2 location)
		{
			unit.Map = null;
			this[location].Unit = null;

			// マップの内容変化を告知
			Changed.InvokeIfNotNull(this);
		}

		/// <summary>
		/// 配置済みのユニットを指定した位置に移動する。
		/// 移動元の座標にはユニットは存在しないことになる。
		/// WarUnitクラスは現在位置のsetプロパティをもっていないので、このメソッドを使う。
		/// </summary>
		/// <param name="unit"></param>
		/// <param name="newLocation">マップチップ座標で表された移動先となる座標。</param>
		public void MoveUnit(WarUnit unit, Point2 newLocation)
		{
			Debug.Assert(unit.Map == this);
			Debug.Assert(this[unit.Location].Unit == unit);

			this[unit.Location].Unit = null;
			unit.Location = newLocation;
			this[newLocation].Unit = unit;

			// マップの内容変化を告知
			Changed.InvokeIfNotNull(this);
		}

		/// <summary>
		///           コレクションを反復処理する列挙子を返します。
		/// </summary>
		/// <returns>
		///           コレクションを反復処理するために使用できる <see cref="T:System.Collections.Generic.IEnumerator`1" />。
		/// </returns>
		/// <filterpriority>1</filterpriority>
		public IEnumerator<Land> GetEnumerator()
		{
			return _map.GetElements().GetEnumerator();
		}

		/// <summary>
		/// コレクションを反復処理する列挙子を返します。
		/// </summary>
		/// <returns>
		/// コレクションを反復処理するために使用できる <see cref="T:System.Collections.IEnumerator" /> オブジェクト。
		/// </returns>
		/// <filterpriority>2</filterpriority>
		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}