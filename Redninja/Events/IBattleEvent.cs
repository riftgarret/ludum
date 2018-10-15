namespace Redninja.Events
{
	/// <summary>
	/// Interface for Battle Events.
	/// </summary>
	public interface IBattleEvent
	{
		IUnitModel Entity { get; }
    } 
}

