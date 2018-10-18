namespace Redninja.Components.Decisions.AI
{
	public class AIConditionFactory
	{
		private AIConditionFactory() { }

		public static IAITargetCondition AlwaysTrue { get; } = As(ex => true);

		public static IAITargetCondition CreateCombatStatCondition(
			int conditionalValue,
			CombatStats combatStat,
			AIValueConditionOperator op,
			AIConditionType conditionType)
			=> new AICombatStatCondition(conditionalValue, combatStat, op, conditionType);

		private static IAITargetCondition As(SimpleAICondition.OnCondition condition) => new SimpleAICondition(condition);		

		public class SimpleAICondition : IAITargetCondition
		{		
			internal delegate bool OnCondition(IUnitModel entity);

			internal OnCondition conditionDelegate;

			internal SimpleAICondition(OnCondition onCondition)
			{
				conditionDelegate = onCondition;
			}

			public bool IsValid(IUnitModel entity) => conditionDelegate(entity);
		}
	}
}
