using System;
using Davfalcon;
using Davfalcon.Buffs;
using Redninja.Components.Clock;

namespace Redninja.Components.Buffs
{
	[Serializable]
	public class UnitBuffManager : UnitBuffManager<IUnit, IBuff>, IUnitBuffManager, IClockSynchronized, IUnitComponent<IUnit>
	{
		private IUnit owner;
		private IClock clock;

		public override void Initialize(IUnit unit)
		{
			base.Initialize(unit);
			owner = unit;
		}

		public event Action<IBuff, IUnit> Effect;
		public event Action<IBuff> BuffExpired;

		public void AddActiveBuff(IBuff buff)
		{
			Add(buff);
			buff.Effect += b => Effect?.Invoke(b, owner);
		}

		// need to hook up buff manager to battle controller to handle events

		private void OnTick(float timeDelta)
		{
			// manager will be responsible for keeping time, buffs will not subscribe to clock
		}

		public void SetClock(IClock clock)
		{
			UnsetClock();

			this.clock = clock;
			clock.Tick += OnTick;
		}

		private void UnsetClock()
		{
			if (clock != null)
			{
				clock.Tick -= OnTick;
				clock = null;
			}
		}

		public void Dispose()
		{
			UnsetClock();
		}
	}
}
