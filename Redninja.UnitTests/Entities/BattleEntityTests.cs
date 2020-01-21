using NSubstitute;
using NUnit.Framework;
using Redninja.Components.Actions;
using Redninja.Components.Combat;
using Redninja.Components.Decisions;

namespace Redninja.Entities.UnitTests
{
	[TestFixture]
    public class BattleEntityTests
    {
        private BattleEntity subject;
        private IUnit mCharacter;
        private MockClock clock;
		private ICombatExecutor mCombatExecutor;
		private IBattleContext mContext;

        [SetUp]
        public void Setup()
        {
			mContext = Substitute.For<IBattleContext>();			
            clock = new MockClock();
            mCharacter = Substitute.For<IUnit>();
			mCombatExecutor = Substitute.For<ICombatExecutor>();
			mContext.CombatExecutor.Returns(mCombatExecutor);
			subject = new BattleEntity(mContext, mCharacter);            
        }
        
    }
}
