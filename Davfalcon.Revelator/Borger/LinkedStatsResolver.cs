using System;

namespace Davfalcon.Revelator.Borger
{
	[Serializable]
	public class LinkedStatsResolver : LinkedStatsResolverBase
	{
		public const int BASE_ATTRIBUTE = 5;
		public const int BASE_HIT = 100;
		public const int BASE_EVADE = 0;

		private int AdjustAttribute(Attributes stat)
		{
			return Stats[stat] - BASE_ATTRIBUTE;
		}

		public override bool Get(Enum stat, out int value)
		{
			bool found = true;
			if (stat.Equals(CombatStats.HP))
			{
				value = 25 * Stats[Attributes.VIT];
			}
			else if (stat.Equals(CombatStats.MP))
			{
				value = 5 * Stats[Attributes.INT] + 5 * Stats[Attributes.WIS];
			}
			else if (stat.Equals(CombatStats.ATK))
			{
				value = 2 * AdjustAttribute(Attributes.STR);
			}
			else if (stat.Equals(CombatStats.DEF))
			{
				value = AdjustAttribute(Attributes.VIT) + AdjustAttribute(Attributes.STR);
			}
			else if (stat.Equals(CombatStats.MAG))
			{
				value = 2 * AdjustAttribute(Attributes.INT);
			}
			else if (stat.Equals(CombatStats.RES))
			{
				value = AdjustAttribute(Attributes.INT) + AdjustAttribute(Attributes.WIS);
			}
			else if (stat.Equals(CombatStats.HIT))
			{
				value = BASE_HIT + AdjustAttribute(Attributes.AGI);
			}
			else if (stat.Equals(CombatStats.AVD))
			{
				value = BASE_EVADE + AdjustAttribute(Attributes.AGI);
			}
			else
			{
				value = 0;
				found = false;
			}
			return found;
		}

		new public static ILinkedStatResolver Default { get; } = new LinkedStatsResolver();
	}
}
