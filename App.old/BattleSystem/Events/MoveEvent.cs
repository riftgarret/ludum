using App.BattleSystem.Entity;
using App.Core.Characters;
using System;
namespace App.BattleSystem.Events
{
    public class MoveEvent : IBattleEvent
    {
        public EntityPosition SrcPosition { get; }
        public EntityPosition DestPosition { get; }

        public BattleEntity SrcEntity { get; }

        public MoveEvent(BattleEntity srcEntity, EntityPosition src, EntityPosition dest)
        {
            this.SrcEntity = srcEntity;
            this.SrcPosition = src;
            this.DestPosition = dest;
        }

        public BattleEventType EventType
        {
            get
            {
                return BattleEventType.MOVE;
            }
        }   

        public override string ToString()
        {
            return $"[MoveEvent: SrcPosition={SrcPosition} DestPosition={DestPosition}, SrcEntity={SrcEntity}]";
        }
    } 
}


