namespace FarenDotNet.War.AbnormalCondition
{
	public class PetrifactionCondition : IAbnormalCondition
	{
		#region IAbnormalCondition Members

		public string Name
		{
			get { return "石化"; }
		}

		public string ID
		{
			get { return "ファーレントゥーガ.石化"; }
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
			unit.ReviseStatus += ReviseStatus;
		}

		public void RemovedConditonTable(Situation situation, WarUnit unit)
		{
			unit.ReviseStatus -= ReviseStatus;
		}

		#endregion

		private static void ReviseStatus(Status status)
		{
			status.IsFreezing = true;
		}
	}
}