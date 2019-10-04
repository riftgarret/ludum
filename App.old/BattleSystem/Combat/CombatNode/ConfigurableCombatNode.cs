
using App.BattleSystem.Effects;
using App.Core.Stats;
using System;
using UnityEngine;

namespace App.BattleSystem.Combat.CombatNode
{
    /// <summary>
    /// This combat node can be configured per property.
    /// </summary>
    public class ConfigurableCombatNode : ICombatNode
    {
        protected float[] propertyAdd;
        protected float[] propertyMultiply;
        protected bool[] flags;

        public ConfigurableCombatNode()
        {
            propertyAdd = new float[(int)CombatPropertyType.COUNT];
            propertyMultiply = new float[(int)CombatPropertyType.COUNT];
            flags = new bool[(int)CombatFlag.COUNT];
        }

        public float GetProperty(CombatPropertyType property)
        {
            return propertyAdd[(int)property];
        }

        public float GetPropertyScalar(CombatPropertyType property)
        {
            return propertyMultiply[(int)property];
        }

        public bool HasFlag(CombatFlag flag)
        {
            return flags[(int)flag];
        }

        /// <summary>
        /// Load the specified combatProperties.
        /// </summary>
        /// <param name="combatProperties">Combat properties.</param>
        public void Load(CombatProperty[] combatProperties)
        {
            if (combatProperties == null)
            {
                Debug.Log("Failed to load combatProperties, null: " + this);
                return;
            }

            foreach (CombatProperty property in combatProperties)
            {
                this.propertyAdd[(int)property.propertyType] = property.add;
                this.propertyMultiply[(int)property.propertyType] = property.scalar;
            }
        }

        public void LoadAttribute(AttributeVector attributes)
        {
            if (attributes != null)
            {
                propertyAdd[(int)CombatPropertyType.STR] = attributes.strength;
                propertyAdd[(int)CombatPropertyType.VIT] = attributes.vitality;
                propertyAdd[(int)CombatPropertyType.DEX] = attributes.dexerity;
                propertyAdd[(int)CombatPropertyType.AGI] = attributes.agility;
                propertyAdd[(int)CombatPropertyType.WIS] = attributes.wisdom;
                propertyAdd[(int)CombatPropertyType.INT] = attributes.inteligence;
                propertyAdd[(int)CombatPropertyType.LUCK] = attributes.luck;
            }
        }

        public void LoadAttributeScalar(AttributeVector attributes)
        {
            if (attributes != null)
            {
                propertyMultiply[(int)CombatPropertyType.STR] = attributes.strength;
                propertyMultiply[(int)CombatPropertyType.VIT] = attributes.vitality;
                propertyMultiply[(int)CombatPropertyType.DEX] = attributes.dexerity;
                propertyMultiply[(int)CombatPropertyType.AGI] = attributes.agility;
                propertyMultiply[(int)CombatPropertyType.WIS] = attributes.wisdom;
                propertyMultiply[(int)CombatPropertyType.INT] = attributes.inteligence;
                propertyMultiply[(int)CombatPropertyType.LUCK] = attributes.luck;
            }
        }

        public void LoadElementAttack(ElementVector elements)
        {
            if (elements != null)
            {
                propertyAdd[(int)CombatPropertyType.CRUSH_ATTACK] = elements.crush;
                propertyAdd[(int)CombatPropertyType.SLASH_ATTACK] = elements.slash;
                propertyAdd[(int)CombatPropertyType.PIERCE_ATTACK] = elements.pierce;
                propertyAdd[(int)CombatPropertyType.LIGHT_ATTACK] = elements.light;
                propertyAdd[(int)CombatPropertyType.DARK_ATTACK] = elements.dark;
                propertyAdd[(int)CombatPropertyType.EARTH_ATTACK] = elements.earth;
                propertyAdd[(int)CombatPropertyType.WIND_ATTACK] = elements.wind;
                propertyAdd[(int)CombatPropertyType.FIRE_ATTACK] = elements.fire;
                propertyAdd[(int)CombatPropertyType.WATER_ATTACK] = elements.water;
            }
        }

        public void LoadElementAttackScalar(ElementVector elements)
        {
            if (elements != null)
            {
                propertyMultiply[(int)CombatPropertyType.CRUSH_ATTACK] = elements.crush;
                propertyMultiply[(int)CombatPropertyType.SLASH_ATTACK] = elements.slash;
                propertyMultiply[(int)CombatPropertyType.PIERCE_ATTACK] = elements.pierce;
                propertyMultiply[(int)CombatPropertyType.LIGHT_ATTACK] = elements.light;
                propertyMultiply[(int)CombatPropertyType.DARK_ATTACK] = elements.dark;
                propertyMultiply[(int)CombatPropertyType.EARTH_ATTACK] = elements.earth;
                propertyMultiply[(int)CombatPropertyType.WIND_ATTACK] = elements.wind;
                propertyMultiply[(int)CombatPropertyType.FIRE_ATTACK] = elements.fire;
                propertyMultiply[(int)CombatPropertyType.WATER_ATTACK] = elements.water;
            }
        }

        public void LoadElementDefense(ElementVector elements)
        {
            if (elements != null)
            {
                propertyAdd[(int)CombatPropertyType.CRUSH_DEFENSE] = elements.crush;
                propertyAdd[(int)CombatPropertyType.SLASH_DEFENSE] = elements.slash;
                propertyAdd[(int)CombatPropertyType.PIERCE_DEFENSE] = elements.pierce;
                propertyAdd[(int)CombatPropertyType.LIGHT_DEFENSE] = elements.light;
                propertyAdd[(int)CombatPropertyType.DARK_DEFENSE] = elements.dark;
                propertyAdd[(int)CombatPropertyType.EARTH_DEFENSE] = elements.earth;
                propertyAdd[(int)CombatPropertyType.WIND_DEFENSE] = elements.wind;
                propertyAdd[(int)CombatPropertyType.FIRE_DEFENSE] = elements.fire;
                propertyAdd[(int)CombatPropertyType.WATER_DEFENSE] = elements.water;
            }
        }

        public void LoadElementDefenseScalar(ElementVector elements)
        {
            if (elements != null)
            {
                propertyMultiply[(int)CombatPropertyType.CRUSH_DEFENSE] = elements.crush;
                propertyMultiply[(int)CombatPropertyType.SLASH_DEFENSE] = elements.slash;
                propertyMultiply[(int)CombatPropertyType.PIERCE_DEFENSE] = elements.pierce;
                propertyMultiply[(int)CombatPropertyType.LIGHT_DEFENSE] = elements.light;
                propertyMultiply[(int)CombatPropertyType.DARK_DEFENSE] = elements.dark;
                propertyMultiply[(int)CombatPropertyType.EARTH_DEFENSE] = elements.earth;
                propertyMultiply[(int)CombatPropertyType.WIND_DEFENSE] = elements.wind;
                propertyMultiply[(int)CombatPropertyType.FIRE_DEFENSE] = elements.fire;
                propertyMultiply[(int)CombatPropertyType.WATER_DEFENSE] = elements.water;
            }
        }

        public void LoadCombatStats(CombatStatsVector combatStats)
        {
            if (combatStats != null)
            {
                propertyAdd[(int)CombatPropertyType.PHYSICAL_POWER] = combatStats.physicalPower;
                propertyAdd[(int)CombatPropertyType.PHYSICAL_DEFENSE] = combatStats.physicalDefense;
                propertyAdd[(int)CombatPropertyType.MAGICAL_POWER] = combatStats.magicPower;
                propertyAdd[(int)CombatPropertyType.MAGICAL_DEFENSE] = combatStats.magicDefense;
                propertyAdd[(int)CombatPropertyType.EVASION] = combatStats.evasion;
                propertyAdd[(int)CombatPropertyType.ACCURACY] = combatStats.accuracy;
                propertyAdd[(int)CombatPropertyType.CRIT_EVASION] = combatStats.critEvasion;
                propertyAdd[(int)CombatPropertyType.CRIT_ACCURACY] = combatStats.critAccuracy;
                propertyAdd[(int)CombatPropertyType.CRIT_DEFENSE] = combatStats.critDefense;
                propertyAdd[(int)CombatPropertyType.CRIT_POWER] = combatStats.critPower;
            }
        }

        public void LoadCombatStatsScalar(CombatStatsVector combatStats)
        {
            if (combatStats != null)
            {
                propertyMultiply[(int)CombatPropertyType.PHYSICAL_POWER] = combatStats.physicalPower;
                propertyMultiply[(int)CombatPropertyType.PHYSICAL_DEFENSE] = combatStats.physicalDefense;
                propertyMultiply[(int)CombatPropertyType.MAGICAL_POWER] = combatStats.magicPower;
                propertyMultiply[(int)CombatPropertyType.MAGICAL_DEFENSE] = combatStats.magicDefense;
                propertyMultiply[(int)CombatPropertyType.EVASION] = combatStats.evasion;
                propertyMultiply[(int)CombatPropertyType.ACCURACY] = combatStats.accuracy;
                propertyMultiply[(int)CombatPropertyType.CRIT_EVASION] = combatStats.critEvasion;
                propertyMultiply[(int)CombatPropertyType.CRIT_ACCURACY] = combatStats.critAccuracy;
                propertyMultiply[(int)CombatPropertyType.CRIT_DEFENSE] = combatStats.critDefense;
                propertyMultiply[(int)CombatPropertyType.CRIT_POWER] = combatStats.critPower;
            }
        }

        public void LoadDamageMin(ElementVector elements)
        {
            if (elements != null)
            {
                propertyAdd[(int)CombatPropertyType.CRUSH_MIN] = elements.crush;
                propertyAdd[(int)CombatPropertyType.SLASH_MIN] = elements.slash;
                propertyAdd[(int)CombatPropertyType.PIERCE_MIN] = elements.pierce;
                propertyAdd[(int)CombatPropertyType.LIGHT_MIN] = elements.light;
                propertyAdd[(int)CombatPropertyType.DARK_MIN] = elements.dark;
                propertyAdd[(int)CombatPropertyType.EARTH_MIN] = elements.earth;
                propertyAdd[(int)CombatPropertyType.WIND_MIN] = elements.wind;
                propertyAdd[(int)CombatPropertyType.FIRE_MIN] = elements.fire;
                propertyAdd[(int)CombatPropertyType.WATER_MIN] = elements.water;
            }
        }

        public void LoadDamageMax(ElementVector elements)
        {
            if (elements != null)
            {
                propertyAdd[(int)CombatPropertyType.CRUSH_MAX] = elements.crush;
                propertyAdd[(int)CombatPropertyType.SLASH_MAX] = elements.slash;
                propertyAdd[(int)CombatPropertyType.PIERCE_MAX] = elements.pierce;
                propertyAdd[(int)CombatPropertyType.LIGHT_MAX] = elements.light;
                propertyAdd[(int)CombatPropertyType.DARK_MAX] = elements.dark;
                propertyAdd[(int)CombatPropertyType.EARTH_MAX] = elements.earth;
                propertyAdd[(int)CombatPropertyType.WIND_MAX] = elements.wind;
                propertyAdd[(int)CombatPropertyType.FIRE_MAX] = elements.fire;
                propertyAdd[(int)CombatPropertyType.WATER_MAX] = elements.water;
            }
        }

        public void LoadDamageScalingAttributes(AttributeVector attributes)
        {
            if (attributes != null)
            {
                propertyAdd[(int)CombatPropertyType.SCALE_STR] = attributes.strength;
                propertyAdd[(int)CombatPropertyType.SCALE_VIT] = attributes.vitality;
                propertyAdd[(int)CombatPropertyType.SCALE_DEX] = attributes.dexerity;
                propertyAdd[(int)CombatPropertyType.SCALE_AGI] = attributes.agility;
                propertyAdd[(int)CombatPropertyType.SCALE_WIS] = attributes.wisdom;
                propertyAdd[(int)CombatPropertyType.SCALE_INT] = attributes.inteligence;
                propertyAdd[(int)CombatPropertyType.SCALE_LUCK] = attributes.luck;
            }
        }

        public void LoadStatusEffectProperty(StatusEffectProperty property, float strength)
        {
            switch (property)
            {
                case StatusEffectProperty.AGI_MOD:
                    propertyAdd[(int)CombatPropertyType.AGI] = strength;
                    break;
                case StatusEffectProperty.DEX_MOD:
                    propertyAdd[(int)CombatPropertyType.DEX] = strength;
                    break;
                case StatusEffectProperty.STR_MOD:
                    propertyAdd[(int)CombatPropertyType.STR] = strength;
                    break;
                case StatusEffectProperty.VIT_MOD:
                    propertyAdd[(int)CombatPropertyType.VIT] = strength;
                    break;
                case StatusEffectProperty.LUCK_MOD:
                    propertyAdd[(int)CombatPropertyType.LUCK] = strength;
                    break;
                case StatusEffectProperty.INT_MOD:
                    propertyAdd[(int)CombatPropertyType.INT] = strength;
                    break;
                case StatusEffectProperty.WIS_MOD:
                    propertyAdd[(int)CombatPropertyType.AGI] = strength;
                    break;


                case StatusEffectProperty.CRIT_CHANCE:
                    propertyAdd[(int)CombatPropertyType.CRIT_ACCURACY] = strength;
                    break;
                case StatusEffectProperty.CRIT_EVASION:
                    propertyAdd[(int)CombatPropertyType.CRIT_EVASION] = strength;
                    break;
                case StatusEffectProperty.CRIT_POWER:
                    propertyAdd[(int)CombatPropertyType.CRIT_POWER] = strength;
                    break;


                case StatusEffectProperty.SPEED:
                case StatusEffectProperty.NONE:
                case StatusEffectProperty.LOSE_TURN:
                case StatusEffectProperty.HP_REGEN:
                case StatusEffectProperty.HEALING_REDUCTION:
                case StatusEffectProperty.FATIGUE_COST:

                default:
                    Logger.d(this, "skipping status effect property: " + property);
                    break;
            }
        }
    } 
}


