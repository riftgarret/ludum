using UnityEngine;
using System.Collections;

/// <summary>
/// Battle entity. Main class that contains all current effects and state of this character in battle.
/// </summary>
public abstract class BattleEntity {

    public interface BattleEntityDelegate {
        void OnDecisionRequired(BattleEntity entity);
    }

    // listenener for battle entity upated
    private BattleEntityDelegate mListener;

	// turn phase
	public TurnState turnState {
		get;
		protected set;
	}

	/// <summary>
	/// Gets the character associated with this element.
	/// </summary>
	/// <value>The character.</value>
	public Character character {
		get;
		protected set;
	}

	/// <summary>
	/// The status effect manager. Manages status effects so when a new effect is added, 
	/// we can tell if its refresh, new, or canceling something else.
	/// </summary>
	private StatusEffectClient mStatusEffectManager;

	/// <summary>
	/// The combat node factory. Used to generate a NodeBuilder which will bring together
	/// an offensive combat nodes for stat aggregation
	/// </summary>
	private CombatNodeFactory mCombatNodeFactory;

	// setup variables
	public BattleEntity(Character character, BattleEntityDelegate listener) {
		mStatusEffectManager = new StatusEffectClient(this);
		mCombatNodeFactory = new CombatNodeFactory (this);
        mListener = listener;
		turnState = new TurnState(this);
		this.character = character;
		this.maxHP = character.maxHP;
		this.currentHP = character.curHP;
	}

	public void InitializeBattlePhase() {
		turnState.SetAction(new BattleActionInitiative(Random.Range(1, 5)));
	}

	public void ApplyStatusEffect(BattleEntity sourceEntity, IStatusEffect statusEffect) {
		mStatusEffectManager.HandleAddStatus(sourceEntity, statusEffect);
	}

	/// <summary>
	/// Is this entity a PC
	/// </summary>
	/// <returns><c>true</c>, if P was ised, <c>false</c> otherwise.</returns>
	public abstract bool isPC {
		get;
	}

	/// <summary>
	/// Raises the requires input event. This should be managed by either a PC to get actions
	/// from the user and pause the game, or from the NPC who should decide automatically based on AI
	/// 
	/// </summary>
	/// <param name="state">State.</param>
    public void OnRequiresInput(TurnState state) {
        mListener.OnDecisionRequired(this);
    }

	public void IncrementGameClock(float gameClockDelta) {
		// TODO, we can modify time if we have that buff here
		turnState.IncrementGameClock(gameClockDelta);
		mStatusEffectManager.OnTimeIncrement(gameClockDelta);
	}

	public bool requireUserInput() {
		return turnState.phase == TurnState.Phase.REQUIRES_INPUT;
	}

	public void OnExecuteTurn(TurnState state) {
		// do action against character
		state.action.OnExecuteAction(state.turnClock);
	}

	/// <summary>
	/// Creates the combat node builder. This will have references to most of the 
	/// Battle Entity, that includes, character, equipment, weapon, status effects
	/// </summary>
	/// <returns>The combat node builder.</returns>
	public CombatNodeBuilder CreateCombatNodeBuilder() {
		return new CombatNodeBuilder(mCombatNodeFactory);
	}

	/// <summary>
	/// Create a default assimilation of the CombatNode, this will choose the first single weapon.
	/// </summary>
	/// <returns>The default combat node.</returns>
	public CompositeCombatNode CreateDefaultCombatNode() {
		return CreateCombatNodeBuilder().Build();
	}

	public IWeapon [] equipedWeapons {
		get { return character.equipedWeapons; }
	}

	public IArmor [] equipedArmor {
		get { return character.equipedArmor; }
	}

	public float currentHP {
		get;
		set;
	}

	public float maxHP {
		get;
		set;
	}

	public StatusEffectClient statusEffectClient {
		get { return mStatusEffectManager; }
	}
}
