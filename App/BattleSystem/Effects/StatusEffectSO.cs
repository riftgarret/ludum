using System;
using UnityEngine;

namespace App.BattleSystem.Effects
{
    public class StatusEffectSO : ScriptableObject
    {
        [SerializeField]
        private StatusEffectParams statusEffect;

        public StatusEffectParams StatusEffect
        {
            get { return statusEffect; }
        }
    } 
}
