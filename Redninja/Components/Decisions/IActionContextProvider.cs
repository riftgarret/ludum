using Redninja.Components.Skills;

namespace Redninja.Components.Decisions
{
	public interface IActionContextProvider
	{
		IActionContext GetActionContext();
		IMovementContext GetMovementContext();		
		ITargetingContext GetTargetingContext(ISkill skill);
	}
}
