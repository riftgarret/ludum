﻿using Davfalcon.Revelator;
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
			mEntityManager.AllEntities.Returns(new List<IBattleEntity>() { mEntity });

			subject.Initialize();
			subject.Start();

			mEntityManager.Received().SetAction(mEntity, Arg.Any<WaitAction>());
			Assert.That(subject.State, Is.EqualTo(GameState.Active));
		}
	}
}
