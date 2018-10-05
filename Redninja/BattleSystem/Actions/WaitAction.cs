using Redninja.Core.Skills;

namespace Redninja.BattleSystem.Actions
{
	public class WaitAction : BattleActionBase
    {        
        public WaitAction(float time)
			: base(0, 0, time)
        {
            SetPhase(PhaseState.Recovering);
		}

		protected override void ExecuteAction(float timeDelta, float time)
        {
            // does nothing
        }
    } 
}
