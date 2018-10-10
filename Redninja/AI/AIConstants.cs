using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.AI
{
	public enum AITargetType
	{
		Self,
		Enemy,
		Ally
	}

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
