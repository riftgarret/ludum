using Redninja.BattleSystem.Events;
using System;
using System.Collections.Generic;

namespace Redninja.BattleSystem.Combat.Operation
{
    /// <summary>
    /// A Combat operation that execute and generate events.
    /// </summary>
    public interface ICombatOperation
    {
        void Execute();
        void GenerateEvents(Queue<IBattleEvent> queue);
    } 
}

