using System;
using System.Collections.Generic;
using Paraiba.Core;
using FarenDotNet.BasicData;
using FarenDotNet.Loader;
using FarenDotNet.Reign;
using FarenDotNet.War.BattleAction;
using FarenDotNet.War.BattleCommand;
using FarenDotNet.War.Loader;
using FarenDotNet.War.UI;
using Paraiba.Linq;

namespace FarenDotNet.War
{
	public class WarUnitBuilder
	{
		private readonly IList<IBattleCommand> _commands;
		private readonly IList<IBattleCommand> _skillCommands;

		public WarUnitBuilder(string gamePath, IList<UnitData> units)
		{
			// スキルデータの読み込み
			var skills = SkillLoader.Load(Global.CharDirs);
			_skillCommands = SkillCommandLoader.Load(skills);

			// 魔法データの読み込み
			var magics = MagicDataLoader.ReadMagicTable(gamePath);
			// 魔法アクションの生成
			var dic = new MagicDictionary(magics);
			var warMagics = WarMagicDataLoader.Load(dic, units);
			Func<ElementSelectWindow> createWindowFunc = () => new ElementSelectWindow(warMagics);

			_commands = new IBattleCommand[] {
				new SingleActionCommand("移動", "移動", Properties.Resources.w_move, new MoveAction()),
				new SingleActionCommand("攻撃", "通常攻撃", Properties.Resources.w_attack, new NormalAttackAction()),
				new ShowWindowCommand<ElementSelectWindow>("魔法",
					"魔法", Properties.Resources.w_magic, createWindowFunc.ToLazyWrap()),
				null,
				new FinishActionCommand("待機", "行動終了", Properties.Resources.w_wait),
			};
		}


		private static List<CommandState> GetCommandStateList(IList<IBattleCommand> commandList)
		{
			const int O = 0; // 使用可能
			const int X = 1; // 使用不可能
			const int N = 2; // 非表示
			var commandEnableTable = new[,] {
				// 移動, 攻撃, 魔法, 特技, 待機
				{ O, O, O, O, O }, // 初期
				{ X, O, X, O, O }, // 移動済み
				{ X, X, X, X, O }, // 行動済み
			};

			var stateTransion = new[,] {
				// 移動, 攻撃, 魔法, 特技, 待機
				{ 1, 2, 2, 2, 2 }, // 初期
				{ 2, 2, 2, 2, 2 }, // 移動済み
				{ 2, 2, 2, 2, 2 }, // 行動済み	HACK: 本当は不要？
			};

			var states = new List<CommandState>();
			foreach (var commandEnable in commandEnableTable.GetRows())
			{
				var cmds = new List<CommandInfo>();
				commandEnable.ForEach((enable, i) => {
					if (enable != N)
						cmds.Add(new CommandInfo(commandList[i], enable == O));
				});

				var state = new CommandState();
				state.CommandInfos = cmds;
				states.Add(state);
			}

			stateTransion.GetRows().ForEach((trans, i) => {
				var transTable = new Dictionary<IBattleCommand, CommandState>();
				commandEnableTable.GetRow(i)
					.Zip(trans)
					.ForEach((t, j) => {
						// 使用可能ならば状態遷移を生成
						if (t.Item1 == O)
							transTable.Add(commandList[j], states[t.Item2]);
					});
				states[i].Transition = transTable;
			});

			return states;
		}


		public WarUnit Create(Unit unit, WarSide side, Area area)
		{
			_commands[3] = _skillCommands[unit.SkillIndex];
			var states = GetCommandStateList(_commands);
			return new WarUnit(unit, side, states[0], area);
		}
	}
}