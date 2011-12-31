using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Diagnostics.Contracts;
using Paraiba.IO;
using FarenDotNet.BasicData;

namespace FarenDotNet.Loader
{
	public class AreaDataLoader
	{
		public List<AreaData> Load(string filepath)
		{
			Contract.Requires(File.Exists(filepath), "filepath");

			var list = new List<AreaData>();
			using (var reader = new StreamReader(filepath, Global.Encoding))
			{
				var sc = new Scanner(reader,
					c => c == ' ' || c == '　' || c == '\t' || c == '\r',
					c => c == '\n');
				int id = 0;
				while (sc.HasNext() && sc.Current() != "End")
				{
					list.Add(new AreaData(++id) {
						Name = sc.Next(),
						AreaType = sc.NextInt32(),
						DefaultIncome = sc.NextInt32(),
						NumCity = sc.NextInt32(),
						NumWall = sc.NextInt32(),
						NumRoad = sc.NextInt32(),
						NeutralPower = sc.NextInt32(),
						DefautlWallRate = sc.NextInt32(),
						FlagLocation = new Point(
							sc.NextInt32(),
							sc.NextInt32()),
						Adjacent = new[] {
							sc.NextInt32(),
							sc.NextInt32(),
							sc.NextInt32(),
							sc.NextInt32(), // 4
							sc.NextInt32(),
							sc.NextInt32(),
							sc.NextInt32(),
							sc.NextInt32(), // 8
						},
						MusicFileName = sc.Next(),
					});
					Debug.Assert(sc.LastDelimiter == '\n'); // 念のために改行が存在するかチェック
				}
			} // end using
			return list;
		}
	}
}