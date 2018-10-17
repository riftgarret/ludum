﻿using System;
using System.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using Redninja.Components.Actions;
using Redninja.Components.Decisions;
using Redninja.Components.Decisions.AI;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;

namespace Redninja.Components.Decisions.AI.UnitTests
{
	[TestFixture]
	public class AISkillRuleTests : AIRuleBaseTests<AISkillRule.Builder, AISkillRule>
	{			
		private AISkillRule.Builder subjectBuilder;

		private IActionsContext mActionHelper;

		// need this to allow builder to complete but we set it to have no resolvement
		private Tuple<ISkill, IAITargetPriority> mInitialSkill;				

		protected override AIRuleBase.BuilderBase<AISkillRule.Builder, AISkillRule> SubjectBuilder => subjectBuilder;

		[SetUp]
		public void Setup()
		{			
			subjectBuilder = new AISkillRule.Builder();
			subjectBuilder.SetRuleTargetType(TargetTeam.Enemy);
			mInitialSkill = AddSkillPriority(null); // required for builder			

			mActionHelper = Substitute.For<IActionsContext>();
			mDecisionHelper.GetActionsContext(Arg.Any<IUnitModel>()).Returns(mActionHelper);

			SetupBuilder();
		}

		private IAITargetCondition AddFilterCondition(bool isTrue, IUnitModel onlyForTarget = null)
		{
			IAITargetCondition mFilterCondition = Substitute.For<IAITargetCondition>();
			mFilterCondition.IsValid(onlyForTarget ?? Arg.Any<IUnitModel>()).Returns(isTrue);
			subjectBuilder.AddFilterCondition(mFilterCondition);
			return mFilterCondition;
		}

		private Tuple<ISkill, IAITargetPriority> AddSkillPriority(IUnitModel bestTarget)
		{
			var mSkill = Substitute.For<ISkill>();
			var mPriority = Substitute.For<IAITargetPriority>();

			mPriority.GetBestTarget(Arg.Any<IEnumerable<IUnitModel>>()).Returns(bestTarget);
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
			mActionHelper.Skills.Returns(new List<ISkill>() { mSkill });

			// make sure the skill has target meta
			var returnedAction = Substitute.For<IBattleAction>();			
			var mTargetHelper = Substitute.For<ITargetingContext>();
			var mSelectedTarget = mTargetHelper.GetSelectedTarget(mSource);

			mTargetHelper.TargetingRule.IsValidTarget(mSource, mSource).Returns(true);			
			mTargetHelper.Ready.Returns(true);			
			mTargetHelper.Skill.Returns(mSkill);
			mDecisionHelper.GetTargetingContext(mSource, mSkill).Returns(mTargetHelper);

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

			mActionHelper.Skills.Returns(new List<ISkill>() { skill1, skill2 });

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
			
			condition.IsValid(Arg.Is<IUnitModel>(x => x == enemy1 || x == enemy2)).Returns(true);			

			ITargetingContext mTargetHelper = Substitute.For<ITargetingContext>();
			mTargetHelper.TargetingRule.IsValidTarget(Arg.Is<IUnitModel>(x => x == enemy1 || x == enemy2), mSource).Returns(true);
			mTargetHelper.Skill.Returns(mInitialSkill.Item1);
			mInitialSkill.Item2.GetBestTarget(Arg.Any<IEnumerable<IUnitModel>>())
				.ReturnsForAnyArgs(x => x.Arg<IEnumerable<IUnitModel>>()
				.First(e => e == enemy1));
			
			subjectBuilder.SetRuleTargetType(TargetTeam.Enemy);
			var subject = SubjectBuilder.Build();

			var result = subject.TryFindTarget(mTargetHelper, mSource, mBem, out ISelectedTarget resultTarget);

			Assert.That(result, Is.True);
			mTargetHelper.Received().GetSelectedTarget(enemy1);
		}

		[Test]
		public void TryFindTarget_DoesNotFindTarget()
		{
			var enemy1 = AddEntity(enemyTeam);
			var enemy2 = AddEntity(enemyTeam);
			var condition = AddFilterCondition(true);

			condition.IsValid(Arg.Is<IUnitModel>(x => x == enemy1 || x == enemy2)).Returns(true);

			ITargetingContext mTargetHelper = Substitute.For<ITargetingContext>();
			mTargetHelper.TargetingRule.IsValidTarget(Arg.Is<IUnitModel>(x => x == enemy1 || x == enemy2), mSource).Returns(false);
			mTargetHelper.Skill.Returns(mInitialSkill.Item1);

			subjectBuilder.SetRuleTargetType(TargetTeam.Enemy);
			var subject = SubjectBuilder.Build();

			var result = subject.TryFindTarget(mTargetHelper, mSource, mBem, out ISelectedTarget resultTarget);

			Assert.That(result, Is.False);			
		}
	}
}
