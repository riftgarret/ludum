using UnityEngine;
using System.Collections;
using App.BattleSystem.Effects;
using App.BattleSystem.Combat.CombatNode;
using App.BattleSystem.Turn;
using App.Core.Equipment;
using App.Core.Characters;
using App.BattleSystem.Actions;
using App.BattleSystem.Combat.Operation;

namespace App.BattleSystem.Entity
{
    /// <summary>
    /// Battle entity. Main class that contains all current effects and state of this character in battle.
    /// </summary>
    public abstract class BattleEntity
    {
        public delegate void OnDecisionRequired(BattleEntity entity);
        public delegate void OnExecuteOperation(BattleEntity entity, ICombatOperation operation);

        // listenener for battle entity upated
        public OnDecisionRequired OnDecisionRequiredDelegate { get; set; }

        public OnExecuteOperation OnExecuteOperationDelegate { get; set; }

        // turn phase
        public PhaseState Phase => Action.Phase;

        public float TurnPercent => Action.PhasePercent;

        private IBattleAction action;
        public IBattleAction Action
        {
            get => action;
            set
            {                
                if(value != null)
                {
                    value.ExecuteCombatOperationDelegate = (ex => OnExecuteOperationDelegate?.Invoke(this, ex));
                }
                action = value;
            }
        }

        /// <summary>
        /// Gets the character associated with this element.
        /// </summary>
        /// <value>The character.</value>
        public Character Character
        {
            get;
            protected set;
        }

        /// <summary>
        /// The status effect manager. Manages status effects so when a new effect is added, 
        /// we can tell if its refresh, new, or canceling something else.
        /// </summary>
        private StatusEffectClient statusEffectManager;

        /// <summary>
        /// The combat node factory. Used to generate a NodeBuilder which will bring together
        /// an offensive combat nodes for stat aggregation
        /// </summary>
        private CombatNodeFactory combatNodeFactory;

        // setup variables
        public BattleEntity(Character character)
        {
            statusEffectManager = new StatusEffectClient(this);
            combatNodeFactory = new CombatNodeFactory(this);                        
            this.Character = character;
            this.MaxHP = character.MaxHP;
            this.CurrentHP = character.curHP;
        }

        public void InitializeBattlePhase()
        {
            // this value is temp until we assign an initiative per character
            Action = new BattleActionInitiative(Random.Range(1, 5));
        }

        public void ApplyStatusEffect(BattleEntity sourceEntity, IStatusEffect statusEffect)
        {
            statusEffectManager.HandleAddStatus(sourceEntity, statusEffect);
        }

        /// <summary>
        /// Is this entity a PC
        /// </summary>
        /// <returns><c>true</c>, if P was ised, <c>false</c> otherwise.</returns>
        public abstract bool IsPC
        {
            get;
        }

        public void IncrementGameClock(float gameClockDelta)
        {
            // TODO, we can modify time if we have that buff here
            Action.IncrementGameClock(gameClockDelta);
            statusEffectManager.OnTimeIncrement(gameClockDelta);

            if (Action.Phase == PhaseState.RECOVER && Action.PhaseComplete >= 1f)
            {
                OnDecisionRequiredDelegate?.Invoke(this);
            }           
        }        

        /// <summary>
        /// Creates the combat node builder. This will have references to most of the 
        /// Battle Entity, that includes, character, equipment, weapon, status effects
        /// </summary>
        /// <returns>The combat node builder.</returns>
        public CombatNodeBuilder CreateCombatNodeBuilder()
        {
            return new CombatNodeBuilder(combatNodeFactory);
        }

        /// <summary>
        /// Create a default assimilation of the CombatNode, this will choose the first single weapon.
        /// </summary>
        /// <returns>The default combat node.</returns>
        public CompositeCombatNode CreateDefaultCombatNode()
        {
            return CreateCombatNodeBuilder().Build();
        }

        public IWeapon[] EquipedWeapons => Character.EquipedWeapons;

        public IArmor[] EquipedArmor => Character.EquipedArmor;

        public float CurrentHP
        {
            get;
            set;
        }

        public float MaxHP
        {
            get;
            set;
        }

        public StatusEffectClient StatusEffectClient
        {
            get { return statusEffectManager; }
        }
    } 
}
