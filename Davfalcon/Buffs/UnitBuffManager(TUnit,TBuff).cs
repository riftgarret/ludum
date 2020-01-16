using System.Collections.Generic;
using System.Linq;
using Davfalcon.Stats;

namespace Davfalcon.Buffs
{
	public class UnitBuffManager<TUnit, TBuff> : StatsProviderTemplate,
		IUnitBuffManager<TUnit, TBuff>,
		IUnitComponent<TUnit>
		where TUnit : class, IUnitTemplate<TUnit>
		where TBuff : class, IBuff<TUnit>
	{
		private IList<TBuff> buffs = new List<TBuff>();

		public TBuff[] GetAllBuffs() => buffs.ToArray();

		public void Add(TBuff buff)
		{
			buffs.Add(buff);
			statsProvider.AddSource(buff);
		}

		public void Remove(TBuff buff)
		{
			buffs.Remove(buff);
			statsProvider.RemoveSource(buff);
		}

		public void RemoveAt(int index)
		{
			var buff = buffs[index];
			buffs.RemoveAt(index);
			statsProvider.RemoveSource(buff);
		}

		public virtual void Initialize(TUnit unit) { }
	}
}
