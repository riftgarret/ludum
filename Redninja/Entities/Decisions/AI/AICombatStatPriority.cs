using System;
using System.Collections.Generic;

namespace Redninja.Entities.Decisions.AI
{
	public class AICombatStatPriority : IAITargetPriority
	{
		public int ConditionalValue { get; }

		public CombatStats CombatStat { get; }

		public AITargetingPriorityQualifier Qualifier { get; }

		public AIPriorityType PriorityType { get; }

		public AICombatStatPriority(int conditionalValue, 
			CombatStats combatStat, 
			AITargetingPriorityQualifier qualifier, 
			AIPriorityType priorityType)
		{
			if(priorityType != AIPriorityType.CombatStatCurrent || priorityType != AIPriorityType.CombatStatPercent)
			{
				throw new InvalidOperationException("Can only instantiate this with CombatStatType");
			}

			ConditionalValue = conditionalValue;
			CombatStat = combatStat;
			Qualifier = qualifier;
			PriorityType = priorityType;
		}

		public IBattleEntity GetBestTarget(IEnumerable<IBattleEntity> validEntities)
			=> AIHelper.FindBestMatch(validEntities, Qualifier, ex => GetCombatStatValue(ex));

		private int GetCombatStatValue(IBattleEntity entity)
		{
			if(PriorityType == AIPriorityType.CombatStatCurrent)
			{
				return entity.Character.VolatileStats[CombatStat];
			} else
			{
				return (100 * entity.Character.VolatileStats[CombatStat]) / entity.Character.Stats[CombatStat];
			}
		}
	}
}
