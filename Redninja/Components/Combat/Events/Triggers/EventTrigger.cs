using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Redninja.Components.Conditions;

namespace Redninja.Components.Combat.Events.Triggers
{
	public class EventTrigger : IEventTrigger
	{
		Type eventType;

		IEnumerable<Condition> conditions;

		public event Action<float, IBattleOperation> BattleOperationReady;

		public void ReadEvent(IBattleEntity self, ICombatEvent e, IBattleContext context)
		{
			if (e.GetType() != eventType) return;

			
		}


	}
}
