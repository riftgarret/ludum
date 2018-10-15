using System;
using System.Collections.Generic;
using System.Linq;
using Redninja.Components.Actions;
using Redninja.Components.Clock;
using Redninja.Components.Targeting;

namespace Redninja.Entities
{
	internal class BattleEntityManager : IBattleEntityManager
	{
		// TEMP for now
		private Tuple<int, int> GridSize { get; } = Tuple.Create(3, 3);

		private HashSet<IBattleEntity> entityMap = new HashSet<IBattleEntity>();

		public IEnumerable<IBattleEntity> Entities => entityMap;
		IEnumerable<IEntityModel> IBattleModel.Entities => Entities;

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
		public IEnumerable<IEntityModel> GetEntitiesInPattern(int anchorRow, int anchorColumn, ITargetPattern pattern)
		{
			return Entities.Where(entity =>
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

		public IEnumerable<IEntityModel> GetEntitiesInPattern(Coordinate anchor, ITargetPattern pattern)
			=> GetEntitiesInPattern(anchor.Row, anchor.Column, pattern);

		/// <summary>
		/// Get Row of entities.
		/// </summary>
		public IEnumerable<IEntityModel> GetEntitiesInRow(int anchorRow)
			=> GetEntitiesInPattern(anchorRow, 0, TargetPatternFactory.CreateRowPattern());

		/// <summary>
		/// Initialize the battle phase, this sets the initial 'Initiative action' 
		/// </summary>
		public void InitializeBattlePhase() => Entities.ToList().ForEach(unit => unit.InitializeBattlePhase());

		/// <summary>
		/// Set the action for this entity.
		/// </summary>
		/// <param name="entity"></param>
		/// <param name="action"></param>
		public void SetAction(IBattleEntity entity, IBattleAction action)
			=> entity.SetAction(action);
	}
}
