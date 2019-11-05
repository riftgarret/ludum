using System;
using System.Collections.Generic;
using System.Linq;
using Davfalcon;
using Davfalcon.Randomization;
using Redninja.Components.Actions;
using Redninja.Components.Buffs;
using Redninja.Components.Combat;
using Redninja.Components.Decisions;
using Redninja.Components.Properties;
using System.Linq;
using Redninja.Components.Decisions.AI;
using Redninja.Components.StatCalculators;
using Davfalcon.Stats;

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

		private IUnit ModifiedUnit => Modifiers.AsModified();

		public string Name => ModifiedUnit.Name;

		public IStatsProperties Stats => ModifiedUnit.Stats;

		public IModifierStack<IUnit> Modifiers { get; } = new ModifierStack<IUnit>();

		TComponent IUnitTemplate<IUnit>.GetComponent<TComponent>(Enum id)
		{
			switch (id) {
				case VolatileUnitComponents.Actions:
					return Actions as TComponent;
				case VolatileUnitComponents.Buffs:
					return Buffs as TComponent;
				default:
					return ModifiedUnit.GetComponent<TComponent>(id);
			}
		}

		// Maybe we can back these with VolatileStats
		public int Team { get; set; }
		public UnitPosition Position { get; private set; } = new UnitPosition(1);

		private IUnitActionManager _actions;
		public IUnitActionManager Actions
		{
			get => _actions;
			private set
			{
				if (_actions != null)
				{
					_actions.Dispose();
					_actions.ActionNeeded -= HandleActionNeeded;
					_actions.ActionSet -= HandleActionSet;
				}
				_actions = value;
				if(_actions != null)
				{
					_actions.ActionNeeded += HandleActionNeeded;
					_actions.ActionSet += HandleActionSet;
				}
			}
		}

		private void HandleActionSet(IBattleEntity e, IOperationSource o) => ActionSet?.Invoke(e, o);
		private void HandleActionNeeded(IBattleEntity e) => ActionNeeded?.Invoke(e);

		public IUnitBuffManager Buffs { get; } = new UnitBuffManager();

		// TODO pull properties from equipment, buffs, class def
		public IEnumerable<ITriggeredProperty> TriggeredProperties => Enumerable.Empty<ITriggeredProperty>();

		public LiveStatContainer HP => LiveStats[LiveStat.LiveHP];
		public LiveStatContainer Resource => LiveStats[LiveStat.LiveResource];

		private readonly Dictionary<LiveStat, LiveStatContainer> liveStats = new Dictionary<LiveStat, LiveStatContainer>();

		public event Action<IBattleEntity> ActionNeeded;
		public event Action<IBattleEntity, IOperationSource> ActionSet;

		public IReadOnlyDictionary<LiveStat, LiveStatContainer> LiveStats => liveStats;

		public BattleEntity(IBattleContext context, IUnit unit)
		{
			this.context = context;
			this.unit = unit;			

			// maybe we don't need this?
			this.combatExecutor = context.CombatExecutor;
			combatExecutor.EntityMoving += OnEntityMoving;

			Actions = new UnitActionManager(context, this);
			//Buffs = new UnitBuffManager(context, this);

			// set up new modifier layer
			//Modifiers.Add(Buffs);
			Modifiers.Bind(() => unit.AsModified());

			// TODO add volatile stats component
			liveStats[LiveStat.LiveHP] = new LiveStatContainer(Stats.CalculateTotalHp());
			liveStats[LiveStat.LiveResource] = new LiveStatContainer(Stats.CalculateTotalResource());			
		}

		// Considering raising this stuff to BEM
		private void OnEntityMoving(IBattleEntity entity, Coordinate c)
		{
			if (entity == this) MovePosition(c.Row, c.Column);
		}

		public void InitializeBattlePhase()
		{
			//combatExecutor.InitializeEntity(this);

			Actions.SetAction(new WaitAction(new RandomInteger(1, 10).Get()));
		}

		// remove this probably
		public void MovePosition(int row, int col)
			=> Position = new UnitPosition(row, col, Position.Size);

		public void Dispose()
		{
			//combatExecutor.CleanupEntity(this);
			combatExecutor.EntityMoving -= OnEntityMoving;

			Actions.Dispose();
			//Buffs.Dispose();
		}

		public void SetAIBehavior(AIRuleSet ruleSet)
		{
			Actions = new AIUnitActionManager(context, this, ruleSet);
		}
	}
}
