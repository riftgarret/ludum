using System;
using System.Collections.Generic;
using System.Linq;
using Davfalcon.Revelator;
using Davfalcon.Revelator.Borger;
using Redninja.Components.Actions;
using Redninja.Skills;

namespace Redninja
{
	/// <summary>
	/// Battle entity. Main class that contains all current effects and state of this character in battle.
	/// </summary>
	internal class BattleEntity : IBattleEntity
	{
		private IClock clock;
		private readonly ICombatExecutor combatExecutor;

		public IUnit Character { get; }

		public int Team { get; set; }
		public bool IsPlayerControlled => ActionDecider.IsPlayer;
		public EntityPosition Position { get; private set; } = new EntityPosition(1);

		public IWeaponAttack Attack { get; }
		public IEnumerable<ISkill> Skills { get; }

		// If we add an action queue here, this will point to the top instead
		public IBattleAction CurrentAction { get; private set; }
		public ActionPhase Phase => CurrentAction?.Phase ?? ActionPhase.Waiting;
		public float PhasePercent => CurrentAction?.PhaseProgress ?? 0;

		public IActionDecider ActionDecider { get; set; }

		public event Action<IBattleEntity> DecisionRequired;

		public BattleEntity(IUnit character, IActionDecider actionDecider, ICombatExecutor combatExecutor)
		{
			this.combatExecutor = combatExecutor;

			Character = character;
			ActionDecider = actionDecider;
		}

		public BattleEntity(IUnit character, IActionDecider actionDecider, ICombatExecutor combatExecutor, ISkillProvider skillProvider)
			: this(character, actionDecider, combatExecutor)
		{
			Attack = skillProvider.GetAttack(Character.Class, Character.Equipment.GetAllEquipmentForSlot(EquipmentType.Weapon).Select(e => e as IWeapon));
			Skills = new List<ISkill>(skillProvider.GetSkills(Character.Class, Character.Level));
		}

		public void InitializeBattlePhase()
		{
			combatExecutor.InitializeEntity(this);
		}

		public void SetAction(IBattleAction action)
		{
			if (CurrentAction != null)
				CurrentAction.Dispose();

			CurrentAction = action;
			CurrentAction.SetClock(clock);
			CurrentAction.Start();
		}

		public void MovePosition(int row, int col)
			=> Position = new EntityPosition(row, col, Position.Size);

		private void OnTick(float timeDelta)
		{
			// Check for buff update interval, then update buffs/status effects

			if (CurrentAction.Phase == ActionPhase.Done)
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

			if (CurrentAction != null)
			{
				CurrentAction.Dispose();
				CurrentAction = null;
			}
		}
		#endregion
	}
}
