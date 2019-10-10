using System;
using NUnit.Framework;
using NSubstitute;
using Redninja.Components.Conditions;
using Redninja.Components.Conditions.Expressions;

namespace Redninja.Data.Schema.Readers.UnitTests
{
	[TestFixture]
	public class ExpressionParserTests
	{
		private ExpressionParser subject;

		[SetUp]
		public void SetUp()
		{
			subject = new ExpressionParser();
		}

		[TestCase("SELF", ConditionTargetType.Self)]
		[TestCase("TARGET", ConditionTargetType.Target)]
		[TestCase("ENEMY", ConditionTargetType.Enemy)]
		[TestCase("ALLY", ConditionTargetType.Ally)]
		[TestCase("ANY", ConditionTargetType.Any)]
		[TestCase("AllyNotSelf", ConditionTargetType.AllyNotSelf)]
		public void TryParseExpression_Unit(string input, ConditionTargetType expectedTargetType)
		{
			bool success = subject.TryParseExpression(input, null, out var result);

			Assert.That(success, Is.True);
			Assert.That(result, Is.InstanceOf(typeof(TargetUnitExpression)));

			TargetUnitExpression resultUnit = (TargetUnitExpression)result;

			Assert.That(resultUnit.TargetType, Is.EqualTo(expectedTargetType));
		}

		[Test]
		public void TryParseExpression_CombatStat([Values] Stat stat, [Values] bool hasPercent)
		{
			string input = stat.ToString() + (hasPercent ? "%" : "");

			var prevChain = Substitute.For<IExpression>();
			prevChain.ResultType.Returns(ExpressionResultType.Unit);

			bool success = subject.TryParseExpression(input, prevChain, out var result);

			Assert.That(success, Is.True);
			Assert.That(result, Is.InstanceOf(typeof(CombatStatExpression)));

			CombatStatExpression csResult = (CombatStatExpression)result;

			Assert.That(csResult.CombatStat, Is.EqualTo(stat));
			Assert.That(csResult.IsPercent, Is.EqualTo(hasPercent));
		}

		[Test]
		public void TryParseException_Group([Values] GroupOp groupOp, 
		                                    [Values(ExpressionResultType.IntValue, ExpressionResultType.Percent)] ExpressionResultType prevResultType)
		{
			string input = groupOp.ToString();

			var prevChain = Substitute.For<IExpression>();
			prevChain.ResultType.Returns(prevResultType);

			bool success = subject.TryParseExpression(input, prevChain, out var result);

			Assert.That(success, Is.True);
			Assert.That(result, Is.InstanceOf(typeof(GroupExpression)));

			GroupExpression gResult = (GroupExpression)result;

			Assert.That(gResult.GroupOp, Is.EqualTo(groupOp));
		}

		[Test]
		public void TryParseException_NumberValue([Values(0, 1, 100)] int value,
		                                          [Values] bool hasPercent)
		{
			string input = value.ToString() + (hasPercent? "%" : "");

			bool success = subject.TryParseExpression(input, null, out var result);

			Assert.That(success, Is.True);
			Assert.That(result, Is.InstanceOf(typeof(NumberExpression)));

			NumberExpression gResult = (NumberExpression)result;

			Assert.That(gResult.Result, Is.EqualTo(value));
			Assert.That(gResult.IsPercent, Is.EqualTo(hasPercent));
		}
	}
}
