using System.Collections.Generic;
using Paraiba.Geometry;

namespace FarenDotNet.War.Scope
{
	/// <summary>
	/// 6方向直線のスコープを表します。
	/// </summary>
	public class StraightScope : AbstractScope
	{
		private readonly int _range;

		public StraightScope(int range, TargetType targetType)
			: base(targetType)
		{
			_range = range;
		}

		public override bool ExistChoice
		{
			get { return true; }
		}

		public override IEnumerable<Point2> GetRangeScope(Situation situation, WarUnit doer)
		{
			return ScopeUtil.GetStraightScopePoints(doer.Location, _range);
		}

		public override IEnumerable<Point2> GetAreaScope(Situation situation, WarUnit doer, Point2 center)
		{
			var v = ScopeUtil.GetDirectionalVector(center - doer.Location);
			var p = doer.Location;
			for (int i = 0; i < _range; i++)
			{
				p += v;
				yield return p;
			}
		}
	}
}