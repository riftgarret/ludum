using App.BattleSystem.Entity;
using System;
using UnityEngine;

namespace App.BattleSystem.Effects
{
    public class StatusEffect : IStatusEffect
    {
        public StatusEffect(StatusEffectParams p, BattleEntity source)
        {
            sourceEntity = source;
            paramz = p;
        }

        private BattleEntity sourceEntity;
        private StatusEffectParams paramz;

        public StatusEffectProperty Property
        {
            get
            {
                return this.paramz.Property;
            }
        }

        public StatusEffectType Type
        {
            get
            {
                return this.paramz.Type;
            }
        }

        public float Strength
        {
            get { return paramz.Strength; }
        }

        public float Duration
        {
            get { return paramz.Duration; }
        }

        public float Capacity
        {
            get { return 10; } // TODO mitigate from character
        }
    } 
}
