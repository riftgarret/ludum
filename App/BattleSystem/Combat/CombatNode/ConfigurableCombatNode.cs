//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using UnityEngine;

public class ConfigurableCombatNode : ICombatNode
{
	protected float [] mPropertyAdd;
	protected float [] mPropertyMultiply;
    protected bool[] mFlags;

	public ConfigurableCombatNode ()
	{
		mPropertyAdd = new float[(int)CombatPropertyType.COUNT];
		mPropertyMultiply = new float[(int)CombatPropertyType.COUNT];
        mFlags = new bool[(int)CombatFlag.COUNT];
	}

	public float GetProperty (CombatPropertyType property)
	{
		return mPropertyAdd[(int)property];
	}

	public float GetPropertyScalar (CombatPropertyType property)
	{
		return mPropertyMultiply[(int)property];
	}

    public bool HasFlag(CombatFlag flag) {
        return mFlags[(int)flag];
    }

	/// <summary>
	/// Load the specified combatProperties.
	/// </summary>
	/// <param name="combatProperties">Combat properties.</param>
	public void Load(CombatProperty [] combatProperties) {
		if(combatProperties == null) {
			Debug.Log("Failed to load combatProperties, null: " + this);
			return;
		}

		foreach(CombatProperty property in combatProperties) {
			this.mPropertyAdd[(int)property.propertyType] = property.add;
			this.mPropertyMultiply[(int)property.propertyType] = property.scalar;
		}
	}

    public void LoadAttribute(AttributeVector attributes) {
        if (attributes != null) {
            mPropertyAdd[(int)CombatPropertyType.STR] = attributes.strength;
            mPropertyAdd[(int)CombatPropertyType.VIT] = attributes.vitality;
            mPropertyAdd[(int)CombatPropertyType.DEX] = attributes.dexerity;
            mPropertyAdd[(int)CombatPropertyType.AGI] = attributes.agility;            
            mPropertyAdd[(int)CombatPropertyType.WIS] = attributes.wisdom;
            mPropertyAdd[(int)CombatPropertyType.INT] = attributes.inteligence;
            mPropertyAdd[(int)CombatPropertyType.LUCK] = attributes.luck;            
        }
    }

    public void LoadAttributeScalar(AttributeVector attributes) {
        if (attributes != null) {
            mPropertyMultiply[(int)CombatPropertyType.STR] = attributes.strength;
            mPropertyMultiply[(int)CombatPropertyType.VIT] = attributes.vitality;
            mPropertyMultiply[(int)CombatPropertyType.DEX] = attributes.dexerity;
            mPropertyMultiply[(int)CombatPropertyType.AGI] = attributes.agility;
            mPropertyMultiply[(int)CombatPropertyType.WIS] = attributes.wisdom;
            mPropertyMultiply[(int)CombatPropertyType.INT] = attributes.inteligence;
            mPropertyMultiply[(int)CombatPropertyType.LUCK] = attributes.luck;
        }
    }

    public void LoadElementAttack(ElementVector elements) {
        if (elements != null) {
            mPropertyAdd[(int)CombatPropertyType.CRUSH_ATTACK] = elements.crush;
            mPropertyAdd[(int)CombatPropertyType.SLASH_ATTACK] = elements.slash;
            mPropertyAdd[(int)CombatPropertyType.PIERCE_ATTACK] = elements.pierce;
            mPropertyAdd[(int)CombatPropertyType.LIGHT_ATTACK] = elements.light;
            mPropertyAdd[(int)CombatPropertyType.DARK_ATTACK] = elements.dark;
            mPropertyAdd[(int)CombatPropertyType.EARTH_ATTACK] = elements.earth;
            mPropertyAdd[(int)CombatPropertyType.WIND_ATTACK] = elements.wind;
            mPropertyAdd[(int)CombatPropertyType.FIRE_ATTACK] = elements.fire;
            mPropertyAdd[(int)CombatPropertyType.WATER_ATTACK] = elements.water;
        }
    }

    public void LoadElementAttackScalar(ElementVector elements) {
        if (elements != null) {
            mPropertyMultiply[(int)CombatPropertyType.CRUSH_ATTACK] = elements.crush;
            mPropertyMultiply[(int)CombatPropertyType.SLASH_ATTACK] = elements.slash;
            mPropertyMultiply[(int)CombatPropertyType.PIERCE_ATTACK] = elements.pierce;
            mPropertyMultiply[(int)CombatPropertyType.LIGHT_ATTACK] = elements.light;
            mPropertyMultiply[(int)CombatPropertyType.DARK_ATTACK] = elements.dark;
            mPropertyMultiply[(int)CombatPropertyType.EARTH_ATTACK] = elements.earth;
            mPropertyMultiply[(int)CombatPropertyType.WIND_ATTACK] = elements.wind;
            mPropertyMultiply[(int)CombatPropertyType.FIRE_ATTACK] = elements.fire;
            mPropertyMultiply[(int)CombatPropertyType.WATER_ATTACK] = elements.water;
        }
    }

	public void LoadElementDefense(ElementVector elements) {
		if(elements != null) {
			mPropertyAdd[(int)CombatPropertyType.CRUSH_DEFENSE] = elements.crush;
			mPropertyAdd[(int)CombatPropertyType.SLASH_DEFENSE] = elements.slash;
			mPropertyAdd[(int)CombatPropertyType.PIERCE_DEFENSE] = elements.pierce;
			mPropertyAdd[(int)CombatPropertyType.LIGHT_DEFENSE] = elements.light;
			mPropertyAdd[(int)CombatPropertyType.DARK_DEFENSE] = elements.dark;
			mPropertyAdd[(int)CombatPropertyType.EARTH_DEFENSE] = elements.earth;
			mPropertyAdd[(int)CombatPropertyType.WIND_DEFENSE] = elements.wind;
			mPropertyAdd[(int)CombatPropertyType.FIRE_DEFENSE] = elements.fire;
			mPropertyAdd[(int)CombatPropertyType.WATER_DEFENSE] = elements.water;
		}
	}

    public void LoadElementDefenseScalar(ElementVector elements) {
        if (elements != null) {
            mPropertyMultiply[(int)CombatPropertyType.CRUSH_DEFENSE] = elements.crush;
            mPropertyMultiply[(int)CombatPropertyType.SLASH_DEFENSE] = elements.slash;
            mPropertyMultiply[(int)CombatPropertyType.PIERCE_DEFENSE] = elements.pierce;
            mPropertyMultiply[(int)CombatPropertyType.LIGHT_DEFENSE] = elements.light;
            mPropertyMultiply[(int)CombatPropertyType.DARK_DEFENSE] = elements.dark;
            mPropertyMultiply[(int)CombatPropertyType.EARTH_DEFENSE] = elements.earth;
            mPropertyMultiply[(int)CombatPropertyType.WIND_DEFENSE] = elements.wind;
            mPropertyMultiply[(int)CombatPropertyType.FIRE_DEFENSE] = elements.fire;
            mPropertyMultiply[(int)CombatPropertyType.WATER_DEFENSE] = elements.water;
        }
    }

    public void LoadCombatStats(CombatStatsVector combatStats) {
        if (combatStats != null) {
            mPropertyAdd[(int)CombatPropertyType.PHYSICAL_POWER] = combatStats.physicalPower;
            mPropertyAdd[(int)CombatPropertyType.PHYSICAL_DEFENSE] = combatStats.physicalDefense;
            mPropertyAdd[(int)CombatPropertyType.MAGICAL_POWER] = combatStats.magicPower;
            mPropertyAdd[(int)CombatPropertyType.MAGICAL_DEFENSE] = combatStats.magicDefense;
            mPropertyAdd[(int)CombatPropertyType.EVASION] = combatStats.evasion;
            mPropertyAdd[(int)CombatPropertyType.ACCURACY] = combatStats.accuracy;
            mPropertyAdd[(int)CombatPropertyType.CRIT_EVASION] = combatStats.critEvasion;
            mPropertyAdd[(int)CombatPropertyType.CRIT_ACCURACY] = combatStats.critAccuracy;
            mPropertyAdd[(int)CombatPropertyType.CRIT_DEFENSE] = combatStats.critDefense;
            mPropertyAdd[(int)CombatPropertyType.CRIT_POWER] = combatStats.critPower;
        }
    }

    public void LoadCombatStatsScalar(CombatStatsVector combatStats) {
        if (combatStats != null) {
            mPropertyMultiply[(int)CombatPropertyType.PHYSICAL_POWER] = combatStats.physicalPower;
            mPropertyMultiply[(int)CombatPropertyType.PHYSICAL_DEFENSE] = combatStats.physicalDefense;
            mPropertyMultiply[(int)CombatPropertyType.MAGICAL_POWER] = combatStats.magicPower;
            mPropertyMultiply[(int)CombatPropertyType.MAGICAL_DEFENSE] = combatStats.magicDefense;
            mPropertyMultiply[(int)CombatPropertyType.EVASION] = combatStats.evasion;
            mPropertyMultiply[(int)CombatPropertyType.ACCURACY] = combatStats.accuracy;
            mPropertyMultiply[(int)CombatPropertyType.CRIT_EVASION] = combatStats.critEvasion;
            mPropertyMultiply[(int)CombatPropertyType.CRIT_ACCURACY] = combatStats.critAccuracy;
            mPropertyMultiply[(int)CombatPropertyType.CRIT_DEFENSE] = combatStats.critDefense;
            mPropertyMultiply[(int)CombatPropertyType.CRIT_POWER] = combatStats.critPower;
        }
    }

    public void LoadDamageMin(ElementVector elements) {
        if (elements != null) {
            mPropertyAdd[(int)CombatPropertyType.CRUSH_MIN] = elements.crush;
            mPropertyAdd[(int)CombatPropertyType.SLASH_MIN] = elements.slash;
            mPropertyAdd[(int)CombatPropertyType.PIERCE_MIN] = elements.pierce;
            mPropertyAdd[(int)CombatPropertyType.LIGHT_MIN] = elements.light;
            mPropertyAdd[(int)CombatPropertyType.DARK_MIN] = elements.dark;
            mPropertyAdd[(int)CombatPropertyType.EARTH_MIN] = elements.earth;
            mPropertyAdd[(int)CombatPropertyType.WIND_MIN] = elements.wind;
            mPropertyAdd[(int)CombatPropertyType.FIRE_MIN] = elements.fire;
            mPropertyAdd[(int)CombatPropertyType.WATER_MIN] = elements.water;
        }
    }

    public void LoadDamageMax(ElementVector elements) {
        if (elements != null) {
            mPropertyAdd[(int)CombatPropertyType.CRUSH_MAX] = elements.crush;
            mPropertyAdd[(int)CombatPropertyType.SLASH_MAX] = elements.slash;
            mPropertyAdd[(int)CombatPropertyType.PIERCE_MAX] = elements.pierce;
            mPropertyAdd[(int)CombatPropertyType.LIGHT_MAX] = elements.light;
            mPropertyAdd[(int)CombatPropertyType.DARK_MAX] = elements.dark;
            mPropertyAdd[(int)CombatPropertyType.EARTH_MAX] = elements.earth;
            mPropertyAdd[(int)CombatPropertyType.WIND_MAX] = elements.wind;
            mPropertyAdd[(int)CombatPropertyType.FIRE_MAX] = elements.fire;
            mPropertyAdd[(int)CombatPropertyType.WATER_MAX] = elements.water;
        }
    }

    public void LoadDamageScalingAttributes(AttributeVector attributes) {
        if (attributes != null) {
            mPropertyAdd[(int)CombatPropertyType.SCALE_STR] = attributes.strength;
            mPropertyAdd[(int)CombatPropertyType.SCALE_VIT] = attributes.vitality;
            mPropertyAdd[(int)CombatPropertyType.SCALE_DEX] = attributes.dexerity;
            mPropertyAdd[(int)CombatPropertyType.SCALE_AGI] = attributes.agility;
            mPropertyAdd[(int)CombatPropertyType.SCALE_WIS] = attributes.wisdom;
            mPropertyAdd[(int)CombatPropertyType.SCALE_INT] = attributes.inteligence;
            mPropertyAdd[(int)CombatPropertyType.SCALE_LUCK] = attributes.luck;
        }
    }

	public void LoadStatusEffectProperty(StatusEffectProperty property, float strength) {
		switch(property) {
		case StatusEffectProperty.AGI_MOD:
			mPropertyAdd[(int)CombatPropertyType.AGI] = strength;
			break;
		case StatusEffectProperty.DEX_MOD:
			mPropertyAdd[(int)CombatPropertyType.DEX] = strength;
			break;
		case StatusEffectProperty.STR_MOD:
			mPropertyAdd[(int)CombatPropertyType.STR] = strength;
			break;
		case StatusEffectProperty.VIT_MOD:
			mPropertyAdd[(int)CombatPropertyType.VIT] = strength;
			break;
		case StatusEffectProperty.LUCK_MOD:
			mPropertyAdd[(int)CombatPropertyType.LUCK] = strength;
			break;
		case StatusEffectProperty.INT_MOD:
			mPropertyAdd[(int)CombatPropertyType.INT] = strength;
			break;
		case StatusEffectProperty.WIS_MOD:
			mPropertyAdd[(int)CombatPropertyType.AGI] = strength;
			break;
			
			
		case StatusEffectProperty.CRIT_CHANCE:
			mPropertyAdd[(int)CombatPropertyType.CRIT_ACCURACY] = strength;
			break;
		case StatusEffectProperty.CRIT_EVASION:
			mPropertyAdd[(int)CombatPropertyType.CRIT_EVASION] = strength;
			break;
		case StatusEffectProperty.CRIT_POWER:
			mPropertyAdd[(int)CombatPropertyType.CRIT_POWER] = strength;
			break;
			
			
		case StatusEffectProperty.SPEED:			
		case StatusEffectProperty.NONE:
		case StatusEffectProperty.LOSE_TURN:
		case StatusEffectProperty.HP_REGEN:
		case StatusEffectProperty.HEALING_REDUCTION:
		case StatusEffectProperty.FATIGUE_COST:
			
		default:
			Logger.d (this, "skipping status effect property: " + property);
			break;
		}
	}
}


