using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[Serializable]
public class AttributeVector {
    public float strength;
    public float vitality;
    public float dexerity;
    public float agility;
    public float inteligence;
    public float wisdom;
    public float luck;

    public static AttributeVector operator *(AttributeVector a1, AttributeVector a2) {
        AttributeVector result = new AttributeVector();
        result.strength = a1.strength * a2.strength;
        result.vitality = a1.vitality * a2.vitality;
        result.dexerity = a1.dexerity * a2.dexerity;
        result.agility = a1.agility * a2.agility;
        result.wisdom = a1.wisdom * a2.wisdom;
        result.inteligence = a1.inteligence * a2.inteligence;
        result.luck = a1.luck * a2.luck;
        return result;
    }

    public float Sum {
        get {
            float result = 0;
            result += strength;
            result += vitality;
            result += dexerity;
            result += agility;
            result += inteligence;
            result += wisdom;
            result += luck;
            return result;
        }
    }
}

