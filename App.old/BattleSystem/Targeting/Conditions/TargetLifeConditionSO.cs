using App.BattleSystem.Entity;
using System;

namespace App.BattleSystem.Targeting.Conditions
{
    public class TargetLifeConditionSO : TargetConditionSO
    {
        public bool isAlive = true;

        public override bool IsValidTarget(BattleEntity entity)
        {
            if (isAlive)
            {
                return entity.CurrentHP > 0;
            }
            else
            {
                return entity.CurrentHP <= 0;
            }
        }
    } 
}


