using NSubstitute;
using NUnit.Framework;
using Redninja.Components.Decisions.AI;

namespace Redninja.Components.Conditions.Expressions.UnitTests
{
	[TestFixture]
	public class CombatStatExpressionTests
	{
		private StatExpression subject;

		[SetUp]
		public void SetUp()
		{

		}

		[Test]
		public void Result_SingleValue([Values(10, 20, 30)]int statValue, [Values(LiveStat.LiveHP, LiveStat.LiveResource)] LiveStat stat)
		{
			IBattleEntity model = Substitute.For<IBattleEntity>();
			model.LiveStats[stat].Returns(new LiveStatContainer(statValue));			

			subject = new StatExpression(new LiveStatEvaluator(stat, false));

			Assert.That(subject.Result(model), Is.EqualTo(statValue));
		}

		[TestCase(10, 100, 10)]
		[TestCase(10, 100, 10)]
		[TestCase(50, 100, 50)]
		[TestCase(500, 1000, 50)]
		[TestCase(1, 2, 50)]
		public void Result_Percent(int volatileValue, int statValue, int expectedValue)
		{
			LiveStat stat = LiveStat.LiveHP;
			IBattleEntity model = Substitute.For<IBattleEntity>();
			model.LiveStats[stat].Returns(new LiveStatContainer(statValue)
			{
				Current = volatileValue,				
			});			

			subject = new StatExpression(new LiveStatEvaluator(stat, true));

			Assert.That(subject.Result(model), Is.EqualTo(expectedValue));
		}
	}
}
