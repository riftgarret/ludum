namespace App.Core.CombatModifiers
{
    public class GeneralOffensiveModifier
    {
        public readonly GeneralOffensiveModifierType type;
        public readonly float modValue;

        public GeneralOffensiveModifier(GeneralOffensiveModifierType modType, float modValue)
        {
            this.type = modType;
            this.modValue = modValue;
        }
    } 
}

