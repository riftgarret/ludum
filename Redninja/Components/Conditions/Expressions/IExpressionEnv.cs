using Redninja.Events;

namespace Redninja.Components.Conditions.Expressions
{
    internal interface IExpressionEnv
    {
        IUnitModel Self { get; }
        IUnitModel Target { get; }
		IUnitModel Source { get; }
		IBattleModel BattleModel { get; }
        IBattleEvent BattleEvent { get; }
    }
}