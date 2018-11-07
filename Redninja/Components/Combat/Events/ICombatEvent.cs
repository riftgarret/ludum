using System.Collections.Generic;
using System.Linq;

namespace Redninja.Components.Combat.Events
{
	/// <summary>
	/// Interface for Battle Events.
	/// </summary>
	public interface ICombatEvent
	{
		IUnitModel Source { get; }
		IUnitModel Target { get; }

		ICombatEvent EventType { get; }
		
		IEnumerable<CombatEventFlag> CombatFlags { get; }

		T Get<T>(CombatEventAttribute attribute);
	}

	/// <summary>
	/// Helper methods
	/// </summary>
	public static class ICombatEventExt
	{
		public static bool HasFlag(this ICombatEvent combatEvent, CombatEventFlag flag)
			=> combatEvent.CombatFlags.Contains(flag);
	}

	public enum CombatEventType
	{
		Healing,
		Damage,
		Movement,
		StatusEffect
	}

	public enum CombatEventFlag
	{
		Death,
		Resurection,		

		StatusEffectRemoved,
		StatusEffectAdded,
	}

	public enum CombatEventAttribute
	{
		// int values
		TotalAmount,
		Amount,
		CriticalAmount,

		// UnitPosition
		OriginalPosition,
		NewPosition,

		// IStatusEffect
		StatusEffect,

		// ISkill
		Skill
	}

	
}

