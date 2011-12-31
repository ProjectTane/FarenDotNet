using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarenDotNet.BasicData;
using System.Diagnostics.Contracts;
using System.Drawing;

namespace FarenDotNet.Loader
{
	public class MasterDataLoader
	{
		public MasterData Load(string filepath)
		{
			Contract.Requires(filepath != null, "filepath");
			var table = new SettingFileReader(filepath);

			var dirs = table["Character"];
			var music = new MasterData.MusicData(
				table["EventMusic"][0],
				table["DecisiveMusic"][0]);

			var revivals = from x in table["MasterRevival"]
						   let s = x.SplitSpace()
						   select new MasterData.Revival(
							   Int32.Parse(s[0]),
							   Int32.Parse(s[1]),
							   Int32.Parse(s[2]),
							   s[3]);

			var inherits = from x in table["MasterInherit"]
						   let s = x.SplitSpace()
						   select new MasterData.Inherit(
							   Int32.Parse(s[0]),
							   s[1],
							   Int32.Parse(s[2]),
							   Int32.Parse(s[3]),
							   s[4]);

			var ghosts = from x in table["Ghost"]
						 from y in x.SplitSpace()
						 select Int32.Parse(y);

			var battleMusic = from x in table["BattleMusic"]
							  let y = x.SplitSpace(2)
							  select new { No = int.Parse(y[0]), Music = y[1] };

			var mBattleMusic = from x in table["MasterBattleMusic"]
							   let y = x.SplitSpace(2)
							   select new { No = int.Parse(y[0]), Music = y[1] };

			int index = 0;
			var masters = new List<MasterData.Master>();
			while (table.HasKey("Master" + (++index))) {
				var data = table["Master" + index];
				var nums = data[2].SplitSpace();
				var clr = Color.FromArgb(
					Int32.Parse(nums[1]),
					Int32.Parse(nums[2]),
					Int32.Parse(nums[3]));

				var battle = battleMusic.FirstOrDefault(x => x.No == index);
				var master = mBattleMusic.FirstOrDefault(x => x.No == index);

				masters.Add(new MasterData.Master(
					index, data[0], data[1],
					Int32.Parse(nums[0]),
					clr, data[3],
					battle == null ? null : battle.Music,
					master == null ? null : master.Music,
					ghosts.Contains(index)));
			}

			return new MasterData(
				dirs,
				music,
				revivals.ToArray(),
				inherits.ToArray(),
				masters);
		}
	}
}
