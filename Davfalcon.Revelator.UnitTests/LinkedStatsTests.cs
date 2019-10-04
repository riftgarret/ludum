using Microsoft.VisualStudio.TestTools.UnitTesting;
using Davfalcon.Revelator.Borger;

namespace Davfalcon.Revelator.UnitTests
{
	[TestClass]
	public class LinkedStatsTests
	{
		private IUnit unit;

		[TestInitialize]
		public void MakeUnit()
			=> unit = Unit.Build(StatsOperations.Default, LinkedStatsResolver.Default, b => b
				.SetBaseStat(Attributes.STR, 15)
				.SetBaseStat(Attributes.VIT, 12)
				.SetBaseStat(Attributes.AGI, 8)
				.SetBaseStat(Attributes.INT, 14)
				.SetBaseStat(Attributes.WIS, 10));

		[TestMethod]
		public void HP()
		{
			Assert.AreEqual(300, unit.Stats[CombatStats.HP]);
		}

		[TestMethod]
		public void MP()
		{
			Assert.AreEqual(120, unit.Stats[CombatStats.MP]);
		}

		[TestMethod]
		public void ATK()
		{
			Assert.AreEqual(20, unit.Stats[CombatStats.ATK]);
		}

		[TestMethod]
		public void DEF()
		{
			Assert.AreEqual(17, unit.Stats[CombatStats.DEF]);
		}

		[TestMethod]
		public void MAG()
		{
			Assert.AreEqual(18, unit.Stats[CombatStats.MAG]);
		}

		[TestMethod]
		public void RES()
		{
			Assert.AreEqual(14, unit.Stats[CombatStats.RES]);
		}

		[TestMethod]
		public void HIT()
		{
			Assert.AreEqual(103, unit.Stats[CombatStats.HIT]);
		}

		[TestMethod]
		public void AVD()
		{
			Assert.AreEqual(3, unit.Stats[CombatStats.AVD]);
		}
	}
}
