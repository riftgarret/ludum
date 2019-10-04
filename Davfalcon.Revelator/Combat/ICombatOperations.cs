namespace Davfalcon.Revelator.Combat
{
	public interface ICombatOperations : IStatsOperations
	{
		int CalculateHitChance(int hit, int dodge);
		int CalculateCritChance(int crit, int dodge);
		int CalculateCritDamage(int critMultiplier, int damage);
	}
}
