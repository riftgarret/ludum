using Redninja.Skills;
using Redninja.Targeting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.Decisions
{
    public class AvailableTargetsResult
    {
        IBattleEntity Entity { get; }        
        ICombatSkill Skill { get; }
        TargetType TargetType => Skill.TargetRule.TargetType;

        private DecisionManager decisionManager;
        private IBattleEntityManager entityManager;        

        internal AvailableTargetsResult(
            DecisionManager decisionManager,
            IBattleEntity entity,
            ICombatSkill skill,
            IBattleEntityManager entityManager)
        {
            this.decisionManager = decisionManager;
            this.entityManager = entityManager;            
            Skill = skill;
            Entity = entity;            
        }

        /// <summary>
        /// Is this entity selectable?
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsValidTargetForRule(IBattleEntity entity) 
            => Skill.TargetRule.IsValidTarget(entity);

        public bool IsInPattern(int anchorRow, int anchorColumn, int targetRow, int targetColumn)
            => Skill.CombatRounds.First(round => round.Pattern.IsInPattern(anchorRow, anchorColumn, targetRow, targetColumn)) != null;

        /// <summary>
        /// Select target and return evaluated Battle Action
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public IBattleAction SelectTarget(IBattleEntity target)
            => decisionManager.CreateAction(Entity, Skill, SelectedTarget.CreateEntityTarget(target));


        /// <summary>
        /// Select target and return evaluated Battle Action
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public IBattleAction SelectTarget(int anchorRow, int anchorColumn, int team)
            => decisionManager.CreateAction(Entity, Skill, SelectedTarget.CreatePositionTarget(anchorRow, anchorColumn, team));
    }
}
