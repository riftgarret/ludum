using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace Redninja.Components.Decisions.AI.UnitTests
{
	[TestFixture]
	public class AICombatStatsPriorityTests
	{
		private AICombatStatPriority subject;
		private List<IUnitModel> entities;

		[SetUp]
		public void Setup()
		{
			entities = new List<IUnitModel>();
		}

		private IUnitModel AddUnit(CombatStats stat, int volValue = 5, int statValue = 10)
		{
			var entity = Substitute.For<IUnitModel>();
			entities.Add(entity);
			entity.Stats[stat].Returns(statValue);
			entity.VolatileStats[stat].Returns(volValue);
			return entity;
		}

		[TestCase(AITargetingPriorityQualifier.Highest, 1, 5, 1, 1)]
		[TestCase(AITargetingPriorityQualifier.Highest, 1, 5, 10, 2)]
		[TestCase(AITargetingPriorityQualifier.Lowest, 1, 5, 10, 0)]
		[TestCase(AITargetingPriorityQualifier.Lowest, 1, 5, 0, 2)]
		[TestCase(AITargetingPriorityQualifier.Average, 1, 5, 10, 1)]
		[TestCase(AITargetingPriorityQualifier.Average, 10, 5, 8, 2)]
		public void GetBestTarget_TestQualifier_CombatStatCurrent(
			AITargetingPriorityQualifier qualifier,
			int vol1, int vol2, int vol3,
			int expectedIndex)
		{

			CombatStats stat = CombatStats.HP;
			AIPriorityType type = AIPriorityType.CombatStatCurrent;

			AddUnit(stat, vol1);
			AddUnit(stat, vol2);
			AddUnit(stat, vol3);

			subject = new AICombatStatPriority(stat, qualifier, type);
			var result = subject.GetBestTarget(entities);

			Assert.That(result, Is.EqualTo(entities[expectedIndex]));
		}

		[TestCase(AIPriorityType.CombatStatCurrent, 1, 1, 2, 2, 3, 3, 2)]
		[TestCase(AIPriorityType.CombatStatPercent, 10, 10, 2, 5, 3, 8, 0)]
		[TestCase(AIPriorityType.CombatStatPercent, 10, 10, 10, 5, 3, 8, 1)]
		public void GetBestTarget_TestType_HighestStat(
			AIPriorityType type,
			int vol1, int stat1,
			int vol2, int stat2,
			int vol3, int stat3,
			int expectedIndex)
		{
			CombatStats stat = CombatStats.HP;
			AITargetingPriorityQualifier qualifier = AITargetingPriorityQualifier.Highest;

			AddUnit(stat, vol1, stat1);
			AddUnit(stat, vol2, stat2);
			AddUnit(stat, vol3, stat3);

			subject = new AICombatStatPriority(stat, qualifier, type);
			var result = subject.GetBestTarget(entities);

			Assert.That(result, Is.EqualTo(entities[expectedIndex]));
		}
	}
}
