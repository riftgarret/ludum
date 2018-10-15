﻿using System.Collections.Generic;
using Davfalcon.Revelator;
using Redninja.Components.Actions;
using Redninja.Components.Targeting;

namespace Redninja.Skills
{
	/// <summary>
	/// Implementation that should contain details of what the skill should do and
	/// target scenario.
	/// </summary>
	public interface ISkill : IDamageSource
	{
		ActionTime Time { get; }

		IReadOnlyList<SkillTargetingSet> Targets { get; }

		IBattleAction GetAction(IBattleEntity entity, ISelectedTarget[] targets);
	}
}
