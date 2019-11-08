using System;
using System.Linq;
using NSubstitute;
using NUnit.Framework;

namespace Redninja.Components.Conditions.Operators.UnitTests
{
	[TestFixture]
	public class ConditionalOperatorTests
	{
		private ConditionalOperator subject;

		[Test]
		public void IsTrue_AllCrossScanned([Values(0, 1, 10)] int leftCount, [Values(0, 1, 10)] int rightCount)
		{
			int expectedResult = leftCount * rightCount;						

			IOperatorCountRequirement requirement = Substitute.For<IOperatorCountRequirement>();

			subject = new ConditionalOperator(ConditionOperatorType.EQ); // ignored type

			subject.IsTrue(Enumerable.Range(1, leftCount).Select(x => (object) x),
						   Enumerable.Range(1, rightCount).Select(x => (object) x),
						   requirement);
			
			requirement.Received().MeetsRequirement(Arg.Any<int>(), Arg.Is(expectedResult));
		}


		[TestCase(0, 1, true)]
		[TestCase(1, 1, false)]
		[TestCase(1, 0, false)]
		[TestCase(0, 10, true)]
		public void IsTrue_OneToOne_LT_IntValue(int left, int right, bool expected)
		{
			subject = new ConditionalOperator(ConditionOperatorType.LT);

			bool result = subject.IsTrue(
				Enumerable.Repeat((object) left, 1),
				Enumerable.Repeat((object) right, 1),
				AnyOpRequirement.Instance
			);

			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase(0, 1, false)]
		[TestCase(1, 1, true)]
		[TestCase(1, 0, false)]
		[TestCase(10, 10, true)]
		public void IsTrue_OneToOne_EQ_IntValue(int left, int right, bool expected)
		{
			subject = new ConditionalOperator(ConditionOperatorType.EQ);

			bool result = subject.IsTrue(
				Enumerable.Repeat((object)left, 1),
				Enumerable.Repeat((object)right, 1),
				AnyOpRequirement.Instance
			);

			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase(0, 1, true)]
		[TestCase(1, 1, false)]
		[TestCase(1, 0, true)]
		[TestCase(10, 10, false)]
		public void IsTrue_OneToOne_NEQ_IntValue(int left, int right, bool expected)
		{
			subject = new ConditionalOperator(ConditionOperatorType.NEQ);

			bool result = subject.IsTrue(
				Enumerable.Repeat((object)left, 1),
				Enumerable.Repeat((object)right, 1),
				AnyOpRequirement.Instance
			);

			Assert.That(result, Is.EqualTo(expected));
		}

		[TestCase(0, 1, 5, true)]
		[TestCase(1, 1, 3, false)]
		[TestCase(1, 0, 2, false)]
		[TestCase(0, 10, 3, true)]
		[TestCase(5, 10, 1, false)]
		public void IsTrue_ManyToOne_LT_IntValue_All(int left, int right, int right2, bool expected)
		{
			subject = new ConditionalOperator(ConditionOperatorType.LT);

			bool result = subject.IsTrue(
				Enumerable.Repeat((object)left, 1),
				new object[] { right, right2 },
				AllOpRequirement.Instance
			);

			Assert.That(result, Is.EqualTo(expected));
		}
	}
}
