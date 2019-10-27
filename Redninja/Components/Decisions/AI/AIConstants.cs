namespace Redninja.Components.Decisions.AI
{
	public enum AIPriorityType
	{
		None,
		StatValue,			
		MostTargets
	}

	public enum AIConditionType
	{		
		AlwaysTrue,
		StatValue,
		Class,
		Position,
		BeneficialStatusEffect,
		HostileStatusEffect
	}

	public enum AIValueConditionOperator
	{
		LT,
		LTE,
		GT,
		GTE,
		EQ
	}

	public enum AITargetingPriorityQualifier
	{
		None,
		Highest,
		Lowest, 
		Average		
	}
}
