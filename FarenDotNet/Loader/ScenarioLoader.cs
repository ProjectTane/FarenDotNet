using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics.Contracts;
using Paraiba.Core;
using System.IO;
using FarenDotNet.BasicData;

namespace FarenDotNet.Loader
{
	public class ScenarioLoader
	{
		public ScenarioData Load(string filepath)
		{
			Contract.Requires(File.Exists(filepath), "filepath");

			var table = new SettingFileReader(filepath);

			string title = null;
			string exp = null;
			int turn = -1;
			IList<int> areas = null;
			IEnumerable<int> takeoffs = null;
			var masters = new List<ScenarioData.Master>();
			var leagues = new List<ScenarioData.League>();
			var locates = new List<ScenarioData.Locate>();

			// 例外処理はめんどいのでパスで
			foreach (var pair in table) {
				switch (pair.Key) {

				case "Name":
					title = pair.Value[0];
					break;

				case "Explanation":
					exp = pair.Value.JoinString(Environment.NewLine);
					break;

				case "Turn":
					turn = Int32.Parse(pair.Value[0]);
					break;

				case "Area":
					// TODO: areas の個数が実際のエリア数と異なっていても動くように修正
					var list = from line in pair.Value
							   from owner in line.SplitSpace()
							   select Int32.Parse(owner);
					areas = list.ToList();
					break;

				case "League":
					foreach (var l in pair.Value) {
						var three = l.SplitSpace();
						leagues.Add(new ScenarioData.League(
							Int32.Parse(three[0]),
							Int32.Parse(three[1]),
							Int32.Parse(three[2])));
					}
					break;

				case "Locate":
					foreach (var l in pair.Value) {
						var three = l.SplitSpace();
						locates.Add(new ScenarioData.Locate(
							three[0],
							Int32.Parse(three[1]),
							Int32.Parse(three[2])));
					}
					break;

				case "TakeOff":
					if (takeoffs != null)
						break;
					takeoffs = from l in pair.Value
							   from n in l.SplitSpace()
							   select Int32.Parse(n);
					break;

				default:
					if (!pair.Key.StartsWith("Master"))
						break;
					if (takeoffs == null) {
						if (table.HasKey("TakeOff")) {
							takeoffs = from l in table["TakeOff"]
									   from n in l.SplitSpace()
									   select Int32.Parse(n);
						}
						else {
							takeoffs = new int[0];
						}
					}
					int num = Int32.Parse(pair.Key.Replace("Master", ""));
					masters.Add(new ScenarioData.Master(
						num,
						pair.Value[0],
						pair.Value[1],
						pair.Value.Skip(2).JoinString(Environment.NewLine),
						takeoffs.Contains(num)));
					break;
				} // end switch
			} // end table

			return new ScenarioData(
				title,
				exp,
				turn,
				masters,
				areas,
				leagues,
				locates);
		}

	}
}
