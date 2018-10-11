using System;
using Redninja.Decisions;
using Redninja.Skills;
using Redninja.Targeting;

namespace Redninja
{
	public interface IBattleView
	{
		event Action<IBattleEntity, IBattleAction> ActionSelected;
		event Action<IBattleEntity, ICombatSkill> SkillSelected;
		event Action<ISelectedTarget> TargetSelected;
		event Action TargetingCanceled;

		void SetBattleModel(IBattleModel model);
		void OnBattleEventOccurred(IBattleEvent battleEvent);
		void OnDecisionNeeded(IBattleEntity entity);
		void SetViewModeTargeting(ISkillTargetingInfo targetingInfo);
		void SetViewModeDefault();
	}
}
