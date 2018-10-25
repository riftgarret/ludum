using Redninja.Components.Combat;
using Redninja.Entities;
using Redninja.Events;

namespace Redninja.Components.Properties
{
	internal interface ITriggeredProperty : IItemProperty
	{
		void OnBattleEvent(IBattleEvent battleEvent, IBattleEntity entity, IBattleEntityManager bem, ICombatExecutor combatExecutor);
	}
}
