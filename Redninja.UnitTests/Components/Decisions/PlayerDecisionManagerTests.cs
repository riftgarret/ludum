using System;
using Ninject;
using NSubstitute;
using NUnit.Framework;
using Redninja.Components.Decisions.Player;
using Redninja.Components.Skills;
using Redninja.Components.Targeting;
using Redninja.Entities;
using Redninja.View;

namespace Redninja.Components.Decisions.UnitTests
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
			IUnitModel mEntity = Substitute.For<IUnitModel>();
			IUnitModel entity = null;

			subject.WaitingForDecision += e => entity = e;
			subject.ProcessNextAction(mEntity, mEntityManager);

			Assert.That(entity, Is.EqualTo(mEntity));
		}

		[Test]
		public void ProcessNextAction_ExceptionMultipleBlockingRequested()
		{
			IUnitModel mEntity = Substitute.For<IUnitModel>();

			subject.ProcessNextAction(mEntity, mEntityManager);

			Assert.Throws<InvalidOperationException>(() => subject.ProcessNextAction(mEntity, mEntityManager));
		}

		[Test]
		public void ProcessNextAction_ViewNotifiedWaitingForDecision()
		{
			IUnitModel mEntity = Substitute.For<IUnitModel>();

			subject.ProcessNextAction(mEntity, mEntityManager);

			mBattleView.Received().OnDecisionNeeded(mEntity);
		}

		[Test]
		public void OnTargetingCanceled_ViewModeDefault()
		{
			IUnitModel mEntity = Substitute.For<IUnitModel>();
			ISkill mSkill = Substitute.For<ISkill>();

			mBattleView.SkillSelected += Raise.Event<Action<IUnitModel, ISkill>>(mEntity, mSkill);
			mBattleView.TargetingCanceled += Raise.Event<Action>();

			mBattleView.Received().SetViewModeDefault();
		}

		[Test]
		public void OnSkillSelected_ViewModeTargeting()
		{
			IUnitModel mEntity = Substitute.For<IUnitModel>();
			ISkill mSkill = Substitute.For<ISkill>();

			mBattleView.SkillSelected += Raise.Event<Action<IUnitModel, ISkill>>(mEntity, mSkill);

			mBattleView.Received().SetViewMode(Arg.Any<ITargetingView>());
		}

		[Test]
		public void OnTargetSelected_ExceptionNoSkillSelected()
		{
			ISelectedTarget mTarget = Substitute.For<ISelectedTarget>();

			Assert.Throws<InvalidOperationException>(() => mBattleView.TargetSelected += Raise.Event<Action<ISelectedTarget>>(mTarget));
		}
	}
}
