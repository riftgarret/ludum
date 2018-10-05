using System;
using System.Collections.Generic;

namespace Redninja.BattleSystem
{
	public interface IBattlePresenter
	{
		event Action<IBattleEvent> BattleEventOccurred;

		void AddBattleEntity(IBattleEntity entity);
		void AddBattleEntities(IEnumerable<IBattleEntity> entities);
		void IncrementGameClock(float timeDelta);
		void Initialize();
		void ProcessBattleOperationQueue();
		void ProcessDecisionQueue();
		void Update();
	}
}