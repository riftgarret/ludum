namespace Redninja.Components.Decisions.AI
{
	public class AIConditionFactory
	{
		private AIConditionFactory() { }

		public static IAITargetCondition AlwaysTrue { get; } = As(ex => true);

		public static IAITargetCondition CreateCombatStatCondition(
			int conditionalValue,
			IStatEvaluator statEvaluator,
			AIValueConditionOperator op)
			=> new AICombatStatCondition(conditionalValue, statEvaluator, op);

		private static IAITargetCondition As(SimpleAICondition.OnCondition condition) => new SimpleAICondition(condition);		

		public class SimpleAICondition : IAITargetCondition
		{		
			internal delegate bool OnCondition(IBattleEntity entity);

			internal OnCondition conditionDelegate;

			internal SimpleAICondition(OnCondition onCondition)
			{
				conditionDelegate = onCondition;
			}

			public bool IsValid(IBattleEntity entity) => conditionDelegate(entity);
		}
	}
}
