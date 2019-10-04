using Davfalcon.Stats;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Davfalcon.UnitTests.TestConstants;

namespace Davfalcon.UnitTests
{
	[TestClass]
	public class StatsTests
	{
		[TestMethod]
		public void StatsMap()
		{
			IEditableStats stats = new StatsMap();
			stats.Set(STAT_NAME, STAT_VALUE);

			Assert.AreEqual(STAT_VALUE, stats.Get(STAT_NAME));
			Assert.AreEqual(STAT_VALUE, stats[STAT_NAME]);
		}

		[TestMethod]
		public void StatsMath()
		{
			IEditableStats a = new StatsMap();
			a.Set(STAT_NAME, STAT_VALUE);
			IEditableStats b = new StatsMap();
			b.Set(STAT_NAME, STAT_VALUE);
			IEditableStats m = new StatsMap();
			m.Set(STAT_NAME, STAT_MULT);
			IStats stats = new StatsCalculator(a, b, m);

			int expected = (STAT_VALUE + STAT_VALUE) * (int)(1 + STAT_MULT / 100f);

			Assert.AreEqual(expected, stats[STAT_NAME]);
			Assert.AreEqual(expected, StatsOperations.Default.Calculate(STAT_VALUE, STAT_VALUE, STAT_MULT));
		}
	}
}
