using Redninja.BattleSystem.Entity;
using Redninja.BattleSystem.Events;
using System;

namespace Redninja.BattleSystem.Effects
{
    public class StatusEffectEvent : IBattleEvent
    {
        public enum StatusEventType
        {
            NEW,
            REPLACED,
            REMOVED,
            EXPIRED,
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusEffectEvent"/> class.
        /// </summary>
        /// <param name="srcEntity">Source entity.</param>
        /// <param name="statusEffectProperty">Status effect property.</param>
        /// <param name="statusEventType">Status event type.</param>
        /// <param name="statusType">Status type.</param>
        public StatusEffectEvent(BattleEntity srcEntity,
                                  StatusEffectProperty statusEffectProperty,
                                  StatusEventType statusEventType,
                                  StatusEffectType statusType)
        {
            this.SrcEntity = srcEntity;
            this.statusEffectProperty = statusEffectProperty;
            this.statusEventType = statusEventType;
            this.statusType = statusType;
        }

        public BattleEntity SrcEntity
        {
            private set;
            get;
        }

        public StatusEventType statusEventType
        {
            private set;
            get;
        }

        public StatusEffectProperty statusEffectProperty
        {
            private set;
            get;
        }

        public StatusEffectType statusType
        {
            private set;
            get;
        }

        public BattleEventType EventType
        {
            get
            {
                return BattleEventType.STATUS_EFFECT;
            }
        }

        public override string ToString()
        {
            return string.Format("[StatusEffectEvent: srcEntity={0}, statusEventType={1}, statusEffect={2}, eventType={3}]", SrcEntity, statusEventType, statusEffectProperty, EventType);
        }

    } 
}

