using System;

namespace Redninja.Decisions.AI
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
			if(conditionType != AIConditionType.CombatStatCurrent || conditionType != AIConditionType.CombatStatPercent)
			{
				throw new InvalidOperationException("Cannot intantiate AICombatStatCondition without proper type");
			}

			ConditionalValue = conditionalValue;
			CombatStat = combatStat;
			Op = op;
			ConditionType = conditionType;
		}

		public bool IsValid(IBattleEntity entity)
			=> AIHelper.EvaluateCondition(GetCombatStatValue(entity), Op, ConditionalValue);

		private int GetCombatStatValue(IBattleEntity entity)
		{
			if (ConditionType == AIConditionType.CombatStatCurrent)
			{
				return entity.Character.VolatileStats[CombatStat];
			}
			else
			{
				return (100 * entity.Character.VolatileStats[CombatStat]) / entity.Character.Stats[CombatStat];
			}
		}
	}
}
