using Redninja.BattleSystem.Entities;
using System;
using UnityEngine;

namespace Redninja.BattleSystem.Targeting.Conditions
{
    public abstract class TargetConditionSO : ScriptableObject
    {
        public abstract bool IsValidTarget(BattleEntity entity);
    } 
}

