using App.BattleSystem.Entity;
using App.BattleSystem.Events;
using App.Core.Characters;
using System;
using System.Collections.Generic;

namespace App.BattleSystem.Combat.Operation
{
    public class PCMoveOperation : ICombatOperation
    {        
        public EntityPosition SrcPosition { get; }
        public EntityPosition DestPosition { get; }

        private PCBattleEntity SrcEntity { get; }

        private MoveEvent result;

        public PCMoveOperation(PCBattleEntity srcEntity, EntityPosition destPosition)
        {
            this.SrcEntity = srcEntity;
            this.SrcPosition = srcEntity.Position;
            this.DestPosition = destPosition;
        }

        public void Execute()
        {
            // need to animate this?
            SrcEntity.MovePosition(DestPosition.Row, DestPosition.Column);
            result = new MoveEvent(SrcEntity, DestPosition, DestPosition);
        }

        public void GenerateEvents(Queue<IBattleEvent> queue)
        {
            if (result != null)
            {
                queue.Enqueue(result);
            }
        }        

        public BattleEventType eventType
        {
            get
            {
                return BattleEventType.MOVE;
            }
        }
    } 
}


