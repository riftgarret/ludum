using Redninja.BattleSystem.Combat.CombatNode;
using Redninja.BattleSystem.Events;
using System;
using System.Collections.Generic;


namespace Redninja.BattleSystem.Combat.Operation.Result
{
    /// <summary>
    /// Calculate Crit chance results.
    /// </summary>
    public class CritChanceLogic : AccuracyEvasionLogic, ICombatLogic
    {

        public void Execute(EntityCombatResolver src, EntityCombatResolver dest)
        {
            CheckExecute();
            accuracy = src.CombatStats.critAccuracy;
            evasion = dest.CombatStats.critDefense;
            chanceToHit = accuracy / Math.Max(accuracy + evasion, 1);
            randomValue = UnityEngine.Random.Range(0f, 1f);
            Logger.d(this, this);
        }

        public void GenerateEvents(EntityCombatResolver src, EntityCombatResolver dest, Queue<IBattleEvent> combatEvents)
        {

        }

    } 
}
