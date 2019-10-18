using System;

namespace Davfalcon.Buffs
{
	[Serializable]
	public abstract class Buff<TUnit> : UnitStatsModifier<TUnit>, IBuff<TUnit> where TUnit : class, IUnitTemplate<TUnit>
	{
		public bool IsDebuff { get; set; }

		public int Duration { get; set; }

		public int Remaining { get; set; }
	}
}
