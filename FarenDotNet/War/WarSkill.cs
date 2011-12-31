using FarenDotNet.BasicData;
using FarenDotNet.War.BattleAction;

namespace FarenDotNet.War
{
	public class WarSkill
	{
		private readonly IBattleAction _action;
		private readonly Skill _skill;

		public WarSkill(Skill skill, IBattleAction action)
		{
			_skill = skill;
			_action = action;
		}

		public Skill Data
		{
			get { return _skill; }
		}

		public IBattleAction Action
		{
			get { return _action; }
		}
	}
}