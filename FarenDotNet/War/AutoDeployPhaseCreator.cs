using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FarenDotNet.War.UI;
using FarenDotNet.War.Phase;
using Paraiba.Geometry;
using FarenDotNet.Reign;

namespace FarenDotNet.War
{
	public class AutoDeployPhaseCreator : PhaseCreator
	{
		public AutoDeployPhaseCreator(Area battleArea, Province friendProvince, Action battleFinish)
		: base(battleArea, friendProvince, battleFinish)
		{
		}

		protected override void CreateDeployUnitPhase(WarPresentationModel model, List<List<WarUnit>> friendUnits, List<WarUnit> enemyUnits, MainWindow mainWindow)
		{
			Phases.DeployUnitPhase.Start = situation_ =>
			{
				// 味方軍の配置
				int i = 0;
				foreach (var funits in friendUnits)
				{
					foreach (var unit in funits)
					{
						situation_.Map.Deploy(unit, new Point2(i + 5, 5));
						i++;
					}
				}

				//敵軍の配置
				i = 0;
				foreach (var unit in enemyUnits)
				{
					situation_.Map.Deploy(unit, new Point2(i + 3, 0));
					i++;
				}

				//初期配置描画をなくすために、nullをつっこむ
				situation_.Map.InitDeployCandidate = null;

				situation_.PhaseManager.ChangePhase(situation_, Phases.BattlePhase);
			};
		}
	}
}
