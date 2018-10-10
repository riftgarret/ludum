using System.Collections.Generic;
using Davfalcon.Revelator;
using Redninja.Actions;

namespace Redninja.Skills
{
	/// <summary>
	/// Implementation that should contain details of what the skill should do and
	/// target scenario.
	/// </summary>
	public interface ICombatSkill : IDamageSource
	{
		ActionTime Time { get; }

		IReadOnlyList<SkillTargetingSet> Targets { get; }
	}
}
