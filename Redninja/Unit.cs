using Davfalcon;

namespace Redninja
{
	public class Unit : UnitTemplate<IUnit>, IUnit
	{
		protected override IUnit Self => this;
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
