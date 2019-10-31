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

		// WeaponTypes
		WeaponTypeDagger,
		WeaponTypeSword,

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

	public enum DamageType
	{
		Physical,
		Fire,
		Bleed
	}

	public enum WeaponSlotType
	{
		Any,
		OneHanded,
		TwoHanded,
		OffHand,		
	}

	public enum WeaponType
	{
		Any,
		Dagger,
		Sword,
		GreatSword,
		Mace
	}

	public enum CalculatedStat
	{
		HPTotal,
		ResourceTotal,
		PhysicalDamageTotal,
		PhysicalReductionTotal,
		PhysicalResistanceTotal,
		PhysicalPenetrationTotal,
	}

	public enum LiveStat
	{
		LiveHP,
		LiveResource
	}

	public enum StatModType
	{
		Additive, Scaling
	}
}
