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
	public struct TickDamageParam
	{
		public Stat dmgTypeExtra, dmgTypeScale;
	}

	public class TickDamageCalculator : StatCalculator<TickDamageParam>
	{		
		public TickDamageCalculator(TickDamageParam param) => Param = param;

		public override int Calculate(IStats stats)
		{
			int val = stats[Param.dmgTypeExtra];

			float scale = stats[Param.dmgTypeScale];

			return (int)(val * scale);
		}

		public override void DamageOperationProcess(OperationContext oc)
		{
			oc.CaptureSourceStat(TickOperationResult.Property.DamageRaw, Param.dmgTypeExtra);
			oc.CaptureSourceStat(TickOperationResult.Property.DamageScale, Param.dmgTypeScale);
		}		
	}

	public static partial class Calculators
	{
		public static readonly TickDamageCalculator BLEED_TICK_DAMAGE = new TickDamageCalculator(new TickDamageParam()
		{
			dmgTypeExtra = Stat.BleedDamageExtra,
			dmgTypeScale = Stat.BleedDamageScale
		});								
	}
}
