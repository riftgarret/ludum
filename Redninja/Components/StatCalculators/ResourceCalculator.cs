using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Davfalcon;

namespace Redninja.Components.StatCalculators
{
	struct ResourceParam
	{
		public Stat raw, statScale, stat, levelScale;
	}

	class ResourceCalculator : StatCalculator<ResourceParam>
	{
		public ResourceCalculator(ResourceParam param) => Param = param;

		protected override ResourceParam Param { get; }

		protected override int CalculateCommon(ResourceParam param, IStats stats)
			=> (int)(stats[param.raw]
			+ (stats.AsScalor(param.statScale) * stats[param.stat])
			+ (stats.AsScalor(param.levelScale) * stats.Calculate(CalculatedStat.Level)));		
	}

	// move these to unit definition
	public static class ResourceStatCalculatorExt
	{
		private static ResourceCalculator HP_CALCULATOR = new ResourceCalculator(
			new ResourceParam()
			{
				raw = Stat.HP,
				statScale = Stat.HPConScale,
				stat = Stat.CON,
				levelScale = Stat.HPLevelScale
			});

		private static ResourceCalculator MANA_CALCULATOR = new ResourceCalculator(
			new ResourceParam()
			{
				raw = Stat.Mana,
				statScale = Stat.ManaIntScale,
				stat = Stat.INT,
				levelScale = Stat.ManaLevelScale
			});

		private static ResourceCalculator STAMINA_CALCULATOR = new ResourceCalculator(
			new ResourceParam()
			{
				raw = Stat.Stamina,
				statScale = Stat.StaminaStrScale,
				stat = Stat.STR,
				levelScale = Stat.StaminaLevelScale
			});

		public static int FinalHp(this IStats stats) => HP_CALCULATOR.Calculate(stats);

		public static int FinalMana(this IStats stats) => MANA_CALCULATOR.Calculate(stats);

		public static int FinalStamina(this IStats stats) => STAMINA_CALCULATOR.Calculate(stats);
	}
}
