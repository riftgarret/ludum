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

namespace Redninja.UnitTests
{
	[TestFixture]
    public class BattlePresenterTests
    {
		private ICombatExecutor mCombatExecutor;
		private IBattleEntityManager mEntityManager;
		private IDecisionManager mDecisionManager;
		private IBattleView mBattleView;
		private IKernel kernel;
		private BattlePresenter.Clock clock;

		private BattlePresenter subject;
        
        [SetUp]
        public void Setup()
        {
			// setup mocks
			mCombatExecutor = Substitute.For<ICombatExecutor>();
			mEntityManager = Substitute.For<IBattleEntityManager>();
			mDecisionManager = Substitute.For<IDecisionManager>();
			mBattleView = Substitute.For<IBattleView>();
			clock = new BattlePresenter.Clock();

			// setup DI
			kernel = new StandardKernel();
			kernel.Bind<ICombatExecutor>().ToConstant(mCombatExecutor);
			kernel.Bind<IBattleEntityManager>().ToConstant(mEntityManager);
			kernel.Bind<IDecisionManager>().ToConstant(mDecisionManager);
			kernel.Bind<IBattleView>().ToConstant(mBattleView);
			kernel.Bind<BattlePresenter.Clock>().ToConstant(clock);

			subject = kernel.Get<BattlePresenter>();
        }

		[TearDown]
		public void TearDown()
		{
			kernel.Dispose();
		}
        
		[Test]
        public void Initialization_EntityReceivedActionsWait()
        {
			IBattleEntity mEntity = Substitute.For<IBattleEntity>();
			mEntityManager.AllEntities.Returns(new List<IBattleEntity>() { mEntity });

			subject.Initialize();

			mEntityManager.Received().SetAction(mEntity, Arg.Any<WaitAction>());
			Assert.That(subject.State, Is.EqualTo(GameState.Active));
        }

		[Test]
		public void OnTargetSelected_ActionCreatedAndAssigned()
		{
			IBattleEntity mEntity = Substitute.For<IBattleEntity>();
			IBattleAction mAction = Substitute.For<IBattleAction>();
			ICombatSkill mSkill = Substitute.For<ICombatSkill>();
			SelectedTarget target = new SelectedTarget(null, 0, 0, 0);

			mDecisionManager.CreateAction(null, null, null).ReturnsForAnyArgs(mAction);

			subject.OnTargetSelected(
				mEntity,
				mSkill,
				target);

			mDecisionManager.Received().CreateAction(mEntity, mSkill, target);
			mEntityManager.Received().SetAction(mEntity, mAction);

			mAction.Received().BattleOperationReady += Arg.Any<Action<IBattleOperation>>();
		}

		[Test]
		public void OnTargetSelected_ViewModeReset()
		{
			IBattleEntity mEntity = Substitute.For<IBattleEntity>();
			IBattleAction mAction = Substitute.For<IBattleAction>();
			ICombatSkill mSkill = Substitute.For<ICombatSkill>();		
			SelectedTarget target = new SelectedTarget(null, 0, 0, 0);

			mDecisionManager.CreateAction(null, null, null).ReturnsForAnyArgs(mAction);

			subject.OnTargetSelected(
				mEntity,
				mSkill,
				target);
			
			mBattleView.Received().SetViewModeDefault();
		}

		[Test]
		public void OnSkillSelected_ViewModeTargeting()
		{
			IBattleEntity mEntity = Substitute.For<IBattleEntity>();			
			ICombatSkill mSkill = Substitute.For<ICombatSkill>();
			SkillTargetMeta meta = new SkillTargetMeta(mEntity, mSkill, mEntityManager);

			mDecisionManager.GetSelectableTargets(null, null).ReturnsForAnyArgs(meta);

			subject.OnSkillSelected(mEntity, mSkill);

			mDecisionManager.Received().GetSelectableTargets(mEntity, mSkill);
			mBattleView.Received().SetViewModeTargeting(meta);
		}
	}
}
