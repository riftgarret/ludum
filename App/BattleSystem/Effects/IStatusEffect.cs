using System;
using UnityEngine;

namespace App.BattleSystem.Effects
{
    public interface IStatusEffect
    {
        StatusEffectProperty Property
        {
            get;
        }

        StatusEffectType Type
        {
            get;
        }

        float Strength
        {
            get;
        }

        float Duration
        {
            get;
        }

        float Capacity
        {
            get;
        }
    } 
}
