namespace Redninja.BattleSystem.Actions
{
	public class InitiativeAction : BaseBattleAction
    {        
        public InitiativeAction(float initiativeTime)
			: base(0, 0, initiativeTime)
        {
            SetPhase(PhaseState.Recovering);
        }        

        protected override void ExecuteAction(float timeDelta, float time)
        {
            // does nothing
        }
    } 
}
