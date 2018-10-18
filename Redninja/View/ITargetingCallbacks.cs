using Redninja.Components.Targeting;

namespace Redninja.View
{
	public interface ITargetingCallbacks
	{
		void SelectTarget(ISelectedTarget target);
		void Cancel();
	}
}
