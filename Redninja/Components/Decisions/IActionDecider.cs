using System;
using Redninja.Components.Actions;

namespace Redninja.Components.Decisions
{
	public interface IActionDecider
	{
		event Action<IUnitModel, IBattleAction> ActionSelected;

		void ProcessNextAction(IUnitModel entity, IBattleModel battleModel);
	}
}
