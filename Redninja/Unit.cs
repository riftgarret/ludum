using Davfalcon;

namespace Redninja
{
	public class Unit : UnitTemplate<IUnit>, IUnit
	{
		protected override IUnit Self => this;
	}

	public enum UnitComponents
	{
		Equipment, Buffs, Skills
	}

	public enum VolatileUnitComponents
	{
		VolatileStats, Actions, Triggers
	}
}
