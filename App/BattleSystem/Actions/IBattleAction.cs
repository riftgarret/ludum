
namespace App.BattleSystem.Action
{
    public interface IBattleAction
    {
        /// <summary>
        /// Important to note action clock should always be called even when the delta time has passed.
        /// the action time threshold, it will be called one last time
        /// </summary>
        /// <param name="actionClock">Action clock.</param>
        void OnExecuteAction(float actionClock);

        float TimePrepare { get; }
        float TimeAction { get; }
        float TimeRecover { get; }
    }
}


