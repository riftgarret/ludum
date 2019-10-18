using System;
using System.Collections.Generic;
using Redninja.Components.Clock;
using Redninja.Components.Combat;
using Redninja.Components.Decisions.AI;
using Redninja.Components.Skills;

namespace Redninja.Entities
{
	public interface IBattleEntityManager : IBattleModel, IClockSynchronized
	{
		new IEnumerable<IBattleEntity> Entities { get; }

		event Action<IBattleEntity> ActionNeeded;
		event Action<IBattleEntity, IOperationSource> ActionSet;

		void SetGrid(int team, Coordinate size);
		void AddEntity(IBattleEntity entity);
		void RemoveEntity(IBattleEntity entity);
		void InitializeBattlePhase();

		void AddPlayerCharacter(IUnit character, int team, Coordinate position, ISkillProvider skillProvider);
		void AddAICharacter(IUnit character, int team, Coordinate position, AIRuleSet behavior, string nameOverride = null);
	}
}
