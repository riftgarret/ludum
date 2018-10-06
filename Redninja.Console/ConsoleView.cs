using System;

namespace Redninja.ConsoleDriver
{
	public class ConsoleView : IBattleView
	{
		public void UpdateEntity(IBattleEntity entity)
		{
			Console.WriteLine(entity.Print());
		}
	}
}
