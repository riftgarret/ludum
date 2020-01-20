using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Davfalcon;
using Redninja.Components.Combat;
using Redninja.Components.Combat.Events;

namespace Redninja.Components.StatCalculators
{
	public struct PenetrationParam
	{
		public Stat dmgTypePenetration;
	}

	public class PenetrationCalculator : StatCalculator<PenetrationParam>
	{		
		public PenetrationCalculator(PenetrationParam param) => Param = param;

		public override int Calculate(IStats stats)
			=> stats[Param.dmgTypePenetration];

		public override void DamageOperationProcess(OperationContext oc)
		{
			oc.CaptureSourceStat(SkillOperationResult.Property.Penetration, Param.dmgTypePenetration);
		}
	}

	public static partial class Calculators
	{
		public static readonly PenetrationCalculator SLASH_PEN = new PenetrationCalculator(new PenetrationParam()
		{
			dmgTypePenetration = Stat.PenetrationSlash
		});

		public static readonly PenetrationCalculator FIRE_PEN = new PenetrationCalculator(new PenetrationParam()
		{
			dmgTypePenetration = Stat.PenetrationFire
		});
		

		public static int FinalSlashPenetration(this IStats stats) => SLASH_PEN.Calculate(stats);

		public static int FinalFirePenetration(this IStats stats) => FIRE_PEN.Calculate(stats);

	}
}
