using Redninja.Targeting;

namespace Redninja.Decisions
{
	public interface ISkillTargetingManager : ISkillTargetingInfo
	{
		void SelectTarget(ISelectedTarget target);
		void Back();
		IBattleAction GetAction();
	}
}
