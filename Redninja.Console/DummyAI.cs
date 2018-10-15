using System;
using Davfalcon.Randomization;
using Redninja.Components.Actions;
using Redninja.Components.Decisions;

namespace Redninja.ConsoleDriver
{
	public class DummyAI : IActionDecider
	{
		public bool IsPlayer => false;

		public event Action<IUnitModel, IBattleAction> ActionSelected;

		public void ProcessNextAction(IUnitModel entity, IBattleModel battleModel)
		{
			ActionSelected?.Invoke(entity, new WaitAction(new RandomInteger(1, 5).Get()));
		}
	}
}
