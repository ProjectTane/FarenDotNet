namespace FarenDotNet.War.AbnormalCondition
{
	public interface IAbnormalCondition
	{
		/// <summary>
		/// 名前
		/// </summary>
		string Name { get; }

		/// <summary>
		/// 一意な識別子
		/// </summary>
		string ID { get; }

		/// <summary>
		/// ユニットに状態異常が追加される直前に呼び出される。
		/// </summary>
		/// <param name="situation"></param>
		/// <param name="unit"></param>
		bool AddingConditonTable(Situation situation, WarUnit unit, IAbnormalCondition oldCond);

		/// <summary>
		/// ユニットに状態異常が追加された直後に呼び出される。
		/// </summary>
		/// <param name="situation"></param>
		/// <param name="unit"></param>
		void AddedConditonTable(Situation situation, WarUnit unit);

		/// <summary>
		/// ユニットの状態異常が削除された直後に呼び出される。
		/// </summary>
		/// <param name="unit"></param>
		/// <param name="situation"></param>
		void RemovedConditonTable(Situation situation, WarUnit unit);
	}
}