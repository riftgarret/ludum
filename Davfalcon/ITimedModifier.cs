namespace Davfalcon
{
	/// <summary>
	/// Exposes properties, events, and functions for dynamic modifiers such as buffs and debuffs.
	/// </summary>
	public interface ITimedModifier : IStatsModifier
	{
		/// <summary>
		/// Gets the total duration of the modifier.
		/// </summary>
		int Duration { get; set; }

		/// <summary>
		/// Gets the remaining duration of the modifier.
		/// </summary>
		int Remaining { get; set; }

		/// <summary>
		/// Resets the modifier's timer.
		/// </summary>
		void Reset();

		/// <summary>
		/// Ticks the modifier. Call this on each time unit.
		/// </summary>
		/// <returns><c>true</c> if the modifier's remaining time has run out; otherwise, <c>false</c>.</returns>
		bool Tick();
	}
}
