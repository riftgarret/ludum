using Redninja.Skills;
using Redninja.Targeting;

namespace Redninja
{
	public interface IBattleViewCallbacks
	{
		void OnSkillSelected(IBattleEntity entity, ICombatSkill skill);
		void OnTargetSelected(ISelectedTarget target);
	}
}
