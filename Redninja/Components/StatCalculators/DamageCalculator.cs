using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Davfalcon;

namespace Redninja.Components.StatCalculators
{
	struct DamageParam
	{
		public Stat dmgTypeDamage, genericTypeDamage;		
	}

	class DamageCalculator : StatCalculator<DamageParam>
	{		
		public DamageCalculator(DamageParam param) => Param = param;

		protected override DamageParam Param { get; }

		protected override int CalculateCommon(DamageParam param, IStats stats)
			=> stats[param.dmgTypeDamage] 
			+ stats[param.genericTypeDamage] 
			+ stats[Stat.DamageAll];
	}	

	public static class DamageCalculatorExt
	{
		private static readonly DamageCalculator SLASH_PEN = new DamageCalculator(new DamageParam()
		{
			dmgTypeDamage = Stat.DamageSlash,
			genericTypeDamage = Stat.DamagePhysical
		});

		private static readonly DamageCalculator FIRE_PEN = new DamageCalculator(new DamageParam()
		{
			dmgTypeDamage = Stat.DamageFire,
			genericTypeDamage = Stat.DamageElemental
		});
		

		public static int FinalSlashDamage(this IStats stats) => SLASH_PEN.Calculate(stats);

		public static int FinalFireDamage(this IStats stats) => FIRE_PEN.Calculate(stats);		
	}
}
