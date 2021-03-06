using System;

namespace App.Core.Characters
{
    [System.Serializable]
    public class PCCharacter : Character
    {                
        public PCSkillSet skillSet;

        public PCCharacter()
        {
            /*		hotkeys = new HotKey[10];
                    for(int i=0; i < hotkeys.Length; i++) {
                        hotkeys[i] = new HotKey(new SkillAttack());
                    }
            */
        }

        public PCCharacter(PCCharacterSO config) : base(config)
        {
            skillSet = new PCSkillSet();
            config.skillsetConfig.InitSkills(skillSet);
        }
    } 
}
