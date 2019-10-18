using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redninja.Components.Actions;
using Redninja.Components.Clock;
using Redninja.Components.Combat;

namespace Redninja.Components.Buffs
{
	public interface IBuffExecutionBehavior : IOperationSource
	{
		void OnClockTick(float delta, ActiveBuff activeBuff);
	}
}
