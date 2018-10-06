using System;
using Redninja.Actions;

namespace Redninja.ConsoleDriver
{
	public class PlayerInput : IActionDecider
	{
		public bool IsPlayer => true;

		public event Action<IBattleEntity, IBattleAction> ActionSelected;

		public void ProcessNextAction(IBattleEntity entity, IBattlePresenter presenter)
		{
			Console.WriteLine("Waiting for player input...");
			Console.ReadKey();
			ActionSelected?.Invoke(entity, new MovementAction(entity, 3, 3));
		}
	}
}
