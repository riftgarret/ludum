using UnityEngine;
using System.Collections;

namespace App.BattleSystem.Entity
{
    public class EnemyBattleEntity : BattleEntity
    {

        // setup variables
        public EnemyBattleEntity(EnemyCharacter character) : base(character)
        {

        }

        public EnemyCharacter enemyCharacter
        {
            get { return (EnemyCharacter)Character; }
        }

        public override bool IsPC
        {
            get
            {
                return false;
            }
        }

        public override string ToString()
        {
            return string.Format("[EnemyBattleEntity: enemyCharacter={0}, hp={1}/{2}]", enemyCharacter, CurrentHP, MaxHP);
        }

    } 
}
