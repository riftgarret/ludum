using System;


namespace App.BattleSystem.Effects
{
    public enum StatusEffectType
    {
        POSITIVE,
        NEGATIVE,
        NULLIFY
    }

    public enum StatusEffectProperty
    {
        NONE,

        // stat effects
        STR_MOD,
        VIT_MOD,
        AGI_MOD,
        DEX_MOD,
        INT_MOD,
        WIS_MOD,
        LUCK_MOD,

        // offensive effects
        CRIT_CHANCE,
        CRIT_POWER,
        SPELL_FAILURE,
        SPELL_DMG,
        HIT_CHANCE,
        PHYSICAL_DMG,


        // defensive effects
        SPELL_RESIST,
        HEALING_REDUCTION,
        CRIT_EVASION,

        // turn / speed effects 
        SPEED,
        FATIGUE_COST,
        LOSE_TURN,

        HP_REGEN






    } 
}

