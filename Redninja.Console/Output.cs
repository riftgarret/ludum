using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Davfalcon.Revelator.Borger;
using Redninja.BattleSystem;

namespace Redninja.ConsoleDriver
{
	public static class Output
	{
		public static string Print(this IBattleEntity entity)
			=> $"{entity.Character.Name} HP: {entity.Character.VolatileStats[CombatStats.HP]}/{entity.Character.Stats[CombatStats.HP]} {entity.Position}";
	}
}
