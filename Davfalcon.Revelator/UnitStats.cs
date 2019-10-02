using System;
using Davfalcon.Stats;

namespace Davfalcon.Revelator
{
	[Serializable]
	public class UnitStats : StatsMap
	{
		private readonly ILinkedStatResolver linker;

		public override int Get(Enum stat)
		{
			if (linker.Get(stat, out int value))
				return value;
			return base.Get(stat);
		}

		public UnitStats(ILinkedStatResolver linker)
		{
			this.linker = linker;
		}
	}
}
