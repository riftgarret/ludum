using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Davfalcon.Revelator;
using NSubstitute;
using NUnit.Framework;
using Redninja.AI;
using Redninja.Decisions;
using Redninja.Skills;
using Redninja.Targeting;

namespace Redninja.UnitTests.AI
{
	[TestFixture]
	public class AISkillRuleTests : AIRuleBaseTests<AISkillRule.Builder, AISkillRule>
	{			
		private AISkillRule.Builder subjectBuilder;

		private IActionPhaseHelper mActionHelper;

		// need this to allow builder to complete but we set it to have no resolvement
		private Tuple<ICombatSkill, IAITargetPriority> mInitialSkill;				

		protected override AIRuleBase.BuilderBase<AISkillRule.Builder, AISkillRule> SubjectBuilder => subjectBuilder;

		[SetUp]
		public void Setup()
		{			
			subjectBuilder = new AISkillRule.Builder();
			subjectBuilder.SetRuleTargetType(TargetTeam.Enemy);
			mInitialSkill = AddSkillPriority(null); // required for builder			

			mActionHelper = Substitute.For<IActionPhaseHelper>();
			mDecisionHelper.GetAvailableSkills(Arg.Any<IBattleEntity>()).Returns(mActionHelper);

			SetupBuilder();
		}

		private IAITargetCondition AddFilterCondition(bool isTrue, IBattleEntity onlyForTarget = null)
		{
			IAITargetCondition mFilterCondition = Substitute.For<IAITargetCondition>();
			mFilterCondition.IsValid(onlyForTarget ?? Arg.Any<IBattleEntity>()).Returns(isTrue);
			subjectBuilder.AddFilterCondition(mFilterCondition);
			return mFilterCondition;
		}

		private Tuple<ICombatSkill, IAITargetPriority> AddSkillPriority(IBattleEntity bestTarget)
		{
			var mSkill = Substitute.For<ICombatSkill>();
			var mPriority = Substitute.For<IAITargetPriority>();

			mPriority.GetBestTarget(Arg.Any<IEnumerable<IBattleEntity>>()).Returns(bestTarget);
			subjectBuilder.AddSkillAndPriority(mSkill, mPriority);
			
			return Tuple.Create(mSkill, mPriority);
		}

		// this is a nasty test and should be just be ran as an integration test later.
		// if this breaks, just comment it out and let me know.
		[Test]
		public void GenerateAction_FilterCondition_FindsSelf()
		{
			subjectBuilder.SetRuleTargetType(TargetTeam.Self);

			// setup the skill
			var skillMeta = AddSkillPriority(mSource);
			var mSkill = skillMeta.Item1;
			mActionHelper.Skills.Returns(new List<ICombatSkill>() { mSkill });

			// make sure the skill has target meta
			var returnedAction = Substitute.For<IBattleAction>();			
			var mTargetHelper = Substitute.For<ITargetPhaseHelper>();
			var mSelectedTarget = mTargetHelper.GetSelectedTarget(mSource);

			mTargetHelper.TargetingRule.IsValidTarget(mSource, mSource).Returns(true);			
			mTargetHelper.Ready.Returns(true);			
			mTargetHelper.Skill.Returns(mSkill);
			mDecisionHelper.GetTargetingManager(mSource, mSkill).Returns(mTargetHelper);

			var subject = subjectBuilder.Build();			

			var result = subject.GenerateAction(mSource, mDecisionHelper);

			mTargetHelper.Received().SelectTarget(mSelectedTarget);
			Assert.That(result, Is.Not.Null);
		}

		[Test]
		public void GetAssignableSkills_FindsOnlyAssignedSkills()
		{
			var skill1 = AddSkillPriority(null).Item1;
			var skill2 = AddSkillPriority(null).Item1;
			var skill3 = AddSkillPriority(null).Item1;

			mActionHelper.Skills.Returns(new List<ICombatSkill>() { skill1, skill2 });

			var subject = subjectBuilder.Build();

			var result = subject.GetAssignableSkills(mActionHelper);

			Assert.That(result, Has.Exactly(2).Items);
			Assert.That(result, Does.Contain(skill1));
			Assert.That(result, Does.Contain(skill2));
			Assert.That(result, Does.Not.Contain(skill3));
		}

		[Test]
		public void TryFindTarget_HasFindsTarget()
		{
			var enemy1 = AddEntity(enemyTeam);			
			var enemy2 = AddEntity(enemyTeam);
			var condition = AddFilterCondition(true);
			
			condition.IsValid(Arg.Is<IBattleEntity>(x => x == enemy1 || x == enemy2)).Returns(true);			

			ITargetPhaseHelper mTargetHelper = Substitute.For<ITargetPhaseHelper>();
			mTargetHelper.TargetingRule.IsValidTarget(Arg.Is<IBattleEntity>(x => x == enemy1 || x == enemy2), mSource).Returns(true);
			mTargetHelper.Skill.Returns(mInitialSkill.Item1);
			mInitialSkill.Item2.GetBestTarget(Arg.Any<IEnumerable<IBattleEntity>>())
				.ReturnsForAnyArgs(x => x.Arg<IEnumerable<IBattleEntity>>()
				.First(e => e == enemy1));
			
			subjectBuilder.SetRuleTargetType(TargetTeam.Enemy);
			var subject = SubjectBuilder.Build();

			ISelectedTarget resultTarget = null;
			var result = subject.TryFindTarget(mTargetHelper, mSource, mBem, out resultTarget);

			Assert.That(result, Is.True);
			mTargetHelper.Received().GetSelectedTarget(enemy1);
		}

		[Test]
		public void TryFindTarget_DoesNotFindTarget()
		{
			var enemy1 = AddEntity(enemyTeam);
			var enemy2 = AddEntity(enemyTeam);
			var condition = AddFilterCondition(true);

			condition.IsValid(Arg.Is<IBattleEntity>(x => x == enemy1 || x == enemy2)).Returns(true);

			ITargetPhaseHelper mTargetHelper = Substitute.For<ITargetPhaseHelper>();
			mTargetHelper.TargetingRule.IsValidTarget(Arg.Is<IBattleEntity>(x => x == enemy1 || x == enemy2), mSource).Returns(false);
			mTargetHelper.Skill.Returns(mInitialSkill.Item1);

			subjectBuilder.SetRuleTargetType(TargetTeam.Enemy);
			var subject = SubjectBuilder.Build();

			ISelectedTarget resultTarget = null;
			var result = subject.TryFindTarget(mTargetHelper, mSource, mBem, out resultTarget);

			Assert.That(result, Is.False);			
		}
	}
}
