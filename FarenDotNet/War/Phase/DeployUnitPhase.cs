using System;
using Paraiba.Core;

namespace FarenDotNet.War.Phase
{
	public class DeployUnitPhase : IPhase
	{
		public Action<Situation> Start { get; set; }

		public Action<Situation> Exit { get; set; }

		#region IPhase Members

		void IPhase.Start(Situation situation)
		{
			Start.InvokeIfNotNull(situation);
		}

		void IPhase.Exit(Situation situation)
		{
			Exit.InvokeIfNotNull(situation);
		}

		#endregion
	}
}