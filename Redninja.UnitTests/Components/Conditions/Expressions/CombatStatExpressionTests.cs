using NSubstitute;
using NUnit.Framework;

namespace Redninja.Components.Conditions.Expressions.UnitTests
{
	[TestFixture]
	public class CombatStatExpressionTests
	{
		private CombatStatExpression subject;

		[SetUp]
		public void SetUp()
		{

		}

		[Test]
		public void Result_SingleValue([Values(10, 20, 30)]int statValue, [Values(CombatStats.HP, CombatStats.Resource)] CombatStats stat)
		{
			IUnitModel model = Substitute.For<IUnitModel>();
			model.VolatileStats[stat].Returns(statValue);

			subject = new CombatStatExpression(stat, false);

			Assert.That(subject.Result(model), Is.EqualTo(statValue));
		}

		[TestCase(10, 100, 10)]
		[TestCase(10, 100, 10)]
		[TestCase(50, 100, 50)]
		[TestCase(500, 1000, 50)]
		[TestCase(1, 2, 50)]
		public void Result_Percent(int volatileValue, int statValue, int expectedValue)
		{
			CombatStats stat = CombatStats.HP;
			IUnitModel model = Substitute.For<IUnitModel>();
			model.VolatileStats[stat].Returns(volatileValue);
			model.Stats[stat].Returns(statValue);

			subject = new CombatStatExpression(stat, true);

			Assert.That(subject.Result(model), Is.EqualTo(expectedValue));
		}
	}
}
