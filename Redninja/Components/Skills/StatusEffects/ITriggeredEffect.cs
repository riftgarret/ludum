using Redninja.Events;

namespace Redninja.Components.Skills.StatusEffects
{
	public interface ITriggeredEffect : IStatusEffect
	{
		void CheckTrigger(IBattleEvent battleEvent);
	}
}
