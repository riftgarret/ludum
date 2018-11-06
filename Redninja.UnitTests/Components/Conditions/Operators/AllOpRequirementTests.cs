using System;
using NUnit.Framework;

namespace Redninja.Components.Conditions.Operators.UnitTests
{
	[TestFixture]
	public class AllOpRequirementTests
	{
		private AllOpRequirement subject;

		[SetUp]
		public void SetUp()
		{
			subject = AllOpRequirement.Instance;
		}


		[TestCase(5, 10, false)]
		[TestCase(0, 10, false)]
		[TestCase(1, 10, false)]
		[TestCase(10, 10, true)]
		[TestCase(1, 1, true)]
		[TestCase(0, 0, false)]
		public void MeetsRequirement(int numTrue, int total, bool expected)
		{
			Assert.That(subject.MeetsRequirement(numTrue, total), Is.EqualTo(expected));
		}
	}
}
