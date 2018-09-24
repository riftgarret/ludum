using App.BattleSystem.Entity;
using System;
namespace App.BattleSystem.Events
{
    public class MoveEvent : IBattleEvent
    {
        PCCharacter.RowPosition srcRow;
        PCCharacter.RowPosition destRow;

        PCBattleEntity srcEntity;

        public MoveEvent(PCBattleEntity srcEntity, PCCharacter.RowPosition srcRow, PCCharacter.RowPosition destRow)
        {
            this.srcEntity = srcEntity;
            this.srcRow = srcRow;
            this.destRow = destRow;
        }

        public BattleEntity SrcEntity
        {
            get
            {
                return srcEntity;
            }
        }

        public BattleEventType EventType
        {
            get
            {
                return BattleEventType.MOVE;
            }
        }

        public PCCharacter.RowPosition SrcRow
        {
            get { return srcRow; }
        }

        public PCCharacter.RowPosition DestRow
        {
            get { return destRow; }
        }

        public override string ToString()
        {
            return string.Format("[MoveEvent: mSrcRow={0}, mDestRow={1}, mSrcEntity={2}]", srcRow, destRow, srcEntity);
        }
    } 
}


