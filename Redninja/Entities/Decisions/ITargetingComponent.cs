using Redninja.Components.Actions;
using Redninja.Components.Targeting;
using Redninja.View;

namespace Redninja.Entities.Decisions
{
	public interface ITargetingComponent : ITargetingView
	{
		new IEntityModel Entity { get; }
		void SelectTarget(ISelectedTarget target);
		bool Back();
		IBattleAction GetAction();
	}
}
