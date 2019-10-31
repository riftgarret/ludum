using System;
using Davfalcon;
using Davfalcon.Buffs;

namespace Redninja.Components.Buffs
{
	[Serializable]
	public class UnitBuffManager : UnitBuffManager<IUnit, IBuff>, IUnitBuffManager, IUnitComponent<IUnit>
	{
		public void AddBuff(IBuff buff)
		{
			Add(buff);
			buff.Expired += RemoveBuff;
		}

		public void RemoveBuff(IBuff buff)
		{
			Remove(buff);
			buff.Dispose();
		}

		void IUnitBuffManager<IUnit, IBuff>.Add(IBuff buff) => AddBuff(buff);
		void IUnitBuffManager<IUnit, IBuff>.Remove(IBuff buff) => RemoveBuff(buff);
	}

	public static class UnitExtension
	{
		public static IUnitBuffManager GetBuffManager(this IUnit unit)
			=> unit.GetComponent<IUnitBuffManager>(UnitComponents.Buffs);
	}
}
