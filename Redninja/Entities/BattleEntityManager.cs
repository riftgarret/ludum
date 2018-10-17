using System;
using System.Collections.Generic;
using System.Linq;
using Redninja.Components.Clock;

namespace Redninja.Entities
{
	internal class BattleEntityManager : IBattleEntityManager
	{
		// TEMP for now
		private Tuple<int, int> GridSize { get; } = Tuple.Create(3, 3);

		private HashSet<IBattleEntity> entityMap = new HashSet<IBattleEntity>();

		public IEnumerable<IBattleEntity> Entities => entityMap;
		IEnumerable<IUnitModel> IBattleModel.Entities => Entities;

		public event Action<IBattleEntity> DecisionRequired;

		private void OnEntityDecisionRequired(IBattleEntity entity)
			=> DecisionRequired?.Invoke(entity);

		public void AddEntity(IBattleEntity entity, IClock clock)
		{
			entity.SetClock(clock);
			entity.DecisionRequired += OnEntityDecisionRequired;
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
	}
}
