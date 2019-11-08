namespace Redninja.Components.Conditions
{
	public enum ExpressionResultType
	{
		Unit,
		ClassName,
		IntValue,
		Percent,
		Battle,
		Event,
		Environment // not to be used, but for consistency
	}

	public enum ConditionOperatorType
	{
		LT,
		LTE,
		GT,
		GTE,
		EQ,
		NEQ
	}

	public enum ConditionalOperatorRequirement
	{
		Any,
		CountOp,
		All,
	}

	public enum GroupOp
	{
		Highest,
		Lowest,
		Avg
	}

	

	public enum ConditionTargetType
	{
		Self,
		Target,
		Source,
		Enemy,
		Ally,
		AllyNotSelf,
		Unit
	}
}
