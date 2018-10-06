using System;
using System.Collections.Generic;
using System.Linq;
using Redninja.Targeting;

namespace Redninja.Entities
{
	public class BattleEntityManager : IBattleEntityManager
	{
		private HashSet<IBattleEntity> entityMap = new HashSet<IBattleEntity>();

		public IEnumerable<IBattleEntity> EnemyEntities => entityMap.Where(entity => !entity.IsPlayerControlled);

		public IEnumerable<IBattleEntity> PlayerEntities => entityMap.Where(entity => entity.IsPlayerControlled);

		public IEnumerable<IBattleEntity> AllEntities => entityMap;

		public event Action<IBattleEntity> DecisionRequired;

		private void OnEntityDecisionRequired(IBattleEntity entity)
			=> DecisionRequired?.Invoke(entity);

		public void AddBattleEntity(IBattleEntity entity)
		{
			entity.DecisionRequired += OnEntityDecisionRequired;
			entityMap.Add(entity);
		}

		public void AddBattleEntity(IBattleEntity entity, IClock clock)
		{
			entity.SetClock(clock);
			AddBattleEntity(entity);
		}

		public void RemoveBattleEntity(IBattleEntity entity)
		{
			entity.Dispose();
			entityMap.Remove(entity);
		}

		/// <summary>
		///  TODO this will take a pattern and location and return all battle entities that are valid.
		/// </summary>
		/// <param name="rowIndex"></param>
		/// <param name="row"></param>
		/// <param name="column"></param>
		/// <returns></returns>
		public IEnumerable<IBattleEntity> GetPattern(int anchorRow, int anchorColumn, bool isEnemies, ITargetPattern pattern)
		{
			return (isEnemies ? EnemyEntities : PlayerEntities)
				.Where(entity =>
			{
				// need to check all squares that are within character
				EntityPosition position = entity.Position;
				for (int targetRow = 0; targetRow < position.Size; targetRow++)
				{
					for (int targetColumn = 0; targetColumn < position.Size; targetColumn++)
					{
						if (pattern.IsInPattern(anchorRow, anchorColumn, targetRow, targetColumn))
						{
							return true;
						}
					}
				}
				return false;
			});
		}

		/// <summary>
		/// Get Row of entities.
		/// </summary>
		/// <param name="anchorRow"></param>
		/// <param name="isEnemy"></param>
		/// <returns></returns>
		public IEnumerable<IBattleEntity> GetRow(int anchorRow, bool isEnemy)
		{
			return GetPattern(anchorRow, 0, isEnemy, TargetPatternFactory.CreateRowPattern());
		}

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