using System;
using UnityEngine;

namespace App.BattleSystem.Effects
{
    [Serializable]
    public class StatusEffectRule
    {
        [SerializeField]
        private StatusEffectRuleTarget targetRule;

        [SerializeField]
        private StatusEffectRuleHitPredicate hitPredicate;



        /// <summary>
        /// Gets the effect.
        /// </summary>
        /// <value>The effect.</value>
        public IStatusEffectRunner Effect
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the rule.
        /// </summary>
        /// <value>The rule.</value>
        public StatusEffectRuleHitPredicate Rule
        {
            get;
            private set;
        }



        public enum StatusEffectRuleTarget
        {
            TARGET,
            TARGET_ROW,
            TARGET_ALL,
            SELF,
            SELF_ROW,
            SELF_ALL
        }

        // Rules to determine when the effect should be proccessed
        public enum StatusEffectRuleHitPredicate
        {
            ALWAYS,
            ON_HIT,
            ON_MISS
        }
    } 
}