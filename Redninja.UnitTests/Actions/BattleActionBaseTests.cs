using NUnit.Framework;
using Redninja.Components.Actions;

namespace Redninja.UnitTests.Actions
{
	[TestFixture]
    public class BattleActionBaseTests
    {        
        private const float DEFAULT_TIME = 5f;
        private TestableBaseBattleActionImpl subject;
        private MockClock clock;

        [SetUp]
        public void Init()
        {            
            clock = new MockClock();
            InitSubject();
        }

        private void InitSubject(float prepare = DEFAULT_TIME, float execute = DEFAULT_TIME, float recover = DEFAULT_TIME)
        {
            subject = new TestableBaseBattleActionImpl(prepare, execute, recover);
            subject.SetClock(clock);
            subject.Start();
        }

        [Test]
        public void TestSetAction_InitialValueIsPrepare()
        {            
            Assert.That(subject.Phase, Is.EqualTo(ActionPhase.Preparing));
        }

        [Test]
        public void TestSetAction_PhaseClockInitialValues()
        {                        
            Assert.That(subject.PhaseTime, Is.EqualTo(DEFAULT_TIME));
            Assert.That(subject.PhaseProgress, Is.EqualTo(0));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        public void TestTransition_ToExecute_PhaseUpdated(float prepareTime)
        {
            InitSubject(prepare: prepareTime);
            clock.IncrementTime(prepareTime);
            Assert.That(subject.Phase, Is.EqualTo(ActionPhase.Executing));
            Assert.That(subject.PhaseProgress, Is.EqualTo(0f));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        public void TestTransition_ToRecover_PhaseUpdated(float executeTime)
        {
            InitSubject(execute: executeTime);
            clock.IncrementTime(DEFAULT_TIME);
            clock.IncrementTime(executeTime);
            Assert.That(subject.Phase, Is.EqualTo(ActionPhase.Recovering));
            Assert.That(subject.PhaseProgress, Is.EqualTo(0f));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        public void TestTransition_FinishRecover_PhaseDone(float recoveryTime)
        {
            InitSubject(recover: recoveryTime);
            clock.IncrementTime(DEFAULT_TIME);
            clock.IncrementTime(DEFAULT_TIME);
            clock.IncrementTime(recoveryTime);
            Assert.That(subject.Phase, Is.EqualTo(ActionPhase.Done));
            Assert.That(subject.PhaseProgress, Is.EqualTo(1f));
        }

        [TestCase(1, 0)]
        [TestCase(3, 0)]
        [TestCase(2, 1)]
        [TestCase(1.5f, 1)]
        [TestCase(3, 2)]
        [TestCase(4.5f, 2)]
        public void TestTransition_DeltaOverAction_PhaseClockZero(float timePassedOneTick, float prepareTime)
        {
            InitSubject(prepare: prepareTime);
            clock.IncrementTime(timePassedOneTick);
            float expectedTime = (timePassedOneTick - prepareTime) / DEFAULT_TIME;
            Assert.That(subject.PhaseProgress, Is.EqualTo(expectedTime));
        }


        [TestCase(0.5f, 1)]
        [TestCase(0.25f, 1)]
        [TestCase(1f, 2)]        
        [TestCase(1.5f, 2)]
        [TestCase(0.05f, 4)]        
        public void TestTransition_DeltaBeforePhase(float timePassedOneTick, float prepareTime)
        {
            InitSubject(prepare: prepareTime);
            clock.IncrementTime(timePassedOneTick);
            float expectedTime = timePassedOneTick / prepareTime;
            Assert.That(subject.PhaseProgress, Is.EqualTo(expectedTime));
        }

        private class TestableBaseBattleActionImpl : BattleActionBase
        {
            internal TestableBaseBattleActionImpl(float prepare, float execute, float recover)
                : base("", prepare, execute, recover)
            {

            }
            
            protected override void ExecuteAction(float timeDelta, float time)
            {
                
            }
        }
    }
}
