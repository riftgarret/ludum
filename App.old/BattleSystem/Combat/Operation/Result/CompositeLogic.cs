using App.BattleSystem.Combat.CombatNode;
using App.BattleSystem.Events;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace App.BattleSystem.Combat.Operation.Result
{
    public class CompositeLogic : BaseCombatLogic, ICombatLogic
    {
        private List<ICombatLogic> compositeResult;

        public CompositeLogic()
        {
            compositeResult = new List<ICombatLogic>();
        }

        public void Add(ICombatLogic result)
        {
            compositeResult.Add(result);
        }

        public void Execute(EntityCombatResolver src, EntityCombatResolver dest)
        {
            CheckExecute();
            foreach (ICombatLogic result in compositeResult)
            {
                result.Execute(src, dest);
            }
        }

        public void GenerateEvents(EntityCombatResolver src, EntityCombatResolver dest, Queue<IBattleEvent> combatEvents)
        {
            foreach (ICombatLogic result in compositeResult)
            {
                result.GenerateEvents(src, dest, combatEvents);
            }
        }
    } 
}
