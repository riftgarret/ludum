using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using Redninja.Components.Targeting;

namespace Redninja.Components.Decisions.AI.UnitTests
{
	[TestFixture]
	public abstract class AIRuleBaseTests<B, T> 
		where B : AIRuleBase.BuilderBase<B, T>
		where T : AIRuleBase
	{		

		protected abstract AIRuleBase.BuilderBase<B, T> SubjectBuilder { get; }		

		// due to base class being called [SetUp] first before derived class, the
		// derived class must call SetupBuilder after its initialized its Builder property.
		protected void SetupBuilder()
		{			
			SubjectBuilder.SetWeight(1);
			SubjectBuilder.SetName("test");
		}				

		[TestCase(0)]
		[TestCase(-1)]
		[TestCase(-20)]
		public void BuilderInvalidWeight_ThrowsException(int weight)
		{
			SubjectBuilder.SetWeight(weight);

			Assert.Throws<InvalidOperationException>(() => SubjectBuilder.Build());
		}

		[TestCase("first")]
		[TestCase("second")]
		[TestCase("the third")]
		public void BuilderName_IsBuilt(string name)
		{
			SubjectBuilder.SetName(name);

			var subject = SubjectBuilder.Build();
			Assert.That(subject.RuleName, Is.EqualTo(name));
		}

		[TestCase(0)]
		[TestCase(10)]
		[TestCase(30)]
		public void BuilderRefresh_IsBuilt(int refresh)
		{
			SubjectBuilder.SetRefreshTime(refresh);

			var subject = SubjectBuilder.Build();
			Assert.That(subject.RefreshTime, Is.EqualTo(refresh));
		}
	}
}
