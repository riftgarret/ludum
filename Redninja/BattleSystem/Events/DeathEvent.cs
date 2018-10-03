using Redninja.BattleSystem.Entities;
using System;

namespace Redninja.BattleSystem.Events
{
    public class DeathEvent : IBattleEvent
    {
        private BattleEntity mSrcEntity;

        public DeathEvent(BattleEntity entity)
        {
            mSrcEntity = entity;
        }

        public BattleEntity SrcEntity
        {
            get
            {
                return mSrcEntity;
            }
        }

        public BattleEventType EventType
        {
            get
            {
                return BattleEventType.DEATH;
            }
        }

        public override string ToString()
        {
            return string.Format("[DeathEvent: srcEntity={0}]", SrcEntity.Character.displayName);
        }
    } 
}
