using System.Collections.Generic;
using Davfalcon.Revelator;
using Redninja.Components.Actions;
using Redninja.Components.Targeting;

namespace Redninja.Components.Skills
{
	/// <summary>
	/// Implementation that should contain details of what the skill should do and
	/// target scenario.
	/// </summary>
	public interface ISkill : IDamageSource
	{
		ActionTime Time { get; }

		IReadOnlyList<SkillTargetingSet> Targets { get; }

		IBattleAction GetAction(IEntityModel entity, ISelectedTarget[] targets);
	}
}
