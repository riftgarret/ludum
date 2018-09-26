using App.BattleSystem.AI;

namespace App.Core.Characters
{
    public class EnemyCharacter : Character
    {

        public EnemyCharacter() { }        

        public EnemySkillSetSO EnemySkillSetSO { private set; get; }

        public EnemyCharacter(EnemyCharacterSO config) : base(config)
        {
            EnemySkillSetSO = config.skillRules;            
        }

    } 
}
