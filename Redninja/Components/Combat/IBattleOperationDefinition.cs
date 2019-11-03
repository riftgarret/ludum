using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redninja.Components.Targeting;

namespace Redninja.Components.Combat
{
	public interface IBattleOperationDefinition
	{
		float ExecutionStart { get; }
		IBattleOperation CreateOperation(IBattleEntity source, ITargetResolver target);
	}
}
