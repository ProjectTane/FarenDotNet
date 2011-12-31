using System.Collections.Generic;
using System.IO;
using Paraiba.Core;
using Paraiba.Geometry;
using Paraiba.IO;
using Paraiba.Linq;

namespace FarenDotNet.War.Loader
{
	public class WarMapLoader
	{
		private readonly string _scenarioPath;

		public WarMapLoader(string scenarioPath)
		{
			_scenarioPath = scenarioPath;
		}

		public static Point2 AtatCoordToGameCoord(Point2 p)
		{
			return new Point2(p.X + (p.Y + 1) / 2, p.Y);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="stageFileName"></param>
		/// <param name="mapChipImageFileName"></param>
		/// <param name="objectFileName"></param>
		/// <param name="town"></param>
		/// <param name="wall"></param>
		/// <param name="road"></param>
		/// <param name="directionsToWarArea">攻め込む方向のリスト。昇順にソートされているのが前提</param>
		/// <returns></returns>
		public WarMap Load(string stageFileName, int town, int wall, int road, IEnumerable<int> directionsToWarArea)
		{
			const int NUMOFMAPCHIP = 32;
			const int DEPLOY_CANDIDATE_NUM = 20;
			const int ENDCITY = 15, ENDWALL = 24, ENDSTREET = 39;

			string gameDataPath = Path.Combine(_scenarioPath, "Data");

			var mapchipID = new int[NUMOFMAPCHIP,NUMOFMAPCHIP];
			var objectID = new int[NUMOFMAPCHIP,NUMOFMAPCHIP];
			var height = new int[NUMOFMAPCHIP,NUMOFMAPCHIP];
			var initDeployCandidates = new List<List<Point2>>();

			using (var filestream = new FileStream(Path.Combine(gameDataPath, stageFileName), FileMode.Open))
			using (var reader = new BinaryReader(filestream))
			{
				// (x,y) = (0,0), (0,1), ..., (0,MAX), (1,0) という順序でチップが記述されている

				// マップチップID
				for (int x = 0; x < NUMOFMAPCHIP; x++)
				{
					for (int y = 0; y < NUMOFMAPCHIP; y++)
					{
						mapchipID[x, y] = reader.ReadByte();
					}
				}
				// オブジェクトID（0は移動不可(ダミー), 1はオブジェクトなし）
				for (int x = 0; x < NUMOFMAPCHIP; x++)
				{
					for (int y = 0; y < NUMOFMAPCHIP; y++)
					{
						objectID[x, y] = reader.ReadByte() + 1;
					}
				}
				// 半分しか表示されないチップを移動不可に設定
				for (int y = 0; y < NUMOFMAPCHIP; y++)
				{
					objectID[0, y] = 0;
					if (!(++y < NUMOFMAPCHIP))
						break;
					objectID[NUMOFMAPCHIP - 1, y] = 0;
				}
				// オブジェクトの生成順序
				for (int x = 0; x < NUMOFMAPCHIP; x++)
				{
					for (int y = 0; y < NUMOFMAPCHIP; y++)
					{
						int rankOrder = reader.ReadByte();
						int id = objectID[x, y] - 1;
						if (id <= 0)
							continue;
						// 指定した個数以上の順序を持つオブジェクトを削除
						if (id <= ENDCITY)
						{
							// 都市
							if (rankOrder > town)
								objectID[x, y] = 1;
						}
						else if (id <= ENDWALL)
						{
							// 壁
							if (rankOrder > wall)
								objectID[x, y] = 1;
						}
						else if (id <= ENDSTREET)
						{
							// 道
							if (rankOrder > road)
								objectID[x, y] = 1;
						}
					}
				}
				// 高さの読み込み
				for (int x = 0; x < NUMOFMAPCHIP; x++)
				{
					for (int y = 0; y < NUMOFMAPCHIP; y++)
					{
						height[x, y] = reader.ReadByte();
					}
				}

				// 座標(x, y)をそれぞれ1+1=2バイトで表現。20組の座標で1エリアからの初期配置候補
				// 攻め込む方向により初期配置候補が異なるので、実際に攻め込んだ方向になるまで読み飛ばす。
				directionsToWarArea.ConcatFirst(0)
					.Zip2Chain()
					.ForEach(t => {
						var list = new List<Point2>();
						reader.SkipBytes((t.Item2 - t.Item1) * 2 * DEPLOY_CANDIDATE_NUM);

						for (int i = 0; i < DEPLOY_CANDIDATE_NUM; i++)
						{
							int x = reader.ReadByte(), y = reader.ReadByte();
							list.Add(AtatCoordToGameCoord(new Point2(x, y)));
						}
						initDeployCandidates.Add(list);
					});
			}

			// 各チップの生成
			var lands = new Land[NUMOFMAPCHIP,NUMOFMAPCHIP];
			for (int x = 0; x < lands.GetLength(0); x++)
			{
				for (int y = 0; y < lands.GetLength(1); y++)
				{
					lands[x, y] = new Land(height[x, y], WarGlobal.Landforms[mapchipID[x, y]],
						objectID[x, y] == 0 ? null : WarGlobal.Constructs[objectID[x, y]]);
				}
			}

			return new WarMap(lands, lands[0, 0], new FarenMoveCalculator(), initDeployCandidates);
		}
	}
}