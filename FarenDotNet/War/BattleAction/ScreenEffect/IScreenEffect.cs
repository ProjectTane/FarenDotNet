using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paraiba.Geometry;

namespace FarenDotNet.War.BattleAction.ScreenEffect
{
	public interface IScreenEffect
	{
		void SetScreenEffect(ActionArguments args, WarUnit doer, Point2 center, IEnumerable<Point2> areaPoints, IEnumerable<Point2> validAreaPoints, Action callbackFunc);
		void SetScreenEffect(ActionArguments args, WarUnit doer, Point2 center, IEnumerable<Point2> areaPoints, IEnumerable<Point2> validAreaPoints, int times, Action<int, int> callbackFunc);
	}
}
