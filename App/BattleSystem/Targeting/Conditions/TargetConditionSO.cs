using App.BattleSystem.Entity;
using System;
using UnityEngine;

namespace App.BattleSystem.Targeting.Conditions
{
    public abstract class TargetConditionSO : ScriptableObject
    {
        public abstract bool IsValidTarget(BattleEntity entity);
    } 
}

