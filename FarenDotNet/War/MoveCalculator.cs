using System;
using System.Collections.Generic;
using System.Diagnostics;
using Paraiba.Collections;
using Paraiba.Geometry;

namespace FarenDotNet.War
{
	public class MovePoint : IComparable<MovePoint>
	{
		public MovePoint(Point2 p, MovePoint from, int remainingMobility)
		{
			Point = p;
			From = from;
			RemainingMobility = remainingMobility;
		}

		public Point2 Point { get; private set; }

		/// <summary>
		/// 最短経路を取った場合の一つ手前の点。起点の場合はnull値を取る。
		/// </summary>
		public MovePoint From { get; private set; }

		/// <summary>残り移動量</summary>
		public int RemainingMobility { get; private set; }

		#region IComparable<MovePoint> Members

		public int CompareTo(MovePoint other)
		{
			return other.RemainingMobility - RemainingMobility;
		}

		#endregion

		public override string ToString()
		{
			return String.Format("{0},{1},{2}", Point, From, RemainingMobility);
		}
	}

	public abstract class MoveCalculator
	{
		protected abstract IEnumerable<Point2> Around(Point2 center);
		protected abstract bool InZOC(WarMap map, WarUnit mover, Point2 location);
		protected abstract bool IsWall(WarMap map, WarUnit mover, Point2 location);
		protected abstract int NeedMobility(WarMap map, WarUnit mover, Point2 location);

		public List<MovePoint> CalcMovableArea(WarMap map, WarUnit mover, Point2 origin)
		{
			var movableArea = new List<MovePoint>();
			var markers = new PriorityQueue<MovePoint>();
			markers.Enqueue(new MovePoint(origin, null, mover.Status.Mobility));

			while (markers.Count > 0) // TODO
			{
				var from = markers.Dequeue();
				var fromPoint = from.Point;

				foreach (var p in Around(fromPoint))
				{
					// すでにより良い結果が求まっているかどうか
					if (movableArea.Exists(mp => mp.Point == p) || markers.Exists(mp => mp.Point == p))
						continue;

					// マップ外かどうか
					if (!map.IsValidPoint(p))
						continue;

					// 進入可能かどうか
					if (IsWall(map, mover, p))
						continue;

					// 必要移動力の取得
					int needed = NeedMobility(map, mover, p);
					Debug.Assert(needed > 0, "必要移動力0以下は指定しないこと");

					// 残り移動力の算出
					int remainingMov = from.RemainingMobility - needed;

					// 残り移動力が尽きているかどうか
					if (remainingMov < 0)
						continue;

					// ZOC内なら残り移動力を0として、さらなる移動を防ぐ
					if (InZOC(map, mover, p) || remainingMov == 0)
					{
						movableArea.Add(new MovePoint(p, from, 0));
					}
					else
					{
						var mp = new MovePoint(p, from, remainingMov);
						markers.Enqueue(mp);
						movableArea.Add(mp);
					}
				}
			}

			return movableArea;
		}
	}

	public class FarenMoveCalculator : MoveCalculator
	{
		/// <summary>
		/// 周辺の座標を返す関数
		/// </summary>
		/// <param name="center">中心座標</param>
		/// <returns>周りの座標</returns>
		protected override IEnumerable<Point2> Around(Point2 center)
		{
			yield return new Point2(center.X - 1, center.Y - 1);
			yield return new Point2(center.X - 0, center.Y - 1);
			yield return new Point2(center.X - 1, center.Y - 0);
			yield return new Point2(center.X + 1, center.Y + 0);
			yield return new Point2(center.X + 0, center.Y + 1);
			yield return new Point2(center.X + 1, center.Y + 1);
		}

		/// <summary>
		/// 指定された座標がZOC範囲内にあるかチェックする関数
		/// </summary>
		/// <param name="map"></param>
		/// <param name="mover"></param>
		/// <param name="location"></param>
		/// <returns></returns>
		protected override bool InZOC(WarMap map, WarUnit mover, Point2 location)
		{
			foreach (var around in Around(location))
			{
				var unit = map[around].Unit;
				if (unit != null && unit.IsOpponent(mover))
					return true;
			}
			return false;
		}

		/// <summary>
		/// 進入可能かどうか
		/// </summary>
		/// <param name="map"></param>
		/// <param name="mover">進入するユニット</param>
		/// <param name="location">進入先</param>
		/// <returns></returns>
		protected override bool IsWall(WarMap map, WarUnit mover, Point2 location)
		{
			return map[location].Unit != null;
		}

		protected override int NeedMobility(WarMap map, WarUnit mover, Point2 location)
		{
			return map[location].Info.RequiredMobility[mover.MoveType];
		}
	}
}