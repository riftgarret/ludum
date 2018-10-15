using System.Collections.Generic;

namespace Redninja.Decisions.AI
{
	public interface IAITargetPriority
	{				
		IBattleEntity GetBestTarget(IEnumerable<IBattleEntity> validEntities);
	}
}