﻿namespace Redninja.Components.Actions
{
	public class WaitAction : BattleActionBase
    {        
        public WaitAction(float time)
			: base("Wait", 0, 0, time)
        {
		}

		protected override void ExecuteAction(float timeDelta, float time)
        {
            // does nothing
        }
    } 
}
