namespace Redninja.Components.Actions.Decisions.AI
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
		GT,
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
