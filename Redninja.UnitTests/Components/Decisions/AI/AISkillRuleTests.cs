using System;
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
	}
}
