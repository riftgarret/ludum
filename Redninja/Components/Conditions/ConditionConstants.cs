using System;
namespace Redninja.Components.Conditions
{
	public enum ExpressionResultType
	{
		Unit,
		ClassName,
		IntValue,
		Percent,
		Target
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
		Enemy,
		Ally,
		AllyNotSelf,
		All
	}
}
