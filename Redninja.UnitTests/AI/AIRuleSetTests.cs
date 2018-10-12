using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using Redninja.AI;

namespace Redninja.UnitTests.AI
{
	[TestFixture]
	public class AIRuleSetTests
	{
		private IBattleEntityManager mBem;		
		private IBattleEntity mSource;
		private IAIHistoryState mHistory;
		private AIRuleSet.Builder builder;

		[SetUp]
		public void Setup()
		{
			mBem = Substitute.For<IBattleEntityManager>();
			mSource = Substitute.For<IBattleEntity>();
			mHistory = Substitute.For<IAIHistoryState>();
			builder = new AIRuleSet.Builder();
		}
		
		private IAIRule CreateMockRule(int weight, bool isValidTrigger, bool willBuildAction)
		{
			IAIRule rule = Substitute.For<IAIRule>();

			rule.Weight.Returns(weight);
			rule.IsValidTriggerConditions(Arg.Any<IBattleEntity>(), Arg.Any<IBattleEntityManager>())
				.Returns(isValidTrigger);
			rule.GenerateAction(Arg.Any<IBattleEntity>(), Arg.Any<IBattleEntityManager>())
				.Returns(willBuildAction ? Substitute.For<IBattleAction>() : null);

			return rule;
		}

		[TestCase(true, true)]
		[TestCase(true, false, false, false, true, true)]
		[TestCase(false, true, false, false, true, false, true, true)]
		[TestCase(true, true, true, false, false, false, true, true)]
		public void ResolveAction_FindsAction(params bool[] validBuildPairs)
		{
			for (int i= 0; i < validBuildPairs.Length; i+=2) 
			{
				builder.AddRule(CreateMockRule(1, validBuildPairs[i], validBuildPairs[i+1]));
			}

			mHistory.IsRuleReady(Arg.Any<IAIRule>()).Returns(true);

			var subject = builder.Build();
			var result = subject.ResolveAction(mSource, mBem, mHistory);

			Assert.That(result, Is.Not.Null);
		}

		[TestCase(false, true)]
		[TestCase(true, false, false, false, true, false)]
		[TestCase(false, true, false, false, true, false, false, false)]
		[TestCase(true, false, true, false, false, false)]
		public void ResolveAction_FailsReturnsNoAction(params bool[] validBuildPairs)
		{			
			for (int i = 0; i < validBuildPairs.Length; i += 2)
			{
				builder.AddRule(CreateMockRule(1, validBuildPairs[i], validBuildPairs[i + 1]));
			}

			mHistory.IsRuleReady(Arg.Any<IAIRule>()).Returns(true);

			var subject = builder.Build();

			var result = subject.ResolveAction(mSource, mBem, mHistory);

			Assert.That(result, Is.Null);
		}

		[Test]
		public void ResolveAction_IgnoredInvalidRuleFromHistory()
		{
			IAIRule mRule = CreateMockRule(1, true, true);
			mHistory.IsRuleReady(mRule).Returns(false);
			builder.AddRule(mRule);

			var subject = builder.Build();

			var result = subject.ResolveAction(mSource, mBem, mHistory);

			mHistory.Received().IsRuleReady(mRule);
			Assert.That(result, Is.Null);
		}
	}
}
