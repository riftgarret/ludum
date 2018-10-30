using System;
using NUnit.Framework;
using Redninja.Components.Conditions;
using Redninja.Components.Conditions.Operators;

namespace Redninja.Data.Schema.Readers.UnitTests
{
	[TestFixture]
	public class ConditionalOpParserTests
	{
		private ConditionOpParser subject;

		[SetUp]
		public void SetUp() 
		{
			subject = new ConditionOpParser();
		}

		[TestCase(">", ConditionOperatorType.GT)]
		[TestCase(">=", ConditionOperatorType.GTE)]
		[TestCase("=", ConditionOperatorType.EQ)]
		[TestCase("!=", ConditionOperatorType.NEQ)]
		[TestCase("<", ConditionOperatorType.LT)]
		[TestCase("<=", ConditionOperatorType.LTE)]
		public void TryParseOp(string input, ConditionOperatorType op)
		{
			bool success = subject.TryParseOp(input, out IConditionalOperator iResult);

			Assert.That(success, Is.True);
			Assert.That(iResult, Is.InstanceOf(typeof(ConditionalOperator)));

			Assert.That(iResult.OperatorType, Is.EqualTo(op));
		}
	}
}
