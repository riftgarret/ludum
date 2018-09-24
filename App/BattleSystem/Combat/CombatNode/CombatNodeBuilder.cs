using App.Core.Skills;

namespace App.BattleSystem.Combat.CombatNode
{
    /// <summary>
    /// Build up a combat node with this builder attaching optional properties as needed.
    /// </summary>
    public class CombatNodeBuilder
    {
        private CombatNodeFactory mFactory;

        // if weapon is provided, weapon index is ignored	
        private int weaponIndex;
        private bool useEquipment;
        private bool useBuffs;

        private SkillCombatNode skillCombatNode;

        public CombatNodeBuilder(CombatNodeFactory factory)
        {
            this.mFactory = factory;
            this.weaponIndex = -1;
            this.useBuffs = true;
            this.useEquipment = true;
        }

        /// <summary>
        /// Sets the index of the weapon. This is set to 0 by default.
        /// </summary>
        /// <param name="weaponIndex">Weapon index.</param>
        public CombatNodeBuilder SetWeaponIndex(int weaponIndex)
        {
            this.weaponIndex = weaponIndex;
            return this;
        }


        public CombatNodeBuilder SetSkillCombatNode(CombatRound round)
        {
            skillCombatNode = new SkillCombatNode(round);
            return this;
        }

        public CombatNodeBuilder SetUseEquipment(bool allow)
        {
            useEquipment = allow;
            return this;
        }

        public CombatNodeBuilder SetUseBuffs(bool allow)
        {
            useBuffs = allow;
            return this;
        }

        public CompositeCombatNode Build()
        {
            // build composite for character
            CompositeCombatNode rootNode = new CompositeCombatNode();
            // child node
            rootNode.AddNode(mFactory.CreateCharacterNode());

            // use all equipment
            if (useEquipment)
            {
                // weaopns
                for (int i = 0; i < mFactory.Entity.EquipedWeapons.Length; i++)
                {
                    rootNode.AddNode(mFactory.CreateWeaponConfigNode(i, i == weaponIndex));
                }

                // armor
                for (int i = 0; i < mFactory.Entity.EquipedArmor.Length; i++)
                {
                    rootNode.AddNode(mFactory.CreateArmorNode(i));
                }
            }

            if (useBuffs)
            {
                rootNode.AddNode(mFactory.CreateStatusEffectNodes());
            }


            if (skillCombatNode != null)
            {
                rootNode.AddNode(skillCombatNode);
            }

            return rootNode;
        }

        /// <summary>
        /// Build the combat resolver directly
        /// </summary>
        /// <returns>The resolver.</returns>
        public EntityCombatResolver BuildResolver()
        {
            return new EntityCombatResolver(mFactory.Entity, Build());
        }
    } 
}
