namespace App.BattleSystem.Combat.CombatNode
{
    public enum CombatPropertyType
    {
        NONE,   // since cannot be null, lets make sure when we customize this we can select none    

        // OFFENSIVE
        TOTAL_DAMAGE,

        EVASION,
        ACCURACY,

        CRIT_EVASION,
        CRIT_ACCURACY,

        CRIT_POWER,
        CRIT_DEFENSE,

        // attributes
        STR,
        VIT,
        DEX,
        AGI,
        INT,
        WIS,
        LUCK,

        // magic
        MAGICAL_POWER,
        MAGICAL_DEFENSE,

        // physical
        PHYSICAL_POWER,
        PHYSICAL_DEFENSE,

        // elements
        SLASH_DEFENSE,
        CRUSH_DEFENSE,
        PIERCE_DEFENSE,
        DARK_DEFENSE,
        LIGHT_DEFENSE,
        WIND_DEFENSE,
        EARTH_DEFENSE,
        WATER_DEFENSE,
        FIRE_DEFENSE,

        SLASH_ATTACK,
        CRUSH_ATTACK,
        PIERCE_ATTACK,
        DARK_ATTACK,
        LIGHT_ATTACK,
        WIND_ATTACK,
        EARTH_ATTACK,
        WATER_ATTACK,
        FIRE_ATTACK,

        // weapon / skill scaling

        SCALE_STR,
        SCALE_VIT,
        SCALE_DEX,
        SCALE_AGI,
        SCALE_INT,
        SCALE_WIS,
        SCALE_LUCK,

        SLASH_MIN,
        CRUSH_MIN,
        PIERCE_MIN,
        DARK_MIN,
        LIGHT_MIN,
        WIND_MIN,
        EARTH_MIN,
        WATER_MIN,
        FIRE_MIN,

        SLASH_MAX,
        CRUSH_MAX,
        PIERCE_MAX,
        DARK_MAX,
        LIGHT_MAX,
        WIND_MAX,
        EARTH_MAX,
        WATER_MAX,
        FIRE_MAX,

        COUNT
    }

    public enum CombatFlag
    {
        NONE,
        ALWAYS_HIT,
        NEVER_HIT,
        ALWAYS_CRIT,
        NEVER_CRIT,
        // etc..
        COUNT
    }

}