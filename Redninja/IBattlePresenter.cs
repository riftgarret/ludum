using System;
using System.Collections.Generic;
using Davfalcon.Revelator;

namespace Redninja
{
	public interface IBattlePresenter
	{
		event Action<IBattleEvent> BattleEventOccurred;

		void AddBattleEntity(IBattleEntity entity);
		void AddBattleEntities(IEnumerable<IBattleEntity> entities);
		void AddCharacter(IUnit character, IActionDecider actionDecider, int row, int col);
		void AddCharacter(IBuilder<IUnit> builder, IActionDecider actionDecider, int row, int col);
		void IncrementGameClock(float timeDelta);
		void Initialize();
		void ProcessBattleOperationQueue();
		void ProcessDecisionQueue();
		void Update();
	}
}