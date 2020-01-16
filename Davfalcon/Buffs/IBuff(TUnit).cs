namespace Davfalcon.Buffs
{
	public interface IBuff<TUnit> : IStatSource where TUnit : class, IUnitTemplate<TUnit>
	{
		bool IsDebuff { get; }

		float Duration { get; }

		float Remaining { get; set; }
	}
}
