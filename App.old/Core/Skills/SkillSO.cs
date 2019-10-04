using App.Util;
using System;
using System.Collections;
using UnityEngine;

namespace App.Core.Skills
{
    [Serializable]
    public class SkillSO : SanitySO, ISkill
    {
        public string displayName;
        public Texture2D icon;

        public string DisplayName
        {
            get
            {
                return displayName;
            }
        }

        protected override void SanityCheck()
        {
            if (string.IsNullOrEmpty(displayName))
            {
                LogNull("displayName");
            }

            if (icon == null)
            {
                LogNull("icon");
            }
        }
    } 
}

