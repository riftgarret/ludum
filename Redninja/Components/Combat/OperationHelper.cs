using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Davfalcon;
using Davfalcon.Stats;

namespace Redninja.Components.Combat
{
	public static class OperationHelper
	{
		public static IStats ExtractWeaponStats(IWeaponSkillParam param, IBattleEntity source)
		{
			// TODO, pull out weapon requirements, and turn them into IStats
			StatsMap stats = new StatsMap();

			stats[Stat.WeaponScaleEval] = 1;

			return stats;
		}		
	}
}
