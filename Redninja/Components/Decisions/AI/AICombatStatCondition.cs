using System;

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
			=> obj is AICombatStatCondition condition &&
				ConditionalValue == condition.ConditionalValue &&
				StatEvaluator == condition.StatEvaluator &&
				Op == condition.Op &&
				ConditionType == condition.ConditionType;

		public override int GetHashCode() => $"{StatEvaluator}{Op}{ConditionalValue}{ConditionType}".GetHashCode();
	}
}
