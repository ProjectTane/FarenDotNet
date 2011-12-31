using System;
using System.Collections.Generic;
using Paraiba.Drawing.Surfaces;
using Paraiba.Geometry;

namespace FarenDotNet.War.BattleAction.ScreenEffect
{
	public class ScreenFrameAnimation : IScreenEffect
	{
		private readonly IList<Surface> _surfaces;

		public ScreenFrameAnimation(IList<Surface> surfaces)
		{
			_surfaces = surfaces;
		}

		#region IScreenEffect メンバ

		public void SetScreenEffect(ActionArguments args, WarUnit doer, Point2 center, IEnumerable<Point2> areaPoints, IEnumerable<Point2> validAreaPoints, Action callbackFunc)
		{
			args.Model.SetFrameAnimationOnScreen(_surfaces, 150, callbackFunc);
		}

		public void SetScreenEffect(ActionArguments args, WarUnit doer, Point2 center, IEnumerable<Point2> areaPoints, IEnumerable<Point2> validAreaPoints, int times, Action<int, int> callbackFunc)
		{
			args.Model.SetFrameAnimationOnScreen(_surfaces, 150, () => {
				for (int i = 1; i <= times; i++)
					callbackFunc(i, times);
			});
		}

		#endregion
	}
}