using System;
using Davfalcon.Randomization;
using Redninja.Components.Actions;
using Redninja.Entities;

namespace Redninja.ConsoleDriver
{
	public class DummyAI : IActionDecider
	{
		public bool IsPlayer => false;

		public event Action<IBattleEntity, IBattleAction> ActionSelected;

		public void ProcessNextAction(IBattleEntity entity, IBattleEntityManager entityManager)
		{
			ActionSelected?.Invoke(entity, new WaitAction(new RandomInteger(1, 5).Get()));
		}
	}
}
