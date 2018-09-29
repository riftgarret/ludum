using UnityEngine;
using System.Collections;
using App.BattleSystem.Entity;
using App.BattleSystem.Combat.Operation;
using App.BattleSystem.Targeting;
using App.Core.Skills;

namespace App.BattleSystem.Actions
{
    public class BattleActionSkill : BaseBattleAction
    {
        // time it takes to prepare, configured and set
        public override float TimePrepare => combatSkill.TimePrepare;
        public override float TimeAction => combatSkill.TimeAction;
        public override float TimeRecover => combatSkill.TimeRecover;

        private ICombatSkill combatSkill;
        private ITargetResolver targetResolver;
        private BattleEntity sourceEntity;

        private int combatRoundCount;
        private int combatRoundIndex;

        public BattleActionSkill(ICombatSkill skill, BattleEntity sourceEntity, ITargetResolver targetResolver)
        {
            this.combatSkill = skill;
            this.sourceEntity = sourceEntity;
            this.targetResolver = targetResolver;
            this.combatRoundIndex = 0;
            this.combatRoundCount = skill.CombatRounds.Length;
            SetPhase(PhaseState.PREPARE);
        }

        /// <summary>
        /// Important to note action clock should always be called even when the delta time has passed.
        /// the action time threshold, it will be called one last time
        /// </summary>
        /// <param name="actionClock">Action clock.</param>
        protected override void ExecuteAction(float actionClock)
        {
            float triggerValue = (float)(combatRoundIndex + 1) / (float)combatRoundCount;

            // while we have passed next click for the action clock (incase huge delta change)
            while (triggerValue <= actionClock)
            {
                DoCombatRound(combatRoundIndex);
                combatRoundIndex++;
                triggerValue = (float)(combatRoundIndex + 1) / (float)combatRoundCount;
            }
        }

        /// <summary>
        /// Dos the combat round. For each target, create a execution of attack or action
        /// </summary>
        /// <param name="index">Index.</param>
        private void DoCombatRound(int index)
        {
            foreach (BattleEntity targetEntity in targetResolver.GetTargets(combatSkill))
            {
                ICombatOperation combatOperation = CombatOperationFactory.createOperation(sourceEntity, targetEntity, combatSkill.CombatRounds[index]);
                ExecuteCombatOperationDelegate?.Invoke(combatOperation);
            }
        }
    }
}
