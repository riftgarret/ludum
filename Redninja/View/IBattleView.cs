using System;
using Redninja.Components.Actions;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;
using Redninja.Events;

namespace Redninja.View
{
	public interface IBattleView
	{
		// replace this with individual events, leave action generation to presenter
		event Action<IUnitModel, IBattleAction> ActionSelected;

		event Action<IUnitModel> MovementInitiated;
		event Action<Coordinate> MovementPathUpdated;
		event Action MovementConfirmed;
		event Action<IUnitModel, ISkill> SkillSelected;
		event Action<ISelectedTarget> TargetSelected;
		event Action TargetingCanceled;

		void SetBattleModel(IBattleModel model);
		void OnBattleEventOccurred(IBattleEvent battleEvent);
		void OnDecisionNeeded(IUnitModel entity);
		void SetViewMode(IMovementView movementState);
		void SetViewMode(ITargetingView targetingState);
		void SetViewModeDefault();
	}
}
