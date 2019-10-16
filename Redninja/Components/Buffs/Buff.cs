using System;
using System.Collections.Generic;
using Davfalcon.Buffs;

namespace Redninja.Components.Buffs
{
	[Serializable]
	public class Buff : Buff<IUnit>, IBuff, IUnit
	{
		public IUnit Owner { get; set; }

		public float TimeDuration { get; set; }
		public float TimeRemaining { get; set; }
		public float EffectInterval { get; set; }

		public event Action<IBuff> Effect;

		protected override IUnit SelfAsUnit => this;

		protected override int Resolve(int baseValue, IReadOnlyDictionary<Enum, int> modifications)
			=> StatFunctions.Resolve(baseValue, modifications);

		protected override Func<int, int, int> GetAggregator(Enum modificationType)
			=> StatFunctions.GetAggregator(modificationType);

		protected override int GetAggregatorSeed(Enum modificationType)
			=> StatFunctions.GetAggregatorSeed(modificationType);
	}
}
