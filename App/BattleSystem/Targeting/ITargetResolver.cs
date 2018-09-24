using App.BattleSystem.Entity;


namespace App.BattleSystem.Targeting
{
    public interface ITargetResolver
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="ITargetResolver"/> is valid target.
        /// </summary>
        /// <value><c>true</c> if is valid target; otherwise, <c>false</c>.</value>
        bool HasValidTargets(ICombatSkill skill);

        BattleEntity[] GetTargets(ICombatSkill skill);
    } 
}
