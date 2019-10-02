using System;

namespace Davfalcon.Revelator.Combat
{
	[Serializable]
	public class StatChange : ILogEntry
	{
		public string Unit { get; }
		public Enum Stat { get; }
		public int Value { get; }

		public StatChange(string unit, Enum stat, int value)
		{
			Unit = unit;
			Stat = stat;
			Value = value;
		}

		public StatChange(IUnit unit, Enum stat, int value)
			: this(unit.Name, stat, value)
		{ }


		public override string ToString()
			=> $"{Unit} {(Value > 0 ? "gains" : "loses")} {Value} {Stat}.";
	}
}
