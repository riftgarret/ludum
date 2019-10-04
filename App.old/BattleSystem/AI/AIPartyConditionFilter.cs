using App.BattleSystem.Entity;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.BattleSystem.AI
{
    /// <summary>
    /// AI party condition filter. This is under the impression we've already filtered our targets to group
    /// </summary>
    public class AIPartyConditionFilter : IAIFilter
    {
        private AISkillRule.PartyCondition partyCondition;
        private int partyCount;

        public AIPartyConditionFilter(AISkillRule.PartyCondition partyCondition, int valueCount)
        {
            this.partyCondition = partyCondition;
            partyCount = valueCount;
        }

        public void FilterEntities(BattleEntity sourceEntity, HashSet<BattleEntity> entities)
        {
            // leftover targets should already be in the party, we should just count and filter out the rest if its needed
            switch (partyCondition)
            {
                case AISkillRule.PartyCondition.PARTY_COUNT_GT:
                    if (entities.Count <= partyCount)
                    {
                        entities.Clear();
                    }
                    break;
                case AISkillRule.PartyCondition.PARTY_COUNT_LT:
                    if (entities.Count >= partyCount)
                    {
                        entities.Clear();
                    }
                    break;
            }
        }
    } 
}
