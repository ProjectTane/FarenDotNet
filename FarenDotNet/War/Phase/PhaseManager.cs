using System.Collections.Generic;
using Paraiba.Core;

namespace FarenDotNet.War.Phase
{
	public class PhaseManager
	{
		private readonly Stack<IPhase> _phaseStack;

		public PhaseManager()
		{
			_phaseStack = new Stack<IPhase>();
		}

		public IPhase CurrentPhase
		{
			get {
				if (_phaseStack.Count != 0)
					return _phaseStack.Peek();
				return null;
			}
		}

		public IEnumerable<IPhase> CurrentPhases
		{
			get { return _phaseStack; }
		}

		public IPhase ChangePhase(Situation situation, IPhase phase)
		{
			var oldPhase = _phaseStack.Pop();
			oldPhase.Exit(situation);

			_phaseStack.Push(phase);
			phase.Start(situation);

			return oldPhase;
		}

		public void StartPhase(Situation situation, IPhase phase)
		{
			_phaseStack.Push(phase);
			phase.Start(situation);
		}

		public IPhase ExitPhase(Situation situation)
		{
			var phase = _phaseStack.Pop();
			phase.Exit(situation);
			return phase;
		}
	}
}