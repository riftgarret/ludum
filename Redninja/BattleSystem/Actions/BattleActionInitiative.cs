using System.Collections;
using Redninja.BattleSystem.Entities;

namespace Redninja.BattleSystem.Actions
{
    public class BattleActionInitiative : BaseBattleAction
    {        
        private float recoverTime;

        public BattleActionInitiative(float initiativeTime)
        {
            recoverTime = initiativeTime;
            SetPhase(PhaseState.Recovering);
        }        

        protected override void ExecuteAction(float actionClock)
        {
            // does nothing
        }

        public override float TimePrepare => 0;

        public override float TimeAction => 0;

        public override float TimeRecover => recoverTime;
    } 
}
