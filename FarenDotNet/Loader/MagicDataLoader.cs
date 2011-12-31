using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarenDotNet.BasicData;
using System.IO;
using Paraiba.IO;

namespace FarenDotNet.Loader
{
	public class MagicDataLoader
	{
		public static MagicData[,][] ReadMagicTable(string scenarioDirPath)
		{
			var magics = Load(scenarioDirPath);
			var magicTable = new MagicData[6, 6][];
			for(int i = 0; i < 6; i++)
			{
				int j = 0;
				// ×
				{
					magicTable[i, j++] = new MagicData[0];
				}
				// D
				{
					var list = new List<MagicData>();
					int d = 0;
					list.Add(magics[i * 8 + d++].Clone(1));
					list.Add(magics[i * 8 + d++].Clone(1));
					magicTable[i, j++] = list.ToArray();
				}
				// C
				{
					var list = new List<MagicData>();
					int d = 0;
					list.Add(magics[i * 8 + d++].Clone(2));
					list.Add(magics[i * 8 + d++].Clone(1));
					list.Add(magics[i * 8 + d++].Clone(1));
					list.Add(magics[i * 8 + d++].Clone(1));
					magicTable[i, j++] = list.ToArray();
				}
				// B
				{
					var list = new List<MagicData>();
					int d = 0;
					list.Add(magics[i * 8 + d++].Clone(3));
					list.Add(magics[i * 8 + d++].Clone(2));
					list.Add(magics[i * 8 + d++].Clone(1));
					list.Add(magics[i * 8 + d++].Clone(1));
					list.Add(magics[i * 8 + d++].Clone(1));
					list.Add(magics[i * 8 + d++].Clone(1));
					magicTable[i, j++] = list.ToArray();
				}
				// A
				{
					var list = new List<MagicData>();
					int d = 0;
					list.Add(magics[i * 8 + d++].Clone(3));
					list.Add(magics[i * 8 + d++].Clone(3));
					list.Add(magics[i * 8 + d++].Clone(2));
					list.Add(magics[i * 8 + d++].Clone(2));
					list.Add(magics[i * 8 + d++].Clone(1));
					list.Add(magics[i * 8 + d++].Clone(1));
					list.Add(magics[i * 8 + d++].Clone(1));
					magicTable[i, j++] = list.ToArray();
				}
				// S
				{
					var list = new List<MagicData>();
					int d = 0;
					list.Add(magics[i * 8 + d++].Clone(3));
					list.Add(magics[i * 8 + d++].Clone(3));
					list.Add(magics[i * 8 + d++].Clone(2));
					list.Add(magics[i * 8 + d++].Clone(2));
					list.Add(magics[i * 8 + d++].Clone(2));
					list.Add(magics[i * 8 + d++].Clone(2));
					list.Add(magics[i * 8 + d++].Clone(1));
					list.Add(magics[i * 8 + d++].Clone(1));
					magicTable[i, j++] = list.ToArray();
				}
			}
			return magicTable;
		}


		private static IList<MagicData> Load(string gameDirPath)
		{
			var dataDir = Path.Combine(gameDirPath, "Data");
			var magicDataFile = Path.Combine(dataDir, "MagicTable");
			var soundNameFile = Path.Combine(dataDir, "SoundFileName");

			var magicList = new List<MagicData>();

			using (var magicReader = new StreamReader(magicDataFile, Global.Encoding))
			using (var soundReader = new StreamReader(soundNameFile, Global.Encoding))
			{
				var sc = new Scanner(magicReader);
				var soundScanner = new Scanner(soundReader);

				int count = 0;
				while(sc.HasNext() && soundScanner.HasNext() && soundScanner.Current() != "End")
				{
					// シナリオに非依存な魔法のインデックスを利用して属性を分ける
					var element = AttackTypes.Magic[count / 8];

					magicList.Add(new MagicData(sc.Next(), (MagicType)sc.NextInt32(),
						new[]{
							new MagicCore((byte)sc.NextInt32(), (byte)sc.NextInt32(), (byte)sc.NextInt32(), (byte)sc.NextInt32()),
							new MagicCore((byte)sc.NextInt32(), (byte)sc.NextInt32(), (byte)sc.NextInt32(), (byte)sc.NextInt32()),
							new MagicCore((byte)sc.NextInt32(), (byte)sc.NextInt32(), (byte)sc.NextInt32(), (byte)sc.NextInt32())
						},
						sc.Next(), element, soundScanner.Next(), "Magic" + count + ".bmp"
					));

					count++;
				}
			}
			return magicList;
		}
	}
}
