using App.Core.CombatModifiers;
using App.Core.Equipment;
using System;

namespace App.BattleSystem.Entity
{
    public interface IBattleEntityWeapon
    {
        float baseDamage
        {
            get;
        }

        PhysicalOffensiveModifier[] offensiveModifiers
        {
            get; // todo move this to physical weapon mod
        }

        StatModifier[] statModifiers
        {
            get;
        }

        DamageType dmgType
        {
            get;
        }

        WeaponType weaponType
        {
            get;
        }
    } 
}

