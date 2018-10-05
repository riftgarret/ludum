using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.BattleSystem.Targeting
{
    /// <summary>
    /// Selected Target should represent the required meta data for the TargetType.
    /// </summary>
    public class SelectedTarget
    {
        public bool IsPosition => TargetEntity == null;

        public IBattleEntity TargetEntity { get; }        

        public int AnchoredPositionRow { get; }

        public int AnchoredPositionColumn { get; }

        public int TeamSide { get; } // TODO rice comment 

        private SelectedTarget(         
            IBattleEntity targetEntity,             
            int anchoredPositionRow, 
            int anchoredPositionColumn, 
            int teamSide)
        {            
            TargetEntity = targetEntity;            
            AnchoredPositionRow = anchoredPositionRow;
            AnchoredPositionColumn = anchoredPositionColumn;
            TeamSide = teamSide;
        }

        public static SelectedTarget CreateEntityTarget(IBattleEntity target)
            => new SelectedTarget(target, -1, -1, -1);

        public static SelectedTarget CreatePositionTarget(int row, int col, int teamSide)
            => new SelectedTarget(null, row, col, teamSide);
    }
}
