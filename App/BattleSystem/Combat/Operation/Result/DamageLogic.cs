
using App.BattleSystem.Combat.CombatNode;
using App.BattleSystem.Events;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace App.BattleSystem.Combat.Operation.Result
{

    /// <summary>
    /// Damage logic encapsulates how much damage was calculated that should be applied.
    /// </summary>
    public class DamageLogic : BaseCombatLogic, ICombatLogic
    {

        private ElementVector defense;
        private ElementVector damage;

        private CritChanceLogic critResult;
        private ElementVector critDamage;

        private float m_HpBefore;
        private float m_HpAfter;

        private float m_TotalDamage;


        public void Execute(EntityCombatResolver src, EntityCombatResolver dest)
        {
            CheckExecute();
            // pull out min and max damage and calculated 'rolled damage'
            ElementVector min = src.DamageMin;
            ElementVector max = src.DamageMax;
            float rand = UnityEngine.Random.Range(0f, 1f);
            ElementVector diff = max - min;
            ElementVector randomDmg = min + (diff * rand);

            // scale damage by vector, this could have been done earlier, same results
            AttributeVector attributes = src.Attributes;
            AttributeVector damageAttributeScalar = src.DamageAttributeScalar;
            AttributeVector resultDamageExtra = attributes * damageAttributeScalar;
            float scaleDamage = resultDamageExtra.Sum;

            // scale damage should be < 1 normally, so we want to ensure this is positive on scaling
            damage = randomDmg * (1 + scaleDamage);

            // scale on dmg bonus
            damage += src.ElementAttackRaw;
            damage *= src.ElementAttackScalar;

            // if is critical 
            critDamage = new ElementVector();

            critResult = new CritChanceLogic();
            critResult.Execute(src, dest);

            if (critResult.Hits)
            {
                float critScale = CombatUtil.CalculateCritDamageScale(src, dest);
                critDamage = (damage * critScale);
            }

            m_HpBefore = dest.Entity.currentHP;

            defense = dest.ElementDefense;

            // apply dmg
            m_TotalDamage = CombatUtil.CalculateDamage(damage, critDamage, defense);
            dest.Entity.currentHP -= m_TotalDamage;

            m_HpAfter = dest.Entity.currentHP;

            Logger.d(this, this);
        }

        public void GenerateEvents(EntityCombatResolver src, EntityCombatResolver dest, Queue<IBattleEvent> combatEvents)
        {
            // forward to combat events
            critResult.GenerateEvents(src, dest, combatEvents);

            combatEvents.Enqueue(new DamageEvent(src.Entity, dest.Entity, damage, critDamage, defense));

            if (m_HpBefore > 0 && m_HpAfter <= 0)
            {
                combatEvents.Enqueue(new DeathEvent(dest.Entity));
            }
        }

        public override string ToString()
        {
            return string.Format("[DamageLogic: m_TotalDamage={6}, m_Defense={0}, m_Damage={1}, m_CritResult={2}, m_CritDamage={3}, m_HpBefore={4}, m_HpAfter={5}]", defense, damage, critResult, critDamage, m_HpBefore, m_HpAfter, m_TotalDamage);
        }

    } 
}
