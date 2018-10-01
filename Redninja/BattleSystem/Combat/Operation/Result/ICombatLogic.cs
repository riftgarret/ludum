using Redninja.BattleSystem.Combat.CombatNode;
using Redninja.BattleSystem.Events;
using System.Collections.Generic;

namespace Redninja.BattleSystem.Combat.Operation.Result
{
    /// <summary>
    /// Combat logic represents a logic node in the calculation of scoring a hit and resolving that damage.
    /// The node is to preserve that event so that we may present this node to the user for fine grain 
    /// information about their success chances.
    /// </summary>
    public interface ICombatLogic
    {
        void Execute(EntityCombatResolver src, EntityCombatResolver dest);
        void GenerateEvents(EntityCombatResolver src, EntityCombatResolver dest, Queue<IBattleEvent> combatEvents);
        bool IsExecuted { get; }
    }

}