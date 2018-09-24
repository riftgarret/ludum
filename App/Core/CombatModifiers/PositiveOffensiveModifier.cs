namespace App.Core.CombatModifiers
{
    public class PositiveOffensiveModifier
    {
        public readonly PositiveOffensiveModifierType type;
        public readonly float modValue;

        public PositiveOffensiveModifier(PositiveOffensiveModifierType modType, float modValue)
        {
            this.type = modType;
            this.modValue = modValue;
        }
    } 
}

