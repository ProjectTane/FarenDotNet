using System;
using System.Collections.Generic;

namespace FarenDotNet.War.AbnormalCondition
{
	/// <summary>
	/// 1キャラが保持する異常状態の集合クラス
	/// </summary>
	public class ConditionSet
	{
		private readonly SortedDictionary<string, IAbnormalCondition> _conditions;
		private readonly WarUnit _unit;

		public ConditionSet(WarUnit unit)
		{
			_conditions = new SortedDictionary<string, IAbnormalCondition>();
			_unit = unit;
		}

		/// <summary>
		/// 新たな状態異常を追加する
		/// </summary>
		/// <param name="cond"></param>
		/// <param name="situation"></param>
		public void Add(IAbnormalCondition cond, Situation situation)
		{
			IAbnormalCondition existingCond;
			if (!_conditions.TryGetValue(cond.ID, out existingCond) ||
				cond.AddingConditonTable(situation, _unit, existingCond))
			{
				_conditions[cond.ID] = cond;
				cond.AddedConditonTable(situation, _unit);
			}
		}

		/// <summary>
		/// 既に状態異常を保持しているか
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public bool Contains(string id)
		{
			return _conditions.ContainsKey(id);
		}

		/// <summary>
		/// 状態異常を消去する
		/// </summary>
		/// <param name="id"></param>
		/// <param name="situation"></param>
		/// <returns></returns>
		public bool Remove(string id, Situation situation)
		{
			IAbnormalCondition cond;
			if (_conditions.TryGetValue(id, out cond))
			{
				bool result = _conditions.Remove(id);
				if (result)
					cond.RemovedConditonTable(situation, _unit);
				return result;
			}
			return false;
		}

		public bool TryGet(string id, out IAbnormalCondition cond)
		{
			return _conditions.TryGetValue(id, out cond);
		}
	}

	///// <summary>
	///// 1キャラが保持する異常状態の集合クラス
	///// </summary>
	//public class ConditionSet
	//{
	//    private readonly IDictionary<Type, IAbnormalCondition> _conditions;
	//    private readonly WarUnit _unit;

	//    public ConditionSet(WarUnit unit)
	//    {
	//        _conditions = new Dictionary<Type, IAbnormalCondition>();
	//        _unit = unit;
	//    }

	//    /// <summary>
	//    /// 新たな状態異常を追加する
	//    /// </summary>
	//    /// <param name="cond"></param>
	//    /// <param name="situation"></param>
	//    public void Add(IAbnormalCondition cond, Situation situation)
	//    {
	//        IAbnormalCondition existingCond;
	//        // 既に状態異常が存在している場合は、追加可能かどうか調べる
	//        if (!_conditions.TryGetValue(cond.GetType(), out existingCond) ||
	//            existingCond.AddingConditonTable(situation, _unit))
	//        {
	//            _conditions[cond.GetType()] = cond;
	//            cond.AddedConditonTable(situation, _unit);
	//        }
	//    }

	//    /// <summary>
	//    /// 既に状態異常を保持しているか
	//    /// </summary>
	//    /// <param name="type"></param>
	//    /// <returns></returns>
	//    public bool Contains(Type type)
	//    {
	//        return _conditions.ContainsKey(type);
	//    }

	//    /// <summary>
	//    /// 既に状態異常を保持しているか
	//    /// </summary>
	//    /// <param name="cond"></param>
	//    /// <returns></returns>
	//    public bool Contains(IAbnormalCondition cond)
	//    {
	//        return _conditions.ContainsKey(cond.GetType());
	//    }

	//    /// <summary>
	//    /// 既に状態異常を保持しているか
	//    /// </summary>
	//    /// <typeparam name="T"></typeparam>
	//    /// <returns></returns>
	//    public bool Contains<T>()
	//        where T : IAbnormalCondition
	//    {
	//        return _conditions.ContainsKey(typeof(T));
	//    }

	//    /// <summary>
	//    /// 状態異常を消去する
	//    /// </summary>
	//    /// <param name="type"></param>
	//    /// <param name="situation"></param>
	//    /// <returns></returns>
	//    public bool Remove(Type type, Situation situation)
	//    {
	//        IAbnormalCondition existingCond;
	//        if (_conditions.TryGetValue(type, out existingCond))
	//        {
	//            bool result = _conditions.Remove(type);
	//            if (result)
	//                existingCond.RemovedConditonTable(situation, _unit);
	//            return result;
	//        }
	//        return false;
	//    }

	//    /// <summary>
	//    /// 状態異常を消去する
	//    /// </summary>
	//    /// <param name="cond"></param>
	//    /// <param name="situation"></param>
	//    /// <returns></returns>
	//    public bool Remove(IAbnormalCondition cond, Situation situation)
	//    {
	//        return Remove(cond.GetType(), situation);
	//    }

	//    /// <summary>
	//    /// 状態異常を消去する
	//    /// </summary>
	//    /// <param name="situation"></param>
	//    /// <returns></returns>
	//    public bool Remove<T>(Situation situation)
	//        where T : IAbnormalCondition
	//    {
	//        return Remove(typeof(T), situation);
	//    }

	//    public bool TryGet(Type type, out IAbnormalCondition outCond)
	//    {
	//        return _conditions.TryGetValue(type, out outCond);
	//    }

	//    public bool TryGet(IAbnormalCondition cond, out IAbnormalCondition outCond)
	//    {
	//        return _conditions.TryGetValue(cond.GetType(), out outCond);
	//    }

	//    public bool TryGet<T>(out T outCond)
	//        where T : IAbnormalCondition
	//    {
	//        IAbnormalCondition tmp;
	//        var result = _conditions.TryGetValue(typeof(T), out tmp);
	//        outCond = (T)tmp;
	//        return result;
	//    }
	//}
}