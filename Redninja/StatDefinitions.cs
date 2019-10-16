namespace Redninja
{
	public enum Stat
    {
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
		HpLevelScale,
	}

	public enum CalculatedStat
	{
		HP,
		Resource,
		PhysicalDamage,
		PhysicalResistance,
	}

	public enum StatModType
	{
		Additive, Scaling
	}
}
