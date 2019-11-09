using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Davfalcon;

namespace Redninja.Components.StatCalculators
{
	struct ReductionParam
	{
		public Stat dmgTypeReduction, genericTypeReduction;
	}

	class ReductionCalculator : StatCalculator<ReductionParam>
	{
		private const int MAX_REDUCTION = 70;

		public ReductionCalculator(ReductionParam param) => Param = param;

		public override int Calculate(IStats stats) => Math.Max(MAX_REDUCTION, base.Calculate(stats));

		protected override ReductionParam Param { get; }

		protected override int CalculateCommon(ReductionParam param, IStats stats)
			=> stats[param.dmgTypeReduction] + stats[param.genericTypeReduction] + stats[Stat.ReductionAll];
	}	

	public static class ReductionCalculatorExt
	{
		private static readonly ReductionCalculator SLASH_REDUCTION = new ReductionCalculator(new ReductionParam()
			{
				dmgTypeReduction = Stat.ReductionSlash,
				genericTypeReduction = Stat.ReductionPhysical
			});

		private static readonly ReductionCalculator FIRE_REDUCTION = new ReductionCalculator(new ReductionParam()
		{
			dmgTypeReduction = Stat.ReductionFire,
			genericTypeReduction = Stat.ReductionElemental
		});
		
		public static int FinalSlashReduction(this IStats stats) => SLASH_REDUCTION.Calculate(stats);

		public static int FinalFireReduction(this IStats stats) => FIRE_REDUCTION.Calculate(stats);

	}
}
