using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Davfalcon.Revelator;
using NSubstitute;
using NUnit.Framework;
using Redninja.AI;
using Redninja.Targeting;

namespace Redninja.UnitTests.AI
{
	[TestFixture]
	public abstract class AIRuleBaseTests<T> where T : AIRuleBase.BuilderBase<T>
	{
		private IBattleEntityManager mBem;		
		private IBattleEntity mSource;
		private int sourceTeam;
		private int enemyTeam;
		private List<IBattleEntity> allEntities;
		
		private TestableRuleBase.Builder subjectBuilder;

		protected abstract AIRuleBase.BuilderBase<T> Builder { get; }

		[SetUp]
		public void Setup()
		{
			enemyTeam = 1;
			sourceTeam = 2;

			mBem = Substitute.For<IBattleEntityManager>();
			mSource = Substitute.For<IBattleEntity>();
			mSource.Team.Returns(sourceTeam);

			allEntities = new List<IBattleEntity>() { mSource };
			mBem.AllEntities.Returns(allEntities);

			subjectBuilder = new TestableRuleBase.Builder();
			subjectBuilder.SetWeight(1);
			subjectBuilder.SetName("test");
		}

		private IBattleEntity AddEntity(int teamId)
		{
			var mEntity = Substitute.For<IBattleEntity>();
			mEntity.Team.Returns(teamId);
			allEntities.Add(mEntity);
			return mEntity;
		}

		private IAITargetCondition AddMockCondition(TargetTeam target, bool isValid, IBattleEntity entityArg = null)
		{
			IAITargetCondition mockCondition = Substitute.For<IAITargetCondition>();
			mockCondition.IsValid(entityArg?? Arg.Any<IBattleEntity>()).Returns(isValid);
			subjectBuilder.AddTriggerCondition(target, mockCondition);
			return mockCondition;
		}
		
		[Test]
		public void IsValidCondition_ReturnsTrue(
			[Values] TargetTeam target, 
			[Range(1, 4)] int numberOfTrueConditions)
		{			
		
			for(int i=0; i < numberOfTrueConditions; i++)
			{
				AddMockCondition(target, true);				
			}

			AddEntity(sourceTeam);
			AddEntity(enemyTeam);

			var subject = subjectBuilder.Build();
			bool result = subject.IsValidTriggerConditions(mSource, mBem);

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

			var subject = subjectBuilder.Build();
			bool result = subject.IsValidTriggerConditions(mSource, mBem);

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

			var subject = subjectBuilder.Build();
			subject.IsValidTriggerConditions(mSource, mBem);

			mCondition.Received().IsValid(Arg.Is<IBattleEntity>(x => entities.Contains(x)));
		}

		[TestCase(0)]
		[TestCase(-1)]
		[TestCase(-20)]
		public void BuilderInvalidWeight_ThrowsException(int weight)
		{
			subjectBuilder.SetWeight(weight);

			Assert.Throws<InvalidOperationException>(() => subjectBuilder.Build());
		}

		[TestCase("first")]
		[TestCase("second")]
		[TestCase("the third")]
		public void BuilderName_IsBuilt(string name)
		{
			subjectBuilder.SetName(name);

			var subject = subjectBuilder.Build();
			Assert.That(subject.RuleName, Is.EqualTo(name));
		}

		[TestCase(0)]
		[TestCase(10)]
		[TestCase(30)]
		public void BuilderRefresh_IsBuilt(int refresh)
		{
			subjectBuilder.SetRefreshTime(refresh);

			var subject = subjectBuilder.Build();
			Assert.That(subject.RefreshTime, Is.EqualTo(refresh));
		}

		/// <summary>
		/// Make this abstract class to be testable.
		/// </summary>
		internal class TestableRuleBase : AIRuleBase
		{			

			public override IBattleAction GenerateAction(IBattleEntity source, IBattleEntityManager bem)
			{
				return null; // ignored in tests
			}

			internal class Builder : AIRuleBase.BuilderBase<Builder>, IBuilder<TestableRuleBase>
			{
				private TestableRuleBase rule;
				internal Builder() => Reset();

				public Builder Reset()
				{
					rule = new TestableRuleBase();
					ResetBase(rule);
					return this;
				}

				public TestableRuleBase Build()
				{
					BuildBase();
					return rule;
				}
			}
		}
	}
}
