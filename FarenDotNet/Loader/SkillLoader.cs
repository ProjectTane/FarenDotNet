using System;
using System.Collections.Generic;
using System.IO;
using Paraiba.IO;
using FarenDotNet.BasicData;

namespace FarenDotNet.Loader
{
	public class SkillLoader
	{
		public static IList<Skill> Load(IList<string> charaDirPathes)
		{
			foreach (string dpath in charaDirPathes)
			{
				var fpath = Path.Combine(dpath, "SpecialTable");
				if (!File.Exists(fpath)) continue;

				try
				{
					using (var reader = new StreamReader(fpath, Global.Encoding))
					{
						var sc = new Scanner(reader);
						var skillList = new List<Skill>();

						while (sc.HasNext() && sc.Current() != "End")
						{
							var skill = new Skill {
								name = sc.Next(),
								scopeType = ((ScopeType)sc.NextInt32()),
								skillType = ((SkillType)sc.NextInt32()),
								mpCost = sc.NextInt32(),
								range = sc.NextInt32(),
								area = sc.NextInt32(),
								power = sc.NextInt32(),
								attackDependency = ((AttackDependency)sc.NextInt32()),
								defenseDependency = ((DefenseDependency)sc.NextInt32()),
								attackType = ((AttackType)sc.NextInt32()),
								soundName = sc.Next(),
								sideSize = sc.NextInt32(),
								imageCount = sc.NextInt32(),
							};
							skillList.Add(skill);
						}

						return skillList;
					}
				} catch (Exception)
				{
				}
			}

			return null;
		}
	}
}