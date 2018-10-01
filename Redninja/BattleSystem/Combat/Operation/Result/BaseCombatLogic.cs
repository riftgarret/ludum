using Redninja.Util;


namespace Redninja.BattleSystem.Combat.Operation.Result
{
    /// <summary>
    /// Base logic to ensure execution is not done twice.
    /// </summary>
    public class BaseCombatLogic
    {
        private bool executed = false;

        protected void CheckExecute()
        {
            if (executed == true)
            {
                Debug.LogError("Object has already been executed, illegal execute action: " + this);
                throw new IllegalStateException("Object has already been executed, illegal execute action: " + this);
            }
            executed = true;
        }

        public bool IsExecuted { get { return executed; } }
    } 
}
