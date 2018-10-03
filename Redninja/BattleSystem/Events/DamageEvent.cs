
using Redninja.BattleSystem.Combat.Operation;
using Redninja.BattleSystem.Entities;
using Redninja.BattleSystem.Events;
using Redninja.Core.Stats;

namespace Redninja.BattleSystem.Events
{
    /// <summary>
    /// Damage event.
    /// </summary>
    public class DamageEvent : IBattleEvent
    {
        private BattleEntity srcEntity;
        private BattleEntity destEntity;
        private ElementVector defense;
        private ElementVector damage;
        private ElementVector critDamage;

        public DamageEvent(BattleEntity srcEntity, BattleEntity destEntity, ElementVector damage, ElementVector critDamage, ElementVector defense)
        {
            this.srcEntity = srcEntity;
            this.destEntity = destEntity;
            this.damage = damage;
            this.critDamage = critDamage;
            this.defense = defense;
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
                return BattleEventType.DAMAGE;
            }
        }

        public BattleEntity DestEntity
        {
            get
            {
                return destEntity;
            }
        }

        public float TotalDamage
        {
            get
            {
                return CombatUtil.CalculateDamage(damage, critDamage, defense);
            }
        }

        public float DamageSum
        {
            get
            {
                return damage.Sum;
            }
        }

        public float CritDamageSum
        {
            get
            {
                return critDamage.Sum;
            }
        }

        public ElementVector Damage
        {
            get
            {
                return damage;
            }
        }

        public ElementVector CritDamage
        {
            get
            {
                return critDamage;
            }
        }

        public bool IsCrit
        {
            get
            {
                return critDamage.Sum > 0;
            }
        }

        public override string ToString()
        {
            return string.Format("[DamageEvent: srcEntity={0}, destEntity={1}, totalDamage={2}, damage={3}, critDamage={4}]",
                                  SrcEntity, DestEntity, TotalDamage, DamageSum, CritDamageSum);
        }
    }
}


