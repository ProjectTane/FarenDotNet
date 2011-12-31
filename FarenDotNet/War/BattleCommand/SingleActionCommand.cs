using System;
using System.Drawing;
using Paraiba.Drawing.Surfaces;
using FarenDotNet.War.BattleAction;

namespace FarenDotNet.War.BattleCommand
{
	public class SingleActionCommand : IBattleCommand
	{
		protected IBattleAction _action;

		public SingleActionCommand(string name, string description, Image image, IBattleAction action)
		{
			Name = name;
			Image = image;
			Description = description;
			_action = action;
		}

		#region IBattleCommand Members

		public string Name { get; private set; }
		public Image Image { get; private set; }
		public string Description { get; private set; }

		public bool CanExecute(ActionArguments args, WarUnit doer)
		{
			return _action.CanBoot(args, doer);
		}

		public void Execute(ActionArguments args, WarUnit doer, Action finished)
		{
			_action.Boot(args, doer, finished);
		}

		#endregion
	}
}