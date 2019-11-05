using System;

namespace Davfalcon.Buffs
{
	[Serializable]
	public abstract class Buff<TUnit> : UnitStatsModifier<TUnit>, IBuff<TUnit> where TUnit : class, IUnitTemplate<TUnit>
	{
		public bool IsDebuff { get; set; }

		public float Duration { get; set; }

		public float Remaining { get; set; }
	}
}
