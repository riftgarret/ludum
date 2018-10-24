using System.Collections.Generic;
using Davfalcon;
using Redninja.Components.Actions;
using Redninja.Components.Targeting;

namespace Redninja.Components.Skills
{
	/// <summary>
	/// Implementation that should contain details of what the skill should do and
	/// target scenario.
	/// </summary>
	public interface ISkill : INameable
	{
		ActionTime Time { get; }

		IReadOnlyList<SkillTargetingSet> Targets { get; }

		IBattleAction GetAction(IUnitModel entity, ISelectedTarget[] targets);
	}
}
