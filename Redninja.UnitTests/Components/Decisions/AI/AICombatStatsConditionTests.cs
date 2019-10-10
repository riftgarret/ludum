using NSubstitute;
using NUnit.Framework;

namespace Redninja.Components.Decisions.AI.UnitTests
{
	[TestFixture]
	public class AICombatStatsConditionTests
	{

		private IUnitModel mEntity;
		private AICombatStatCondition subject;

		[SetUp]
		public void Setup()
		{
			mEntity = Substitute.For<IUnitModel>();
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
		public void IsValid_TestOp_CombatStatCurrent(			
			AIValueConditionOperator op, 						
			int deltaFromValue,
			bool expected)
		{
			int value = 50;
			int volatileValue = value + deltaFromValue;

			Stat stat = Stat.HP;
			AIConditionType type = AIConditionType.CombatStatCurrent;

			mEntity.VolatileStats[stat].Returns(volatileValue);

			subject = new AICombatStatCondition(value, stat, op, type);
			var result = subject.IsValid(mEntity);

			Assert.That(result, Is.EqualTo(expected));
		}


		[TestCase(AIConditionType.CombatStatCurrent, 50, 50, 50, true)]
		[TestCase(AIConditionType.CombatStatCurrent, 50, 50, 51, false)]
		[TestCase(AIConditionType.CombatStatPercent, 50, 100, 50, true)]
		[TestCase(AIConditionType.CombatStatPercent, 50, 20, 10, true)]
		[TestCase(AIConditionType.CombatStatPercent, 50, 2, 1, true)]
		[TestCase(AIConditionType.CombatStatPercent, 80, 10, 8, true)]
		[TestCase(AIConditionType.CombatStatPercent, 80, 10, 7, false)]
		public void IsValid_TestType_EqualStat(
			AIConditionType type, 
			int value, 
			int statValue, 
			int volatileValue, 
			bool expected)
		{			

			Stat stat = Stat.HP;
			AIValueConditionOperator op = AIValueConditionOperator.EQ;

			mEntity.Stats[stat].Returns(statValue);
			mEntity.VolatileStats[stat].Returns(volatileValue);

			subject = new AICombatStatCondition(value, stat, op, type);
			var result = subject.IsValid(mEntity);

			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
