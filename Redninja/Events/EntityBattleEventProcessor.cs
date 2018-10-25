using Redninja.Components.Combat;
using Redninja.Components.Properties;
using Redninja.Entities;

namespace Redninja.Events
{
	internal class EntityBattleEventProcessor : IBattleEventProcessor
	{
		private ICombatExecutor combatExecutor;
		private IBattleEntityManager bem;

		public EntityBattleEventProcessor(ICombatExecutor combatExecutor, IBattleEntityManager bem)
		{
			this.combatExecutor = combatExecutor;
			this.bem = bem;
		}

		public void ProcessEvent(IBattleEvent battleEvent)
		{
			foreach(IBattleEntity entity in bem.Entities) 
			{
				ProcessEntity(battleEvent, entity);
			}
		}

		internal void ProcessEntity(IBattleEvent battleEvent, IBattleEntity entity)
		{
			foreach (ITriggeredProperty property in entity.TriggeredProperties)
			{
				property.OnBattleEvent(battleEvent, entity, bem, combatExecutor);
			}
		}
	}
}
