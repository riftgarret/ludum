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
	public struct ResistanceParam
	{
		public Stat dmgTypeResistance;
	}

	public class ResistanceCalculator : StatCalculator<ResistanceParam>
	{		
		public ResistanceCalculator(ResistanceParam param) => Param = param;		

		public override int Calculate(IStats stats)
			=> stats[Param.dmgTypeResistance];

		public override void DamageOperationProcess(OperationContext oc)
		{
			oc.CaptureTargetStat(SkillOperationResult.Property.Resistance, Param.dmgTypeResistance);
		}
	}

	public static partial class Calculators
	{
		public static readonly ResistanceCalculator SLASH_RES = new ResistanceCalculator(new ResistanceParam()
		{
			dmgTypeResistance = Stat.ResistanceSlash
		});

		public static readonly ResistanceCalculator FIRE_RES = new ResistanceCalculator(new ResistanceParam()
		{
			dmgTypeResistance = Stat.ResistanceFire
		});				

		public static int FinalSlashResistance(this IStats stats) => SLASH_RES.Calculate(stats);

		public static void PopulateSlashResistance(OperationContext oc)
		{

		}

		public static int FinalFireResistance(this IStats stats) => FIRE_RES.Calculate(stats);
	}
}
