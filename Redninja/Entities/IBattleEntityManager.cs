using System;
using System.Collections.Generic;
using Redninja.Components.Actions;
using Redninja.Components.Clock;

namespace Redninja.Entities
{
	internal interface IBattleEntityManager : IBattleModel, IClockSynchronized
	{
		new IEnumerable<IBattleEntity> Entities { get; }

		event Action<IBattleEntity> ActionNeeded;
		event Action<IBattleEntity, IBattleAction> ActionSet;

		void AddGrid(int team, Coordinate size);
		void AddEntity(IBattleEntity entity);
		void RemoveEntity(IBattleEntity entity);
		void InitializeBattlePhase();
	}
}
