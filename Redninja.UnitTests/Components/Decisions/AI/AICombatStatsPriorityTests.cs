using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;

namespace Redninja.Components.Decisions.AI.UnitTests
{
	[TestFixture]
	public class AICombatStatsPriorityTests
	{
		private AIStatPriority subject;
		private List<IBattleEntity> entities;

		[SetUp]
		public void Setup()
		{
			entities = new List<IBattleEntity>();
		}

		private IBattleEntity AddUnit(LiveStat stat, int volValue = 5, int statValue = 10)
		{
			var entity = Substitute.For<IBattleEntity>();
			entities.Add(entity);
			entity.LiveStats[stat].Returns(new LiveStatContainer(statValue)
			{
				Current = volValue,
			});
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

			LiveStat stat = LiveStat.LiveHP;			

			AddUnit(stat, vol1);
			AddUnit(stat, vol2);
			AddUnit(stat, vol3);

			subject = new AIStatPriority(new LiveStatEvaluator(stat, false), qualifier);
			var result = subject.GetBestTarget(entities);

			Assert.That(result, Is.EqualTo(entities[expectedIndex]));
		}

		[TestCase(false, 1, 1, 2, 2, 3, 3, 2)]
		[TestCase(true, 10, 10, 2, 5, 3, 8, 0)]
		[TestCase(true, 9, 10, 5, 5, 3, 8, 1)]
		public void GetBestTarget_TestType_HighestStat(
			bool isPercent,
			int vol1, int stat1,
			int vol2, int stat2,
			int vol3, int stat3,
			int expectedIndex)
		{
			LiveStat stat = LiveStat.LiveHP;
			AITargetingPriorityQualifier qualifier = AITargetingPriorityQualifier.Highest;

			AddUnit(stat, vol1, stat1);
			AddUnit(stat, vol2, stat2);
			AddUnit(stat, vol3, stat3);

			subject = new AIStatPriority(new LiveStatEvaluator(stat, isPercent), qualifier);
			var result = subject.GetBestTarget(entities);

			Assert.That(result, Is.EqualTo(entities[expectedIndex]));
		}
	}
}
