using App.BattleSystem.Entity;
using System;
using System.Collections.Generic;

namespace App.BattleSystem.AI
{
    public class AIAcceptAllFilter : IAIFilter
    {
        public void FilterEntities(BattleEntity sourceEntity, HashSet<BattleEntity> entities)
        {
            // do nothing
        }
    } 
}


