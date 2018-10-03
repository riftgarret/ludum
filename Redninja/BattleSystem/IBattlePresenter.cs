using System.Collections.Generic;

namespace Redninja.BattleSystem
{
	public interface IBattlePresenter
	{
		void Initialize(IEnumerable<IBattleEntity> units);
		void OnGUI();
		void ProcessBattleOperationQueue();
		void ProcessDecisionQueue();
	}
}