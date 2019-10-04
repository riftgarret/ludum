using App.BattleSystem.Actions;
using App.BattleSystem.Entity;
using App.BattleSystem.Targeting;
using App.Core.Characters;
using App.Core.Skills;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.BattleSystem.AI
{
    /// <summary>
    /// AI Skill resolver is the process in which a NPC needs to have a skill calculated based
    /// on the Skill rules and weights set for conditions.
    /// </summary>
    public class AISkillResolver
    {                
        public IBattleAction ResolveAction(BattleEntityManager entityManager, EnemyBattleEntity enemyEntity)
        {
            // convert our skill rules so we can do math
            List<AISkillComposite> skillComposites = CreateSkillComposites(enemyEntity.EnemyCharacter.EnemySkillSetSO);

            // for each skill, lets evaulate if we will use it
            float totalWeight = 0;
            List<AISkillResultSet> possibleSkills = new List<AISkillResultSet>();            

            foreach (AISkillComposite composite in skillComposites)
            {
                SelectableTargetManager selectManager = SelectableTargetManager.CreateAllowedTargets(enemyEntity, entityManager, composite.skill);
                HashSet<BattleEntity> targetSet = new HashSet<BattleEntity>();
                foreach (SelectableTarget target in selectManager.TargetList)
                {
                    foreach (BattleEntity entity in target.entities)
                    {
                        targetSet.Add(entity);
                    }
                }

                // filter targets
                composite.targetFilter.FilterEntities(enemyEntity, targetSet);
                // filter condition
                composite.conditionFilter.FilterEntities(enemyEntity, targetSet);

                // if we still have targets, save, otherwise move on
                if (targetSet.Count == 0)
                {
                    continue;
                }
                // lets save this and move on
                totalWeight += composite.weight;
                AISkillResultSet result = new AISkillResultSet();
                result.possibleTargets = targetSet;
                result.composite = composite;
                result.selectManager = selectManager;
                possibleSkills.Add(result);
            }

            if (possibleSkills.Count == 0)
            {
                throw new Exception("We should make sure we set this a default skill, like attack");
            }

            // first, if we have no items or 1 item, just use that instead of randoming to our skill
            if (possibleSkills.Count == 1)
            {
                AISkillResultSet result = possibleSkills[0];
                return GenerateBattleAction(enemyEntity, result, entityManager);
            }


            // now lets random the weight and see where it lies
            float calculatedValue = UnityEngine.Random.Range(0, totalWeight);

            // lets use that skill, we will iterate there adding weights to see if we lie within our weight
            foreach (AISkillResultSet result in possibleSkills)
            {
                if (calculatedValue <= result.composite.weight)
                {
                    return GenerateBattleAction(enemyEntity, result, entityManager);
                }
                // decrement the value, its the same as using a cursor
                calculatedValue -= result.composite.weight;
            }

            throw new Exception("Failed to find an appropriate skill, random failed");
        }

        private IBattleAction GenerateBattleAction(EnemyBattleEntity enemyEntity, AISkillResultSet result, BattleEntityManager entityManager)
        {
            List<BattleEntity> possibleList = new List<BattleEntity>(result.possibleTargets);
            AISkillComposite composite = result.composite;

            // choose random target
            int index = 0; // default to first index we know exists
            if (possibleList.Count > 1)
            {
                index = Mathf.FloorToInt(UnityEngine.Random.Range(0, possibleList.Count));
            }

            // get the selected / random index
            BattleEntity targetEntity = possibleList[index];

            // create a list so we can create the proper TargetResolver
            // TODO this can be optimized to remove this step, its a lot of work
            SelectableTarget selectableTarget = result.selectManager.GetSelectableTarget(targetEntity);

            if (selectableTarget == null)
            {
                Debug.Log("Fuck.. target was not found in AI ActivateSkill");
            }

            // get the target resolver
            ITargetResolver targetResolver = TargetResolverFactory.CreateTargetResolver(enemyEntity, selectableTarget, entityManager);

            return BattleActionFactory.CreateBattleAction(composite.skill, enemyEntity, targetResolver);
        }



        private List<AISkillComposite> CreateSkillComposites(EnemySkillSetSO skillSet)
        {
            List<AISkillComposite> skills = new List<AISkillComposite>();

            foreach (AISkillRule rule in skillSet.skillRules)
            {
                AISkillComposite composite = new AISkillComposite();
                composite.skill = rule.Skill; // temp lvl 1 skill, probably stay like that
                composite.skillRule = rule;
                composite.conditionFilter = rule.CreateConditionFilter();
                composite.targetFilter = rule.CreateTargetFilter();
                skills.Add(composite);
            }

            return skills;
        }

        

        public class AISkillResultSet
        {
            public AISkillComposite composite;
            public HashSet<BattleEntity> possibleTargets;
            public SelectableTargetManager selectManager;
        }

        public class AISkillComposite
        {
            public ICombatSkill skill;
            public AISkillRule skillRule;
            public IAIFilter targetFilter;
            public IAIFilter conditionFilter;
            public float weight { get { return skillRule.Weight; } }
            public AISkillRule.ConditionResolveTarget resolvedTarget { get { return skillRule.ResolvedTarget; } }
        }
    } 
}
