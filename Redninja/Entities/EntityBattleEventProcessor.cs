﻿using System.Collections.Generic;
using System.Linq;
using Redninja.Components.Combat;
using Redninja.Components.Combat.Events;
using Redninja.Components.Properties;

namespace Redninja.Entities
{
	/// <summary>
	/// Entity battle event processor. Where event triggers are processed and skills, buffs, etc are applied
	/// based on the IBattleEvents being generated.
	/// </summary>
	internal class EntityBattleEventProcessor : IBattleEventProcessor
	{
		private ICombatExecutor combatExecutor;
		private IBattleEntityManager bem;

		public EntityBattleEventProcessor(ICombatExecutor combatExecutor, IBattleEntityManager bem)
		{
			this.combatExecutor = combatExecutor;
			this.bem = bem;
		}

		public void ProcessEvent(ICombatEvent battleEvent)
		{
			foreach(IBattleEntity entity in bem.Entities) 
			{
				ProcessEntity(battleEvent, entity);
			}
		}

		internal void ProcessEntity(ICombatEvent battleEvent, IBattleEntity entity)
		{
			foreach (ITriggeredProperty property in entity.TriggeredProperties)
			{
				if (!RefreshValid(entity, property)) continue;

				bool conditionsMet = HasConditions(battleEvent, entity, property.Conditions);
				if(!conditionsMet) {
					if(property.TriggerType == TriggerPropertyType.ConditionBuff)
						RemoveConstantBuff(battleEvent, entity, property);

					continue;
				}

				bool updateRefreshTimer = false;

				//property.OnBattleEvent(battleEvent, entity, bem, combatExecutor);
				switch(property.TriggerType) {
					case TriggerPropertyType.Attack:
						updateRefreshTimer = ApplyAttack(battleEvent, entity, property);
						break;
					case TriggerPropertyType.ConditionBuff:
						ApplyConstantBuff(battleEvent, entity, property);
						break;
					case TriggerPropertyType.TimedBuff:
						updateRefreshTimer = ApplyTimedtBuff(battleEvent, entity, property);
						break;
					case TriggerPropertyType.Skill:
						updateRefreshTimer = ApplySkill(battleEvent, entity, property);
						break;
				}

				if (updateRefreshTimer)
					RecordRefresh(entity, property);
			}
		}

		internal virtual bool ApplyAttack(ICombatEvent battleEvent, IBattleEntity entity, ITriggeredProperty property) 
		{
			// TODO
			return true;
		}

		internal virtual void ApplyConstantBuff(ICombatEvent battleEvent, IBattleEntity entity, ITriggeredProperty property)
		{
			// TODO
		}

		internal virtual void RemoveConstantBuff(ICombatEvent battleEvent, IBattleEntity entity, ITriggeredProperty property)
		{
			// TODO
		}

		internal virtual bool ApplyTimedtBuff(ICombatEvent battleEvent, IBattleEntity entity, ITriggeredProperty property)
		{
			// TODO
			return true;
		}

		internal virtual bool ApplySkill(ICombatEvent battleEvent, IBattleEntity entity, ITriggeredProperty property)
		{
			// TODO
			return true;
		}

		internal virtual bool HasConditions(ICombatEvent battleEvent, IBattleEntity entity, IEnumerable<ITriggerCondition> conditions) {
			// TODO
			return true;
		}

		internal virtual IEnumerable<IBattleEntity> FilterTargets(ICombatEvent battleEvent, IBattleEntity entity, IEnumerable<ITriggerCondition> conditions)
		{
			// TODO
			return Enumerable.Empty<IBattleEntity>();
		}

		internal virtual bool RefreshValid(IBattleEntity entity, ITriggeredProperty property)
		{
			if (property.TriggerType == TriggerPropertyType.ConditionBuff)
				return true;

			// TODO track time? looks similar to how we process AI skills.
			return true;
		}

		internal virtual void RecordRefresh(IBattleEntity entity, ITriggeredProperty property) {
			// TODO
		}
	}
}