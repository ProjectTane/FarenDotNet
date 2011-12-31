using FarenDotNet.War.UI;

namespace FarenDotNet.War.BattleAction
{
	public struct ActionArguments
	{
		public readonly WarPresentationModel Model;
		public readonly Situation Situation;

		public ActionArguments(Situation situation, WarPresentationModel model)
		{
			Situation = situation;
			Model = model;
		}
	}
}