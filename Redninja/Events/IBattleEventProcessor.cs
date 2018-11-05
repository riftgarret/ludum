using System;
using Redninja.Components.Operations;

namespace Redninja.Events
{
	internal interface IBattleEventProcessor
	{
		void ProcessEvent(IBattleEvent battleEvent);
	}
}
