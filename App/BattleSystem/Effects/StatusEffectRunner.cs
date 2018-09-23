
namespace App.BattleSystem.Effects
{
    public class StatusEffectRunner : IStatusEffectRunner
    {
        private BattleEntity sourceEntity;
        private float timeCounter;
        private IStatusEffect statusEffect;

        public StatusEffectRunner(BattleEntity sourceEntity, IStatusEffect statusEffect)
        {
            this.sourceEntity = sourceEntity;
            this.statusEffect = statusEffect;
        }

        public BattleEntity SourceEntity
        {
            get
            {
                return sourceEntity;
            }
        }

        public StatusEffectProperty Property
        {
            get
            {
                return statusEffect.Property;
            }
        }

        public StatusEffectType Type
        {
            get
            {
                return statusEffect.Type;
            }
        }

        public float Strength
        {
            get
            {
                return statusEffect.Strength;
            }
        }

        public float Duration
        {
            get
            {
                return statusEffect.Duration;
            }
        }

        public float Capacity
        {
            get
            {
                return statusEffect.Capacity;
            }
        }

        public void IncrementDurationTime(float timeDelta)
        {
            timeCounter += timeDelta;
        }


        public float TotalDurationLength
        {
            get
            {
                return statusEffect.Duration;
            }
        }

        public float CurrentDurationLength
        {
            get
            {
                return timeCounter;
            }
        }

        public bool IsExpired
        {
            get
            {
                return timeCounter >= statusEffect.Duration;
            }
        }
    }
}


