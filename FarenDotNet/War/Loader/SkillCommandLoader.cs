using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Paraiba.Drawing;
using FarenDotNet.BasicData;
using FarenDotNet.War.BattleAction;
using FarenDotNet.War.BattleAction.ScreenEffect;
using FarenDotNet.War.BattleCommand;
using FarenDotNet.War.Scope;

namespace FarenDotNet.War.Loader
{
	public class SkillCommandLoader
	{
		private static readonly Func<IScope, Skill, int, IScreenEffect, IBattleAction>
			DefaultCreator = (scope, skill, maxTimes, effect) =>
				new SkillAction(scope, new MPCost(skill.MPCost), skill.Power, maxTimes, skill.AtkType, skill.AtkDependency, skill.DefDependency, effect);

		private static readonly Func<IScope, Skill, int, IScreenEffect, IBattleAction>
			WallCrashCreator = (scope, skill, maxTimes, effect) =>
				new WallCrashAction(scope, new MPCost(skill.MPCost), effect, 0, AttackType.なし);

		public static List<IBattleCommand> Load(IList<Skill> skills)
		{
			var results = new List<IBattleCommand>(skills.Count) {
				new DisableCommand("特技", "なし", Properties.Resources.w_skill),
			};

			foreach (var skill in skills)
			{
				IScreenEffect screenEffect = null;
				string path = null;
				foreach(var dir in Global.CharDirs)
				{
					path = Path.Combine(dir, skill.Name + ".bmp");
					if (File.Exists(path))
						break;
				}
				var surfaces = WarGlobal.BmpManager.GetSurfaces(path, skill.SideSize, skill.SideSize,
					fpath => BitmapUtil.Load(fpath, Color.Black),
					(bmp, w, h) => bmp.SplitToBitmaps(w, h, skill.ImageCount));
				var maxTimes = 1;
				var targetType = TargetType.ENEMY;
				var createFunc = DefaultCreator;

				switch (skill.ScopeType)
				{
				case ScopeType.Arrow:
					screenEffect = new ArrowAnimation(surfaces);
					maxTimes = 3;
					break;
				case ScopeType.Breath:
					screenEffect = new ChipFrameAnimation(surfaces);
					maxTimes = 3;
					break;
				case ScopeType.Heal:
					screenEffect = new ChipFrameAnimation(surfaces);
					targetType = TargetType.FRIEND;
					break;
				case ScopeType.Magic:
					screenEffect = new ChipFrameAnimation(surfaces);
					break;
				case ScopeType.WallBreak:
					screenEffect = new ChipFrameAnimation(surfaces);
					createFunc = WallCrashCreator;
					break;
				case ScopeType.Unknown:
					screenEffect = new ChipFrameAnimation(surfaces);
					break;
				case ScopeType.SixWay:
					screenEffect = new ChipFrameAnimation(surfaces);
					break;
				case ScopeType.BlackDragonSword:
					screenEffect = new ChipFrameAnimation(surfaces);
					break;
				default:
					break;
				}

				// 行為の範囲や対象を設定する
				IScope scope;
				if (skill.Range < 0)
					scope = new StraightScope(skill.Area, targetType);
				else if (skill.Area != 0)
					scope = new DefaultScope(skill.Range, skill.Area, targetType);
				else
					scope = new WholeScope(targetType);

				var action = createFunc(scope, skill, maxTimes, screenEffect);
				var command = new SingleActionCommand(skill.Name, skill.Name, Properties.Resources.w_skill, action);

				results.Add(command);
			}

			return results;
		}
	}
}