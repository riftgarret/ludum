using Davfalcon.Revelator;
using Davfalcon.Revelator.Combat;
using Ninject;
using NSubstitute;
using NUnit.Framework;
using Redninja.Actions;
using Redninja.Decisions;
using Redninja.Entities;
using Redninja.Skills;
using Redninja.Targeting;
using System;
using System.Collections.Generic;

namespace Redninja.Decisions.UnitTests
{
	[TestFixture]
	public class PlayerDecisionManagerTests
	{
		private IBattleEntityManager mEntityManager;
		private IBattleView mBattleView;
		private IKernel kernel;

		private PlayerDecisionManager subject;

		[SetUp]
		public void Setup()
		{
			// setup mocks
			mEntityManager = Substitute.For<IBattleEntityManager>();
			mBattleView = Substitute.For<IBattleView>();

			// setup DI
			kernel = new StandardKernel();
			kernel.Bind<IBattleEntityManager>().ToConstant(mEntityManager);
			kernel.Bind<IBattleView>().ToConstant(mBattleView);
			kernel.Bind<IDecisionHelper>().ToConstant(Substitute.For<IDecisionHelper>());

			subject = kernel.Get<PlayerDecisionManager>();
		}

		[TearDown]
		public void TearDown()
		{
			kernel.Dispose();
		}

		[Test]
		public void ProcessNextAction_EventRaisedWaitingForDecision()
		{
			IBattleEntity mEntity = Substitute.For<IBattleEntity>();
			IBattleEntity entity = null;

			subject.WaitingForDecision += e => entity = e;
			subject.ProcessNextAction(mEntity, mEntityManager);

			Assert.That(entity, Is.EqualTo(mEntity));
		}

		[Test]
		public void ProcessNextAction_ExceptionMultipleBlockingRequested()
		{
			IBattleEntity mEntity = Substitute.For<IBattleEntity>();

			subject.ProcessNextAction(mEntity, mEntityManager);

			Assert.Throws<InvalidOperationException>(() => subject.ProcessNextAction(mEntity, mEntityManager));
		}

		[Test]
		public void ProcessNextAction_ViewNotifiedWaitingForDecision()
		{
			IBattleEntity mEntity = Substitute.For<IBattleEntity>();

			subject.ProcessNextAction(mEntity, mEntityManager);

			mBattleView.Received().OnDecisionNeeded(mEntity);
		}

		[Test]
		public void OnTargetingCanceled_ViewModeDefault()
		{
			IBattleEntity mEntity = Substitute.For<IBattleEntity>();
			ICombatSkill mSkill = Substitute.For<ICombatSkill>();

			mBattleView.SkillSelected += Raise.Event<Action<IBattleEntity, ICombatSkill>>(mEntity, mSkill);
			mBattleView.TargetingCanceled += Raise.Event<Action>();

			mBattleView.Received().SetViewModeDefault();
		}

		[Test]
		public void OnSkillSelected_ViewModeTargeting()
		{
			IBattleEntity mEntity = Substitute.For<IBattleEntity>();
			ICombatSkill mSkill = Substitute.For<ICombatSkill>();

			mBattleView.SkillSelected += Raise.Event<Action<IBattleEntity, ICombatSkill>>(mEntity, mSkill);

			mBattleView.Received().SetViewModeTargeting(Arg.Any<ISkillTargetingInfo>());
		}

		[Test]
		public void OnTargetSelected_ExceptionNoSkillSelected()
		{
			ISelectedTarget mTarget = Substitute.For<ISelectedTarget>();

			Assert.Throws<InvalidOperationException>(() => mBattleView.TargetSelected += Raise.Event<Action<ISelectedTarget>>(mTarget));
		}
	}
}
