using Redninja.BattleSystem.Entities;

namespace Redninja.BattleSystem.Events
{
    public class ResistEvent : IBattleEvent
    {
        private BattleEntity srcEntity;
        private BattleEntity destEntity;


        public ResistEvent(BattleEntity srcEntity, BattleEntity destEntity)
        {
            this.srcEntity = srcEntity;
            this.destEntity = destEntity;
        }

        /// <summary>
        /// Gets the source entity. This is the attacker
        /// </summary>
        /// <value>The source entity.</value>
        public BattleEntity SrcEntity
        {
            get
            {
                return srcEntity;
            }
        }

        /// <summary>
        /// Gets the destination entity. This is the defender who dodged
        /// </summary>
        /// <value>The destination entity.</value>
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
                return BattleEventType.RESIST;
            }
        }

        public override string ToString()
        {
            return string.Format("[ResistEvent: mSrcEntity={0}, mDestEntity={1}]", srcEntity, destEntity);
        }

    } 
}

