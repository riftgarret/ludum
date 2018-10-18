using System.Collections.Generic;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;

namespace Redninja.Components.Decisions
{
	public interface ITargetingView
	{
		IUnitModel Entity { get; }

		// View probably doesn't need all these, clean up later
		ISkill Skill { get; }
		SkillTargetingSet TargetingSet { get; }
		ITargetingRule TargetingRule { get; }
		TargetType TargetType { get; }
		bool Ready { get; }

		IEnumerable<IUnitModel> GetTargetableEntities();
		bool IsValidTarget(IUnitModel targetEntity);
		bool IsInPattern(int anchorRow, int anchorColumn, int targetRow, int targetColumn);
		ISelectedTarget GetSelectedTarget(IUnitModel target);
		ISelectedTarget GetSelectedTarget(int team, Coordinate anchor);
	}
}
