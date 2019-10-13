using System.Collections.Generic;
using Redninja.Components.Actions;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;

namespace Redninja.Components.Decisions
{
	public interface ITargetingContext : IActionProvider
	{
		ISkill Skill { get; }

		IReadOnlyList<ITargetSpec> TargetSpecs { get; }

		bool IsReady { get; }		
	}
}
