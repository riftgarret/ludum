using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Davfalcon;

namespace Redninja.Components.StatCalculators
{
	struct ResistanceParam
	{
		public Stat dmgTypeResistance;
	}

	class ResistanceCalculator : StatCalculator<ResistanceParam>
	{		
		public ResistanceCalculator(ResistanceParam param) => Param = param;

		protected override ResistanceParam Param { get; }

		protected override int CalculateCommon(ResistanceParam param, IStats stats)
			=> stats[param.dmgTypeResistance];
	}	

	public static class ResistanceCalculatorExt
	{
		private static readonly ResistanceCalculator SLASH_RES = new ResistanceCalculator(new ResistanceParam()
		{
			dmgTypeResistance = Stat.ResistanceSlash
		});

		private static readonly ResistanceCalculator FIRE_RES = new ResistanceCalculator(new ResistanceParam()
		{
			dmgTypeResistance = Stat.ResistanceFire
		});
		

		public static int FinalSlashResistance(this IStats stats) => SLASH_RES.Calculate(stats);

		public static int FinalFireResistance(this IStats stats) => FIRE_RES.Calculate(stats);

	}
}
