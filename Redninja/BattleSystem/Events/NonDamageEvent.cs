using Redninja.BattleSystem.Entity;

namespace Redninja.BattleSystem.Events
{
    public class NonDamageEvent : IBattleEvent
    {
        private BattleEntity srcEntity;
        private BattleEntity destEntity;

        public NonDamageEvent(BattleEntity srcEntity, BattleEntity destEntity)
        {
            this.srcEntity = srcEntity;
            this.destEntity = destEntity;
        }

        public BattleEntity SrcEntity
        {
            get
            {
                return srcEntity;
            }
        }

        public BattleEntity DestEntity
        {
            get
            {
                return destEntity;
            }
        }

        public BattleEventType EventType
        {
            get
            {
                return BattleEventType.NON_DAMAGE;
            }
        }

        public override string ToString()
        {
            return string.Format("[NonDamageEvent: mSrcEntity={0}, mDestEntity={1}]", srcEntity, destEntity);
        }

    } 
}

