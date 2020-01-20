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
	public struct DamageParam
	{
		public Stat
			extra,
			scale,
			genericExtra,
			genericScale;
	}

	public class DamageCalculator : StatCalculator<DamageParam>
	{		
		public DamageCalculator(DamageParam param) => Param = param;
		
		public override int Calculate(IStats stats)
		{
			float valExtra = 0;
			valExtra += stats[Param.extra];
			valExtra += stats[Param.genericExtra];
			valExtra += stats[Stat.DamageAllExtra];

			float valScale = 100; // because 1 int = 1%, start at 100%
			valScale += stats[Param.scale];
			valScale += stats[Param.genericScale];
			valScale += stats[Stat.DamageAllScale];

			return Math.Max((int)(valExtra * valScale / 100F), 0);
		}
			
		public override void DamageOperationProcess(OperationContext oc)
		{
			oc.CaptureSourceStat(SkillOperationResult.Property.DamageRaw, Param.extra);
			oc.CaptureSourceStat(SkillOperationResult.Property.DamageRaw, Param.genericExtra);
			oc.CaptureSourceStat(SkillOperationResult.Property.DamageRaw, Stat.DamageAllExtra);

			oc.CaptureSourceStat(SkillOperationResult.Property.DamageScale, Param.scale);
			oc.CaptureSourceStat(SkillOperationResult.Property.DamageScale, Param.genericScale);
			oc.CaptureSourceStat(SkillOperationResult.Property.DamageScale, Stat.DamageAllScale);
		}
	}

	public static partial class Calculators
	{
		public static readonly DamageCalculator SLASH_DMG = new DamageCalculator(new DamageParam()
		{
			extra = Stat.DamageSlashExtra,
			scale = Stat.DamageSlashScale,
			genericExtra = Stat.DamagePhysicalExtra,
			genericScale = Stat.DamagePhysicalScale
		});

		public static readonly DamageCalculator FIRE_DMG = new DamageCalculator(new DamageParam()
		{
			extra = Stat.DamageFireExtra,
			scale = Stat.DamageFireScale,
			genericExtra = Stat.DamageFireExtra,
			genericScale = Stat.DamageFireScale
		});

		public static void SlashDamageOp(this OperationContext oc) => SLASH_DMG.DamageOperationProcess(oc);

		public static void FireDamageOp(this OperationContext oc) => FIRE_DMG.DamageOperationProcess(oc);


		public static int FinalSlashDamage(this IStats stats) => SLASH_DMG.Calculate(stats);

		public static int FinalFireDamage(this IStats stats) => FIRE_DMG.Calculate(stats);		
	}
}
