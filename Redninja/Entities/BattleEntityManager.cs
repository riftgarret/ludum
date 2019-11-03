using System;
using System.Collections.Generic;
using System.Linq;
using Redninja.Components.Clock;
using Redninja.Components.Combat;
using Redninja.Components.Decisions.AI;
using Redninja.Components.Skills;

namespace Redninja.Entities
{
	internal class BattleEntityManager : IBattleEntityManager
	{
		private IBattleContext context;
		private IClock clock;		
		private readonly Dictionary<int, Coordinate> grids = new Dictionary<int, Coordinate>();
		private readonly HashSet<IBattleEntity> entityMap = new HashSet<IBattleEntity>();

		public float Time => clock.Time;

		public IEnumerable<IBattleEntity> Entities => entityMap;
		IEnumerable<IBattleEntity> IBattleModel.Entities => Entities;

		public event Action<IBattleEntity> ActionNeeded;
		public event Action<IBattleEntity, IOperationSource> ActionSet;

		public BattleEntityManager(IBattleContext context)
		{
			this.context = context;
			SetClock(context.Clock);
		}

		public void AddPlayerCharacter(IUnit character, int teamId, Coordinate position, ISkillProvider skillProvider)
		{
			IBattleEntity entity = AddCharacter(character, teamId, position);
			context.SystemProvider.SetSkillProvider(entity, skillProvider);
		}

		public void AddAICharacter(IUnit character, int teamId, Coordinate position, AIRuleSet aiBehavior, string nameOverride = null)
		{
			IBattleEntity entity = AddCharacter(character, teamId, position, nameOverride);
			// TODO this should be attached to IUnit 
			context.SystemProvider.SetSkillProvider(entity, new AISkillProvider(aiBehavior));
			entity.SetAIBehavior(aiBehavior);
		}

		private IBattleEntity AddCharacter(IUnit character, int team, Coordinate position, string nameOverride = null)
		{
			BattleEntity entity = new BattleEntity(context, character)
			{
				Team = team
			};
			entity.MovePosition(position.Row, position.Column);
			AddEntity(entity);
			return entity;
		}

		public Coordinate GetGridSizeForTeam(int team) => grids[team];

		public void SetGrid(int team, Coordinate size) => grids[team] = size;

		public void AddEntity(IBattleEntity entity)
		{
			entity.ActionNeeded += e => ActionNeeded?.Invoke(e);
			entity.ActionSet += (e, o) => ActionSet?.Invoke(e, o);
			entityMap.Add(entity);
		}

		public void RemoveEntity(IBattleEntity entity)
		{
			entity.Dispose();
			entityMap.Remove(entity);
		}

		/// <summary>
		/// Initialize the battle phase, this sets the initial 'Initiative action' 
		/// </summary>
		public void InitializeBattlePhase() => Entities.ToList().ForEach(unit => unit.InitializeBattlePhase());

		public void SetClock(IClock clock)
		{
			this.clock = clock;
		}

		public void Dispose()
		{
			if (clock != null)
			{
				clock = null;
			}

			foreach (IBattleEntity entity in Entities)
			{
				entity.Dispose();
			}
		}
	}
}
