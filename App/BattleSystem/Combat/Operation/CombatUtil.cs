using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class CombatUtil {   
	    

	/// <summary>
	/// Calculates the damage.
	/// </summary>
	/// <returns>The damage.</returns>
	/// <param name="dmgVec">Dmg vec.</param>
	/// <param name="critVec">Crit vec.</param>
	/// <param name="defenseVec">Defense vec.</param>
	public static float CalculateDamage (ElementVector dmgVec, ElementVector critVec, ElementVector defenseVec) {
		ElementVector totalDmg = dmgVec + critVec;
		return (totalDmg * totalDmg / (totalDmg + defenseVec).Max(1)).Sum;
	}

    /// <summary>
    /// Crit damage, can scale from 0 to 1x damage depending on crit defense
    /// </summary>
    /// <param name="src"></param>
    /// <param name="dest"></param>
    /// <returns></returns>
    public static float CalculateCritDamageScale(CombatResolver src, CombatResolver dest) {        
        float critPower = src.CombatStats.critPower;
        float critDefense = dest.CombatStats.critDefense;
        float value = (critPower - critDefense) / critPower;
        return Math.Max(0, value);
    }
} 

