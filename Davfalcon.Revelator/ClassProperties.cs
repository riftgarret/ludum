using System;
using Davfalcon.Stats;

namespace Davfalcon.Revelator
{
	[Serializable]
	public class ClassProperties : IDescribable, IEditableDescription
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public IEditableStats BaseStats { get; private set; }
		public IEditableStats StatGrowths { get; private set; }

		public ClassProperties()
		{
			BaseStats = new StatsMap();
			StatGrowths = new StatsMap();
		}
	}
}
