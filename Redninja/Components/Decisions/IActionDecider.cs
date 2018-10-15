using System;
using Redninja.Components.Actions;

namespace Redninja.Components.Decisions
{
	public interface IActionDecider
	{
		/// <summary>
		/// Considering removing this property, try to avoid using it. Better for the presenter to be agnostic about who's controlling the char.
		/// </summary>
		bool IsPlayer { get; }

		event Action<IUnitModel, IBattleAction> ActionSelected;

		void ProcessNextAction(IUnitModel entity, IBattleModel battleModel);
	}
}
