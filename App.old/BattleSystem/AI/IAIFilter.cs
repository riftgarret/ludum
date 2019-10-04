using App.BattleSystem.Entity;
using System.Collections.Generic;

namespace App.BattleSystem.AI
{
    public interface IAIFilter
    {
        void FilterEntities(BattleEntity sourceEntity, HashSet<BattleEntity> entities);
    }

}

