using Redninja.Components.Decisions;
using Redninja.Components.Combat.Events;

namespace Redninja.View
{
	public interface IBattleView
	{
		void SetBattleModel(IBattleModel model);
		void OnBattleEventOccurred(ICombatEvent battleEvent);
		void OnDecisionNeeded(IUnitModel entity);
		void Resume();
	}
}
