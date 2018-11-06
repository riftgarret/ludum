using System;

namespace Redninja.Components.Decisions.AI
{
	public class AICombatStatCondition : IAITargetCondition
	{
		public int ConditionalValue { get; }

		public CombatStats CombatStat { get; }

		public AIValueConditionOperator Op { get; }

		public AIConditionType ConditionType { get; }

		public AICombatStatCondition(int conditionalValue,
			CombatStats combatStat,
			AIValueConditionOperator op,
			AIConditionType conditionType)
		{
			if (conditionType != AIConditionType.CombatStatCurrent && conditionType != AIConditionType.CombatStatPercent)
			{
				throw new InvalidOperationException("Cannot intantiate AICombatStatCondition without proper type");
			}

			ConditionalValue = conditionalValue;
			CombatStat = combatStat;
			Op = op;
			ConditionType = conditionType;
		}

		public bool IsValid(IUnitModel entity)
			=> AIHelper.EvaluateCondition(GetCombatStatValue(entity), Op, ConditionalValue);

		private int GetCombatStatValue(IUnitModel entity)
		{
			if (ConditionType == AIConditionType.CombatStatCurrent)
			{
				return entity.VolatileStats[CombatStat];
			}
			else
			{
				return (100 * entity.VolatileStats[CombatStat]) / entity.Stats[CombatStat];
			}
		}

		public override bool Equals(object obj)
			=> obj is AICombatStatCondition condition &&
				ConditionalValue == condition.ConditionalValue &&
				CombatStat == condition.CombatStat &&
				Op == condition.Op &&
				ConditionType == condition.ConditionType;

		public override int GetHashCode() => $"{CombatStat}{Op}{ConditionalValue}{ConditionType}".GetHashCode();
	}
}
