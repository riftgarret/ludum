using NSubstitute;
using NUnit.Framework;

namespace Redninja.Components.Decisions.AI.UnitTests
{
	[TestFixture]
	public class AICombatStatsConditionTests
	{

		private IBattleEntity mEntity;
		private AICombatStatCondition subject;

		[SetUp]
		public void Setup()
		{
			mEntity = Substitute.For<IBattleEntity>();
		}

		[TestCase(AIValueConditionOperator.EQ, 1, false)]
		[TestCase(AIValueConditionOperator.EQ, 0, true)]
		[TestCase(AIValueConditionOperator.EQ, -1, false)]
		[TestCase(AIValueConditionOperator.LT, 1, false)]
		[TestCase(AIValueConditionOperator.LT, 0, false)]
		[TestCase(AIValueConditionOperator.LT, -1, true)]
		[TestCase(AIValueConditionOperator.LTE, 1, false)]
		[TestCase(AIValueConditionOperator.LTE, 0, true)]
		[TestCase(AIValueConditionOperator.LTE, -1, true)]
		[TestCase(AIValueConditionOperator.GT, 1, true)]
		[TestCase(AIValueConditionOperator.GT, 0, false)]
		[TestCase(AIValueConditionOperator.GT, -1, false)]
		[TestCase(AIValueConditionOperator.GTE, 1, true)]
		[TestCase(AIValueConditionOperator.GTE, 0, true)]
		[TestCase(AIValueConditionOperator.GTE, -1, false)]		
		public void IsValid_TestOp_LiveStat(			
			AIValueConditionOperator op, 						
			int deltaFromValue,
			bool expected)
		{
			LiveStat stat = LiveStat.LiveHP;
			int value = 50;
			int volatileValue = value + deltaFromValue;

			mEntity.LiveStats[stat].Returns(new LiveStatContainer()
			{
				Current = value
			});

			subject = new AICombatStatCondition(value, new LiveStatEvaluator(stat, false), op);
			var result = subject.IsValid(mEntity);

			Assert.That(result, Is.EqualTo(expected));
		}


		[TestCase(false, 50, 50, 50, true)]
		[TestCase(false, 50, 50, 51, false)]
		[TestCase(true, 50, 100, 50, true)]
		[TestCase(true, 50, 20, 10, true)]
		[TestCase(true, 50, 2, 1, true)]
		[TestCase(true, 80, 10, 8, true)]
		[TestCase(true, 80, 10, 7, false)]
		public void IsValid_TestType_EqualStat(
			bool isPercent,
			int value, 
			int statValue, 
			int volatileValue, 
			bool expected)
		{			

			LiveStat stat = LiveStat.LiveHP;
			AIValueConditionOperator op = AIValueConditionOperator.EQ;

			mEntity.LiveStats[stat].Returns(new LiveStatContainer()
			{
				Current = value,
				Max = statValue
			});
			
			subject = new AICombatStatCondition(value, new LiveStatEvaluator(stat, isPercent), op);
			var result = subject.IsValid(mEntity);

			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
