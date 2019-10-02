using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Davfalcon.UnitTests.TestConstants;

namespace Davfalcon.UnitTests
{
	[TestClass]
	public class UnitModifierTests
	{
		private IUnit unit;

		[TestInitialize]
		public void GenerateUnit()
		{
			BasicUnit unit = BasicUnit.Create();
			unit.BaseStats[STAT_NAME] = 10;
			this.unit = unit;
		}

		[TestMethod]
		public void UnitStatsModifier()
		{
			UnitStatsModifier modifier = new UnitStatsModifier();

			modifier.Additions[STAT_NAME] = 10;
			modifier.Multipliers[STAT_NAME] = 20;

			unit.Modifiers.Add(modifier);

			Assert.AreEqual(24, unit.Stats[STAT_NAME]);

			modifier = new UnitStatsModifier();

			modifier.Additions[STAT_NAME] = 5;
			modifier.Multipliers[STAT_NAME] = 80;

			unit.Modifiers.Add(modifier);

			Assert.AreEqual(50, unit.Stats[STAT_NAME]);

			Assert.AreEqual(unit.Stats[STAT_NAME], unit.StatsDetails.Final[STAT_NAME]);
			Assert.AreEqual(10, unit.StatsDetails.Base[STAT_NAME]);
		}

		[TestMethod]
		public void RemoveModifier()
		{
			UnitStatsModifier modifier1 = new UnitStatsModifier();
			UnitStatsModifier modifier2 = new UnitStatsModifier();
			UnitStatsModifier modifier3 = new UnitStatsModifier();

			modifier1.Additions[STAT_NAME] = 1;
			modifier2.Additions[STAT_NAME] = 2;
			modifier3.Additions[STAT_NAME] = 3;

			unit.Modifiers.Add(modifier1);
			unit.Modifiers.Add(modifier2);
			unit.Modifiers.Add(modifier3);

			Assert.AreEqual(16, unit.Stats[STAT_NAME]);

			unit.Modifiers.Remove(modifier2);

			Assert.AreEqual(14, unit.Stats[STAT_NAME]);
		}

		[TestMethod]
		public void TimedModifier()
		{
			TimedModifier modifier = new TimedModifier
			{
				Duration = 1
			};
			modifier.Reset();
			Assert.AreEqual(modifier.Duration, modifier.Remaining);
			modifier.Tick();
			Assert.AreEqual(0, modifier.Remaining);
			modifier.Tick();
			Assert.AreEqual(0, modifier.Remaining);
		}
	}
}
