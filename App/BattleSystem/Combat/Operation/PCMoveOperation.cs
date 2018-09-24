using App.BattleSystem.Entity;
using App.BattleSystem.Events;
using App.Core.Characters;
using System;
namespace App.BattleSystem.Combat.Operation
{
    public class PCMoveOperation
    {
        // TODO flush out
        PCCharacter.RowPosition mSrcRow;
        PCCharacter.RowPosition mDestRow;

        PCBattleEntity mSrcEntity;

        public PCMoveOperation(PCBattleEntity srcEntity, PCCharacter.RowPosition destRow)
        {
            this.mSrcEntity = srcEntity;
            this.mSrcRow = srcEntity.pcCharacter.rowPosition;
            this.mDestRow = destRow;
        }

        public IBattleEvent Execute()
        {
            // need to animate this?
            mSrcEntity.pcCharacter.rowPosition = mDestRow;
            return new MoveEvent(mSrcEntity, mSrcRow, mDestRow);
        }

        public BattleEntity srcEntity
        {
            get
            {
                return srcEntity;
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


