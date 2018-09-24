using App.BattleSystem.AI;

namespace App.Core.Characters
{
    public class EnemyCharacter : Character
    {

        public EnemyCharacter() { }

        private AISkillResolver skillResolver;
        public AISkillResolver SkillResolver
        {
            get { return skillResolver; }
        }

        public EnemyCharacter(EnemyCharacterSO config) : base(config)
        {
            skillResolver = new AISkillResolver(config.skillRules);
        }

    } 
}
