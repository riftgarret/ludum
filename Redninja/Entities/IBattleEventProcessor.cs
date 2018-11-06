using Redninja.Components.Combat.Events;

namespace Redninja.Entities
{
	internal interface IBattleEventProcessor
	{
		void ProcessEvent(ICombatEvent battleEvent);
	}
}
