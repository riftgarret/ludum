namespace Davfalcon.Revelator.Combat
{
	public class CombatOperations : StatsOperations, ICombatOperations
	{
		public virtual int CalculateHitChance(int hit, int dodge)
			=> hit - dodge;
		public virtual int CalculateCritChance(int crit, int dodge)
			=> crit;
		public virtual int CalculateCritDamage(int critMultiplier, int damage)
			=> damage * critMultiplier;

		protected CombatOperations() { }

		new public static CombatOperations Default { get; } = new CombatOperations();
	}
}
