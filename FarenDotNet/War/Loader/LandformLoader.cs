using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using Paraiba.Drawing;
using Paraiba.IO;
using FarenDotNet.BasicData;

namespace FarenDotNet.War.Loader
{
	public class LandformLoader
	{
		private readonly string _scenarioPath;
		public const int EndCity = 15, EndWall = 24, EndStreet = 39;

		public LandformLoader(string scenarioPath)
		{
			_scenarioPath = scenarioPath;
		}

		private List<LandformInfo> LoadInfo()
		{
			const int nMoveType = 10;

			var landformNames = new[] {
				"移動不可",
				null, "平地", "木", "岩", "雪木", "浅瀬", "海", "深海", "荒地", "雪原",
				"砂漠", "山地", "火口", "沼地", "森", null, "街", "壁", "道", "城",
			};
			var neededMobilities = new int[landformNames.Length][];
			var techniqueOffsets = new int[landformNames.Length][];
			for (int i = 0; i < landformNames.Length; i++)
			{
				neededMobilities[i] = new int[nMoveType];
				techniqueOffsets[i] = new int[nMoveType];
			}

			var path = Path.Combine(_scenarioPath, Path.Combine("Data", "Move&Skill"));
			using (var reader = new StreamReader(path, WarGlobal.Encoding))
			{
				var sc = new Scanner(reader);
				for (int i = 0; i < nMoveType; i++)
				{
					// 移動不可
					neededMobilities[0][i] = 999;
					for (int j = 1; j < landformNames.Length; j++)
					{
						neededMobilities[j][i] = sc.NextInt32();
						techniqueOffsets[j][i] = sc.NextInt32();
					}
				}
			}

			var landforms = new List<LandformInfo>(landformNames.Length);
			for (int i = 0; i < landformNames.Length; i++)
			{
				landforms.Add(new LandformInfo(landformNames[i], neededMobilities[i], techniqueOffsets[i]));
			}
			return landforms;
		}

		public void Load()
		{
			string gamePicturePath = Path.Combine(_scenarioPath, "Picture");
			string gameDataPath = Path.Combine(_scenarioPath, "Data");

			// 地形タイプ情報の読み込み
			var landformInfos = LoadInfo();

			// 各チップ画像の取得
			var landImages = WarGlobal.BmpManager.GetSurfaces(
			    Path.Combine(gamePicturePath, "BMAPCHAR.BMP"), WarGlobal.ChipSize);

			var objectImages = WarGlobal.BmpManager.GetSurfaces(
			    Path.Combine(gamePicturePath, "BMAPOBJ.BMP"), WarGlobal.ChipSize,
			    path => BitmapUtil.Load(path, Color.Black));

			// 地形チップの生成
			var landforms = new Landform[landImages.Count];
			// 地形タイプの読み込み
			using (var reader = new StreamReader(Path.Combine(gameDataPath, "TikeiType"), WarGlobal.Encoding))
			{
				var sc = new Scanner(reader);
				for (int i = 0; i < landforms.Length; i++)
				{
					int id = sc.NextInt32(NumberStyles.HexNumber);
					landforms[i] = new Landform(landImages[i], landformInfos[id + 1]);
				}
			}
			WarGlobal.Landforms = landforms;

			// オブジェクトチップの生成
			var constructs = new Construct[objectImages.Count + 2];
			// 0番目は移動不可, １番目はnull
			for (int i = 2; i < objectImages.Count; i++)
			{
				int id;
				if (i > EndWall)
					id = 18;
				else if (i > EndCity)
					id = 17;
				else
					id = 16;
				constructs[i] = new Construct(objectImages[i - 1], landformInfos[id + 1]);
			}
			// 移動不可オブジェクトの生成
			constructs[0] = new Construct(null, landformInfos[0]);

			WarGlobal.Constructs = constructs;
		}
	}
}
