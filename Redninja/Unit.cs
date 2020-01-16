using Davfalcon;
using Davfalcon.Stats;

namespace Redninja
{
	public class Unit : UnitTemplate<IUnit>, IUnit
	{
		protected override IUnit Self => this;

		public CoreStat CoreStats { get; } = new CoreStat();

		public class CoreStat : StatsMap, IStatSource
		{
			public string Name => "Character Stats";

			public IStats Stats => this;
		}

		public Unit()
		{
			statsProvider.AddSource(CoreStats);
		}
	}

	public enum UnitComponents
	{
		Equipment, Skills, Buffs
	}

	public enum VolatileUnitComponents
	{
		VolatileStats, Buffs, Actions, Triggers
	}
}
