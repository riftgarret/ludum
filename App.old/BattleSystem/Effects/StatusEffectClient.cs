using App.BattleSystem.Entity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace App.BattleSystem.Effects
{
    /// <summary>
    /// Status effect manager. To manage buffs, debuffs and other states that may or may not be curable
    /// </summary>
    public class StatusEffectClient
    {

        public delegate void OnStatusEvent(StatusEffectEvent statusEvent);

        public OnStatusEvent OnStatusEventDelegate { get; set; }

        // for fast access to get those effects
        private Dictionary<StatusEffectProperty, StatusEffectNode> effectNodeMap;
        // TODO should have a sorted list of status effects for sake of loading GUI
        private BattleEntity battleEntity;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusEffectManager"/> class.
        /// </summary>
        /// <param name="entity">The Entity to manage. </param>
        public StatusEffectClient(BattleEntity entity)
        {
            effectNodeMap = new Dictionary<StatusEffectProperty, StatusEffectNode>();
            battleEntity = entity;
        }

        /// <summary>
        /// Handles the add status.
        /// </summary>
        /// <param name="effect">Effect.</param>
        /// <param name="printLog">If set to <c>true</c> print log.</param>
        public void HandleAddStatus(BattleEntity sourceEntity, IStatusEffect effect)
        {
            StatusEffectNode node = GetNode(effect);
            bool wasEmpty = node.isEmpty;
            node.ApplyEffect(sourceEntity, effect);
            CheckCleanup(node);
            // check to see if we should fire an event
            if (wasEmpty && !node.isEmpty)
            {
                OnStatusEventDelegate?.Invoke(new StatusEffectEvent(
                    battleEntity,
                    effect.Property,
                    StatusEffectEvent.StatusEventType.NEW,
                    effect.Type));
            }
            else if (!wasEmpty && node.isEmpty)
            {
                OnStatusEventDelegate?.Invoke(new StatusEffectEvent(
                    battleEntity,
                    effect.Property,
                    StatusEffectEvent.StatusEventType.REMOVED,
                    effect.Type));
            }
        }

        /// <summary>
        /// Raises the time increment event.
        /// </summary>
        /// <param name="timeDelta">Time delta.</param>
        public void OnTimeIncrement(float timeDelta)
        {
            foreach (StatusEffectProperty key in effectNodeMap.Keys)
            {
                StatusEffectNode node = effectNodeMap[key];
                node.IncrementTime(timeDelta);
                // TODO coroutine to cleanup?
                CheckCleanup(node);
                if (node.isEmpty)
                {
                    OnStatusEventDelegate?.Invoke(new StatusEffectEvent(
                        battleEntity,
                        key,
                        StatusEffectEvent.StatusEventType.EXPIRED,
                        StatusEffectType.NULLIFY));
                }
            }
        }

        public List<StatusEffectSummary> Summaries
        {
            get
            {
                List<StatusEffectSummary> result = new List<StatusEffectSummary>();

                foreach (StatusEffectProperty key in effectNodeMap.Keys)
                {
                    StatusEffectNode node = effectNodeMap[key];
                    StatusEffectSummary summary = new StatusEffectSummary();
                    summary.netValue = node.NetValue;
                    summary.property = key;
                    result.Add(summary);
                }

                return result;
            }
        }


        /// <summary>
        /// Lazy create the node.
        /// </summary>
        /// <returns>The node.</returns>
        /// <param name="effect">Effect.</param>
        private StatusEffectNode GetNode(IStatusEffect effect)
        {
            StatusEffectNode node = null;
            effectNodeMap.TryGetValue(effect.Property, out node);
            if (node == null)
            {
                node = new StatusEffectNode(this);
                effectNodeMap[effect.Property] = node;
            }
            return node;
        }

        //
        public class StatusEffectSummary
        {
            /// <summary>
            /// The net value.
            /// </summary>
            public float netValue { get; internal set; }
            /// <summary>
            /// The property.
            /// </summary>
            public StatusEffectProperty property { get; internal set; }
        }

        private void CheckCleanup(StatusEffectNode node)
        {
            // TODO cleanup9
        }

        public void debugPopulateRunners(List<IStatusEffectRunner> runnerList)
        {
            foreach (StatusEffectNode node in effectNodeMap.Values)
            {
                node.debugPopulateRunners(runnerList);
            }
        }

        // An internal data structure to manage the possible single instance types of these types of
        // buffs and debuffs being applied. Using a Chain of Responsibility pattern to delegate its rules
        // for clearing, and rebuffing.
        class StatusEffectNode
        {
            // TODO return node type, so we can easily grab attributes from 

            // even though we cant have a magical debuff and magical buff at the same time
            // its a lot less tedious to debug if we just have separate pointers
            private StatusEffectAggregator positiveEffects = new StatusEffectAggregator();
            private StatusEffectAggregator negativeEffects = new StatusEffectAggregator();

            private StatusEffectClient parent;

            public StatusEffectNode(StatusEffectClient manager)
            {
                this.parent = manager;
            }

            public void ApplyEffect(BattleEntity sourceEntity, IStatusEffect effect)
            {

                switch (effect.Type)
                {
                    case StatusEffectType.POSITIVE:
                        ApplyEffect(sourceEntity, effect, positiveEffects);
                        break;
                    case StatusEffectType.NEGATIVE:
                        ApplyEffect(sourceEntity, effect, negativeEffects);
                        break;
                    case StatusEffectType.NULLIFY:
                        ClearEffects();
                        break;
                }

            }

            /// <summary>
            /// Applies the magical buff effect. If we have a debuff, lets remove it instead.
            /// </summary>
            /// <param name="effect">Effect.</param>
            private void ApplyEffect(BattleEntity sourceEntity, IStatusEffect effect, StatusEffectAggregator aggregator)
            {
                aggregator.AddEffect(sourceEntity, effect);
            }

            private void ClearEffects()
            {
                positiveEffects.Clear();
                negativeEffects.Clear();
            }

            public float NetValue
            {
                get { return positiveEffects.NetValue - negativeEffects.NetValue; }
            }

            /// <summary>
            /// Increments the time. Potential place for expiring an event
            /// </summary>
            /// <param name="timeDelta">Time delta.</param>
            public void IncrementTime(float timeDelta)
            {
                positiveEffects.IncrementTime(timeDelta);
                negativeEffects.IncrementTime(timeDelta);
            }

            public bool isEmpty
            {
                get
                {
                    return positiveEffects.Count == 0 && negativeEffects.Count == 0;
                }
            }

            public void debugPopulateRunners(List<IStatusEffectRunner> runnerList)
            {
                positiveEffects.debugPopulateRunners(runnerList);
                negativeEffects.debugPopulateRunners(runnerList);
            }

            class StatusEffectAggregator
            {
                private List<IStatusEffectRunner> effects = new List<IStatusEffectRunner>();
                private float maxCapacity;
                private float netValue;


                public void AddEffect(BattleEntity sourceEntity, IStatusEffect statusEffect)
                {
                    // TODO sorted list from capacity solves max capacity problem
                    // TODO sorted list from endDuration solves checking all 

                    IStatusEffectRunner runner = new StatusEffectRunner(sourceEntity, statusEffect);
                    maxCapacity = Math.Max(maxCapacity, runner.Capacity);
                    effects.Add(runner);
                    netValue += runner.Strength;
                }

                public void IncrementTime(float timeDelta)
                {
                    foreach (IStatusEffectRunner runner in effects)
                    {
                        if (runner != null)
                        {
                            runner.IncrementDurationTime(timeDelta);

                            if (runner.IsExpired)
                            {
                                effects.Remove(runner);
                                netValue -= runner.Strength;
                            }
                        }
                    }
                }

                public float NetValue
                {
                    get { return Math.Min(netValue, maxCapacity); }
                }

                public int Count
                {
                    get { return effects.Count; }
                }

                public void Clear()
                {
                    effects.Clear();
                    maxCapacity = 0;
                }

                public void debugPopulateRunners(List<IStatusEffectRunner> runners)
                {
                    runners.AddRange(effects);
                }
            }
        }
    } 
}


