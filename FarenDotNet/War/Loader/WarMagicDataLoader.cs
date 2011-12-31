using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using Paraiba.Drawing;
using Paraiba.Core;
using FarenDotNet.BasicData;
using FarenDotNet.War.AbnormalCondition;
using FarenDotNet.War.BattleAction;
using FarenDotNet.War.BattleAction.ScreenEffect;
using FarenDotNet.War.Scope;
using FarenDotNet.War.UI;
using Paraiba.Linq;

namespace FarenDotNet.War.Loader
{
	public class WarMagicDataLoader
	{
		private static readonly Func<IScope, MagicCore, AttackType, IScreenEffect, IBattleAction>
			_clearConditionCreator = (scope, core, type, effect) =>
				new ClearBadConditonAction(scope, new MPCost(core.MPCost), effect);

		private static readonly Func<IScope, MagicCore, AttackType, IScreenEffect, IBattleAction>
			_defaultCreator = (scope, core, type, effect) =>
				new MagicAction(scope, new MPCost(core.MPCost), core.Power, type, effect);

		private static readonly Func<IScope, MagicCore, AttackType, IScreenEffect, IBattleAction>
			_delugeCreator = (scope, core, type, effect) =>
				new DelugeAction(scope, new MPCost(core.MPCost), effect, core.Power, type, WarGlobal.Landforms[16]);

		private static readonly Func<IScope, MagicCore, AttackType, IScreenEffect, IBattleAction>
			_volcanoCreator = (scope, core, type, effect) =>
				new VolcanoAction(scope, new MPCost(core.MPCost), effect, core.Power, type);

		private static readonly Func<IScope, MagicCore, AttackType, IScreenEffect, IBattleAction>
			_wallCrashCreator = (scope, core, type, effect) =>
				new WallCrashAction(scope, new MPCost(core.MPCost), effect, core.Power, type);

		private static readonly Func<IScope, MagicCore, AttackType, IScreenEffect, IBattleAction>
			_requiemCreator = (scope, core, type, effect) =>
				new RequiemAction(scope, new MPCost(core.MPCost), effect, core.Power, type);

		public static IList<IList<WarMagicData>>[,] Load(MagicDictionary magicTable, IList<UnitData> unitDatas)
		{
			var magicAttributeTypes = AttackTypes.Magic;
			var magicCommands = new IList<IList<WarMagicData>>[6,6];

			for (int j = 0; j < magicAttributeTypes.Count; j++)
			{
				switch (magicAttributeTypes[j])
				{
				case AttackType.火:
					CreateFireMagic(magicCommands, j, magicTable, unitDatas);
					break;
				case AttackType.水:
					CreateWaterMagic(magicCommands, j, magicTable, unitDatas);
					break;
				case AttackType.風:
					CreateWindMagic(magicCommands, j, magicTable, unitDatas);
					break;
				case AttackType.土:
					CreateSoilMagic(magicCommands, j, magicTable, unitDatas);
					break;
				case AttackType.光:
					CreateShineMagic(magicCommands, j, magicTable, unitDatas);
					break;
				case AttackType.闇:
					CreateDarkMagic(magicCommands, j, magicTable, unitDatas);
					break;
				default:
					throw new ArgumentOutOfRangeException("magicAttributeTypes", "魔法の属性ではない属性です");
				}
			}
			return magicCommands;
		}

		private static void CreateDarkMagic(IList<IList<WarMagicData>>[,] magicCommands,
			int j, MagicDictionary magicDic, IList<UnitData> unitDatas)
		{
			for (byte level = 0; level <= 5; level++)
			{
				var magicList = magicDic[AttackType.闇, level];
				var magicDataList = new List<IList<WarMagicData>>(magicList.Count);

				magicList.ForEach((magic_, i_) => {
					IList<WarMagicData> warMagicData = null;
					switch (i_)
					{
					case 0:
						//"ブラッドサック"
						warMagicData = CreateMagic(magic_, AttackType.吸収, true, _defaultCreator, TargetType.ENEMY);
						break;
					case 1:
						//"ポイズンクラッシュ"
						warMagicData = CreateMagic(magic_, AttackType.毒, true, _defaultCreator, TargetType.ENEMY);
						break;
					case 2:
						//"マジック"
						warMagicData = CreateStatusChangeMagic(magic_, true, StatusKey.Mag, TargetType.FRIEND);
						break;
					case 3:
						//"パラライズ"
						warMagicData = CreateAbnormalConditionMagic(magic_, true, new ParalysisCondition(), TargetType.ENEMY);
						break;
					case 4:
						//"エレメント"
						warMagicData = CreateSummonMagic(magic_, unitDatas, "DarkElement");
						break;
					case 5:
						//"イリュージョン"
						warMagicData = CreateAbnormalConditionMagic(magic_, true, new PhantasmCondition(), TargetType.ENEMY);
						break;
					case 6:
						//"デス"
						warMagicData = CreateMagic(magic_, AttackType.死, false, _defaultCreator, TargetType.ENEMY);
						break;
					case 7:
						//"エクスプロージョン"
						warMagicData = CreateMagic(magic_, magic_.Element, false, _defaultCreator, TargetType.ENEMY);
						break;
					default:
						break;
					}
					magicDataList.Add(warMagicData);
				});

				magicCommands[j, level] = magicDataList;
			}
		}

		private static void CreateShineMagic(IList<IList<WarMagicData>>[,] magicCommands,
			int j, MagicDictionary magicDic, IList<UnitData> unitDatas)
		{
			for (byte level = 0; level <= 5; level++)
			{
				var magicList = magicDic[AttackType.光, level];
				var magicDataList = new List<IList<WarMagicData>>(magicList.Count);

				magicList.ForEach((magic_, i_) => {
					IList<WarMagicData> warMagicData = null;
					switch (i_)
					{
					case 0:
						//"シャイニング"
						warMagicData = CreateMagic(magic_, magic_.Element, true, _defaultCreator, TargetType.ENEMY);
						break;
					case 1:
						//"ヒール"
						warMagicData = CreateMagic(magic_, AttackType.体力回復, true, _defaultCreator, TargetType.FRIEND);
						break;
					case 2:
						//"レジスト"
						warMagicData = CreateStatusChangeMagic(magic_, true, StatusKey.Res, TargetType.FRIEND);
						break;
					case 3:
						//"キュアオール"
						warMagicData = CreateMagic(magic_, AttackType.なし, true, _clearConditionCreator, TargetType.FRIEND);
						break;
					case 4:
						//"オールヒール"
						warMagicData = CreateMagic(magic_, AttackType.体力回復, true, _defaultCreator, TargetType.FRIEND);
						break;
					case 5:
						//"エレメント"
						warMagicData = CreateSummonMagic(magic_, unitDatas, "LightElement");
						break;
					case 6:
						//"レクイエム"
						warMagicData = CreateMagic(magic_, magic_.Element, false, _requiemCreator, TargetType.ENEMY);
						break;
					case 7:
						//"ノヴァ"
						warMagicData = CreateMagic(magic_, magic_.Element, false, _defaultCreator, TargetType.ENEMY);
						break;
					default:
						break;
					}
					magicDataList.Add(warMagicData);
				});

				magicCommands[j, level] = magicDataList;
			}
		}

		private static void CreateSoilMagic(IList<IList<WarMagicData>>[,] magicCommands,
			int j, MagicDictionary magicDic, IList<UnitData> unitDatas)
		{
			for (byte level = 0; level <= 5; level++)
			{
				var magicList = magicDic[AttackType.土, level];
				var magicDataList = new List<IList<WarMagicData>>(magicList.Count);

				magicList.ForEach((magic_, i_) => {
					IList<WarMagicData> warMagicData = null;
					switch (i_)
					{
					case 0:
						//"アシッドクラウド"
						warMagicData = CreateMagic(magic_, magic_.Element, true, _defaultCreator, TargetType.ENEMY);
						break;
					case 1:
						//"キュアスリープ"
						warMagicData = CreateAbnormalConditionMagic(magic_, true, new SleepCondition(), TargetType.FRIEND);
						break;
					case 2:
						//"アーマー"
						warMagicData = CreateStatusChangeMagic(magic_, true, StatusKey.Def, TargetType.FRIEND);
						break;
					case 3:
						//"グラビテイト"
						warMagicData = CreateAbnormalConditionMagic(magic_, true, new AttackCountChangeCondition(-1), TargetType.ENEMY);
						break;
					case 4:
						//"エレメント"
						warMagicData = CreateSummonMagic(magic_, unitDatas, "SoilElement");
						break;
					case 5:
						//"ストーンエッジ"
						warMagicData = CreateAbnormalConditionMagic(magic_, true, new PetrifactionCondition(), TargetType.ENEMY);
						break;
					case 6:
						//"アースクウェイク"
						warMagicData = CreateMagic(magic_, magic_.Element, false, _defaultCreator, TargetType.ENEMY);
						break;
					case 7:
						//"ウォールスマッシュ"
						warMagicData = CreateMagic(magic_, magic_.Element, false, _wallCrashCreator, TargetType.ENEMY);
						break;
					default:
						break;
					}
					magicDataList.Add(warMagicData);
				});

				magicCommands[j, level] = magicDataList;
			}
		}

		private static void CreateWaterMagic(IList<IList<WarMagicData>>[,] magicCommands,
			int j, MagicDictionary magicDic, IList<UnitData> unitDatas)
		{
			for (byte level = 0; level <= 5; level++)
			{
				var magicList = magicDic[AttackType.水, level];
				var magicDataList = new List<IList<WarMagicData>>(magicList.Count);

				magicList.ForEach((magic_, i_) => {
					IList<WarMagicData> warMagicData = null;
					switch (i_)
					{
					case 0:
						//"フリーズ"
						warMagicData = CreateMagic(magic_, magic_.Element, true, _defaultCreator, TargetType.ENEMY);
						break;
					case 1:
						//"キュアストーン"
						warMagicData = CreateAbnormalConditionMagic(magic_, true, new PetrifactionCondition(), TargetType.FRIEND);
						break;
					case 2:
						//"スキル"
						warMagicData = CreateStatusChangeMagic(magic_, true, StatusKey.Tec, TargetType.FRIEND);
						break;
					case 3:
						//"フレイムガード"
						warMagicData = CreateAbnormalConditionMagic(magic_, true,
							new ResistivityChangeCondition(magic_.Element, ResistivityType.強い), TargetType.FRIEND);
						break;
					case 4:
						//"エレメント"
						warMagicData = CreateSummonMagic(magic_, unitDatas, "WaterElement");
						break;
					case 5:
						//"マジックリカバー"
						warMagicData = CreateStatusChangeMagic(magic_, true, StatusKey.MpAutoHeal, TargetType.FRIEND);
						break;
					case 6:
						//"ブリザード"
						warMagicData = CreateMagic(magic_, magic_.Element, false, _defaultCreator, TargetType.ENEMY);
						break;
					case 7:
						//"デリュージ"
						warMagicData = CreateMagic(magic_, magic_.Element, false, _delugeCreator, TargetType.ENEMY);
						break;
					default:
						break;
					}
					magicDataList.Add(warMagicData);
				});

				magicCommands[j, level] = magicDataList;
			}
		}

		private static void CreateWindMagic(IList<IList<WarMagicData>>[,] magicCommands,
			int j, MagicDictionary magicDic, IList<UnitData> unitDatas)
		{
			for (byte level = 0; level <= 5; level++)
			{
				var magicList = magicDic[AttackType.風, level];
				var magicDataList = new List<IList<WarMagicData>>(magicList.Count);

				magicList.ForEach((magic_, i_) => {
					IList<WarMagicData> warMagicData = null;
					switch (i_)
					{
					case 0:
						//"エアカッター"
						warMagicData = CreateMagic(magic_, magic_.Element, true, _defaultCreator, TargetType.ENEMY);
						break;
					case 1:
						//"キュアパラライズ"
						warMagicData = CreateAbnormalConditionMagic(magic_, true, new ParalysisCondition(), TargetType.FRIEND);
						break;
					case 2:
						//"スピード"
						warMagicData = CreateStatusChangeMagic(magic_, true, StatusKey.Agi, TargetType.FRIEND);
						break;
					case 3:
						//"フライ"
						warMagicData = CreateAbnormalConditionMagic(magic_, true, new MoveTypeChangeCondition(MoveType.飛行), TargetType.FRIEND);
						break;
					case 4:
						//"エレメント"
						warMagicData = CreateSummonMagic(magic_, unitDatas, "WindElement");
						break;
					case 5:
						//"アゲン"
						warMagicData = CreateAbnormalConditionMagic(magic_, true, new AttackCountChangeCondition(1), TargetType.FRIEND);
						break;
					case 6:
						//"ライトニング"
						warMagicData = CreateMagic(magic_, magic_.Element, false, _defaultCreator, TargetType.ENEMY);
						break;
					case 7:
						//"トルネード"
						warMagicData = CreateMagic(magic_, magic_.Element, false, _defaultCreator, TargetType.ENEMY);
						break;
					default:
						break;
					}
					magicDataList.Add(warMagicData);
				});

				magicCommands[j, level] = magicDataList;
			}
		}

		private static void CreateFireMagic(IList<IList<WarMagicData>>[,] magicCommands,
			int j, MagicDictionary magicDic, IList<UnitData> unitDatas)
		{
			for (byte level = 0; level <= 5; level++)
			{
				var magicList = magicDic[AttackType.火, level];
				var magicDataList = new List<IList<WarMagicData>>(magicList.Count);

				magicList.ForEach((magic_, i_) => {
					IList<WarMagicData> warMagicData = null;
					switch (i_)
					{
					case 0:
						//"ファイア"
						warMagicData = CreateMagic(magic_, magic_.Element, true, _defaultCreator, TargetType.ENEMY);
						break;
					case 1:
						//"キュアポイズン"
						warMagicData = CreateAbnormalConditionMagic(magic_, true, new PoisonCondition(), TargetType.FRIEND);
						break;
					case 2:
						//"アタック"
						warMagicData = CreateStatusChangeMagic(magic_, true, StatusKey.Atk, TargetType.FRIEND);
						break;
					case 3:
						//"ウォーターガード"
						warMagicData = CreateAbnormalConditionMagic(magic_, true,
							new ResistivityChangeCondition(magic_.Element, ResistivityType.強い), TargetType.FRIEND);
						break;
					case 4:
						//"エレメント"
						warMagicData = CreateSummonMagic(magic_, unitDatas, "FireElement");
						break;
					case 5:
						//"リカバー"
						warMagicData = CreateStatusChangeMagic(magic_, true, StatusKey.HpAutoHeal, TargetType.FRIEND);
						break;
					case 6:
						//"メテオストライク"
						warMagicData = CreateMagic(magic_, magic_.Element, false, _defaultCreator, TargetType.ENEMY);
						break;
					case 7:
						//"ヴォルケーノ"
						warMagicData = CreateMagic(magic_, magic_.Element, false, _volcanoCreator, TargetType.ENEMY);
						break;
					default:
						break;
					}
					magicDataList.Add(warMagicData);
				});

				magicCommands[j, level] = magicDataList;
			}
		}

		private static IList<WarMagicData> CreateStatusChangeMagic(MagicData magic, bool isChipEffect, StatusKey targetkey, TargetType targetType)
		{
			var name = magic.Name;
			var description = magic.Description;
			var cores = magic.Cores;
			var magicDatas = new List<WarMagicData>(cores.Length);
			foreach (var core in cores)
			{
				// 行為の範囲や対象を設定する
				IScope scope;
				if (core.Area != 0)
					scope = new DefaultScope(core.Range, core.Area, targetType);
				else
					scope = new WholeScope(targetType);

				// 画面効果の設定
				var screenEffect = CreateScreenEffect(magic, isChipEffect);

				// 消費MPの設定
				var consumption = new MPCost(core.MPCost);

				// ステータス変化の状態異常の設定
				var cond = new StatusChangeCondition(targetkey, core.Power);

				// 行為の設定
				var action = new GiveAbnormalConditionAction(scope, consumption, screenEffect, cond);

				// 魔法データを作成する
				var magicData = new WarMagicData(name, description, action, core.MPCost, core.Area, core.Range);
				magicDatas.Add(magicData);
			}
			return magicDatas;
		}

		private static IList<WarMagicData> CreateAbnormalConditionMagic(MagicData magic, bool isChipEffect, IAbnormalCondition cond, TargetType targetType)
		{
			var name = magic.Name;
			var description = magic.Description;
			var cores = magic.Cores;
			var magicDatas = new List<WarMagicData>(cores.Length);
			foreach (var core in cores)
			{
				// 行為の範囲や対象を設定する
				IScope scope;
				if (core.Area != 0)
					scope = new DefaultScope(core.Range, core.Area, targetType);
				else
					scope = new WholeScope(targetType);

				// 画面効果を設定する
				var screenEffect = CreateScreenEffect(magic, isChipEffect);

				// 消費MPの設定
				var consumption = new MPCost(core.MPCost);

				// 行為の設定
				var action = new GiveAbnormalConditionAction(scope, consumption, screenEffect, cond);

				// 魔法データを作成する
				var magicData = new WarMagicData(name, description, action, core.MPCost, core.Area, core.Range);
				magicDatas.Add(magicData);
			}
			return magicDatas;
		}

		private static IList<WarMagicData> CreateMagic(MagicData magic, AttackType type, bool isChipEffect,
		                                               Func<IScope, MagicCore, AttackType, IScreenEffect, IBattleAction> creator, TargetType targetType)
		{
			var name = magic.Name;
			var description = magic.Description;

			var cores = magic.Cores;
			var magicDatas = new List<WarMagicData>(cores.Length);
			foreach (var core in cores)
			{
				// 行為の範囲や対象を設定する
				IScope scope;
				if (core.Area != 0)
					scope = new DefaultScope(core.Range, core.Area, targetType);
				else
					scope = new WholeScope(targetType);

				// 画面効果を設定する
				var screenEffect = CreateScreenEffect(magic, isChipEffect);

				// 行為を設定する
				var action = creator(scope, core, type, screenEffect);

				// 魔法データを作成する
				var magicData = new WarMagicData(name, description, action, core.MPCost, core.Area, core.Range);
				magicDatas.Add(magicData);
			}
			return magicDatas;
		}

		private static IList<WarMagicData> CreateSummonMagic(MagicData magic, IList<UnitData> unitDatas, string id)
		{
			var name = magic.Name;
			var description = magic.Description;

			var cores = magic.Cores;
			var magicDatas = new List<WarMagicData>(cores.Length);
			cores.ForEach((core, i) => {
				// 行為の範囲や対象を設定する
				var scope = new DefaultScope(core.Range, core.Area, TargetType.NONE);

				// 消費MPの設定
				var consumption = new MPCost(core.MPCost);

				// 画面効果を設定する
				var screenEffect = CreateScreenEffect(magic, true);

				// 行為を設定する
				var unitData = unitDatas.FirstOrDefault(unit_ => unit_.ID == id + (i + 1));
				var action = new SummonAction(scope, consumption, unitData, screenEffect);

				// 魔法データを作成する
				var magicData = new WarMagicData(name, description, action, core.MPCost, core.Area, core.Range);
				magicDatas.Add(magicData);
			});
			return magicDatas;
		}

		private static IScreenEffect CreateScreenEffect(MagicData magic, bool isChipEffect)
		{
			IScreenEffect screenEffect;
			
			var path = Path.Combine(Path.Combine(Global.ScenarioDir, "Picture"), magic.AnimationFileName);
			if (isChipEffect)
			{
				// TODO: エフェクトローダーの作成
				var surfaces = WarGlobal.BmpManager.GetSurfaces(path, 32, 32,
					path_ => BitmapUtil.Load(path_, Color.Black));
				screenEffect = new ChipFrameAnimation(surfaces);
			}
			else
			{
				// TODO: エフェクトローダーの作成
				var surfaces = WarGlobal.BmpManager.GetSurfaces(path, 128, 128);
				screenEffect = new ScreenFrameAnimation(surfaces);
			}
			return screenEffect;
		}
	}
}