using System;
using System.Collections.Generic;
using System.Linq;
using Davfalcon;
using Davfalcon.Randomization;
using Redninja.Components.Actions;
using Redninja.Components.Clock;
using Redninja.Components.Combat;
using Redninja.Components.Decisions.AI;
using Redninja.Components.Properties;

namespace Redninja.Entities
{
	/// <summary>
	/// Battle entity. Main class that contains all current effects and state of this character in battle.
	/// </summary>
	internal class BattleEntity : IBattleEntity
	{
		private readonly IUnit unit;
		private readonly IBattleContext context;
		private readonly ICombatExecutor combatExecutor;
		private readonly IDictionary<Enum, IUnitComponent<IUnit>> components = new Dictionary<Enum, IUnitComponent<IUnit>>();

		private IUnit Unit => unit.AsModified();

		#region Unit interface
		public string Name => Unit.Name;
		public IStatsProperties Stats => Unit.Stats;
		public IModifierStack<IUnit> Modifiers => Unit.Modifiers;
		TComponent IUnitTemplate<IUnit>.GetComponent<TComponent>(Enum id)
		{
			if (components.ContainsKey(id)) return components[id] as TComponent;
			else return Unit.GetComponent<TComponent>(id);
		}
		#endregion

		// Maybe we can back these with VolatileStats
		public int Team { get; set; }
		public UnitPosition Position { get; private set; } = new UnitPosition(1);

		public IUnitActionManager Actions { get; private set; }

		// TODO pull properties from equipment, buffs, class def
		public IEnumerable<ITriggeredProperty> TriggeredProperties => Enumerable.Empty<ITriggeredProperty>();

		public BattleEntity(IBattleContext context, IUnit unit)
		{
			this.context = context;
			this.unit = unit;

			// maybe we don't need this?
			this.combatExecutor = context.CombatExecutor;
			combatExecutor.EntityMoving += OnEntityMoving;

			Actions = new UnitActionManager(context, this);
			// buff manager has to be connected too
		}

		// Considering raising this stuff to BEM
		private void OnEntityMoving(IBattleEntity entity, Coordinate c)
		{
			if (entity == this) MovePosition(c.Row, c.Column);
		}

		public void InitializeBattlePhase()
		{
			combatExecutor.InitializeEntity(this);

			Actions.SetAction(new WaitAction(new RandomInteger(1, 10).Get()));
		}

		public void MovePosition(int row, int col)
			=> Position = new UnitPosition(row, col, Position.Size);

		public void Dispose()
		{
			combatExecutor.CleanupEntity(this);
			combatExecutor.EntityMoving -= OnEntityMoving;

			Actions.Dispose();
		}

		public void SetAIBehavior(AIRuleSet ruleSet)
		{
			Actions = new AIUnitActionManager(context, this, ruleSet);
		}
	}
}
