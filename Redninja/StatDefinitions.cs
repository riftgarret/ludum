namespace Redninja
{
	public enum Stat
    {
		// basic stats
        HP,
		HPScale,
		DEF,
		ATK,
        Resource,
		STR,
		STRScale,
		CON,
		AGI,
		DEX,
		INT,
		WIS,
		CHA,
		LUK,
		Level,

		// Bleed 	
		BleedDamageConst,
		BleedDamageScale,
		BleedReduction,

		// Physical base stats
		PhysicalDamage,
		PhysicalDamageScale,
		PhysicalDamagePenetration,
		PhysicalDaggerDamage,
		FlagPhysicalDamageAlsoUsesCon,

		PhysicalDamageResistance,
		PhysicalDamageReduction,
		PhysicalDamageReductionCap,
		


		HpLevelScale,
	}

	public enum WeaponType
	{
		Dagger,
		Sword
	}

	public enum DamageType
	{
		Physical,
		Fire,
		Bleed
	}

	public enum CalculatedStat
	{
		HP,
		Resource,
		PhysicalDamage,
		PhysicalReduction,
		PhysicalResistance,
		PhysicalPenetration,
	}

	public enum StatModType
	{
		Additive, Scaling
	}
}
