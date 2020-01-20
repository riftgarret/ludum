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
	public struct ReductionParam
	{
		public Stat dmgTypeReduction, genericTypeReduction;
	}

	public class ReductionCalculator : StatCalculator<ReductionParam>
	{		
		public ReductionCalculator(ReductionParam param) => Param = param;

		public override int Calculate(IStats stats)
		{
			int val = 0;
			val += stats[Param.dmgTypeReduction];
			val += stats[Param.genericTypeReduction];
			val += stats[Stat.ReductionAll];

			return Math.Min(val, GameRules.MAX_REDUCTION);			
		}

		public override void DamageOperationProcess(OperationContext oc)
		{
			oc.CaptureSourceStat(SkillOperationResult.Property.Reduction, Param.dmgTypeReduction);
			oc.CaptureSourceStat(SkillOperationResult.Property.Reduction, Param.genericTypeReduction);
			oc.CaptureSourceStat(SkillOperationResult.Property.Reduction, Stat.ReductionAll);
		}		
	}

	public static partial class Calculators
	{
		public static readonly ReductionCalculator SLASH_REDUCTION = new ReductionCalculator(new ReductionParam()
		{
			dmgTypeReduction = Stat.ReductionSlash,
			genericTypeReduction = Stat.ReductionPhysical
		});

		public static readonly ReductionCalculator FIRE_REDUCTION = new ReductionCalculator(new ReductionParam()
		{
			dmgTypeReduction = Stat.ReductionFire,
			genericTypeReduction = Stat.ReductionElemental
		});
		
		public static int FinalSlashReduction(this IStats stats) => SLASH_REDUCTION.Calculate(stats);

		public static int FinalFireReduction(this IStats stats) => FIRE_REDUCTION.Calculate(stats);

	}
}
