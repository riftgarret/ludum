using UnityEngine;
using System.Collections;
using App.Core.Skills;

namespace App.Core.Characters
{
    [System.Serializable]
    public class PCSkillSet
    {
        public HotKey[] hotKeys;

        public ICombatSkill[] skills;
    }

}