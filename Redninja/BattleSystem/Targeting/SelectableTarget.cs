using Redninja.BattleSystem.Entities;
using System.Collections.Generic;

namespace Redninja.BattleSystem.Targeting
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
