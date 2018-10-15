using System.Collections.Generic;

namespace Redninja.Entities.Decisions.AI
{
	public interface IAITargetPriority
	{				
		IBattleEntity GetBestTarget(IEnumerable<IBattleEntity> validEntities);
	}
}
