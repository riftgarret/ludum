using System.Collections.Generic;

namespace Redninja.Components.Actions.Decisions.AI
{
	public interface IAITargetPriority
	{				
		IBattleEntity GetBestTarget(IEnumerable<IBattleEntity> validEntities);
	}
}