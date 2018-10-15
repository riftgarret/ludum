﻿using System.Collections.Generic;
using Ninject;
using NSubstitute;
using NUnit.Framework;
using Redninja.Components.Actions;
using Redninja.Entities;
using Redninja.Entities.Decisions;
using Redninja.Presenter;
using Redninja.View;

namespace Redninja.UnitTests
{
	[TestFixture]
	public class BattlePresenterTests
	{
		private ICombatExecutor mCombatExecutor;
		private IBattleEntityManager mEntityManager;
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
			mBattleView = Substitute.For<IBattleView>();
			clock = new BattlePresenter.Clock();

			// setup DI
			kernel = new StandardKernel();
			kernel.Bind<ICombatExecutor>().ToConstant(mCombatExecutor);
			kernel.Bind<IBattleEntityManager>().ToConstant(mEntityManager);
			kernel.Bind<IBattleView>().ToConstant(mBattleView);
			kernel.Bind<IDecisionHelper>().ToConstant(Substitute.For<IDecisionHelper>());
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
			mEntityManager.Entities.Returns(new List<IBattleEntity>() { mEntity });

			subject.Initialize();
			subject.Start();

			mEntityManager.Received().SetAction(mEntity, Arg.Any<WaitAction>());
			Assert.That(subject.State, Is.EqualTo(GameState.Active));
		}
	}
}
