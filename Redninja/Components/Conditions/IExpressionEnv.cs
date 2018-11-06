using Redninja.Components.Combat.Events;

namespace Redninja.Components.Conditions
{
	public interface IExpressionEnv
	{
		IUnitModel Self { get; }
		IUnitModel Target { get; }
		IUnitModel Source { get; }
		IBattleModel BattleModel { get; }
		IBattleEvent BattleEvent { get; }
	}
}
