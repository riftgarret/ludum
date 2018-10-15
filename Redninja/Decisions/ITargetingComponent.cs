using Redninja.Targeting;

namespace Redninja.Decisions
{
	public interface ITargetingComponent : ITargetingState
	{
		void SelectTarget(ISelectedTarget target);
		bool Back();
		IBattleAction GetAction();
	}
}
