using Redninja.BattleSystem.Entity;
using Redninja.Core.Stats;

namespace Redninja.BattleSystem.Combat.CombatNode
{
    /// <summary>
    /// Entity Resolve combat Resolver provides us a way to request how much total damage per property would should
    /// be calculated. An example: The Character has weapons and armor equiped that adds fire damage property as well as
    /// its base skill for fire damage. This class calling 'GetProperty(..)' will provide you with calculated damage for
    /// its Fire skill.
    /// </summary>
    public class EntityCombatResolver
    {
        private BattleEntity entity;
        private ICombatNode rootNode;

        public EntityCombatResolver(BattleEntity entity) : this(entity, entity.CreateDefaultCombatNode())
        {
        }

        public EntityCombatResolver(BattleEntity entity, ICombatNode rootNode)
        {
            this.rootNode = rootNode;
            this.entity = entity;
        }

        public BattleEntity Entity
        {
            get { return entity; }
        }

        /// <summary>
        /// Get calculated property. Where RAW * SCALED is Redninja.ied.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public float GetProperty(CombatPropertyType property)
        {
            float ret = GetPropertyRaw(property);
            ret *= GetPropertyScalar(property);
            return ret;
        }

        /// <summary>
        /// Get the sum of RAW.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public float GetPropertyRaw(CombatPropertyType property)
        {
            return rootNode.GetProperty(property);
        }

        /// <summary>
        /// Get the sum of SCALE.
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public float GetPropertyScalar(CombatPropertyType property)
        {
            float scaleValue = rootNode.GetPropertyScalar(property);
            return 1 + (scaleValue / 100f);
        }

        public ElementVector DamageMin
        {
            get
            {
                ElementVector dmg = new ElementVector();
                // start stats with skill / item parts
                dmg.crush = GetProperty(CombatPropertyType.CRUSH_MIN);
                dmg.slash = GetProperty(CombatPropertyType.SLASH_MIN);
                dmg.pierce = GetProperty(CombatPropertyType.PIERCE_MIN);
                dmg.light = GetProperty(CombatPropertyType.LIGHT_MIN);
                dmg.dark = GetProperty(CombatPropertyType.DARK_MIN);
                dmg.fire = GetProperty(CombatPropertyType.FIRE_MIN);
                dmg.water = GetProperty(CombatPropertyType.WATER_MIN);
                dmg.earth = GetProperty(CombatPropertyType.EARTH_MIN);
                dmg.wind = GetProperty(CombatPropertyType.WIND_MIN);
                return dmg;
            }
        }

        public ElementVector DamageMax
        {
            get
            {
                ElementVector dmg = new ElementVector();
                // start stats with skill / item parts
                dmg.crush = GetProperty(CombatPropertyType.CRUSH_MAX);
                dmg.slash = GetProperty(CombatPropertyType.SLASH_MAX);
                dmg.pierce = GetProperty(CombatPropertyType.PIERCE_MAX);
                dmg.light = GetProperty(CombatPropertyType.LIGHT_MAX);
                dmg.dark = GetProperty(CombatPropertyType.DARK_MAX);
                dmg.fire = GetProperty(CombatPropertyType.FIRE_MAX);
                dmg.water = GetProperty(CombatPropertyType.WATER_MAX);
                dmg.earth = GetProperty(CombatPropertyType.EARTH_MAX);
                dmg.wind = GetProperty(CombatPropertyType.WIND_MAX);
                return dmg;
            }
        }

        public AttributeVector DamageAttributeScalar
        {
            get
            {
                AttributeVector attributes = new AttributeVector();
                attributes.strength = GetProperty(CombatPropertyType.SCALE_STR);
                attributes.vitality = GetProperty(CombatPropertyType.SCALE_VIT);
                attributes.dexerity = GetProperty(CombatPropertyType.SCALE_DEX);
                attributes.agility = GetProperty(CombatPropertyType.SCALE_AGI);
                attributes.wisdom = GetProperty(CombatPropertyType.SCALE_WIS);
                attributes.inteligence = GetProperty(CombatPropertyType.SCALE_INT);
                attributes.luck = GetProperty(CombatPropertyType.SCALE_LUCK);
                return attributes;
            }
        }

        public ElementVector ElementDefense
        {
            get
            {
                ElementVector elements = new ElementVector();
                elements.crush = GetProperty(CombatPropertyType.CRUSH_DEFENSE);
                elements.slash = GetProperty(CombatPropertyType.SLASH_DEFENSE);
                elements.pierce = GetProperty(CombatPropertyType.PIERCE_DEFENSE);
                elements.light = GetProperty(CombatPropertyType.LIGHT_DEFENSE);
                elements.dark = GetProperty(CombatPropertyType.DARK_DEFENSE);
                elements.fire = GetProperty(CombatPropertyType.FIRE_DEFENSE);
                elements.water = GetProperty(CombatPropertyType.WATER_DEFENSE);
                elements.earth = GetProperty(CombatPropertyType.EARTH_DEFENSE);
                elements.wind = GetProperty(CombatPropertyType.WIND_DEFENSE);
                return elements;
            }
        }

        public ElementVector ElementAttackRaw
        {
            get
            {
                ElementVector elements = new ElementVector();
                elements.crush = GetPropertyRaw(CombatPropertyType.CRUSH_ATTACK);
                elements.slash = GetPropertyRaw(CombatPropertyType.SLASH_ATTACK);
                elements.pierce = GetPropertyRaw(CombatPropertyType.PIERCE_ATTACK);
                elements.light = GetPropertyRaw(CombatPropertyType.LIGHT_ATTACK);
                elements.dark = GetPropertyRaw(CombatPropertyType.DARK_ATTACK);
                elements.fire = GetPropertyRaw(CombatPropertyType.FIRE_ATTACK);
                elements.water = GetPropertyRaw(CombatPropertyType.WATER_ATTACK);
                elements.earth = GetPropertyRaw(CombatPropertyType.EARTH_ATTACK);
                elements.wind = GetPropertyRaw(CombatPropertyType.WIND_ATTACK);
                return elements;
            }
        }

        public ElementVector ElementAttackScalar
        {
            get
            {
                ElementVector elements = new ElementVector();
                elements.crush = GetPropertyScalar(CombatPropertyType.CRUSH_ATTACK);
                elements.slash = GetPropertyScalar(CombatPropertyType.SLASH_ATTACK);
                elements.pierce = GetPropertyScalar(CombatPropertyType.PIERCE_ATTACK);
                elements.light = GetPropertyScalar(CombatPropertyType.LIGHT_ATTACK);
                elements.dark = GetPropertyScalar(CombatPropertyType.DARK_ATTACK);
                elements.fire = GetPropertyScalar(CombatPropertyType.FIRE_ATTACK);
                elements.water = GetPropertyScalar(CombatPropertyType.WATER_ATTACK);
                elements.earth = GetPropertyScalar(CombatPropertyType.EARTH_ATTACK);
                elements.wind = GetPropertyScalar(CombatPropertyType.WIND_ATTACK);
                return elements;
            }
        }

        public AttributeVector Attributes
        {
            get
            {
                AttributeVector attributes = new AttributeVector();
                attributes.strength = GetProperty(CombatPropertyType.STR);
                attributes.vitality = GetProperty(CombatPropertyType.VIT);
                attributes.dexerity = GetProperty(CombatPropertyType.DEX);
                attributes.agility = GetProperty(CombatPropertyType.AGI);
                attributes.wisdom = GetProperty(CombatPropertyType.WIS);
                attributes.inteligence = GetProperty(CombatPropertyType.INT);
                attributes.luck = GetProperty(CombatPropertyType.LUCK);
                return attributes;
            }
        }

        public CombatStatsVector CombatStats
        {
            get
            {
                CombatStatsVector combatStats = new CombatStatsVector();
                // TODO merge combat stats via attributes stats
                combatStats.accuracy = GetProperty(CombatPropertyType.ACCURACY);
                combatStats.evasion = GetProperty(CombatPropertyType.EVASION);
                combatStats.critAccuracy = GetProperty(CombatPropertyType.CRIT_ACCURACY);
                combatStats.critEvasion = GetProperty(CombatPropertyType.CRIT_EVASION);
                combatStats.critPower = GetProperty(CombatPropertyType.CRIT_POWER);
                combatStats.critDefense = GetProperty(CombatPropertyType.CRIT_DEFENSE);
                combatStats.physicalPower = GetProperty(CombatPropertyType.PHYSICAL_POWER);
                combatStats.physicalDefense = GetProperty(CombatPropertyType.PHYSICAL_DEFENSE);
                combatStats.magicPower = GetProperty(CombatPropertyType.MAGICAL_POWER);
                combatStats.magicDefense = GetProperty(CombatPropertyType.MAGICAL_DEFENSE);
                return combatStats;
            }
        }

    } 
}
