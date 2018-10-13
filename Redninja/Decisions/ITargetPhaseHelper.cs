using Redninja.Targeting;

namespace Redninja.Decisions
{
	public interface ITargetPhaseHelper : ISkillTargetingInfo
	{
		void SelectTarget(ISelectedTarget target);
		bool Back();
		IBattleAction GetAction();
	}
}
