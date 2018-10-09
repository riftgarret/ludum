using System;
using System.Linq;
using Davfalcon.Revelator;
using Davfalcon.Revelator.Borger;
using Redninja.Actions;

namespace Redninja.ConsoleDriver
{
	public class PlayerInput : IActionDecider
	{
		private readonly IWeapon weapon = new Weapon.Builder(EquipmentType.Weapon, WeaponType.Sword)
			.SetName("Longsword")
			.SetDamage(20)
			.AddDamageType(DamageType.Physical)
			.Build();

		public bool IsPlayer => true;

		public event Action<IBattleEntity, IBattleAction> ActionSelected;

		public void ProcessNextAction(IBattleEntity entity, IBattleEntityManager entityManager)
		{
			Console.WriteLine("Waiting for player input...");
			ConsoleKey key = Console.ReadKey().Key;

			IBattleAction action = null;

			switch (key)
			{
				case ConsoleKey.A:
					if (entityManager.EnemyEntities.Count() > 0)
						action = new AttackAction(entity, entityManager.EnemyEntities.First(), weapon, 0.25f, 0.5f, 0.75f);
					break;
				case ConsoleKey.M:
					action = new MovementAction(entity, entity.Position.Row + 1, entity.Position.Column + 1);
					break;
				default:
					action = new WaitAction(5);
					break;
			}

			ActionSelected?.Invoke(entity, action);
		}
	}
}
