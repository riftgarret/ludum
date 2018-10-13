using Redninja.Skills;
using Redninja.Targeting;

namespace Redninja
{
	public interface IBattleViewCallbacks
	{
		void OnSkillSelected(IBattleEntity entity, ISkill skill);
		void OnTargetSelected(ISelectedTarget target);
	}
}
