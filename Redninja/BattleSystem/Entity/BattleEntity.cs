using System;
using Davfalcon.Revelator;
using Davfalcon.Revelator.Combat;
using Redninja.BattleSystem.Actions;

namespace Redninja.BattleSystem.Entity
{
	/// <summary>
	/// Battle entity. Main class that contains all current effects and state of this character in battle.
	/// </summary>
	public class BattleEntity : IGameClock, IBattleEntity
	{
		public event Action<IBattleEntity> DecisionRequired;

		private readonly ICombatResolver combatResolver;
		private float timer = 0;

		public IUnit Character { get; }

		// If we add an action queue here, this will point to the top instead
		public IBattleAction Action { get; set; }
		public PhaseState Phase => Action?.Phase ?? PhaseState.REQUIRES_INPUT;
		public float PhasePercent => Action?.PhasePercent ?? 0;

		public int Team { get; set; }
		public bool IsPlayerControlled { get; set; }
		public EntityPosition Position { get; private set; } = new EntityPosition(1);

		public BattleEntity(IUnit character, ICombatResolver combatResolver)
		{
			// Instead of using ICombatResolver directly, consider extending/wrapping an implementation that calls events
			this.combatResolver = combatResolver;

			Character = character;

			// Combat node stuff should move to action resoluton
			//combatNodeFactory = new CombatNodeFactory(this);

		}

		public void InitializeBattlePhase()
		{
			// this value is temp until we assign an initiative per character
			//Action = new BattleActionInitiative(Random.Range(1, 5));
			combatResolver.Initialize(Character);
		}

		public void MovePosition(int row, int col)
			=> Position = new EntityPosition(row, col, Position.Size);

		public void IncrementGameClock(float gameClockDelta)
		{
			timer += gameClockDelta;

			Action.IncrementGameClock(gameClockDelta);

			// Check for buff update interval, then update buffs/status effects

			// Add a DONE state
			if (Action.Phase == PhaseState.RECOVER && Action.PhaseComplete >= 1f)
			{
				DecisionRequired?.Invoke(this);

				// If we add an action queue, pop the completed action off here
			}
		}
	}
}
