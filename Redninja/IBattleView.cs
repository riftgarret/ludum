using Redninja.Decisions;

namespace Redninja
{
	public interface IBattleView
	{
		void UpdateEntity(IBattleEntity entity);
		void SetViewModeTargeting(SkillTargetMeta targets);
		void SetViewModeDefault();
	}
}
