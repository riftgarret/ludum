using System;
using NUnit.Framework;

namespace Redninja.Components.Conditions.Operators.UnitTests
{
	public class OpCountRequirementTests
	{
		private OpCountRequirement subject;

		[TestCase(1, 2, false)]
		[TestCase(1, 2, false)]
		[TestCase(0, 1, true)]
		[TestCase(1, 1, false)]
		public void TryComplete_LT1(int numberTrue, int total, bool expectedResult)
		{
			subject = new OpCountRequirement(ConditionOperatorType.LT, 1);
			bool result = subject.MeetsRequirement(numberTrue, total);

			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[TestCase(1, 2, true)]
		[TestCase(1, 2, true)]
		[TestCase(3, 2, false)]
		[TestCase(1, 1, true)]
		public void TryComplete_LTE1(int numberTrue, int total, bool expectedResult)
		{
			subject = new OpCountRequirement(ConditionOperatorType.LTE, 1);
			bool result = subject.MeetsRequirement(numberTrue, total);

			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[TestCase(1, 1, true)]
		[TestCase(2, 2, false)]
		[TestCase(0, 1, false)]
		[TestCase(1, 3, true)]
		public void TryComplete_EQ1(int numberTrue, int total, bool expectedResult)
		{
			subject = new OpCountRequirement(ConditionOperatorType.EQ, 1);
			bool result = subject.MeetsRequirement(numberTrue, total);

			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[TestCase(1, 1, false)]
		[TestCase(2, 2, true)]
		[TestCase(0, 1, true)]
		[TestCase(1, 3, false)]
		public void TryComplete_NEQ1(int numberTrue, int total, bool expectedResult)
		{
			subject = new OpCountRequirement(ConditionOperatorType.NEQ, 1);
			bool result = subject.MeetsRequirement(numberTrue, total);

			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[TestCase(1, 1, false)]
		[TestCase(2, 2, true)]
		[TestCase(0, 1, false)]
		[TestCase(3, 3, true)]
		public void TryComplete_GT1(int numberTrue, int total, bool expectedResult)
		{
			subject = new OpCountRequirement(ConditionOperatorType.GT, 1);
			bool result = subject.MeetsRequirement(numberTrue, total);

			Assert.That(result, Is.EqualTo(expectedResult));
		}

		[TestCase(1, 1, true)]
		[TestCase(2, 2, true)]
		[TestCase(0, 1, false)]
		[TestCase(3, 3, true)]
		public void TryComplete_GTE1(int numberTrue, int total, bool expectedResult)
		{
			subject = new OpCountRequirement(ConditionOperatorType.GTE, 1);
			bool result = subject.MeetsRequirement(numberTrue, total);

			Assert.That(result, Is.EqualTo(expectedResult));
		}
	}
}
