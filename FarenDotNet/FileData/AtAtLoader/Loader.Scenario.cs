using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Paraiba.IO;
using System.IO;

namespace FarenDotNet.FileData.AtAtLoader
{
	public partial class Loader
	{

		private List<Scenario> LoadScenarios(string path)
		{
			int no = 0;
			var list = new List<Scenario>();
			while (true)
			{
				no++;
				string file = Path.Combine(path, "Data", "Scenario" + no);
				if (File.Exists(file) == false)
					break;
			}
			return list;
		}

		private Scenario LoadScenario(string path)
		{
			return null;
		}
	}
}
