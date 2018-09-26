using UnityEngine;
using System.Collections;
using App.BattleSystem.Effects;
using App.BattleSystem.Combat.CombatNode;
using App.BattleSystem.Turn;
using App.BattleSystem.Action;
using App.Core.Equipment;
using App.Core.Characters;

namespace App.BattleSystem.Entity
{
    /// <summary>
    /// Battle entity. Main class that contains all current effects and state of this character in battle.
    /// </summary>
    public abstract class BattleEntity
    {
        public delegate void OnDecisionRequired(BattleEntity entity);
        public delegate void OnExecutionStarted(BattleEntity entity, IBattleAction action);

        // listenener for battle entity upated
        public OnDecisionRequired OnDecisionRequiredDelegate { get; set; }

        public OnExecutionStarted OnExecutionStartedDelegate { get; set; }

        // turn phase
        public PhaseState Phase => actionHandler.Phase;

        public float TurnPercent => actionHandler.TurnPercent;

        private BattleEntityActionHandler actionHandler;

        public void SetAction(IBattleAction action) => actionHandler.SetAction(action);

        public IBattleAction Action => actionHandler.Action;

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
            actionHandler = new BattleEntityActionHandler();
            actionHandler.OnStartActionExecutionDelegate += OnExecuteStart;
            this.Character = character;
            this.MaxHP = character.MaxHP;
            this.CurrentHP = character.curHP;
        }

        public void InitializeBattlePhase()
        {
            // this value is temp until we assign an initiative per character
            SetAction(new BattleActionInitiative(Random.Range(1, 5)));
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
            actionHandler.IncrementGameClock(gameClockDelta);
            statusEffectManager.OnTimeIncrement(gameClockDelta);

            if (actionHandler.Phase == PhaseState.REQUIRES_INPUT)
            {
                OnDecisionRequiredDelegate?.Invoke(this);
            }
        }

        /// <summary>
        /// Hooked delegate from TurnState.
        /// </summary>
        private void OnExecuteStart() => OnExecutionStartedDelegate.Invoke(this, actionHandler.Action);        

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
