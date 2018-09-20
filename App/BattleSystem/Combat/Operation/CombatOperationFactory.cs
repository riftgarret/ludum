using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class CombatOperationFactory {

    public static ICombatOperation createOperation(BattleEntity src, BattleEntity dest, CombatRound combatRound) {
        CombatResolver srcRes = CombatResolverFactory.CreateSource(src, combatRound);
        CombatResolver destRes = CombatResolverFactory.CreateDestination(dest);



		HitChanceLogic hitChanceLogic = new HitChanceLogic ();
		DamageLogic damageLogic = new DamageLogic ();

		CombatOperation.Builder builder = new CombatOperation.Builder ();
		builder.AddLogic(damageLogic)
			.Require(delegate(ICombatLogic [] conditions) {
					HitChanceLogic hitChance = (HitChanceLogic) conditions[0];
					return hitChance.Hits;
			}, hitChanceLogic);			

		AddHitChanceStatusEffectRules(builder, hitChanceLogic, combatRound);


		return builder.Build(srcRes, destRes);
    }


	private static void AddHitChanceStatusEffectRules(CombatOperation.Builder builder, HitChanceLogic hitChance, CombatRound combatRound) {
		if(combatRound.statusEffectRules == null) {
			return;
		}

		foreach(StatusEffectRule rule in combatRound.statusEffectRules) {
		//	builder.AddLogic(
		}
	}
}
