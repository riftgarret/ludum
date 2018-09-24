using App.BattleSystem.Action;
using App.BattleSystem.Entity;
using NSubstitute;
using NUnit.Framework;

namespace App.UnitTests.BattleSystem.Entity
{
    [TestFixture]
    public class TurnStateTests
    {
        private const float DEFAULT_TIME = 5f;
        private TurnState subject;

        [SetUp]
        public void Init()
        {
            subject = new TurnState();
        }

        private IBattleAction createAction(float prepare = DEFAULT_TIME, float execute = DEFAULT_TIME, float recover = DEFAULT_TIME)
        {
            var sub = Substitute.For<IBattleAction>();
            sub.TimePrepare.Returns(prepare);
            sub.TimeAction.Returns(execute);
            sub.TimeRecover.Returns(recover);
            return sub;
        }

        [Test]
        public void TestSetAction_InitialValueIsPrepare()
        {
            subject.SetAction(createAction());
            Assert.That(subject.Phase, Is.EqualTo(TurnState.PhaseState.PREPARE));
        }

        [Test]
        public void TestSetAction_TurnClockInitialValues()
        {
            subject.SetAction(createAction());
            Assert.That(subject.TurnClock, Is.EqualTo(0));
            Assert.That(subject.TurnComplete, Is.EqualTo(DEFAULT_TIME));
            Assert.That(subject.TurnPercent, Is.EqualTo(0));
        }

        [Test]
        public void TestTransition_ToExecute_FiresEvent()
        {
            var callback = Substitute.For<TurnState.OnStartActionExecution>();
            subject.OnStartActionExecutionDelegate += callback;
            subject.SetAction(createAction());
            subject.IncrementGameClock(DEFAULT_TIME);
            callback.Received().Invoke();
            Assert.That(subject.Phase, Is.EqualTo(TurnState.PhaseState.EXECUTE));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        public void TestTransition_ToExecute_PhaseUpdated(float prepareTime)
        {
            subject.SetAction(createAction(prepare: prepareTime));
            subject.IncrementGameClock(prepareTime);
            Assert.That(subject.Phase, Is.EqualTo(TurnState.PhaseState.EXECUTE));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        public void TestTransition_ToRecover_PhaseUpdated(float executeTime)
        {            
            subject.SetAction(createAction(execute: executeTime));
            subject.IncrementGameClock(DEFAULT_TIME);
            subject.IncrementGameClock(executeTime);
            Assert.That(subject.Phase, Is.EqualTo(TurnState.PhaseState.RECOVER));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        public void TestTransition_ToInputRequired_PhaseUpdated(float recoveryTime)
        {
            subject.SetAction(createAction(recover: recoveryTime));
            subject.IncrementGameClock(DEFAULT_TIME);
            subject.IncrementGameClock(DEFAULT_TIME);
            subject.IncrementGameClock(recoveryTime);
            Assert.That(subject.Phase, Is.EqualTo(TurnState.PhaseState.REQUIRES_INPUT));
        }

        [TestCase(1, 0)]
        [TestCase(3, 0)]
        [TestCase(2, 1)]
        [TestCase(1.5f, 1)]
        [TestCase(3, 2)]
        [TestCase(4.5f, 2)]
        public void TestTransition_DeltaOverAction_TurnClockZero(float timePassedOneTick, float prepareTime)
        {
            subject.SetAction(createAction(prepare: prepareTime));
            subject.IncrementGameClock(timePassedOneTick);
            Assert.That(subject.TurnClock, Is.EqualTo(0));
        }


        [TestCase(0.5f, 1)]
        [TestCase(0.25f, 1)]
        [TestCase(1f, 2)]        
        [TestCase(1.5f, 2)]
        [TestCase(0.05f, 4)]        
        public void TestTransition_DeltaBeforeTurn(float timePassedOneTick, float prepareTime)
        {
            subject.SetAction(createAction(prepare: prepareTime));
            subject.IncrementGameClock(timePassedOneTick);
            Assert.That(subject.TurnClock, Is.EqualTo(timePassedOneTick));
        }
    }
}