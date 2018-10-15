using System.Collections.Generic;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;

namespace Redninja.View
{
	public interface ITargetingView
	{
		IEntityModel Entity { get; }
		ISkill Skill { get; }
		SkillTargetingSet TargetingSet { get; }
		ITargetingRule TargetingRule { get; }
		TargetType TargetType { get; }
		bool Ready { get; }

		IEnumerable<IEntityModel> GetTargetableEntities();
		bool IsValidTarget(IEntityModel targetEntity);
		bool IsInPattern(int anchorRow, int anchorColumn, int targetRow, int targetColumn);
		ISelectedTarget GetSelectedTarget(IEntityModel target);
		ISelectedTarget GetSelectedTarget(int team, Coordinate anchor);
	}
}
