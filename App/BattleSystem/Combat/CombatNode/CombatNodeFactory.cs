namespace App.BattleSystem.Combat.CombatNode
{
    /// <summary>
    /// Combnat noade factory to generate nodes with specific paramters. 
    /// </summary>
    public class CombatNodeFactory
    {
        private BattleEntity entity;

        public BattleEntity Entity
        {
            get { return entity; }
        }

        private CharacterCombatNode mCachedCharacterNode;

        public CombatNodeFactory(BattleEntity entity)
        {
            this.entity = entity;
        }

        public ICombatNode CreateWeaponConfigNode(int equipedWeaponIndex, bool isActiveWeapon)
        {
            IWeapon weapon = entity.equipedWeapons[equipedWeaponIndex];
            return CreateWeaponConfigNode(weapon, isActiveWeapon);
        }

        public ICombatNode CreateWeaponConfigNode(IWeapon config, bool isActiveWeapon)
        {
            return new WeaponCombatNode(config, isActiveWeapon);
        }

        public ICombatNode CreateArmorNode(int armorPosition)
        {
            IArmor armor = entity.character.equipedArmor[armorPosition];
            return new ArmorCombatNode(armor);
        }

        public ICombatNode CreateCharacterNode()
        {
            if (mCachedCharacterNode == null)
            {
                mCachedCharacterNode = new CharacterCombatNode(entity.character);
            }
            return mCachedCharacterNode;
        }

        public ICombatNode CreateStatusEffectNodes()
        {
            // map status effects to buff properties
            ConfigurableCombatNode configNode = new ConfigurableCombatNode();
            foreach (StatusEffectClient.StatusEffectSummary summary in entity.statusEffectClient.Summaries)
            {
                configNode.LoadStatusEffectProperty(summary.property, summary.netValue);
            }
            return configNode;
        }
    } 
}

