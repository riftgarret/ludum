using Redninja.Skills;
using Redninja.Targeting;

namespace Redninja.Decisions
{
	public interface ISkillTargetingInfo
	{
		IBattleEntity Entity { get; }
		ICombatSkill Skill { get; }
		SkillTargetingSet TargetingSet { get; }
		ITargetingRule TargetingRule { get; }
		TargetType TargetType { get; }
		bool Ready { get; }

		bool IsValidTarget(IBattleEntity targetEntity);
		bool IsInPattern(int anchorRow, int anchorColumn, int targetRow, int targetColumn);
		ISelectedTarget GetSelectedTarget(IBattleEntity target);
		ISelectedTarget GetSelectedTarget(int team, Coordinate anchor);
	}
}
