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
        private IUnit mockCharacter;
        private ICombatResolver mockCombatResolver;
        private MockClock clock;

        [SetUp]
        public void Setup()
        {
            clock = new MockClock();
            mockCharacter = Substitute.For<IUnit>();
            mockCombatResolver = Substitute.For<ICombatResolver>();
            subject = new BattleEntity(mockCharacter, mockCombatResolver);
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
            IBattleAction action = Substitute.For<IBattleAction>();

            subject.SetAction(action);

            action.Received().SetClock(clock);
            action.Received().Start();
        }

        [Test]
        public void SetAction_PreviousActionDisposed()
        {
            IBattleAction action1 = Substitute.For<IBattleAction>();
            IBattleAction action2 = Substitute.For<IBattleAction>();

            subject.SetAction(action1);
            subject.SetAction(action2);

            action1.Received().Dispose();
        }

        [Test]
        public void OnTick_ActionDone_InvokeRequireDelegate()
        {
            IBattleAction action = Substitute.For<IBattleAction>();
            subject.SetAction(action);
            action.Phase.Returns(PhaseState.Done);

            bool triggered = false;
            subject.DecisionRequired += (x => triggered = true);

            clock.IncrementTime(1);

            Assert.That(triggered, Is.True);
        }
    }
}
