using System;

namespace Davfalcon.Equipment
{
	[Serializable]
	public abstract class CompoundEquipment<TUnit, TEquipmentType> : Equipment<TUnit, TEquipmentType>
		where TUnit : class, IUnitTemplate<TUnit>
		where TEquipmentType : Enum
	{
		public IModifierStack<TUnit> Modifiers { get; } = new ModifierStack<TUnit>();

		public override TUnit AsModified() => Modifiers.AsModified();

		public override void Bind(TUnit target)
		{
			base.Bind(target);
			Modifiers.Bind(SelfAsUnit);
		}
	}
}
