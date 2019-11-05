namespace Davfalcon.Buffs
{
	public interface IBuff<TUnit> : IStatsModifier<TUnit> where TUnit : class, IUnitTemplate<TUnit>
	{
		bool IsDebuff { get; }

		float Duration { get; }

		float Remaining { get; set; }
	}
}
