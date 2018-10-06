using System;

namespace Redninja
{
	public interface IActionDecider
	{
		bool IsPlayer { get; }
		event Action<IBattleEntity, IBattleAction> ActionSelected;
		void ProcessNextAction(IBattleEntity entity, IBattlePresenter presenter);
	}
}
