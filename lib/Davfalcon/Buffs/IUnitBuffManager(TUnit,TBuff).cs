namespace Davfalcon.Buffs
{
	public interface IUnitBuffManager<TUnit, TBuff> : IModifier<TUnit>, IUnitComponent<TUnit>
		where TUnit : class, IUnitTemplate<TUnit>
		where TBuff : IBuff<TUnit>
	{
		TBuff[] GetAllBuffs();
		void Add(TBuff buff);
		void Remove(TBuff buff);
		void RemoveAt(int index);
	}
}
