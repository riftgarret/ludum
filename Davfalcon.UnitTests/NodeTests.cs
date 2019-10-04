using System.Collections.Generic;
using Davfalcon.Nodes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static Davfalcon.UnitTests.TestConstants;

namespace Davfalcon.UnitTests
{
	[TestClass]
	public class NodeTests
	{
		private IUnit unit;

		[TestInitialize]
		public void GenerateUnit()
		{
			BasicUnit unit = BasicUnit.Create();
			unit.Name = "Test unit";
			unit.BaseStats[STAT_NAME] = 10;
			this.unit = unit;
		}

		[TestMethod]
		public void Aggregation()
		{
			INode node = new AggregatorNode("Test", new List<INode>() {
				new ConstantNode("Test 1", 1),
				new ConstantNode("Test 2", 2),
				new ConstantNode("Test 3", 3) });

			Assert.AreEqual(6, node.Value);
		}

		private class MultiplyAggregator : IAggregator
		{
			public int AggregateSeed => 1;

			public int Aggregate(int a, int b)
				=> a * b;
		}

		[TestMethod]
		public void AggregationWithAggregator()
		{
			INode node = new AggregatorNode("Test", new List<INode>() {
				new ConstantNode("Test 1", 2),
				new ConstantNode("Test 2", 2),
				new ConstantNode("Test 3", 2) },
				new MultiplyAggregator());

			Assert.AreEqual(8, node.Value);
		}

		[TestMethod]
		public void BaseStatNode()
		{
			INode node = unit.StatsDetails.GetStatNode(STAT_NAME);
			INode baseNode = unit.StatsDetails.GetBaseStatNode(STAT_NAME);

			Assert.AreEqual(10, node.Value);
			Assert.AreEqual(10, baseNode.Value);
		}

		[TestMethod]
		public void StatNodeWithModifier()
		{
			UnitStatsModifier modifier = new UnitStatsModifier();

			modifier.Name = "Test modifier";
			modifier.Additions[STAT_NAME] = 10;
			modifier.Multipliers[STAT_NAME] = 20;

			unit.Modifiers.Add(modifier);

			INode node = unit.StatsDetails.GetStatNode(STAT_NAME);

			Assert.AreEqual(24, unit.Stats[STAT_NAME]);

			Assert.AreEqual(24, node.Value);
		}
	}
}
