namespace Redninja.Components.Decisions.AI
{
	public class AIConditionFactory
	{
		private AIConditionFactory() { }

		public static IAITargetCondition AlwaysTrue => As(ex => true);

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
