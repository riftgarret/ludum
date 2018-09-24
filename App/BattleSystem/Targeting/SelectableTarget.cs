using App.BattleSystem.Entity;
using System.Collections.Generic;

namespace App.BattleSystem.Targeting
{
    public class SelectableTarget
    {
        public readonly string targetName;
        public readonly List<BattleEntity> entities;
        public readonly ResolvedTargetEnum resolvedTargetType;

        public SelectableTarget(string targetName, List<BattleEntity> entities, ResolvedTargetEnum resolvedTargetType)
        {
            this.targetName = targetName;
            this.entities = entities;
            this.resolvedTargetType = resolvedTargetType;
        }
    }

}
