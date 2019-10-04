using App.BattleSystem.Targeting;

namespace App.Core.Skills
{

    public interface ICombatSkill : ISkill
    {
        float TimePrepare { get; }
        float TimeAction { get; }
        float TimeRecover { get; }

        TargetingRule TargetRule { get; }
        CombatRound[] CombatRounds { get; }
    }
}