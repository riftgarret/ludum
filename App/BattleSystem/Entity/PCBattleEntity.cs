using UnityEngine;
using System.Collections;

namespace App.BattleSystem.Entity
{
    public class PCBattleEntity : BattleEntity
    {

        // setup variables
        public PCBattleEntity(PCCharacter character) : base(character)
        {
        }

        public PCCharacter pcCharacter
        {
            get { return (PCCharacter)Character; }
        }

        public PCSkillSet SkillSet
        {
            get { return pcCharacter.skillSet; }
        }

        public override bool IsPC
        {
            get { return true; }
        }

        public override string ToString()
        {
            return string.Format("[PCBattleEntity: pcCharacter={0}, hp={1}/{2}]", pcCharacter, CurrentHP, MaxHP);
        }
    } 
}
