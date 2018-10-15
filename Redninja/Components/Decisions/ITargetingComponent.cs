using Redninja.Components.Actions;
using Redninja.Components.Targeting;
using Redninja.View;

namespace Redninja.Components.Decisions
{
	public interface ITargetingComponent : ITargetingState
	{
		void SelectTarget(ISelectedTarget target);
		bool Back();
		IBattleAction GetAction();
	}
}
