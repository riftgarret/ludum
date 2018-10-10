using Redninja.Decisions;

namespace Redninja
{
	public interface IBattleView
	{
		void UpdateEntity(IBattleEntity entity);
		void SetViewModeTargeting(ISkillTargetingInfo targets);
		void SetViewModeDefault();
	}
}
