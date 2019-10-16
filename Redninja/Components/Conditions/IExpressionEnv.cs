using Redninja.Components.Combat.Events;

namespace Redninja.Components.Conditions
{
	public interface IExpressionEnv
	{
		IBattleEntity Self { get; }
		IBattleEntity Target { get; }
		IBattleEntity Source { get; }
		IBattleModel BattleModel { get; }
		ICombatEvent BattleEvent { get; }
	}
}
