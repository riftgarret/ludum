using App.BattleSystem.AI;
using System;
using UnityEngine;

namespace App.Core.Characters
{
    public class EnemySkillSetSO : ScriptableObject
    {
        [SerializeField]
        private AISkillRule[] aiSkillRules = new AISkillRule[5];
        public AISkillRule[] skillRules
        {
            get { return aiSkillRules; }
        }
    } 
}
