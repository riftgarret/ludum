using System;

/// <summary>
/// Combat round. To be serialized in setting up combat skills
/// </summary>
[Serializable]
public class CombatRound
{
    public int weaponIndex;
	public CombatOperationType operationType = CombatOperationType.PHYSICAL_ATTACK;
	public StatusEffectRule [] statusEffectRules = new StatusEffectRule[0];
	public CombatProperty [] combatProperties = new CombatProperty[0];
}

