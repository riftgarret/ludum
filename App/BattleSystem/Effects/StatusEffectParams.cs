using System;
using UnityEngine;

namespace App.BattleSystem.Effects
{
    [Serializable]
    public class StatusEffectParams
    {
        [SerializeField]
        private StatusEffectProperty property;

        [SerializeField]
        private StatusEffectType type;

        [SerializeField]
        private float strength;

        [SerializeField]
        private float duration;


        public StatusEffectProperty Property
        {
            get
            {
                return this.property;
            }
        }

        public StatusEffectType Type
        {
            get
            {
                return this.type;
            }
        }

        public float Strength
        {
            get { return strength; }
        }

        public float Duration
        {
            get { return duration; }
        }
    } 
}
