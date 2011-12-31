namespace FarenDotNet.BasicData
{
	public struct IndexerFromMoveType<T>
	{
		private readonly T[] array;

		public IndexerFromMoveType(T[] array)
		{
			this.array = array;
		}

		public T this[MoveType mt]
		{
			get { return array[(int)mt]; }
		}
	}

	public class LandformInfo
	{
		public LandformInfo(string name, int[] mobilibity, int[] tecRevision)
		{
			Name = name;
			RequiredMobility = new IndexerFromMoveType<int>(mobilibity);
			Revision = new IndexerFromMoveType<int>(tecRevision);
		}

		public string Name { get; private set; }

		public IndexerFromMoveType<int> RequiredMobility { get; private set; }

		public IndexerFromMoveType<int> Revision { get; private set; }
	}
}