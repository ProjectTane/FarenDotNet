using System;

namespace FarenDotNet.War.AbnormalCondition
{
	public class PhantasmCondition : IAbnormalCondition
	{
		#region IAbnormalCondition Members

		public string Name
		{
			get { return "幻想"; }
		}

		public string ID
		{
			get { return "ファーレントゥーガ.幻想"; }
		}

		/// <summary>
		/// ユニットに状態異常が追加される直前に呼び出される。
		/// </summary>
		/// <param name="situation"></param>
		/// <param name="unit"></param>
		public bool AddingConditonTable(Situation situation, WarUnit unit, IAbnormalCondition oldCond)
		{
			return false;
		}

		public void AddedConditonTable(Situation situation, WarUnit unit)
		{
			throw new NotImplementedException();
		}

		public void RemovedConditonTable(Situation situation, WarUnit unit)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}