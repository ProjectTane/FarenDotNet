using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paraiba.Drawing.Animations.Surfaces.Sprites;
using Paraiba.Drawing.Surfaces;
using Paraiba.Geometry;
using Paraiba.Core;

namespace FarenDotNet.War.BattleAction.ScreenEffect
{
	public class ArrowAnimation : IScreenEffect
	{
		private readonly IList<Surface> _surfaces;

		public ArrowAnimation(IList<Surface> surfaces)
		{
			_surfaces = surfaces;
		}

		public void SetScreenEffect(ActionArguments args, WarUnit doer, Point2 center, IEnumerable<Point2> areaPoints, IEnumerable<Point2> validAreaPoints, Action callbackFunc)
		{
			args.Model.SetDirectedUniformMotionAnimationOnMap(_surfaces, center, areaPoints.First(), 32, callbackFunc);
		}

		public void SetScreenEffect(ActionArguments args, WarUnit doer, Point2 center, IEnumerable<Point2> areaPoints, IEnumerable<Point2> validAreaPoints, int times, Action<int, int> callbackFunc)
		{
			var model = args.Model;
			int delay = 0;
			for (int i = 1; i <= times; i++)
			{
				foreach (var p in areaPoints)
				{
					var anime = model.CreateDirectedUniformMotionAnimationOnMap(_surfaces, doer.Location, p, 0.125f);
					anime = new ExtendTimeAnimationSprite(anime, delay, 0);
					model.ChipAnimations.Add(anime, callbackFunc.GetCurrying(i, times));
					delay += 300;
				}
			}
		}
	}
}
