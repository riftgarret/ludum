using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Targeting
{
    /// <summary>
    /// Selected Target should represent the required meta data for the TargetType.
    /// </summary>
    public class SelectedTarget
    {
        public TargetType TargetType => TargetEntity == null? TargetType.Positional : TargetType.Target;

        public IBattleEntity TargetEntity { get; }        

        public int AnchoredPositionRow { get; }

        public int AnchoredPositionColumn { get; }

        public int Team { get; } 

        private SelectedTarget(         
            IBattleEntity targetEntity,             
            int anchoredPositionRow, 
            int anchoredPositionColumn, 
            int teamSide)
        {            
            TargetEntity = targetEntity;            
            AnchoredPositionRow = anchoredPositionRow;
            AnchoredPositionColumn = anchoredPositionColumn;
            Team = teamSide;
        }

        public static SelectedTarget CreateEntityTarget(IBattleEntity target)
            => new SelectedTarget(target, -1, -1, -1);

        public static SelectedTarget CreatePositionTarget(int row, int col, int team)
            => new SelectedTarget(null, row, col, team);
    }
}
