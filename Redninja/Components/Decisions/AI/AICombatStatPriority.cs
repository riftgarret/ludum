using System;
using System.Collections.Generic;

namespace Redninja.Components.Decisions.AI
{
	public class AICombatStatPriority : IAITargetPriority
	{
		public LiveStat LiveStat { get; }

		public AITargetingPriorityQualifier Qualifier { get; }

		public AIPriorityType PriorityType { get; }

		public AICombatStatPriority(LiveStat liveStat,
			AITargetingPriorityQualifier qualifier,
			AIPriorityType priorityType)
		{
			if (priorityType != AIPriorityType.CombatStatCurrent && priorityType != AIPriorityType.CombatStatPercent)
			{
				throw new InvalidOperationException("Can only instantiate this with CombatStatType");
			}

			LiveStat = liveStat;
			Qualifier = qualifier;
			PriorityType = priorityType;
		}

		public IBattleEntity GetBestTarget(IEnumerable<IBattleEntity> validEntities)
			=> AIHelper.FindBestMatch(validEntities, Qualifier, ex => GetCombatStatValue(ex));

		private int GetCombatStatValue(IBattleEntity entity)
		{
			if (PriorityType == AIPriorityType.CombatStatCurrent)
			{
				return entity.LiveStats[LiveStat].Current;
			}
			else
			{
				return (int)(100 * entity.LiveStats[LiveStat].Percent);
			}
		}

		public override bool Equals(object obj)
			=> obj is AICombatStatPriority priority &&
				LiveStat == priority.LiveStat &&
				Qualifier == priority.Qualifier &&
				PriorityType == priority.PriorityType;

		public override int GetHashCode() => $"{LiveStat}{Qualifier}{PriorityType}".GetHashCode();
	}
}
