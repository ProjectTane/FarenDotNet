using System.Collections.Generic;
using Paraiba.Geometry;

namespace FarenDotNet.War.Scope
{
	public static class ScopeUtil
	{
		public static List<Point2> GetScopePoints(Point2 originPoint, int range)
		{
			var points = new List<Point2>(range * (3 * range + 3) + 1);
			for (int j = -range; j <= range; j++)
			{
				points.Add(new Point2(originPoint.X + j, originPoint.Y));
			}
			for (int i = 1; i <= range; i++)
			{
				for (int j = -range; j <= range - i; j++)
				{
					points.Add(new Point2(originPoint.X + j, originPoint.Y - i));
					points.Add(new Point2(originPoint.X + j + i, originPoint.Y + i));
				}
			}
			return points;
		}

		public static List<Point2> GetStraightScopePoints(Point2 originPoint, int range)
		{
			var points = new List<Point2>(range * 6);
			for (int j = 1; j <= range; j++)
			{
				points.Add(new Point2(originPoint.X - j, originPoint.Y + 0));
				points.Add(new Point2(originPoint.X - j, originPoint.Y - j));
				points.Add(new Point2(originPoint.X + 0, originPoint.Y - j));
				points.Add(new Point2(originPoint.X + j, originPoint.Y + 0));
				points.Add(new Point2(originPoint.X + j, originPoint.Y + j));
				points.Add(new Point2(originPoint.X + 0, originPoint.Y + j));
			}
			return points;
		}

		public static Vector2 GetDirectionalVector(Vector2 v)
		{
			if (v.X == 0)
			{
				if (v.Y > 0)
					return new Vector2(0, 1);
				else if (v.Y < 0)
					return new Vector2(0, -1);
			}
			else if (v.X > 0)
			{
				if (v.Y == 0)
					return new Vector2(1, 0);
				else if (v.Y == v.X)
					return new Vector2(1, 1);
			}
			else
			{
				if (v.Y == 0)
					return new Vector2(-1, 0);
				else if (v.Y == v.X)
					return new Vector2(-1, -1);
			}
			return new Vector2(0, 0);
		}
	} // end class
}