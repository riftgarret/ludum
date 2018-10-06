using System;

namespace Redninja.ConsoleDriver
{
	public static class Output
	{
		public static string Print(this IBattleEntity entity)
			=> $"{entity.Character.Name} HP: {entity.Character.VolatileStats[CombatStats.HP]}/{entity.Character.Stats[CombatStats.HP]} {entity.Position}" + Environment.NewLine +
			   $"\tAction: {entity.CurrentAction.GetType()} {entity.CurrentAction.Phase} {(int)(entity.CurrentAction.PhaseProgress * 100)}%";
	}
}
