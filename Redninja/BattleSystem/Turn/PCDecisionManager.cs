using System.Collections;
using System.Collections.Generic;
using Redninja.BattleSystem.Entities;
using Redninja.BattleSystem.Targeting;
using Redninja.BattleSystem.Actions;
using Redninja.Core.Skills;
using Redninja.Util;

namespace Redninja.BattleSystem.Turn
{
    /// <summary>
    /// This class manages the state when the player needs to select a skill and targets.
    /// </summary>
    public class PCDecisionManager
    {

        public enum DecisionState
        {
            IDLE,
            SKILL,
            TARGET
        }

        private Queue<PCBattleEntity> turnQueue;
        public delegate void OnActionSelected(BattleEntity source, IBattleAction action);

        /// <summary>
        /// On completing selecting a skill and targets, this will indicate the action is created.
        /// </summary>
        public OnActionSelected OnActionSelectedDelegate { get; set; }

        public PCDecisionManager()
        {
            turnQueue = new Queue<PCBattleEntity>();
            this.currentSelectedSkill = null;
            this.decisionState = DecisionState.IDLE;
        }

        /// <summary>
        /// Gets the state of the decision. Either selecting a skill, or targeting with selected skill
        /// </summary>
        /// <value>The state of the decision.</value>
        public DecisionState decisionState
        {
            private set;
            get;
        }

        /// <summary>
        /// Gets the current selected skill. Will be null if not selected
        /// </summary>
        /// <value>The current selected skill.</value>
        public ICombatSkill currentSelectedSkill
        {
            private set;
            get;
        }

        /// <summary>
        /// Gets the current target list.
        /// </summary>
        /// <value>The current target list.</value>
        public SelectableTargetManager currentTargetManager
        {
            private set;
            get;
        }

        /// <summary>
        /// Queues the PC into the turn list.
        /// </summary>
        /// <param name="pc">Pc.</param>
        public void QueuePC(PCBattleEntity pc)
        {
            bool isNewPC = (turnQueue.Count == 0);
            turnQueue.Enqueue(pc);
            // if we see we are the added new pc, lets make sure we are in
            // the correct decision state
            if (isNewPC)
            {
                decisionState = DecisionState.SKILL;
                currentSelectedSkill = null;
            }
        }

        /// <summary>
        /// Selects next character (if there are any) for the next turn
        /// </summary>
        public void NextTurn()
        {
            PCBattleEntity cur = turnQueue.Dequeue();
            turnQueue.Enqueue(cur);
            decisionState = DecisionState.SKILL;
            currentSelectedSkill = null;
        }

        /// <summary>
        /// Set the action to the current top battle entity selected.
        /// </summary>
        /// <param name="action">Action.</param>
        public void SelectSkill(ICombatSkill skill, BattleEntityManager entityManager)
        {
            if (turnQueue.Count == 0)
            {
                // do nothing bad state
                RLog.E(this, "Bad state, PCTurnManager.SelectSkill when no PC available");
                return;
            }

            currentSelectedSkill = skill;
            currentTargetManager = SelectableTargetManager.CreateAllowedTargets(turnQueue.Peek(), entityManager, skill);
            decisionState = DecisionState.TARGET;
        }

        //
        public void SelectTarget(SelectableTarget target, BattleEntityManager entityManager)
        {
            if (turnQueue.Count == 0)
            {
                // do nothing bad state
                RLog.E(this, "Bad state, PCTurnManager.SelectSkill when no PC available");
                return;
            }

            PCBattleEntity sourceEntity = turnQueue.Dequeue();
            ITargetResolver targetResolver = TargetResolverFactory.CreateTargetResolver(sourceEntity, target, entityManager);
            IBattleAction action = BattleActionFactory.CreateBattleAction(currentSelectedSkill, sourceEntity, targetResolver);
            OnActionSelectedDelegate.Invoke(sourceEntity, action);
            //        (sourceEntity, action);		
            currentSelectedSkill = null;
            decisionState = (turnQueue.Count > 0 ? DecisionState.SKILL : DecisionState.IDLE);
        }

        /// <summary>
        /// Gets the current PC Battle Entity.
        /// </summary>
        /// <value>The current entity.</value>
        public PCBattleEntity currentEntity
        {
            get
            {
                if (turnQueue.Count > 0)
                {
                    return turnQueue.Peek();
                }
                return null;
            }
        }


    } 
}
