using System;
using Redninja.Events;

namespace Redninja.Components
{
	public interface IActiveProperty : IItemProperty
	{
		void OnBattleEvent(IBattleEvent be, IUnitModel owner, IBattleModel battleModel);
	}
}
