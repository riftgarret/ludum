using App.BattleSystem.Combat.CombatNode;
using App.BattleSystem.Combat.Operation.Result;
using App.BattleSystem.Effects;
using App.BattleSystem.Entity;


namespace App.BattleSystem.Combat.Operation
{
    /// <summary>
    /// Create CombatOperation depending on round parameters.  
    /// </summary>
    public class CombatOperationFactory
    {

        public static ICombatOperation createOperation(BattleEntity src, BattleEntity dest, CombatRound combatRound)
        {
            EntityCombatResolver srcRes = CombatResolverFactory.CreateSource(src, combatRound);
            EntityCombatResolver destRes = CombatResolverFactory.CreateDestination(dest);

            HitChanceLogic hitChanceLogic = new HitChanceLogic();
            DamageLogic damageLogic = new DamageLogic();

            CombatOperation.Builder builder = new CombatOperation.Builder();
            builder.AddLogic(damageLogic)
                .Require(delegate (ICombatLogic[] conditions)
                {
                    HitChanceLogic hitChance = (HitChanceLogic)conditions[0];
                    return hitChance.Hits;
                }, hitChanceLogic);

            AddHitChanceStatusEffectRules(builder, hitChanceLogic, combatRound);


            return builder.Build(srcRes, destRes);
        }


        private static void AddHitChanceStatusEffectRules(CombatOperation.Builder builder, HitChanceLogic hitChance, CombatRound combatRound)
        {
            if (combatRound.statusEffectRules == null)
            {
                return;
            }

            foreach (StatusEffectRule rule in combatRound.statusEffectRules)
            {
                //	builder.AddLogic(
            }
        }
    } 
}
