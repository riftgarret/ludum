using Redninja.BattleSystem.Entity;

namespace Redninja.BattleSystem.Events
{
    public class HealEvent : IBattleEvent
    {
        private BattleEntity srcEntity;
        private BattleEntity destEntity;
        private float mHeal;
        private float mCritHeal;

        public HealEvent(BattleEntity srcEntity, BattleEntity destEntity, float heal, float critHeal)
        {
            this.srcEntity = srcEntity;
            this.destEntity = destEntity;
            this.mHeal = heal;
            this.mCritHeal = critHeal;

        }

        public BattleEntity SrcEntity
        {
            get
            {
                return srcEntity;
            }
        }

        public BattleEventType EventType
        {
            get
            {
                return BattleEventType.MOVE;
            }
        }

        public BattleEntity DestEntity
        {
            get
            {
                return destEntity;
            }
        }

        public float TotalHeal
        {
            get
            {
                return mHeal + mCritHeal;
            }
        }

        public float Heal
        {
            get
            {
                return mHeal;
            }
        }

        public float CritHeal
        {
            get
            {
                return mCritHeal;
            }
        }

        public override string ToString()
        {
            return string.Format("[HealEvent: mSrcEntity={0}, mDestEntity={1}, mHeal={2}, mCritHeal={3}]", srcEntity, destEntity, mHeal, mCritHeal);
        }

    }
}


