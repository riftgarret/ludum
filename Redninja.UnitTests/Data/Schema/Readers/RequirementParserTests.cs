using System;
using NUnit.Framework;
using Redninja.Components.Conditions;
using Redninja.Components.Conditions.Operators;

namespace Redninja.Data.Schema.Readers.UnitTests
{
	[TestFixture]
	public class RequirementParserTests
	{
		private RequirementParser subject;

		[SetUp]
		public void SetUp() {
			subject = new RequirementParser();
		}

		[TestCase("any", typeof(AnyOpRequirement), ConditionalOperatorRequirement.Any)]
		[TestCase("ALL", typeof(AllOpRequirement), ConditionalOperatorRequirement.All)]
		[TestCase("> 3", typeof(OpCountRequirement), ConditionalOperatorRequirement.CountOp)]
		public void TryParseRequirement_Type(string raw, Type expectedType, ConditionalOperatorRequirement requirementType)
		{
			bool success = subject.TryParseRequirement(raw, out IOperatorCountRequirement result);

			Assert.That(success, Is.True);
			Assert.That(result, Is.InstanceOf(expectedType));
		}

		[TestCase("> 1", ConditionOperatorType.GT, 1)]
		[TestCase("> 2", ConditionOperatorType.GT, 2)]
		[TestCase(">= 1", ConditionOperatorType.GTE, 1)]
		[TestCase(">= 2", ConditionOperatorType.GTE, 2)]
		[TestCase("= 1", ConditionOperatorType.EQ, 1)]
		[TestCase("= 2", ConditionOperatorType.EQ, 2)]
		[TestCase("!= 1", ConditionOperatorType.NEQ, 1)]
		[TestCase("!= 2", ConditionOperatorType.NEQ, 2)]
		[TestCase("< 1", ConditionOperatorType.LT, 1)]
		[TestCase("< 2", ConditionOperatorType.LT, 2)]
		[TestCase("<= 1", ConditionOperatorType.LTE, 1)]
		[TestCase("<= 2", ConditionOperatorType.LTE, 2)]
		public void TryParseRequirement_OpRequirement(string input, ConditionOperatorType expectedCond, int expectedCount)
		{		
			bool success = subject.TryParseRequirement(input, out IOperatorCountRequirement iResult);

			Assert.That(success, Is.True);

			OpCountRequirement result = (OpCountRequirement)iResult;
			Assert.That(result.Count, Is.EqualTo(expectedCount));
			Assert.That(result.OpType, Is.EqualTo(expectedCond));
		}
	}
}
