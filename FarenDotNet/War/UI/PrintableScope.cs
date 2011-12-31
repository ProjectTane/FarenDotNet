using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarenDotNet.War.Scope;
using Paraiba.Geometry;
using Paraiba.Utility;

namespace FarenDotNet.War.UI
{
	/// <summary>
	/// GUI がスコープを描画する際に必要な情報
	/// </summary>
	public class PrintableScope
	{
		private IScope scope;
		private Situation situation;
		private WarUnit doer;
		public IList<Point2> RangeChips { get; private set; }
		public IList<Point2> ValidRangeChips { get; private set; }

		public PrintableScope(IScope scope, Situation situation, WarUnit doer)
		{
			this.scope = scope;
			this.situation = situation;
			this.doer = doer;

			IEnumerable<Point2> rangeScope;
			this.ValidRangeChips = scope.GetValidRangeScope(situation, doer, out rangeScope).ToArray();
			this.RangeChips = rangeScope.ToArray();
		}

		public PrintableScope(IScope scope, IList<Point2> rangePoints, IList<Point2> validRangePoints, Situation situation, WarUnit doer)
		{
			this.scope = scope;
			this.situation = situation;
			this.doer = doer;
			this.RangeChips = rangePoints;
			this.ValidRangeChips = validRangePoints;
		}

		public IEnumerable<Point2> GetAreaChips(Point2 center)
		{
			return scope.GetAreaScope(situation, doer, center);
		}
	}
}
