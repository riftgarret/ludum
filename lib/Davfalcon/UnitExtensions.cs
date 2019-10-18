namespace Davfalcon
{
	public static class UnitExtensions
	{
		public static TUnit AsModified<TUnit>(this TUnit unit) where TUnit : class, IUnitTemplate<TUnit>
			=> unit.Modifiers.AsModified();
	}
}
