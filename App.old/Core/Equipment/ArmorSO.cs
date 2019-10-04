using App.BattleSystem.Combat.CombatNode;
using App.Core.Stats;

namespace App.Core.Equipment
{

    public class ArmorSO : EquipmentSO, IArmor
    {

        public ArmorPosition armorPosition = ArmorPosition.TORSO;
        public ArmorType armorType = ArmorType.LIGHT;
        public ElementVector resists = new ElementVector();
        public CombatProperty[] additionalCombatProperties;

        protected override void SanityCheck()
        {
            base.SanityCheck();

            if (additionalCombatProperties == null)
            {
                LogNull("combatProperties");
            }

            if (resists == null)
            {
                LogNull("resists");
            }
        }

        public ArmorPosition ArmorPosition
        {
            get
            {
                return armorPosition;
            }
        }

        public ArmorType ArmorType
        {
            get
            {
                return armorType;
            }
        }
    } 
}
 