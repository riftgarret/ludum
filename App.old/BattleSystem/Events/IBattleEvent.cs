using App.BattleSystem.Entity;
using System;
namespace App.BattleSystem.Events
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

