using App.BattleSystem.Combat.CombatNode;
using App.BattleSystem.Events;
using System;
using System.Collections.Generic;
using UnityEngine;


namespace App.BattleSystem.Combat.Operation
{
    namespace App.BattleSystem.Combat.Operation
    {
        public class CombatOperationExecutor
        {

            public delegate void OnCombatEvent(IBattleEvent e);

            public OnCombatEvent OnCombatEventDelegate { get; set; }

            public void Execute(ICombatOperation combatOperation)
            {

                // execute
                combatOperation.Execute();

                // prepare list
                Queue<IBattleEvent> events = new Queue<IBattleEvent>();
                combatOperation.GenerateEvents(events);

                // process events
                ProcessEvents(events);
            }

            private void ProcessEvents(Queue<IBattleEvent> events)
            {
                // do stuff with leftovers?
                List<IBattleEvent> processedEvents = new List<IBattleEvent>();
                foreach (IBattleEvent e in events)
                {
                    OnCombatEventDelegate?.Invoke(e);
                }
            }


            /// <summary>
            /// Execute an operation and manage check resulting event with CombatStatusEffect Rules if any
            /// </summary>
            /// <param name="operation">Operation.</param>
            private void ExecuteAttackOperation(ICombatOperation operation,
                                                EntityCombatResolver srcResolver,
                                                EntityCombatResolver destResolver,
                                                StatusEffectRule[] effectRules)
            {
                // check to see if we were alive before executing the event
                BattleEntity destEntity = destResolver.Entity;
                bool wasAlive = destEntity.currentHP > 0;

                // execute and apply damage
                IBattleEvent battleEvent = null;
                BattleEventType eventType = battleEvent.EventType;

                // notify resulting battle event
                OnCombatEventDelegate?.Invoke(battleEvent);

                // check to see if it was a damage event to see if we killed them
                if (eventType == BattleEventType.DAMAGE && wasAlive && destEntity.currentHP <= 0)
                {
                    destEntity.character.curHP = 0;
                    DeathEvent deathEvent = new DeathEvent(destEntity);
                    OnCombatEventDelegate?.Invoke(deathEvent);
                }

                // lets see if we hit the target or not
                bool hitTarget = eventType == BattleEventType.DAMAGE
                                || eventType == BattleEventType.NON_DAMAGE
                                || eventType == BattleEventType.ITEM;

                bool missedTarget = eventType == BattleEventType.DODGE
                                || eventType == BattleEventType.RESIST;

                // iterate through combnat effects to see what should apply
                foreach (StatusEffectRule combatStatusEffect in effectRules)
                {
                    switch (combatStatusEffect.rule)
                    {
                        case StatusEffectRule.StatusEffectRuleHitPredicate.ON_HIT:
                            if (hitTarget)
                            {
                                ApplyEffect(combatStatusEffect, srcResolver.Entity);
                            }
                            break;
                        case StatusEffectRule.StatusEffectRuleHitPredicate.ON_MISS:
                            if (missedTarget)
                            {
                                ApplyEffect(combatStatusEffect, srcResolver.Entity);
                            }
                            break;
                        case StatusEffectRule.StatusEffectRuleHitPredicate.ALWAYS:
                        default:
                            ApplyEffect(combatStatusEffect, srcResolver.Entity);
                            break;
                    }
                }

            }

            /// <summary>
            /// Applies the effect. Notify the StatusEffect to the event manager
            /// </summary>
            /// <param name="effects">Effects.</param>
            /// <param name="targetEntity">Target entity.</param>
            private void ApplyEffect(StatusEffectRule combatEffectRule, BattleEntity srcEntity)
            {
                //		BattleEntity destEntity = combatEffectRule.rule;
                //		IStatusEffectRunner statusEffect = combatEffectRule.effect;
                // first directly apply the effect, it will notify the event from the StatusEffectManager
                //		destEntity.ApplyStatusEffect (statusEffect);
            }
        }
    } 
}
