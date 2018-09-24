namespace App.Core.CombatModifiers
{
    public class PhysicalOffensiveModifier
    {
        public readonly PhysicalOffensiveModifierType type;
        public readonly float modValue;

        public PhysicalOffensiveModifier(PhysicalOffensiveModifierType modType, float modValue)
        {
            this.type = modType;
            this.modValue = modValue;
        }
    } 
}

