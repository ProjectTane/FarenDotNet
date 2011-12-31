using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FarenDotNet.War.Phase
{
	public static class Phases
	{
		public static readonly DeployUnitPhase DeployUnitPhase = new DeployUnitPhase();
		public static readonly BattlePhase BattlePhase = new BattlePhase();
		public static readonly TurnPhase TurnPhase = new TurnPhase();
		public static readonly InitiativePhase InitiativePhase = new InitiativePhase();
	}
}
