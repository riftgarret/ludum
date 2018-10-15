using Davfalcon.Revelator;
using NSubstitute;
using NUnit.Framework;
using Redninja.Components.Actions;
using Redninja.Components.Combat;
using Redninja.Components.Decisions;
using Redninja.Entities;

namespace Redninja.UnitTests.Entities
{
	[TestFixture]
    public class BattleEntityTests
    {
        private BattleEntity subject;
        private IUnit mCharacter;
        private IActionDecider mActionDecider;
        private MockClock clock;
		private ICombatExecutor mCombatExecutor;

        [SetUp]
        public void Setup()
        {
            clock = new MockClock();
            mCharacter = Substitute.For<IUnit>();
			mActionDecider = Substitute.For<IActionDecider>();
			mCombatExecutor = Substitute.For<ICombatExecutor>();
            subject = new BattleEntity(mCharacter, mActionDecider, mCombatExecutor);
            subject.SetClock(clock);
        }
        
        [TestCase(1, 3)]
        [TestCase(0, 1)]
        [TestCase(0, 0)]
        public void MovePosition_PositionExpected(int expectedX, int expectedY)
        {
            int originalSize = subject.Position.Size;
            subject.MovePosition(expectedX, expectedY);
            Assert.That(subject.Position, 
                Is.EqualTo(new UnitPosition(expectedX, expectedY, originalSize)));
        }

        [Test]
        public void SetAction_GetsClock()
        {
            IBattleAction mAction = Substitute.For<IBattleAction>();

            subject.SetAction(mAction);

            mAction.Received().SetClock(clock);
            mAction.Received().Start();
        }

        [Test]
        public void SetAction_PreviousActionDisposed()
        {
            IBattleAction mAction1 = Substitute.For<IBattleAction>();
            IBattleAction mAction2 = Substitute.For<IBattleAction>();

            subject.SetAction(mAction1);
            subject.SetAction(mAction2);

            mAction1.Received().Dispose();
        }

        [Test]
        public void OnTick_ActionDone_InvokeRequireDelegate()
        {
            IBattleAction mAction = Substitute.For<IBattleAction>();
            subject.SetAction(mAction);
            mAction.Phase.Returns(ActionPhase.Done);

            bool triggered = false;
            subject.DecisionRequired += (x => triggered = true);

            clock.IncrementTime(1);

            Assert.That(triggered, Is.True);
        }
    }
}
