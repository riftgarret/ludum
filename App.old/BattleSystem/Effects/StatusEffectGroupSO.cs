using System;
using UnityEngine;

namespace App.BattleSystem.Effects
{
    /// <summary>
    /// StatusEffectGroup, there may be many different instances that cause the same effect. We want to make sure we 
    /// associate each instant that shares a group with the same object for sake of asssigning text / graphic information
    /// as well as applying multiple buffs of the same type.
    /// 
    /// </summary>
    public class StatusEffectGroupSO : ScriptableObject
    {
        // IF THIS IS A STAT GROUP, LETS SET IT HERE
        [SerializeField]
        private StatusEffectProperty combatProperty = StatusEffectProperty.NONE;
        // TODO special case status effect group: 
        // ie: poison, haste, onEvent()

        // GUI information 

        // icons
        [SerializeField]
        private StatusEffectGroupGUIProperty negativeGUIProperty = null;

        [SerializeField]
        private StatusEffectGroupGUIProperty positiveGUIProperty = null;



        // accessors
        public StatusEffectProperty StatusEffectProperty
        {
            get { return combatProperty; }
        }

        public StatusEffectGroupGUIProperty NegativeGUIProperty
        {
            get
            {
                return this.negativeGUIProperty;
            }
        }

        public StatusEffectGroupGUIProperty PositiveGUIProperty
        {
            get
            {
                return this.positiveGUIProperty;
            }
        }

    } 
}


