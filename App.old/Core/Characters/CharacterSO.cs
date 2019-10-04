using App.Core.Equipment;
using App.Core.Stats;
using App.Util;

namespace App.Core.Characters
{
    public class CharacterSO : SanitySO
    {
        // character name
        public string displayName;

        // stats
        public AttributeVector attributes = new AttributeVector();
        public CombatStatsVector combatStats = new CombatStatsVector();
        public ElementVector elementDefense = new ElementVector();
        public ElementVector elementAttack = new ElementVector();

        // level and class
        public int level;
        public CharacterClassSO charClass;

        // equipment
        public WeaponSO[] weapons;
        public ArmorSO[] armors;


        protected override void SanityCheck()
        {
            LogEmptyArray("weapons", weapons);
            LogEmptyArray("armors", armors);
        }

        public Character GenerateCharacter()
        {
            return null; // TODO Create BUILDER class for Character using Params
        }
    } 
}

