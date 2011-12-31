using System;

namespace FarenDotNet.War.Phase
{
	public interface IPhase
	{
		void Start(Situation situation);
		void Exit(Situation situation);
	}
}