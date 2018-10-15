using System;
using System.Collections.Generic;
using Redninja.Components.Actions;

namespace Redninja.Entities
{
	public interface IBattleEntityManager : IBattleModel
	{
		new IEnumerable<IBattleEntity> Entities { get; }

		event Action<IBattleEntity> DecisionRequired;

		void AddBattleEntity(IBattleEntity entity, IClock clock);
		void RemoveBattleEntity(IBattleEntity entity);
		void SetAction(IBattleEntity entity, IBattleAction action);
		void InitializeBattlePhase();
	}
}
