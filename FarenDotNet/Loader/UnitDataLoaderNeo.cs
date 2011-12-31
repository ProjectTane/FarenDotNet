using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarenDotNet.BasicData;
using System.IO;

namespace FarenDotNet.Loader
{
	public class UnitDataLoaderNeo
	{
		const int OFFSET = 0x544;
		const int UNIT_SIZE = 0x144;
	
		// ----- ----- ----- PUBLIC ----- ----- -----
		public IList<UnitData> LoadUnits(IEnumerable<string> dirs)
		{
			var list = new List<UnitData>();
			foreach (var dir in dirs)
				LoadUnits(dir, list, true, true, true);
			return list;
		}

		public IList<UnitData> LoadUnits(string dir)
		{
			var list = new List<UnitData>();
			LoadUnits(dir, list, true, true, true);
			return list;
		}

		// ----- ----- ----- PRIVATE ----- ----- -----
		private void LoadUnits(string dir, List<UnitData> list,
			bool createMaster, bool createCommon, bool createUnique)
		{
			string filePath = Path.Combine(dir, "CharacterData");
			using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
			using (var reader = new BinaryReader(file)){
				reader.ReadInt32();     // 0x8C == 140固定？
				int charaEndNum = reader.ReadInt32();
				int masterEndNum = reader.ReadInt32();
				int commonEndNum = reader.ReadInt32();
				int uniqueEndNum = reader.ReadInt32();

				if (createMaster) {
					int count = masterEndNum - 1;
					var buff = new byte[count * UNIT_SIZE];
					file.Read(buff, OFFSET, buff.Length);
					var m = new MemoryStream(buff);
				}
				if (createMaster) {
					int count = commonEndNum - masterEndNum;
					var buff = new byte[count * UNIT_SIZE];
					file.Read(buff, OFFSET + masterEndNum * UNIT_SIZE, buff.Length);
					var m = new MemoryStream(buff);
				}
				if (createMaster) {
					int count = uniqueEndNum - commonEndNum;
					var buff = new byte[count * UNIT_SIZE];
					file.Read(buff, OFFSET + commonEndNum * UNIT_SIZE, buff.Length);
					var m = new MemoryStream(buff);
				}
			}
		}

		private UnitData LoadMaster(MemoryStream stream, int count)
		{

			return null;
		}
	}
}
