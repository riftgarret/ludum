using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Davfalcon;

namespace Redninja.Components.StatCalculators
{
	struct PenetrationParam
	{
		public Stat dmgTypePenetration;
	}

	class PenetrationCalculator : StatCalculator<PenetrationParam>
	{		
		public PenetrationCalculator(PenetrationParam param) => Param = param;

		protected override PenetrationParam Param { get; }

		protected override int CalculateCommon(PenetrationParam param, IStats stats)
			=> stats[param.dmgTypePenetration];
	}	

	public static class PenetrationCalculatorExt
	{
		private static readonly PenetrationCalculator SLASH_PEN = new PenetrationCalculator(new PenetrationParam()
		{
			dmgTypePenetration = Stat.PenetrationSlash
		});

		private static readonly PenetrationCalculator FIRE_PEN = new PenetrationCalculator(new PenetrationParam()
		{
			dmgTypePenetration = Stat.PenetrationFire
		});
		

		public static int FinalSlashPenetration(this IStats stats) => SLASH_PEN.Calculate(stats);

		public static int FinalFirePenetration(this IStats stats) => FIRE_PEN.Calculate(stats);

	}
}
