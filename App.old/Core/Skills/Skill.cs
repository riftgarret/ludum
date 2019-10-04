using System;
using UnityEngine;

namespace App.Core.Skills
{
    [Serializable]
    public abstract class Skill
    {
        public Skill(SkillSO config, int level)
        {
            mLevel = level;
            mSkillConfig = config;
        }

        [SerializeField]
        protected int mLevel;

        public int level
        {
            get { return mLevel; }
            set { mLevel = value; }
        }

        [SerializeField]
        protected SkillSO mSkillConfig;

        public SkillSO skillConfig
        {
            get { return mSkillConfig; }
            set { mSkillConfig = value; }
        }
    }

}