using Redninja.Components.Skills;
using Redninja.Components.Targeting;
using Redninja.Entities;

namespace Redninja.View
{
	// This interface probably isn't needed anymore, converted to view events
	public interface IBattleViewCallbacks
	{
		void OnSkillSelected(IBattleEntity entity, ISkill skill);
		void OnTargetSelected(ISelectedTarget target);
	}
}
