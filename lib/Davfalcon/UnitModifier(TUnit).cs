using System;

namespace Davfalcon
{
	/// <summary>
	/// Abstract base class for unit modifiers.
	/// </summary>
	/// <typeparam name="TUnit">The interface used by the unit's implementation.</typeparam>
	[Serializable]
	public abstract class UnitModifier<TUnit> : Modifier<TUnit>, IUnitTemplate<TUnit>
		where TUnit : class, IUnitTemplate<TUnit>
	{
		/// <summary>
		/// Should be implemented by concrete classes to return <c>this</c>.
		/// </summary>
		protected abstract TUnit SelfAsUnit { get; }

		/// <summary>
		/// Returns a representation of the modified unit.
		/// </summary>
		/// <returns>A representation of the modified unit.</returns>
		public override TUnit AsModified() => SelfAsUnit;

		// Default passthrough behavior
		string IUnitTemplate<TUnit>.Name => Target.Name;
		IStatsProperties IUnitTemplate<TUnit>.Stats => Target.Stats;
		IModifierStack<TUnit> IUnitTemplate<TUnit>.Modifiers => Target.Modifiers;
		TComponent IUnitTemplate<TUnit>.GetComponent<TComponent>(Enum id) => Target.GetComponent<TComponent>(id);
	}
}
