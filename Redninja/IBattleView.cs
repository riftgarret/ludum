using System;
using Redninja.Decisions;
using Redninja.Skills;
using Redninja.Targeting;

namespace Redninja
{
	public interface IBattleView
	{
		// replace this with individual events, leave action generation to presenter
		event Action<IBattleEntity, IBattleAction> ActionSelected;

		event Action<IBattleEntity> MovementInitiated;
		event Action<Coordinate> MovementPathUpdated;
		event Action MovementConfirmed;
		event Action<IBattleEntity, ISkill> SkillSelected;
		event Action<ISelectedTarget> TargetSelected;
		event Action TargetingCanceled;

		void SetBattleModel(IBattleModel model);
		void OnBattleEventOccurred(IBattleEvent battleEvent);
		void OnDecisionNeeded(IBattleEntity entity);
		void SetViewMode(IMovementState movementState);
		void SetViewMode(ITargetingState targetingState);
		void SetViewModeDefault();
	}
}
