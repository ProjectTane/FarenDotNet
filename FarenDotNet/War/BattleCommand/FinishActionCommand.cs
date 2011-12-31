using System;
using System.Drawing;
using Paraiba.Drawing.Surfaces;
using FarenDotNet.War.BattleAction;

namespace FarenDotNet.War.BattleCommand
{
	public class FinishActionCommand : IBattleCommand
	{
		public FinishActionCommand(string name, string description, Image image)
		{
			Name = name;
			Description = description;
			Image = image;
		}

		#region IBattleCommand Members

		public string Name { get; private set; }
		public string Description { get; private set; }
		public Image Image { get; private set; }

		public bool CanExecute(ActionArguments args, WarUnit doer)
		{
			return true;
		}

		public void Execute(ActionArguments args, WarUnit doer, Action finished)
		{
			args.Situation.PhaseManager.ExitPhase(args.Situation);
			finished();
		}

		#endregion
	}
}