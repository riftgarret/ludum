using UnityEngine;
using System.Collections;
using App.BattleSystem.Effects;
using App.BattleSystem.Combat.CombatNode;
using App.BattleSystem.Turn;

namespace App.BattleSystem.Entity
{
    /// <summary>
    /// Battle entity. Main class that contains all current effects and state of this character in battle.
    /// </summary>
    public abstract class BattleEntity
    {

        public delegate void OnDecisionRequired(BattleEntity entity);

        // listenener for battle entity upated
        public OnDecisionRequired OnDecisionRequiredDelegate { get; set; }

        // turn phase
        public TurnState TurnState
        {
            get;
            protected set;
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
            TurnState = new TurnState(this);
            this.Character = character;
            this.MaxHP = character.maxHP;
            this.CurrentHP = character.curHP;
        }

        public void InitializeBattlePhase()
        {
            TurnState.SetAction(new BattleActionInitiative(Random.Range(1, 5)));
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

        /// <summary>
        /// Raises the requires input event. This should be managed by either a PC to get actions
        /// from the user and pause the game, or from the NPC who should decide automatically based on AI
        /// 
        /// </summary>
        /// <param name="state">State.</param>
        public void OnRequiresInput(TurnState state)
        {
            OnDecisionRequiredDelegate?.Invoke(this);
        }

        public void IncrementGameClock(float gameClockDelta)
        {
            // TODO, we can modify time if we have that buff here
            TurnState.IncrementGameClock(gameClockDelta);
            statusEffectManager.OnTimeIncrement(gameClockDelta);
        }

        public bool requireUserInput()
        {
            return TurnState.phase == TurnState.Phase.REQUIRES_INPUT;
        }

        public void OnExecuteTurn(TurnState state)
        {
            // do action against character
            state.action.OnExecuteAction(state.turnClock);
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

        public IWeapon[] EquipedWeapons
        {
            get { return Character.equipedWeapons; }
        }

        public IArmor[] EquipedArmor
        {
            get { return Character.equipedArmor; }
        }

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
