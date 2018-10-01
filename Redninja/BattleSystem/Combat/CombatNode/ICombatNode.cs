namespace Redninja.BattleSystem.Combat.CombatNode
{
    /// <summary>
    /// Combat Node interface chat contains flags and property values.
    /// </summary>
    public interface ICombatNode
    {
        float GetProperty(CombatPropertyType property);
        float GetPropertyScalar(CombatPropertyType property);
        bool HasFlag(CombatFlag flag);
    }
}


