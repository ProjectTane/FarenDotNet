using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using Paraiba.Drawing;
using Paraiba.Drawing.Surfaces;
using FarenDotNet.BasicData;

namespace FarenDotNet.Loader
{
	public class UnitDataLoader
	{
		private static UnitData ReadUnit(IEnumerable<string> charaDirPathes, BinaryReader reader, bool isMaster)
		{
			var unit = new UnitData();
			unit.IsMaster = isMaster; // マスターかどうか
			// ユニット名
			//unsafe {
			//    sbyte* buf = stackalloc sbyte[22];
			//    for (int i = 0; i < 21; i++)
			//        buf[i] = reader.ReadSByte();
			//    buf[21] = 0;
			//    unit.Name = new String(buf);
			//}
			unit.Name = BytesToStr(reader.ReadBytes(21));

			unit.MagicLevel = reader.ReadBytes(6); // 魔法レベル（火～闇）
			unit.Resistivity = new Resistivity(reader.ReadBytes(15));
			unit.IsUnique = (reader.ReadByte() != 0); // 人材ユニットかどうか
			unit.IsVagrant = (reader.ReadByte() == 0); // 放浪ユニットかどうか
			unit.IsSpecial = (reader.ReadUInt16() != 0); // 特殊ユニットかどうか
			unit.Species = (Species)reader.ReadUInt16(); // 種族
			unit.Cost = reader.ReadUInt16(); // 費用
			unit.MoveType = (MoveType)reader.ReadUInt16(); // 移動タイプ
			unit.Mobility = ReadByDecimal(reader.ReadUInt16()); // 移動量

			// ステータス
			unit.HP = reader.ReadUInt16();
			unit.MP = reader.ReadUInt16();
			unit.Atk = reader.ReadUInt16();
			unit.Def = reader.ReadUInt16();
			unit.Tec = reader.ReadUInt16();
			unit.Agi = reader.ReadUInt16();
			unit.Mag = reader.ReadUInt16();
			unit.Res = reader.ReadUInt16();

			unit.Morale = reader.ReadUInt16(); // 士気
			unit.Courage = reader.ReadUInt16(); // 勇猛
			unit.HpHeal = reader.ReadUInt16();
			unit.MpHeal = reader.ReadUInt16();

			unit.SkillIndex = reader.ReadUInt16();
			unit.SkillTimes = reader.ReadUInt16();
			reader.ReadUInt16(); // SKIP				// 不明
			unit.ClassChangeNo = reader.ReadUInt16();
			unit.Exp = reader.ReadUInt16();

			unit.EntryTurn = reader.ReadUInt16(); // 出現ターン
			unit.LimitTurn = reader.ReadUInt16(); // 放浪上限
			unit.HelpID = reader.ReadUInt16();

			// 通常攻撃
			const int MAX_ATTACK = 5;
			var tmpAtkAttr = new AttackType[MAX_ATTACK];
			for (int i = 0; i < MAX_ATTACK; i++)
				tmpAtkAttr[i] = (AttackType)(reader.ReadUInt16() - (UInt16)AttackType.なし);
			var tmpAtkPower = new byte[MAX_ATTACK];
			for (int i = 0; i < MAX_ATTACK; i++)
				tmpAtkPower[i] = (byte)reader.ReadUInt16();
			unit.Attacks = new DefaultAttack[MAX_ATTACK];
			for (int i = 0; i < MAX_ATTACK; i++)
				unit.Attacks[i] = new DefaultAttack(tmpAtkPower[i], tmpAtkAttr[i]);

			// 雇用可能なユニット
			unit.Summon = new int[8];
			for (int i = 0; i < unit.Summon.Count; i++)
				unit.Summon[i] = reader.ReadUInt16();

			for (int i = 0; i < 16; i++) // SKIP
				reader.ReadByte();
			unit.IsTamer = (reader.ReadByte() != 0); // ビーストマスターかどうか

			for (int i = 0; i < 5; i++) // SKIP
				reader.ReadByte();

			var imgUnit = LoadBitmap(charaDirPathes, "Char" + reader.ReadInt32() + ".bmp");
			var imgFace = LoadBitmap(charaDirPathes, "Face" + reader.ReadInt32() + ".bmp");
			var imgFlag = LoadBitmapSet(charaDirPathes, "Flag" + reader.ReadInt32() + ".bmp");

			unit.Images = new UnitImageResources(imgUnit, imgFace, imgFlag);
			// ユニットID
			//unsafe {
			//    sbyte* buf = stackalloc sbyte[22];
			//    for (int i = 0; i < 21; i++)
			//        buf[i] = reader.ReadSByte();
			//    buf[21] = 0;
			//    unit.ID = new String(buf);
			//}
			unit.ID = BytesToStr(reader.ReadBytes(21));
			for (int i = 0; i < 10; i++) // SKIP
				reader.ReadByte();

			unit.IsUndead = (reader.ReadByte() != 0); // アンデットかどうか

			for (int i = 0; i < 128; i++) // SKIP
				reader.ReadByte();
			return unit;
		}

		/// <summary>
		/// ユニットのリストをロードする。
		/// </summary>
		/// <param name="unitDirPathes">ユニットデータが保存されいてるディレクトリの一覧</param>
		/// <returns>ユニットのリスト</returns>
		public static IList<UnitData> LoadList(IList<string> unitDirPathes)
		{
			List<UnitData> result = null;
			Global.CharDirs = unitDirPathes;

			foreach (var dpath in unitDirPathes)
			{
				var unitList = new List<UnitData>();
				var fpath = Path.Combine(dpath, "CharacterData");
				using (var fs = new FileStream(fpath, FileMode.Open))
				using (var reader = new BinaryReader(fs, Global.Encoding))
				{
					reader.ReadInt32(); // 0x8C == 140固定？
					int charaEndNum = reader.ReadInt32();
					int masterEndNum = reader.ReadInt32();
					int commonEndNum = reader.ReadInt32();
					int uniqueEndNum = reader.ReadInt32();

					// Seek()メソッドの実装は保証されていないので使用しない
					for (int i = 0; i < 0x530; i++)
						reader.ReadByte();

					{
						int i = 1;
						for (; i < masterEndNum; i++)
							unitList.Add(ReadUnit(unitDirPathes, reader, true));
						for (; i < commonEndNum; i++)
							unitList.Add(ReadUnit(unitDirPathes, reader, false));
						for (; i < uniqueEndNum; i++)
							unitList.Add(ReadUnit(unitDirPathes, reader, false));
					}
				}

				// クラスチェンジの設定
				foreach (var unit in unitList)
				{
					var nextNo = unit.ClassChangeNo;
					if (nextNo > 0)
						unit.ClassChange = unitList[nextNo - 1];
				}

				if (result == null)
					result = unitList;
				else
					result.AddRange(unitList);
			}
			return result;
		}

		// ----- ----- 以下読み込みに使うメソッド ----- -----
		/// <summary>
		/// ファイルの存在を確認してからロードする
		/// </summary>
		private static Surface LoadBitmap(IEnumerable<string> charaDirPathes, string fname)
		{
			foreach (var dpath in charaDirPathes)
			{
				var fpath = Path.Combine(dpath, fname);
				if (File.Exists(fpath))
				{
					return Global.BmpManager.GetSurface(fpath, path => BitmapUtil.Load(path, Color.Black));
				}
			}
			return null;
		}

		private static IList<Surface> LoadBitmapSet(IEnumerable<string> charaDirPathes, string fname)
		{
			foreach (var dpath in charaDirPathes)
			{
				var fpath = Path.Combine(dpath, fname);
				if (File.Exists(fpath))
				{
					return Global.BmpManager.GetSurfaces(fpath, 32, 32, path => BitmapUtil.Load(path, Color.Black));
				}
			}
			return new Surface[0];
		}

		private static int ReadByDecimal(int arg)
		{
			return (arg >> 1) + (arg & 0x0F);
		}

		private static string BytesToStr(byte[] data)
		{
			int length = 0;
			for (; length < data.Length; length++)
			{
				if (data[length] == 0) break;
			}
			return Global.Encoding.GetString(data, 0, length);
		}
	}
}