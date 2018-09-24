using UnityEngine;
using System.Collections;
using App.Core.Skills;

namespace App.Core.Characters
{
    public class HotKey
    {
        public ICombatSkill skill
        {
            get;
            set;
        }

        public HotKey() { }
        public HotKey(ICombatSkill skill)
        {
            this.skill = skill;
        }

    } 
}
