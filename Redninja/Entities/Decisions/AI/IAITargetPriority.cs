using System.Collections.Generic;

namespace Redninja.Entities.Decisions.AI
{
	public interface IAITargetPriority
	{				
		IEntityModel GetBestTarget(IEnumerable<IEntityModel> validEntities);
	}
}
