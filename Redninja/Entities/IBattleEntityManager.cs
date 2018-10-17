using System;
using System.Collections.Generic;
using Redninja.Components.Actions;
using Redninja.Components.Clock;

namespace Redninja.Entities
{
	public interface IBattleEntityManager : IBattleModel
	{
		new IEnumerable<IBattleEntity> Entities { get; }

		event Action<IBattleEntity> ActionNeeded;
		event Action<IBattleEntity, IBattleAction> ActionSet;

		void AddEntity(IBattleEntity entity, IClock clock);
		void RemoveEntity(IBattleEntity entity);
		void InitializeBattlePhase();
	}
}
