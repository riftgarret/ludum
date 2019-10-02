using System;

namespace Davfalcon
{
	/// <summary>
	/// Abstract base class for unit modifiers.
	/// </summary>
	[Serializable]
	public abstract class UnitModifier : IUnitModifier, IEditableDescription
	{
		/// <summary>
		/// Gets or sets the modifier's name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the modifier's description.
		/// </summary>
		public string Description { get; set; }

		[NonSerialized]
		private IUnit target;

		/// <summary>
		/// Gets the <see cref="IUnit"/> that will be presented as the unit.
		/// </summary>
		protected virtual IUnit InterfaceUnit => Target;

		/// <summary>
		/// Gets the <see cref="IUnit"/> the modifier is bound to.
		/// </summary>
		public IUnit Target => target;

		/// <summary>
		/// Binds the modifier to an <see cref="IUnit"/>.
		/// </summary>
		/// <param name="target">The <see cref="IUnit"/> to bind the modifier to.</param>
		public virtual void Bind(IUnit target) => this.target = target;

		#region IUnit implementation
		string IUnit.Name => InterfaceUnit.Name;
		string IUnit.Class => InterfaceUnit.Class;
		int IUnit.Level => InterfaceUnit.Level;
		IStats IStatsHolder.Stats => InterfaceUnit.Stats;
		IStatsPackage IStatsHolder.StatsDetails => InterfaceUnit.StatsDetails;
		IUnitModifierStack IUnit.Modifiers => InterfaceUnit.Modifiers;
		#endregion
	}
}
