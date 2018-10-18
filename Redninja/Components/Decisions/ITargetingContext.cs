using Redninja.Components.Targeting;

namespace Redninja.Components.Decisions
{
	public interface ITargetingContext : ITargetingView, IActionProvider
	{
		void SelectTarget(ISelectedTarget target);
		bool Back();
	}
}
