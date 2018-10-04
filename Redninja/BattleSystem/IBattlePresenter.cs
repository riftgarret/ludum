using System;
using System.Collections.Generic;

namespace Redninja.BattleSystem
{
	public interface IBattlePresenter
	{
		event Action<IBattleEvent> BattleEventOccurred;

		void AddBattleEntity(IBattleEntity entity);
		void IncrementGameClock(float timeDelta);
		void Initialize(IEnumerable<IBattleEntity> units);
		void ProcessBattleOperationQueue();
		void ProcessDecisionQueue();
		void Update();
	}
}