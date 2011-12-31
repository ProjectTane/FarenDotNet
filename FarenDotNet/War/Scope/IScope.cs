using System.Collections.Generic;
using Paraiba.Geometry;

namespace FarenDotNet.War.Scope
{
	public interface IScope
	{
		bool ExistChoice { get; }
		IEnumerable<Point2> GetRangeScope(Situation situation, WarUnit doer);
		IList<Point2> GetValidRangeScope(Situation situation, WarUnit doer, out IEnumerable<Point2> rangeScope);
		IEnumerable<Point2> GetAreaScope(Situation situation, WarUnit doer, Point2 center);
		IEnumerable<Point2> GetValidAreaScope(Situation situation, WarUnit doer, Point2 center, out IEnumerable<Point2> areaScope);
	}
}