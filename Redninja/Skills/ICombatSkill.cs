using System.Collections.Generic;
using Davfalcon.Revelator;
using Redninja.Actions;
using Redninja.Targeting;

namespace Redninja.Skills
{
	/// <summary>
	/// Implementation that should contain details of what the skill should do and
	/// target scenario.
	/// </summary>
	public interface ICombatSkill : IDamageSource
	{
		ActionTime Time { get; }

		ITargetingRule TargetRule { get; }

		IList<SkillExecutionTrigger> Operations { get; }

		IBattleAction CreateAction(IBattleEntity entity, ISelectedTarget target);
	}
}
