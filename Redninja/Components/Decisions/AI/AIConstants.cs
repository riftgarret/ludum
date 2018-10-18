namespace Redninja.Components.Decisions.AI
{
	public enum AIPriorityType
	{
		None,
		CombatStatPercent,			
		CombatStatCurrent,
		MostTargets
	}

	public enum AIConditionType
	{		
		AlwaysTrue,
		CombatStatPercent,
		CombatStatCurrent,
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
