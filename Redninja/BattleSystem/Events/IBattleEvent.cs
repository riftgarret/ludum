using Redninja.BattleSystem.Entity;
using System;
namespace Redninja.BattleSystem.Events
{
    /// <summary>
    /// Interface for Battle Events.
    /// </summary>
    public interface IBattleEvent
    {            
        BattleEntity SrcEntity
        {
            get;
        }

        BattleEventType EventType
        {
            get;
        }
    } 
}

