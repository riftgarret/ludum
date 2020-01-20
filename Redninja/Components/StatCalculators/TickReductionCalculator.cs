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
	public struct TickReductionParam
	{
		public Stat dmgTypeReduction;
	}

	public class TickReductionCalculator : StatCalculator<TickReductionParam>
	{		
		public TickReductionCalculator(TickReductionParam param) => Param = param;

		public override int Calculate(IStats stats)
		{
			int val = 0;
			val += stats[Param.dmgTypeReduction];

			return val;			
		}

		public override void DamageOperationProcess(OperationContext oc)
		{
			oc.CaptureSourceStat(TickOperationResult.Property.Reduction, Param.dmgTypeReduction);						
		}		
	}

	public static partial class Calculators
	{
		public static readonly TickReductionCalculator BLEED_TICK_REDUCTION = new TickReductionCalculator(new TickReductionParam()
		{
			dmgTypeReduction = Stat.BleedDamageReduction,			
		});								
	}
}
