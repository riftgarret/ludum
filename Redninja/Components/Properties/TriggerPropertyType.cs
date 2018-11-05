using System;
namespace Redninja.Components.Properties
{
	public enum TriggerPropertyType
	{
		ConditionBuff, // while hps < 30, always have this up
		TimedBuff,  // when hp's drop < 30, grant 6 seconds of this buff
		Skill,
		Attack
	}
}
