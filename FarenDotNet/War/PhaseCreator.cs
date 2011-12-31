using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Paraiba.Drawing.Animations.Surfaces.Sprites;
using Paraiba.Utility;
using FarenDotNet.War.UI;
using FarenDotNet.War.Phase;
using Paraiba.Geometry;
using FarenDotNet.War.BattleAction;
using Paraiba.TaskList;
using FarenDotNet.Reign;
using System.Windows.Forms;

namespace FarenDotNet.War
{
	public enum BattleResult
	{
		Win, Lose, TwentyTurn, None
	};

	public class PhaseCreator
	{
		protected BattleResult _result;
		protected Area _battleArea;
		protected Province _friendProvince;
		protected Action _battleFinish;

		public PhaseCreator(Area battleArea, Province friendProvince, Action battleFinish)
		{
			_result = BattleResult.None;
			_battleArea = battleArea;
			_friendProvince = friendProvince;
			_battleFinish = battleFinish;
		}

		public void CreatePhase(WarPresentationModel model, List<List<WarUnit>> friendUnits, List<WarUnit> enemyUnits, MainWindow mainWindow)
		{
			CreateDeployUnitPhase(model, friendUnits, enemyUnits, mainWindow);
			CreateBattlePhase(model, friendUnits, enemyUnits, mainWindow);
			CreateTurnPhase(model, friendUnits, enemyUnits, mainWindow);
			CreateInitiativePhase(model, friendUnits, enemyUnits, mainWindow);
		}

		protected virtual void CreateInitiativePhase(WarPresentationModel model, List<List<WarUnit>> friendUnits, List<WarUnit> enemyUnits, MainWindow mainWindow)
		{
			Phases.InitiativePhase.Start = situation_ =>
			{
				model.MapWindow.Refresh();
				if (situation_.ActiveUnit.Side.IsPlayer)
				{
					// ユニットのコマンド状態を初期化する
					situation_.ActiveUnit.ResetCommandState();
				}
				else
				{
					#region AI

					var doer = situation_.ActiveUnit;
					var ar = new ActionArguments(situation_, model);

					var actionEnumrator = new AI.AI().GetActionWithCoroutine(situation_);


					Action actionCoroutine = null;

					// AIの行動選択と実行のコルーチン
					actionCoroutine = () =>
					{
						// 非同期実行のためのAIの行動選択デリゲートの生成
						Func<bool> selectNextAction = actionEnumrator.MoveNext;
						var asyncResult = selectNextAction.BeginInvoke(null, null);

						// TaskList にAIの行動選択完了後の、戦闘行動実行を予約する
						// TaskList を使うことで、ウィンドウメッセージ処理のスレッドによる同期実行が行われる
						TaskList<int>.Task task = null;
						task = (taskArgs_, time_) =>
						{
							// 非同期実行が完了するまでは何もしない
							if (!asyncResult.IsCompleted) return;

							// 非同期実行が完了すれば、戻り値を取得する
							if (selectNextAction.EndInvoke(asyncResult))
							{
								// AIが選択したActionが存在する
								var battleAction = actionEnumrator.Current.Item1;
								battleAction.BootAI(ar, doer, Delegates.NOP);
								battleAction.Execute(ar, doer, actionEnumrator.Current.Item2, actionCoroutine);
							}
							else
							{
								// AIが選択したActionが存在しないので、ユニットの行動終了
								situation_.PhaseManager.ExitPhase(situation_);
							}
							// タスクリストからこのデリゲートを削除する
							Global.MainLoop.TickEvents.Remove(task);
						};
						Global.MainLoop.TickEvents.Add(Double.MinValue, task);
					};

					// AIの行動選択と実行のコルーチンを実行
					actionCoroutine();

					#endregion
				}
			};

			Phases.InitiativePhase.Exit = situation_ =>
			{
				var friends = situation_.Sides.Single(side => side.IsPlayer);
				var enemies = situation_.Sides.Single(side => !side.IsPlayer);

				_result = checkBattleEnd(situation_, friends, enemies);
				if (_result != BattleResult.None)
				{
					situation_.PhaseManager.ExitPhase(situation_);
					return;
				}

				model.CancelCommandStack.Clear();
				situation_.ActiveUnit.Side.UpdateForceAndBrave(situation_.Units.Alive);

				var unit = situation_.TurnManager.GetInitiativeUnit(situation_);
				if (unit != null)
				{
					situation_.ActiveUnit = unit;
					situation_.PhaseManager.StartPhase(situation_, Phases.InitiativePhase);
				}
				else
				{
					situation_.PhaseManager.ExitPhase(situation_);
				}
			};
		}

		protected virtual void CreateTurnPhase(WarPresentationModel model, List<List<WarUnit>> friendUnits, List<WarUnit> enemyUnits, MainWindow mainWindow)
		{
			Phases.TurnPhase.Start = situation_ =>
			{
				situation_.TurnManager.Turn++;
				mainWindow.SetTurn(situation_.TurnManager.Turn);

				var unit = situation_.TurnManager.GetInitiativeUnit(situation_);
				if (unit != null)
				{
					situation_.ActiveUnit = unit;
					situation_.PhaseManager.StartPhase(situation_, Phases.InitiativePhase);
				}
				else
				{
					situation_.PhaseManager.ExitPhase(situation_);
				}
			};

			Phases.TurnPhase.Exit = situation_ =>
			{
				if (situation_.TurnCount >= 20)
					_result = BattleResult.TwentyTurn;

				if (_result != BattleResult.None)
				{
					situation_.PhaseManager.ExitPhase(situation_);
					return;
				}

				foreach (var unit in situation_.Units.Alive)
				{
					if (unit.Status.HpAutoHeal > 0)
					{
						unit.HealHP(situation_, unit, unit.Status.HpAutoHeal);
						model.SetHealAnimationOnMap(unit.Status.HpAutoHeal, unit.Location, null);
					}
					if (unit.Status.MpAutoHeal > 0)
					{
						unit.HealMP(situation_, unit, unit.Status.MpAutoHeal);
						var anime = model.CreateStringAnimationOnMap(unit.Status.MpAutoHeal.ToString(), unit.Location, Color.Blue, 500);
						model.ChipAnimations.Add(new ExtendTimeAnimationSprite(anime, 700, 0));
					}
				}
				situation_.PhaseManager.StartPhase(situation_, Phases.TurnPhase);
			};
		}

		protected virtual void CreateBattlePhase(WarPresentationModel model, List<List<WarUnit>> friendUnits, List<WarUnit> enemyUnits, MainWindow mainWindow)
		{
			Phases.BattlePhase.Start = situation_ =>
			{
				situation_.PhaseManager.StartPhase(situation_, Phases.TurnPhase);
			};

			Phases.BattlePhase.Exit = situation_ =>
			{
				//TODO: area更新、経験値はリアルタイムで
				//_battleArea.Unitsを更新

				//街、道、壁の更新
				updateAreaWithCityRoadWall(situation_.Map);

				switch (_result)
				{
					case BattleResult.Win:
						if (_friendProvince != null)
							_battleArea.Province = _friendProvince;
						break;
					case BattleResult.Lose:
						break;
					case BattleResult.TwentyTurn:
						MessageBox.Show("20ターン経過しました。消耗戦になるので退却します。");
						break;
					default:
						break;
				}
				mainWindow.Dispose();
				_battleFinish();
			};
		}

		protected virtual void CreateDeployUnitPhase(WarPresentationModel model, List<List<WarUnit>> friendUnits, List<WarUnit> enemyUnits, MainWindow mainWindow)
		{
			Phases.DeployUnitPhase.Start = situation_ =>
			{
				var deployWindow = new DeploymentWindow(friendUnits, model, situation_.Map, () =>
				{
					//敵軍の配置
					int i = 0;
					foreach (var unit in enemyUnits)
					{
						situation_.Map.Deploy(unit, new Point2(i + 10, 10));
						i++;
					}

					//いらなくなったので、早めにGCで消えてもらう
					friendUnits = null;
					enemyUnits = null;

					situation_.PhaseManager.ChangePhase(situation_, Phases.BattlePhase);
				});

				deployWindow.Show();
			};
		}

		private void updateAreaWithCityRoadWall(WarMap map)
		{
			int city = 0, road = 0, wall = 0;
			for (int widht = 0; widht < map.Width; widht++)
			{
				for (int height = 0; height < map.Height; height++)
				{
					var land = map[widht, height];
					if (land.Construct == null)
						continue;

					switch (land.Construct.Info.Name)
					{
						case "街":
							city++;
							break;
						case "道":
							road++;
							break;
						case "壁":
							wall++;
							break;
					}
				}
			}
			_battleArea.NumCity100 = city * 100;
			_battleArea.NumRoad100 = road * 100;
			_battleArea.NumWall100 = wall * 100;
		}

		private BattleResult checkBattleEnd(Situation situation_, WarSide friends, WarSide enemy)
		{
			var friendMaster = situation_.Units.WarUnits.SingleOrDefault((unit) => { return unit.IsMaster && unit.Side.IsPlayer; });
			if (friendMaster != null && !friendMaster.Alive)
			{
				MessageBox.Show("マスターが死亡しました。あなたの負けです。");
				return BattleResult.Lose;
			}

			var enemyMaster = situation_.Units.WarUnits.SingleOrDefault((unit) => { return unit.IsMaster && !unit.Side.IsPlayer; });
			if (enemyMaster != null && !enemyMaster.Alive)
			{
				MessageBox.Show("相手のマスターを倒しました。あなたの勝ちです。");
				return BattleResult.Win;
			}


			if (checkAnnihilation(situation_, enemy))
			{
				MessageBox.Show("相手を全滅させました。あなたの勝ちです。");
				return BattleResult.Win;
			}

			if (checkAnnihilation(situation_, friends))
			{
				MessageBox.Show("全滅しました。あなたの負けです。");
				return BattleResult.Lose;
			}

			return BattleResult.None;
		}

		/// <summary>
		/// 指定してWarSideのユニットが全員死亡しているか、退却していればtrue
		/// </summary>
		/// <param name="situation_"></param>
		/// <param name="side"></param>
		/// <returns></returns>
		private bool checkAnnihilation(Situation situation_, WarSide side)
		{
			return situation_.Units.WarUnits.Where(unit => unit.Side == side)
				.All(unit => unit.IsEscaped || !unit.Alive);
		}

	}
}
