using System;
using System.Linq;

namespace Redninja.ConsoleDriver
{
	public static class Output
	{
		public static void Print(this IUnitModel entity)
			=> Console.WriteLine(
				$"{entity.Character.Name} HP: {entity.Character.VolatileStats[CombatStats.HP]}/{entity.Character.Stats[CombatStats.HP]} ({(Coordinate)entity.Position})" + Environment.NewLine +
				$"\tAction: {entity.CurrentActionName} [{entity.Phase} {(int)(entity.PhaseProgress * 100)}%]");
	}
}
