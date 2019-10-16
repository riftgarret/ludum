namespace Redninja.Components.Combat.Events
{
	/// <summary>
	/// Interface for Battle Events.
	/// </summary>
	public interface ICombatEvent
	{
		IBattleEntity Source { get; }
		IBattleEntity Target { get; }
	} 
}

