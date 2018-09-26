using App.BattleSystem.Actions;
using App.BattleSystem.Entity;
using NSubstitute;
using NUnit.Framework;

namespace App.UnitTests.BattleSystem.Entity
{
    [TestFixture]
    public class BaseBattleActionTests
    {        
        private const float DEFAULT_TIME = 5f;
        private TestableBaseBattleActionImpl subject;

        [SetUp]
        public void Init()
        {
            subject = new TestableBaseBattleActionImpl();
            SetPhaseTimes();
        }

        private void SetPhaseTimes(float prepare = DEFAULT_TIME, float execute = DEFAULT_TIME, float recover = DEFAULT_TIME)
        {
            subject.prepare = prepare;
            subject.action = execute;
            subject.recover = recover;
            subject.SetPhase(PhaseState.PREPARE);
        }

        [Test]
        public void TestSetAction_InitialValueIsPrepare()
        {
            Assert.That(subject.Phase, Is.EqualTo(PhaseState.PREPARE));
        }

        [Test]
        public void TestSetAction_PhaseClockInitialValues()
        {            
            Assert.That(subject.PhaseClock, Is.EqualTo(0));
            Assert.That(subject.PhaseComplete, Is.EqualTo(DEFAULT_TIME));
            Assert.That(subject.PhasePercent, Is.EqualTo(0));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        public void TestTransition_ToExecute_PhaseUpdated(float prepareTime)
        {
            SetPhaseTimes(prepare: prepareTime);
            subject.IncrementGameClock(prepareTime);
            Assert.That(subject.Phase, Is.EqualTo(PhaseState.EXECUTE));
            Assert.That(subject.PhaseClock, Is.EqualTo(0f));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        public void TestTransition_ToRecover_PhaseUpdated(float executeTime)
        {            
            SetPhaseTimes(execute: executeTime);
            subject.IncrementGameClock(DEFAULT_TIME);
            subject.IncrementGameClock(executeTime);
            Assert.That(subject.Phase, Is.EqualTo(PhaseState.RECOVER));
            Assert.That(subject.PhaseClock, Is.EqualTo(0f));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        public void TestTransition_FinishRecover_PhaseStaysSameAt1(float recoveryTime)
        {
            SetPhaseTimes(recover: recoveryTime);
            subject.IncrementGameClock(DEFAULT_TIME);
            subject.IncrementGameClock(DEFAULT_TIME);
            subject.IncrementGameClock(recoveryTime);
            Assert.That(subject.Phase, Is.EqualTo(PhaseState.RECOVER));
            Assert.That(subject.PhasePercent, Is.EqualTo(1f));
        }

        [TestCase(1, 0)]
        [TestCase(3, 0)]
        [TestCase(2, 1)]
        [TestCase(1.5f, 1)]
        [TestCase(3, 2)]
        [TestCase(4.5f, 2)]
        public void TestTransition_DeltaOverAction_PhaseClockZero(float timePassedOneTick, float prepareTime)
        {
            SetPhaseTimes(prepare: prepareTime);
            subject.IncrementGameClock(timePassedOneTick);
            Assert.That(subject.PhaseClock, Is.EqualTo(0));
        }


        [TestCase(0.5f, 1)]
        [TestCase(0.25f, 1)]
        [TestCase(1f, 2)]        
        [TestCase(1.5f, 2)]
        [TestCase(0.05f, 4)]        
        public void TestTransition_DeltaBeforePhase(float timePassedOneTick, float prepareTime)
        {
            SetPhaseTimes(prepare: prepareTime);
            subject.IncrementGameClock(timePassedOneTick);
            Assert.That(subject.PhaseClock, Is.EqualTo(timePassedOneTick));
        }

        private class TestableBaseBattleActionImpl : BaseBattleAction
        {

            internal float prepare, action, recover;

            internal void SetPhase(PhaseState state)
            {
                base.SetPhase(state);
            }
           
            public override float TimePrepare => prepare;

            public override float TimeAction => action;

            public override float TimeRecover => recover;

            protected override void ExecuteAction(float actionClock)
            {
                // TODO update for test
            }
        }
    }
}