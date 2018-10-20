using System;
using System.Collections.Generic;
using System.Linq;
using Redninja.Components.Actions;
using Redninja.Components.Clock;

namespace Redninja.Entities
{
	internal class BattleEntityManager : IBattleEntityManager
	{
		private IClock clock;
		private readonly Dictionary<int, Coordinate> grids = new Dictionary<int, Coordinate>();
		private readonly HashSet<IBattleEntity> entityMap = new HashSet<IBattleEntity>();

		public float Time => clock.Time;

		public IEnumerable<IBattleEntity> Entities => entityMap;
		IEnumerable<IUnitModel> IBattleModel.Entities => Entities;

		public event Action<IBattleEntity> ActionNeeded;
		public event Action<IBattleEntity, IBattleAction> ActionSet;

		public BattleEntityManager(IClock clock)
		{
			SetClock(clock);
		}

		public Coordinate GetGridSizeForTeam(int team) => grids[team];

		public void AddGrid(int team, Coordinate size) => grids[team] = size;

		public void AddEntity(IBattleEntity entity)
		{			
			entity.SetClock(clock);
			entity.ActionNeeded += ActionNeeded;
			entity.ActionSet += ActionSet;
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
