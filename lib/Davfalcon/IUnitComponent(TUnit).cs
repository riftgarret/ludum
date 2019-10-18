namespace Davfalcon
{
	public interface IUnitComponent<TUnit> where TUnit : class, IUnitTemplate<TUnit>
	{
		void Initialize(TUnit unit);
	}
}
