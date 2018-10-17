using System;
using System.Collections.Generic;
using Redninja.Components.Clock;

namespace Redninja.Entities
{
	public interface IBattleEntityManager : IBattleModel
	{
		new IEnumerable<IBattleEntity> Entities { get; }

		event Action<IBattleEntity> DecisionRequired;

		void AddEntity(IBattleEntity entity, IClock clock);
		void RemoveEntity(IBattleEntity entity);
		void InitializeBattlePhase();
	}
}
