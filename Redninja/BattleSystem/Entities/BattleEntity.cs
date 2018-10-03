using System;
using Davfalcon.Revelator;
using Davfalcon.Revelator.Combat;

namespace Redninja.BattleSystem.Entities
{
	/// <summary>
	/// Battle entity. Main class that contains all current effects and state of this character in battle.
	/// </summary>
	public class BattleEntity : IBattleEntity
	{
		private IClock clock;
		private readonly ICombatResolver combatResolver;

		public IUnit Character { get; }

		// If we add an action queue here, this will point to the top instead
		public IBattleAction Action { get; set; }
		public PhaseState Phase => Action?.Phase ?? PhaseState.Waiting;
		public float PhasePercent => Action?.PhaseProgress ?? 0;

		public int Team { get; set; }
		public bool IsPlayerControlled { get; set; }
		public EntityPosition Position { get; private set; } = new EntityPosition(1);

		public event Action<IBattleEntity> DecisionRequired;

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

		private void OnTick(float timeDelta)
		{
			// Check for buff update interval, then update buffs/status effects

			if (Action.Phase == PhaseState.Done)
			{
				DecisionRequired?.Invoke(this);
				// If we add an action queue, pop the completed action off here
			}
		}

		#region Clock binding
		public void SetClock(IClock clock)
		{
			// Check to unbind from previous clock just in case
			Dispose();

			this.clock = clock;
			clock.Tick += OnTick;
		}

		public void Dispose()
		{
			if (clock != null)
			{
				clock.Tick -= OnTick;
				clock = null;
			}
		}
		#endregion
	}
}
