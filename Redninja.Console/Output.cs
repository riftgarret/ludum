using System;
using System.Linq;

namespace Redninja.ConsoleDriver
{
	public static class Output
	{
		public static void Print(this IUnitModel entity)
			=> Console.WriteLine(
				$"{entity.Name} HP: {entity.VolatileStats[CombatStats.HP]}/{entity.Stats[CombatStats.HP]} ({(Coordinate)entity.Position})" + Environment.NewLine +
				$"\tAction: {entity.CurrentActionName} [{entity.Phase} {(int)(entity.PhaseProgress * 100)}%]");
	}
}
