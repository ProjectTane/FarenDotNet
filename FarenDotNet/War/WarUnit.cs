using System;
using System.Collections.Generic;
using System.Linq;
using Paraiba.Drawing.Surfaces;
using Paraiba.Core;
using Paraiba.Geometry;
using Paraiba.Utility;
using FarenDotNet.BasicData;
using FarenDotNet.Reign;
using FarenDotNet.War.AbnormalCondition;
using FarenDotNet.War.BattleCommand;

namespace FarenDotNet.War
{
	// ステータス強化の状態異常を簡潔に記述するためにキーを導入
	// 対象のステータスを表現する際にキーを使わずに、デリゲートを使うのならば不要
	public enum StatusKey
	{
		MaxHP,
		MaxMP,
		HP,
		MP,
		Atk,
		Def,
		Tec,
		Agi,
		Mag,
		Res,
		Mobility /*移動力*/,
		HpAutoHeal,
		MpAutoHeal,
		Activity
	}

	/// <summary>
	/// Clone を容易に行うためにまとめられたステータスの集合
	/// ReviseStatus という一つのデリゲートに補正メソッドをまとめることができる。
	/// 一つのデリゲートにまとめる必要がないのならば、このクラスも不要である
	/// </summary>
	public class Status : ICloneable
	{
		private readonly int[] _parameters;
		public List<DefaultAttack> DefaultAttacks;
		public MoveType MoveType;
		public ListIndexer<StatusKey, int> Params;
		public Resistivity Resistivity;

		private Status(Status status)
		{
			Params = new ListIndexer<StatusKey, int>(status._parameters);
			Resistivity = status.Resistivity.Clone();
			MoveType = status.MoveType;
			DefaultAttacks = status.DefaultAttacks.ToList();
		}

		public Status(Unit unit, int[] parameters)
		{
			_parameters = parameters;
			Params = new ListIndexer<StatusKey, int>(parameters);
			Resistivity = unit.Resistivity;
			MoveType = unit.MoveType;
			DefaultAttacks = unit.Attacks.ToList();
		}

		public int MaxHP
		{
			get { return Params[StatusKey.MaxHP]; }
		}

		public int MaxMP
		{
			get { return Params[StatusKey.MaxMP]; }
		}

		public int HP
		{
			get { return Params[StatusKey.HP]; }
		}

		public int MP
		{
			get { return Params[StatusKey.MP]; }
		}

		public int Atk
		{
			get { return Params[StatusKey.Atk]; }
		}

		public int Def
		{
			get { return Params[StatusKey.Def]; }
		}

		public int Tec
		{
			get { return Params[StatusKey.Tec]; }
		}

		public int Agi
		{
			get { return Params[StatusKey.Agi]; }
		}

		public int Mag
		{
			get { return Params[StatusKey.Mag]; }
		}

		public int Res
		{
			get { return Params[StatusKey.Res]; }
		}

		public int Mobility
		{
			get { return Params[StatusKey.Mobility]; }
		}

		public int HpAutoHeal
		{
			get { return Params[StatusKey.HpAutoHeal]; }
		}

		public int MpAutoHeal
		{
			get { return Params[StatusKey.MpAutoHeal]; }
		}

		public int Activity
		{
			get { return Params[StatusKey.Activity]; }
		}

		/// <summary>
		/// 行動不可能かどうか（行動値が固定されいてるかどうか）
		/// </summary>
		public bool IsFreezing { get; set; }

		#region ICloneable Members

		public Status Clone()
		{
			return new Status(this);
		}

		#endregion

		#region ICloneable メンバ

		object ICloneable.Clone()
		{
			return Clone();
		}

		#endregion
	}

	public struct CommandInfo
	{
		public readonly IBattleCommand Command;
		public readonly bool Enable;

		public CommandInfo(IBattleCommand command)
			: this(command, true)
		{
		}

		public CommandInfo(IBattleCommand command, bool enable)
		{
			Command = command;
			Enable = enable;
		}
	}

	public class CommandState
	{
		public IDictionary<IBattleCommand, CommandState> Transition { get; set; }
		public IList<CommandInfo> CommandInfos { get; set; }
	}

	public class WarUnit
	{
		// ----- ----- ----- フィールド ----- ----- -----
		private readonly ConditionSet _conditions;
		private readonly CommandState _initCommandState;
		private readonly ListIndexer<AttackType, byte> _magicLevels;
		private readonly WarSide _side;
		private readonly Unit _unit;
		private CommandState _commandState;
		private Point2 _location;
		private WarMap _map;
		private Status _status;
		private bool _visible;
		private Area _area;

		public WarUnit(Unit unit, WarSide warSide, CommandState initState, Area area)
		{
			_unit = unit;
			// ステータスの生成
			CreateStatus();
			// コマンド状態の初期化
			_initCommandState = _commandState = initState;
			_visible = true;
			_unit = unit;
			unit.Acted = true;
			_magicLevels = new ListIndexer<AttackType, byte>(unit.MagicLevel, i => (int)i - (int)AttackType.火);
			_side = warSide;
			_conditions = new ConditionSet(this);
			_area = area;
		}

		// ----- ----- ----- プロパティ ----- ----- -----
		public string Name
		{
			get { return _unit.Name; }
		}

		public ConditionSet Conditions
		{
			get { return _conditions; }
		}

		public Status Status
		{
			get
			{
				if (ReviseStatus != null)
				{
					var status = _status.Clone();
					ReviseStatus(status);
					return status;
				}
				return _status;
			}
		}

		public Status OriginalStatus
		{
			get { return _status; }
		}

		public MoveType MoveType
		{
			get { return Status.MoveType; }
		}

		public Resistivity Resistivity
		{
			get { return Status.Resistivity; }
		}

		public IList<DefaultAttack> DefaultAttacks
		{
			get { return Status.DefaultAttacks; }
		}

		public UnitRank Rank
		{
			get { return _unit.Rank; }
		}

		public Species Species
		{
			get { return _unit.Species; }
		}

		public int SkillTimes
		{
			get { return _unit.SkillTimes; }
		}

		/// <summary>
		/// 技量値（命中判定で利用するパラメータ）の補正率(1.0で変化なし)
		/// </summary>
		public double LandRev
		{
			get { return (Map[Location].Info.Revision[MoveType] + 100) / 100.0; }
		}

		public ListIndexer<AttackType, byte> MagicLevels
		{
			get { return _magicLevels; }
		}

		public int Exp
		{
			get { return _unit.Exp; }
		}

		public bool IsEscaped { get; set; }

		public bool Alive
		{
			get { return Status.HP > 0; }
		}
		
		public Surface ChipImage
		{
			get { return _unit.Images.Icon; }
		}

		public Surface FaceImage
		{
			get { return _unit.Images.Face; }
		}

		/// <summary>
		/// 表示するコマンドと使用可能かどうかの値の組のリスト
		/// </summary>
		public IList<CommandInfo> Commands
		{
			get
			{
				if (ReviseCommands != null)
				{
					var list = new List<CommandInfo>(_commandState.CommandInfos);
					ReviseCommands(list);
					return list;
				}
				return _commandState.CommandInfos;
			}
		}

		/// <summary>
		/// 戦闘マップ上で描画可能かどうか
		/// </summary>
		public bool Visible
		{
			get { return _visible && Alive; }
			set { _visible = value; }
		}

		public WarMap Map
		{
			get { return _map; }
			internal set { _map = value; }		// Map 内で書き換え
		}

		public Point2 Location
		{
			get { return _location; }
			internal set { _location = value; }	// Map 内で書き換え
		}

		public WarSide Side
		{
			get { return _side; }
		}

		public bool IsMaster
		{
			get { return _unit.IsMaster; }
		}

		/// <summary>アンデッド属性を持つか</summary>
		public bool IsUndead
		{
			get { return _unit.IsUndead; }
		}

		// ----- ----- ----- デリゲート ----- ----- -----
		/// <summary>
		/// ステータスを補正するデリゲート
		/// </summary>
		public event Action<Status> ReviseStatus;

		/// <summary>
		/// コマンド状態を補正するデリゲート
		/// </summary>
		public event Action<List<CommandInfo>> ReviseCommands;

		/// <summary>
		/// ダメージを受けた際に行われるイベント
		/// </summary>
		public event Action<Situation, WarUnit, WarUnit, int> DamageEvent;

		public event Action<Situation, WarUnit, WarUnit> DiedEvent;

		// ----- ----- ----- メソッド ----- ----- -----

		public WarUnit GetPrototype(WarSide side)
		{
			return new WarUnit(_unit, side, _initCommandState, _area);
		}

		private void CreateStatus()
		{
			var parameters = new int[StatusKey.HP.GetCount()];
			var indexer = new ListIndexer<StatusKey, int>(parameters);
			indexer[StatusKey.MaxHP] = indexer[StatusKey.HP] = _unit.HP;
			indexer[StatusKey.MaxMP] = indexer[StatusKey.MP] = _unit.MP;
			indexer[StatusKey.Atk] = _unit.Atk;
			indexer[StatusKey.Def] = _unit.Def;
			indexer[StatusKey.Tec] = _unit.Tec;
			indexer[StatusKey.Agi] = _unit.Agi;
			indexer[StatusKey.Mag] = _unit.Mag;
			indexer[StatusKey.Res] = _unit.Res;
			indexer[StatusKey.Mobility] = _unit.Mobility;
			indexer[StatusKey.HpAutoHeal] = _unit.HpHeal;
			indexer[StatusKey.MpAutoHeal] = _unit.MpHeal;
			indexer[StatusKey.Activity] = 0;
			_status = new Status(_unit, parameters);
		}

		public void AddActivity(Situation situation, int value)
		{
			_status.Params[StatusKey.Activity] += value;
		}

		public void Die(Situation situation, WarUnit doer)
		{
			_status.Params[StatusKey.HP] = 0;
			Map.UnDeploy(this, Location);

			if (_area != null)
				_area.Units.Remove(_unit);

			DiedEvent.InvokeIfNotNull(situation, this, doer);
		}

		public void DamageHP(Situation situation, WarUnit doer, int value)
		{
			int newValue = _status.Params[StatusKey.HP] - value;
			_status.Params[StatusKey.HP] = newValue > 0 ? newValue : 0;
			DamageEvent.InvokeIfNotNull(situation, this, doer, value);
			if (!Alive)
				Die(situation, doer);
		}

		public void HealHP(Situation situation, WarUnit doer, int value)
		{
			int newValue = _status.Params[StatusKey.HP] + value;
			_status.Params[StatusKey.HP] = newValue <= _status.MaxHP ? newValue : _status.MaxHP;
		}

		public void ExpendMP(Situation situation, WarUnit doer, int value)
		{
			int newValue = _status.Params[StatusKey.MP] - value;
			_status.Params[StatusKey.MP] = newValue > 0 ? newValue : _status.MaxMP;
		}

		public void HealMP(Situation situation, WarUnit doer, int value)
		{
			int newValue = _status.Params[StatusKey.MP] + value;
			_status.Params[StatusKey.MP] = newValue <= _status.MaxMP ? newValue : _status.MaxMP;
		}

		public void ChangeCommandState(IBattleCommand cmd)
		{
			_commandState = _commandState.Transition[cmd];
		}

		public Action SaveCommandState()
		{
			var backup = _commandState;
			return () => _commandState = backup;
		}

		public void ResetCommandState()
		{
			_commandState = _initCommandState;
		}

		public bool IsOpponent(WarUnit unit)
		{
			return Side.IsOpponent(unit.Side);
		}
	}
}