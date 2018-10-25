using Redninja.Components.Decisions;
using Redninja.Components.Combat.Events;

namespace Redninja.View
{
	public interface IBattleView
	{
		void SetBattleModel(IBattleModel model);
		void OnBattleEventOccurred(IBattleEvent battleEvent);
		void OnDecisionNeeded(IUnitModel entity);
		void Resume();
		void SetViewMode(IBaseCallbacks callbacks);
		void SetViewMode(IActionsView actionsContext, ISkillsCallbacks callbacks);
		void SetViewMode(IMovementView movementContext, IMovementCallbacks callbacks);
		void SetViewMode(ITargetingView targetingContext, ITargetingCallbacks callbacks);
	}
}
