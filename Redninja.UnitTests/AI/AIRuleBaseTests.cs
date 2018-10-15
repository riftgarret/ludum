using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using Redninja.Components.Actions.Decisions;
using Redninja.Components.Actions.Decisions.AI;
using Redninja.Components.Targeting;

namespace Redninja.UnitTests.AI
{
	[TestFixture]
	public abstract class AIRuleBaseTests<B, T> 
		where B : AIRuleBase.BuilderBase<B, T>
		where T : AIRuleBase
	{
		protected IDecisionHelper mDecisionHelper;
		protected IBattleModel mBem;
		protected IBattleEntity mSource;
		protected int sourceTeam;
		protected int enemyTeam;
		protected List<IBattleEntity> allEntities;			

		protected abstract AIRuleBase.BuilderBase<B, T> SubjectBuilder { get; }		

		[SetUp]
		public void BaseSetup()
		{
			enemyTeam = 1;
			sourceTeam = 2;

			mBem = Substitute.For<IBattleModel>();
			mSource = Substitute.For<IBattleEntity>();
			mSource.Team.Returns(sourceTeam);

			mDecisionHelper = Substitute.For<IDecisionHelper>();
			mDecisionHelper.EntityModel.Returns(mBem);

			allEntities = new List<IBattleEntity>() { mSource };
			mBem.AllEntities.Returns(allEntities);			
		}

		// due to base class being called [SetUp] first before derived class, the
		// derived class must call SetupBuilder after its initialized its Builder property.
		protected void SetupBuilder()
		{			
			SubjectBuilder.SetWeight(1);
			SubjectBuilder.SetName("test");
		}

		protected IBattleEntity AddEntity(int teamId)
		{
			var mEntity = Substitute.For<IBattleEntity>();
			mEntity.Team.Returns(teamId);
			allEntities.Add(mEntity);
			return mEntity;
		}

		protected IAITargetCondition AddMockCondition(TargetTeam target, bool isValid, IBattleEntity entityArg = null)
		{			
			IAITargetCondition mockCondition = Substitute.For<IAITargetCondition>();
			mockCondition.IsValid(entityArg?? Arg.Any<IBattleEntity>()).Returns(isValid);
			SubjectBuilder.AddTriggerCondition(target, mockCondition);
			return mockCondition;
		}
		
		[Test]
		public void IsValidCondition_ReturnsTrue(
			[Values] TargetTeam target, 
			[Range(1, 4)] int numberOfTrueConditions)
		{			
			for (int i=0; i < numberOfTrueConditions; i++)
			{
				AddMockCondition(target, true);				
			}

			AddEntity(sourceTeam);
			AddEntity(enemyTeam);

			var subject = SubjectBuilder.Build();			
			bool result = subject.IsValidTriggerConditions(mSource, mDecisionHelper);

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsValidCondition_ReturnsFalse(
			[Values] TargetTeam target,
			[Range(0, 2)] int numberOfTrueConditions)
		{			
			for (int i = 0; i < numberOfTrueConditions; i++)
			{
				AddMockCondition(target, true);
			}

			AddMockCondition(target, false);	// add one to fail the test of AND statements

			AddEntity(sourceTeam);
			AddEntity(enemyTeam);

			var subject = SubjectBuilder.Build();
			bool result = subject.IsValidTriggerConditions(mSource, mDecisionHelper);

			Assert.That(result, Is.False);
		}

		[Test]
		public void IsValidCondition_ChecksTarget([Values] TargetTeam target)
		{
			IEnumerable<IBattleEntity> entities;
			switch(target)
			{
				case TargetTeam.Ally:
					entities = allEntities.Where(x => x.Team == mSource.Team);
					break;
				case TargetTeam.Self:
					entities = allEntities.Where(x => x == mSource);
					break;
				case TargetTeam.Enemy:
					entities = allEntities.Where(x => x.Team != mSource.Team);
					break;
				case TargetTeam.Any:
				default:
					entities = allEntities;
					break;
			}

			AddEntity(sourceTeam);
			AddEntity(enemyTeam);

			var mCondition = AddMockCondition(target, true);

			var subject = SubjectBuilder.Build();
			subject.IsValidTriggerConditions(mSource, mDecisionHelper);

			mCondition.Received().IsValid(Arg.Is<IBattleEntity>(x => entities.Contains(x)));
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
