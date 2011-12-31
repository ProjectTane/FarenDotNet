using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarenDotNet.FileData
{
	public class GameData
	{
		// ----- ----- ----- ----- ----- Field ----- ----- ----- ----- -----
		string _title;
		string _path;

		// ----- ----- ----- ----- ----- Property ----- ----- ----- ----- -----
		public string Title { get { return _title; } }
		public string Path { get { return _path; } }

		public IList<Scenario> Scenarios { get; set; }
		public IList<Province> Provinces { get; set; }
		public IList<Unit> Units { get; set; }
		public FieldMap FieldMap { get; set; }

		// ----- ----- ----- ----- ----- Method ----- ----- ----- ----- -----
		public GameData(string title, string path)
		{
			_title = title;
			_path = path;
		}
	}
}
