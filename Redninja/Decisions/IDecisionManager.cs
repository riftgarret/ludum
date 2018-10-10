using Redninja.Skills;
using Redninja.Targeting;

namespace Redninja.Decisions
{
	public interface IDecisionManager
	{
		//IBattleAction CreateAction(IBattleEntity entity, ICombatSkill combatSkill, SelectedTargets target);
		SkillSelectionMeta GetAvailableSkills(IBattleEntity entity);
		SkillTargetMeta GetSelectableTargets(IBattleEntity entity, ICombatSkill combatSkill);
	}
}