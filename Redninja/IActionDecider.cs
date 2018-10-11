using System;

namespace Redninja
{
	public interface IActionDecider
	{
		/// <summary>
		/// Considering removing this property, try to avoid using it. Better for the presenter to be agnostic about who's controlling the char.
		/// </summary>
		bool IsPlayer { get; }

		event Action<IBattleEntity, IBattleAction> ActionSelected;

		void ProcessNextAction(IBattleEntity entity, IBattleEntityManager entityManager);
	}
}
