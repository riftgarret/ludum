using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Components.Combat.Events.Triggers
{
	public interface IEventTrigger : IOperationSource
	{
		void ReadEvent(IBattleEntity self, ICombatEvent e, IBattleContext context);
	}
}
