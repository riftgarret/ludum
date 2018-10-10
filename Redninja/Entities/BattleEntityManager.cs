using System;
using System.Collections.Generic;
using System.Linq;
using Redninja.Targeting;

namespace Redninja.Entities
{
	public class BattleEntityManager : IBattleEntityManager
	{
		// TEMP for now
		private Tuple<int, int> GridSize { get; } = new Tuple<int, int>(3, 3);

		private HashSet<IBattleEntity> entityMap = new HashSet<IBattleEntity>();

		public IEnumerable<IBattleEntity> EnemyEntities => entityMap.Where(entity => !entity.IsPlayerControlled);

		public IEnumerable<IBattleEntity> PlayerEntities => entityMap.Where(entity => entity.IsPlayerControlled);

		public IEnumerable<IBattleEntity> AllEntities => entityMap;

		public event Action<IBattleEntity> DecisionRequired;

		private void OnEntityDecisionRequired(IBattleEntity entity)
			=> DecisionRequired?.Invoke(entity);

		public void AddBattleEntity(IBattleEntity entity, IClock clock)
		{
			entity.SetClock(clock);
			entity.DecisionRequired += OnEntityDecisionRequired;
			entityMap.Add(entity);
		}

		public void RemoveBattleEntity(IBattleEntity entity)
		{
			entity.Dispose();
			entityMap.Remove(entity);
		}

		/// <summary>
		/// Gets all entities within the specified pattern.
		/// </summary>
		/// <param name="anchorRow"></param>
		/// <param name="anchorColumn"></param>
		/// <param name="pattern"></param>
		/// <returns></returns>
		public IEnumerable<IBattleEntity> GetEntitiesInPattern(int anchorRow, int anchorColumn, ITargetPattern pattern)
		{
			return AllEntities.Where(entity =>
			{
				for (int r = entity.Position.Row; r <= entity.Position.Bound.Row; r++)
				{
					for (int c = entity.Position.Column; c <= entity.Position.Bound.Column; c++)
					{
						if (pattern.ContainsLocation(anchorRow, anchorColumn, r, c))
							return true;
					}
				}
				return false;
			});
		}

		public IEnumerable<IBattleEntity> GetEntitiesInPattern(Coordinate anchor, ITargetPattern pattern)
			=> GetEntitiesInPattern(anchor.Row, anchor.Column, pattern);

		/// <summary>
		/// Get Row of entities.
		/// </summary>
		public IEnumerable<IBattleEntity> GetEntitiesInRow(int anchorRow)
			=> GetEntitiesInPattern(anchorRow, 0, TargetPatternFactory.CreateRowPattern());

		/// <summary>
		/// Initialize the battle phase, this sets the initial 'Initiative action' 
		/// </summary>
		public void InitializeBattlePhase() => AllEntities.ToList().ForEach(unit => unit.InitializeBattlePhase());

		/// <summary>
		/// Set the action for this entity.
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="action"></param>
		public void SetAction(IBattleEntity entity, IBattleAction action)
			=> entity.SetAction(action);
	}
}