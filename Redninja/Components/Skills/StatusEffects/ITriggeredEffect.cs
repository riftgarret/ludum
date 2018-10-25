using Redninja.Components.Combat;
using Redninja.Components.Combat.Events;

namespace Redninja.Components.Skills.StatusEffects
{
	public interface ITriggeredEffect : IStatusEffect
	{
		void CheckTrigger(IBattleEvent battleEvent);
	}
}
