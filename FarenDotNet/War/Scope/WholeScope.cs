using System.Collections.Generic;
using Paraiba.Geometry;

namespace FarenDotNet.War.Scope
{
	public class WholeScope : AbstractScope
	{
		public WholeScope(TargetType targetType)
			: base(targetType)
		{
		}

		public override bool ExistChoice
		{
			get { return false; }
		}

		public override IEnumerable<Point2> GetRangeScope(Situation situation, WarUnit doer)
		{
			yield return doer.Location;
		}

		public override IList<Point2> GetValidRangeScope(Situation situation, WarUnit doer, out IEnumerable<Point2> rangeScope)
		{
			var validRangePoints = new List<Point2>();
			rangeScope = new[] { doer.Location };

			foreach (var p in situation.Map.ValidPoints)
			{
				if (IsValidPoint(situation, doer, p))
				{
					validRangePoints.Add(doer.Location);
					break;
				}
			}
			return validRangePoints;
		}

		public override IEnumerable<Point2> GetAreaScope(Situation situation, WarUnit doer, Point2 center)
		{
			return situation.Map.ValidPoints;
		}
	}
}