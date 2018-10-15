using System;
using Redninja.Components.Actions;

namespace Redninja
{
	public interface IBattleEntityManager : IBattleModel
	{
		event Action<IBattleEntity> DecisionRequired;

		void AddBattleEntity(IBattleEntity entity, IClock clock);
		void RemoveBattleEntity(IBattleEntity entity);
		void SetAction(IBattleEntity entity, IBattleAction action);
		void InitializeBattlePhase();
	}
}
