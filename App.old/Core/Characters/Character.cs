using App.Core.Equipment;
using App.Core.Stats;

namespace App.Core.Characters
{
    /// <summary>
    /// PC character. Customizable, levelable, etc
    /// </summary>
    [System.Serializable]
    public abstract class Character
    {
        // current stats
        public float curHP;
        public float curResource;

        //
        public float MaxHP
        {
            get { return charClass.CalculateHitpoints(this); }
        }

        // TODO maybe add this later
        public float MaxResource
        {
            get { return 100f; }
        }

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
        private WeaponConfig mWeaponConfig;
        private ArmorConfig mArmorConfig;

        public IWeapon[] EquipedWeapons
        {
            get { return mWeaponConfig.equipedWeapons; }
        }

        public IArmor[] EquipedArmor
        {
            get { return mArmorConfig.equipedArmor; }
        }
        // armor, accessory

        // empty constructor
        public Character() { }

        // used for cloning
        public static Character CreateFromConfig(CharacterSO config)
        {
            if (config is EnemyCharacterSO)
            {
                return new EnemyCharacter((EnemyCharacterSO)config);
            }
            else
            {
                return new PCCharacter((PCCharacterSO)config);
            }
        }

        protected Character(CharacterSO other)
        {
            // name
            displayName = other.displayName;

            // stats
            attributes = other.attributes;
            elementAttack = other.elementAttack;
            elementDefense = other.elementDefense;
            combatStats = other.combatStats;


            // level and class
            level = other.level;
            charClass = other.charClass;

            // create armor and weapon configs from class rules
            mWeaponConfig = charClass.CreateWeaponConfig();
            mArmorConfig = charClass.CreateArmorConfig();


            for (int i = 0; i < other.weapons.Length; i++)
            {
                mWeaponConfig.EquipWeapon(other.weapons[i], i);
            }

            // TODO, this should be tightly coupled with SO implementation
            //		for (int i=0; i < other.weapons.Length; i++) {
            //			mArmorConfig.EquipArmor(other.armors[i], i);
            //		}

            curHP = MaxHP;
        }

        public override string ToString()
        {
            return string.Format("[Character: displayName={0}]", displayName);
        }

    } 
}
