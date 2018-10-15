using System.Collections.Generic;
using Redninja.Skills;
using Redninja.Targeting;

namespace Redninja.Decisions
{
	public interface ITargetingState
	{
		IBattleEntity Entity { get; }
		ISkill Skill { get; }
		SkillTargetingSet TargetingSet { get; }
		ITargetingRule TargetingRule { get; }
		TargetType TargetType { get; }
		bool Ready { get; }

		IEnumerable<IBattleEntity> GetTargetableEntities();
		bool IsValidTarget(IBattleEntity targetEntity);
		bool IsInPattern(int anchorRow, int anchorColumn, int targetRow, int targetColumn);
		ISelectedTarget GetSelectedTarget(IBattleEntity target);
		ISelectedTarget GetSelectedTarget(int team, Coordinate anchor);
	}
}
