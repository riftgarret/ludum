using System;
using System.Collections.Generic;

namespace Redninja.Components.Decisions.AI
{
	public class AICombatStatPriority : IAITargetPriority
	{		
		public CombatStats CombatStat { get; }

		public AITargetingPriorityQualifier Qualifier { get; }

		public AIPriorityType PriorityType { get; }

		public AICombatStatPriority(CombatStats combatStat,
			AITargetingPriorityQualifier qualifier,
			AIPriorityType priorityType)
		{
			if(priorityType != AIPriorityType.CombatStatCurrent && priorityType != AIPriorityType.CombatStatPercent)
			{
				throw new InvalidOperationException("Can only instantiate this with CombatStatType");
			}
	
			CombatStat = combatStat;
			Qualifier = qualifier;
			PriorityType = priorityType;
		}

		public IUnitModel GetBestTarget(IEnumerable<IUnitModel> validEntities)
			=> AIHelper.FindBestMatch(validEntities, Qualifier, ex => GetCombatStatValue(ex));

		private int GetCombatStatValue(IUnitModel entity)
		{
			if(PriorityType == AIPriorityType.CombatStatCurrent)
			{
				return entity.Character.VolatileStats[CombatStat];
			} else
			{
				return (100 * entity.Character.VolatileStats[CombatStat]) / entity.Character.Stats[CombatStat];
			}
		}

		public override bool Equals(object obj)
		{
			var priority = obj as AICombatStatPriority;
			return priority != null &&
				   CombatStat == priority.CombatStat &&
				   Qualifier == priority.Qualifier &&
				   PriorityType == priority.PriorityType;
		}
	}
}
