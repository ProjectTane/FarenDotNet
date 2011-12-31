using System;
using System.Drawing;
using Paraiba.Drawing.Surfaces;
using Paraiba.Utility;
using FarenDotNet.War.BattleAction;
using FarenDotNet.War.UI;

namespace FarenDotNet.War.BattleCommand
{
	public class ShowWindowCommand<TWindow> : IBattleCommand
		where TWindow : IWindowForShowWindowCommand
	{
		private readonly Wrap<TWindow> _targetWindow;

		public ShowWindowCommand(string name, string description, Image image, Wrap<TWindow> targetWindow)
		{
			Name = name;
			Description = description;
			Image = image;
			_targetWindow = targetWindow;
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
			// このタイミングで生成したほうが無駄が少ない
			_targetWindow.Value.Show(args.Model, doer, finished);
		}

		#endregion
	}
}