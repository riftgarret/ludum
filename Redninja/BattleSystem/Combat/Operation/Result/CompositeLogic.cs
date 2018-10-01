using Redninja.BattleSystem.Combat.CombatNode;
using Redninja.BattleSystem.Events;
using System;
using System.Collections.Generic;


namespace Redninja.BattleSystem.Combat.Operation.Result
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
