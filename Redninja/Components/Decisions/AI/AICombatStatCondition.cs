using System;
using System.Collections.Generic;

namespace Redninja.Components.Decisions.AI
{
	public class AICombatStatCondition : IAITargetCondition
	{
		public int ConditionalValue { get; }

		public IStatEvaluator StatEvaluator { get; }

		public AIValueConditionOperator Op { get; }

		public AIConditionType ConditionType { get; } = AIConditionType.StatValue;

		public AICombatStatCondition(int conditionalValue,
			IStatEvaluator statEvaluator,
			AIValueConditionOperator op)
		{
			StatEvaluator = statEvaluator;
			Op = op;
			ConditionalValue = conditionalValue;
		}

		public bool IsValid(IBattleEntity entity)
			=> AIHelper.EvaluateCondition(StatEvaluator.Eval(entity), Op, ConditionalValue);

		public override bool Equals(object obj)
		{
			var condition = obj as AICombatStatCondition;
			return condition != null &&
				   ConditionalValue == condition.ConditionalValue &&
				   EqualityComparer<IStatEvaluator>.Default.Equals(StatEvaluator, condition.StatEvaluator) &&
				   Op == condition.Op &&
				   ConditionType == condition.ConditionType;
		}

		public override int GetHashCode()
		{
			var hashCode = 1515522564;
			hashCode = hashCode * -1521134295 + ConditionalValue.GetHashCode();
			hashCode = hashCode * -1521134295 + EqualityComparer<IStatEvaluator>.Default.GetHashCode(StatEvaluator);
			hashCode = hashCode * -1521134295 + Op.GetHashCode();
			hashCode = hashCode * -1521134295 + ConditionType.GetHashCode();
			return hashCode;
		}
	}
}
