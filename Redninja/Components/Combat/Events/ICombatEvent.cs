namespace Redninja.Components.Combat.Events
{
	/// <summary>
	/// Interface for Battle Events.
	/// </summary>
	public interface ICombatEvent
	{
		IUnitModel Source { get; }
		IUnitModel Target { get; }
	} 
}

