namespace Redninja
{
	public enum Stat
    {	
		// Core stats
		Level,

		// volatile stats
		HP,
		HPConScale,
		HPLevelScale,
		Stamina,
		StaminaStrScale,
		StaminaLevelScale,
		Mana,
		ManaIntScale,
		ManaLevelScale,

		// attribute stats		
		STR,		
		CON,
		AGI,
		DEX,
		INT,
		WIS,

		// WeaponTypes
		WeaponTypeDagger,
		WeaponTypeSword,

		// Bleed 	
		BleedDamageExtra,
		BleedDamageScale,
		BleedDamageReduction,

		// Reduction base stats
		ReductionAll,	
		ReductionPhysical,
		ReductionElemental,
		ReductionFire,
		ReductionSlash,

		// Penetration
		PenetrationSlash,
		PenetrationFire,

		// Resistance
		ResistanceSlash,
		ResistanceFire,

		// Damage
		DamageAllExtra,
		DamageAllScale,

		DamagePhysicalExtra,
		DamagePhysicalScale,
		DamageElementalExtra,
		DamageElementalScale,

		DamageSlashExtra,
		DamageSlashScale,
		DamageFireExtra,
		DamageFireScale,

		// extracted stats pre-execution
		WeaponScaleEval
	}

	public enum DamageType
	{
		Slash,
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
		Zero,
		HP,
		Mana,
		Stamina,
		Level,
		SlashDamage,
		SlashReduction,
		SlashResistance,
		SlashPenetration,
		FireDamage,
		FireReduction,
		FireResistance,
		FirePenetration
	}

	public enum LiveStat
	{
		HP,
		Mana,
		Stamina,
	}

	public enum StatModType
	{
		Additive, Scaling
	}
}
