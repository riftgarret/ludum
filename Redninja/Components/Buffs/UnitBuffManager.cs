using System;
using Davfalcon;
using Davfalcon.Buffs;

namespace Redninja.Components.Buffs
{
	[Serializable]
	public class UnitBuffManager : UnitBuffManager<IUnit, IBuff>, IUnitBuffManager, IUnitComponent<IUnit>
	{
		protected IBattleContext BattleContext { get; }

		protected IBattleEntity BattleEntity { get; }

		public event Action<IBuff, IBattleEntity> Effect;
		public event Action<IBuff> BuffExpired;

		public UnitBuffManager(IBattleContext context, IBattleEntity entity)
		{
			BattleContext = context;
			BattleEntity = entity;

			BattleContext.Clock.Tick += OnTick;
		}

		public void AddActiveBuff(IBuff buff)
		{
			Add(buff);
			buff.Effect += b => Effect?.Invoke(b, BattleEntity);
		}

		private void OnTick(float timeDelta)
		{
			// manager will be responsible for keeping time, buffs will not subscribe to clock
		}

		private void UnsetClock()
		{
			if (BattleContext != null)
			{
				BattleContext.Clock.Tick -= OnTick;
			}
		}

		public void Dispose()
		{
			UnsetClock();
		}
	}
}
