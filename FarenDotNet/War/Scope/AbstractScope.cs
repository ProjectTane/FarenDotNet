using System.Collections.Generic;
using Paraiba.Geometry;

namespace FarenDotNet.War.Scope
{
	public abstract class AbstractScope : IScope
	{
		private readonly TargetType _targetType;

		protected AbstractScope(TargetType targetType)
		{
			_targetType = targetType;
		}

		#region IScope メンバ

		public abstract bool ExistChoice { get; }

		public abstract IEnumerable<Point2> GetRangeScope(Situation situation, WarUnit doer);

		public virtual IList<Point2> GetValidRangeScope(Situation situation, WarUnit doer, out IEnumerable<Point2> rangeScope)
		{
			var validRangePoints = new List<Point2>();
			rangeScope = GetRangeScope(situation, doer);

			// 対象が存在するかどうかチェック
			foreach (var rangePoint in rangeScope)
			{
				foreach (var p in GetAreaScope(situation, doer, rangePoint))
				{
					if (IsValidPoint(situation, doer, p))
					{
						validRangePoints.Add(rangePoint);
						break;
					}
				}
			}
			return validRangePoints;
		}

		public abstract IEnumerable<Point2> GetAreaScope(Situation situation, WarUnit doer, Point2 center);

		public virtual IEnumerable<Point2> GetValidAreaScope(Situation situation, WarUnit doer, Point2 center, out IEnumerable<Point2> areaScope)
		{
			var validAreaPoints = new List<Point2>();
			areaScope = GetAreaScope(situation, doer, center);

			foreach (var p in areaScope)
			{
				if (IsValidPoint(situation, doer, p))
				{
					validAreaPoints.Add(p);
				}
			}
			return validAreaPoints;
		}

		protected virtual bool IsValidPoint(Situation situation, WarUnit doer, Point2 p)
		{
			var unit = situation.Map[p].Unit;

			// チップの情報からターゲットタイプの値を算出
			TargetType type;
			if (unit == null)
			{
				type = TargetType.NONE;
			}
			else
			{
				if (doer.IsOpponent(unit))
					type = TargetType.ENEMY;
				else
					type = TargetType.FRIEND;
			}

			// スコープのターゲットタイプに内包されるかチェック
			return (_targetType & type) != 0;
		}

		#endregion
	}
}