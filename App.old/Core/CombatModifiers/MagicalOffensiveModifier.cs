namespace App.Core.CombatModifiers
{
    public class MagicalOffensiveModifier
    {
        public readonly MagicalOffensiveModifierType type;
        public readonly float modValue;

        public MagicalOffensiveModifier(MagicalOffensiveModifierType modType, float modValue)
        {
            this.type = modType;
            this.modValue = modValue;
        }
    } 
}

