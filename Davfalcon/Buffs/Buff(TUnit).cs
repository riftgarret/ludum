using System;
using Davfalcon.Stats;

namespace Davfalcon.Buffs
{
	[Serializable]
	public abstract class Buff<TUnit> : IBuff<TUnit> where TUnit : class, IUnitTemplate<TUnit>
	{
		public bool IsDebuff { get; set; }

		public float Duration { get; set; }

		public float Remaining { get; set; }

		public abstract string Name { get; set; }

		public abstract IStats Stats { get; }
	}
}
