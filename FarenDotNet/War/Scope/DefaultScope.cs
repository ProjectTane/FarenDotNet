using System.Collections.Generic;
using Paraiba.Geometry;

namespace FarenDotNet.War.Scope
{
	/// <summary>
	/// 標準的なスコープを表します。
	/// </summary>
	public class DefaultScope : AbstractScope
	{
		private readonly int _area;
		private readonly int _range;

		public DefaultScope(int range, int area, TargetType targetType)
			: base(targetType)
		{
			_range = range;
			_area = area;
		}

		public override bool ExistChoice
		{
			get { return _range != 0; }
		}

		public override IEnumerable<Point2> GetRangeScope(Situation situation, WarUnit doer)
		{
			return ScopeUtil.GetScopePoints(doer.Location, _range); // 射程は0で1マス
		}

		public override IEnumerable<Point2> GetAreaScope(Situation situation, WarUnit doer, Point2 center)
		{
			return ScopeUtil.GetScopePoints(center, _area - 1); // 範囲は1で1マス
		}
	}
}