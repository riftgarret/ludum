using Davfalcon.Revelator;
using Davfalcon.Revelator.Combat;
using NSubstitute;
using NUnit.Framework;
using Redninja.BattleSystem;
using Redninja.BattleSystem.Entities;

namespace Redninja.UnitTests.BattleSystem.Entities
{
    [TestFixture]
    public class BattleEntityTests
    {
        private BattleEntity subject;
        private IUnit mCharacter;
        private ICombatResolver mCombatResolver;
        private MockClock clock;

        [SetUp]
        public void Setup()
        {
            clock = new MockClock();
            mCharacter = Substitute.For<IUnit>();
            mCombatResolver = Substitute.For<ICombatResolver>();
            subject = new BattleEntity(mCharacter, mCombatResolver);
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
                Is.EqualTo(new EntityPosition(expectedX, expectedY, originalSize)));
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
            mAction.Phase.Returns(PhaseState.Done);

            bool triggered = false;
            subject.DecisionRequired += (x => triggered = true);

            clock.IncrementTime(1);

            Assert.That(triggered, Is.True);
        }
    }
}
