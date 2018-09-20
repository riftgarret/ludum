using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class SelectableTargetManager {

	// names to display
	private const string ROW_FRONT_NAME = "Front Row";
	private const string ROW_MIDDLE_NAME = "Middle Row";
	private const string ROW_BACK_NAME = "Back Row";

	private const string ROW_CURRENT_NAME = "Current Row";

	private const string ALL_ENEMY = "All Enemies";
	private const string ALL_ALLIES = "All Allies";

	/// <summary>
	/// Creates the allowed targets.
	/// </summary>
	/// <returns>The allowed targets.</returns>
	/// <param name="origin">Origin.</param>
	/// <param name="entityManager">Entity manager.</param>`
	/// <param name="skill">Skill.</param>
	public static SelectableTargetManager CreateAllowedTargets(BattleEntity origin, BattleEntityManagerComponent entityManager, ICombatSkill skill) {
		HashSet<BattleEntity> entitySet = new HashSet<BattleEntity>(entityManager.allEntities);
		List<SelectableTarget> targetList = new List<SelectableTarget>();
		Dictionary<BattleEntity, SelectableTarget> targetMap = new Dictionary<BattleEntity, SelectableTarget>();

		// filter
		FilterSkill(entitySet, skill);

		// populate targets
		if(origin.isPC) {
			PopulateSelectableTargets(targetList, (PCBattleEntity) origin, entitySet, skill);
		}
		else {
			PopulateSelectableTargets(targetList, (EnemyBattleEntity) origin, entitySet, skill);
		}


		// create map
		foreach(SelectableTarget target in targetList) {
			foreach(BattleEntity entity in target.entities) {
				targetMap[entity] = target;
			}
		}

		return new SelectableTargetManager(skill, targetList, targetMap);
	}



	/// <summary>
	/// Filters out battle entities from the set that are not valid targets for the skill.
	/// </summary>
	/// <param name="entitySet">Entity set.</param>
	/// <param name="skill">Skill.</param>
	private static void FilterSkill(HashSet<BattleEntity> entitySet, ICombatSkill skill) {
		// if we assigned a predicate, lets remove those entities

		entitySet.RemoveWhere(delegate(BattleEntity obj) {
			return !skill.TargetRule.IsValidTarget(obj);			
		});

	}

	/// <summary>
	/// Populates the selectable targets.
	/// </summary>
	/// <param name="entityList">Entity list.</param>
	/// <param name="origin">Origin.</param>
	/// <param name="entitySet">Entity set.</param>
	/// <param name="skill">Skill.</param>
	private static void PopulateSelectableTargets(List<SelectableTarget> entityList, 
	                                              EnemyBattleEntity origin, 
	                                              HashSet<BattleEntity> entitySet, 
	                                              ICombatSkill skill) {
		switch(skill.TargetRule.primaryTargetType) {
		case TargetingType.SELF_ROW:
		case TargetingType.ALL:
			PopulateAllTargets(entityList, entitySet);
			break;
		case TargetingType.ROW:
			PopulateRowTargets(entityList, entitySet);
			break;
		case TargetingType.SELF:
			PopulateSelfTargets(entityList, entitySet, origin);
			break;		
		case TargetingType.SINGLE:
			PopulateSingleTargets(entityList, entitySet);
			break;
		}
	}

	/// <summary>
	/// Populates the selectable targets.
	/// </summary>
	/// <param name="entityList">Entity list.</param>
	/// <param name="origin">Origin.</param>
	/// <param name="entitySet">Entity set.</param>
	/// <param name="skill">Skill.</param>
	private static void PopulateSelectableTargets(List<SelectableTarget> entityList, 
	                                              PCBattleEntity origin, 
	                                              HashSet<BattleEntity> entitySet, 
	                                              ICombatSkill skill) {
		switch(skill.TargetRule.primaryTargetType) {
		case TargetingType.ALL:
			PopulateAllTargets(entityList, entitySet);
			break;
		case TargetingType.ROW:
			PopulateRowTargets(entityList, entitySet);
			break;
		case TargetingType.SELF:
			PopulateSelfTargets(entityList, entitySet, origin);
			break;
		case TargetingType.SELF_ROW:
			PopulateSelfRowTargets(entityList, entitySet, origin);
			break;
		case TargetingType.SINGLE:
			PopulateSingleTargets(entityList, entitySet);
			break;
		}
	}

	/// <summary>
	/// Add each individual entities
	/// </summary>
	/// <param name="entityList">Entity list.</param>
	/// <param name="entitySet">Entity set.</param>
	private static void PopulateSingleTargets(List<SelectableTarget> entityList, 
	                                                 HashSet<BattleEntity> entitySet) {

		foreach(BattleEntity entity in entitySet) {
			entityList.Add(
				new SelectableTarget(entity.character.displayName, 
			                     new List<BattleEntity>(new BattleEntity[]{entity}),
			                     ResolvedTargetEnum.SINGLE));
		}
	}

	/// <summary>
	/// If the SELF target still is in the filter, it will add it here
	/// </summary>
	/// <param name="entityList">Entity list.</param>
	/// <param name="entitySet">Entity set.</param>
	/// <param name="sourceEntity">Source entity.</param>
	private static void PopulateSelfTargets(List<SelectableTarget> entityList, 
	                                                 HashSet<BattleEntity> entitySet,
	                                                  BattleEntity sourceEntity){
		foreach(BattleEntity entity in entitySet) {
			if(entity == sourceEntity) {
				entityList.Add(
					new SelectableTarget(entity.character.displayName, 
				                     new List<BattleEntity>(new BattleEntity[]{entity}),
				                     ResolvedTargetEnum.SELF));
				break;
			}
		}
	}

	private static void PopulateSelfRowTargets(List<SelectableTarget> entityList, 
	                                                  HashSet<BattleEntity> entitySet,
	                                                  PCBattleEntity sourceEntity){
		SelectableTarget rowTarget = new SelectableTarget(ROW_CURRENT_NAME, new List<BattleEntity>(), ResolvedTargetEnum.SELF_ROW);
		PCCharacter.RowPosition rowPos = sourceEntity.pcCharacter.rowPosition;
		foreach(BattleEntity entity in entitySet) {
			if(entity.isPC && ((PCBattleEntity)entity).pcCharacter.rowPosition == rowPos) {
				rowTarget.entities.Add(entity);
			}
		}

		if(rowTarget.entities.Count > 0) {
			entityList.Add(rowTarget);
		}
	}

	/// <summary>
	/// Populates selectable targets based on All Enemies or All Allies.
	/// </summary>
	/// <param name="entityList">Entity list.</param>
	/// <param name="entitySet">Entity set.</param>
	private static void PopulateAllTargets(List<SelectableTarget> entityList, 
	                                                 HashSet<BattleEntity> entitySet) {

		// create the bins for when we find the values
		SelectableTarget enemyTarget = new SelectableTarget(ALL_ENEMY, new List<BattleEntity>(), ResolvedTargetEnum.ENEMY_ALL);
		SelectableTarget allyTarget = new SelectableTarget(ALL_ALLIES, new List<BattleEntity>(), ResolvedTargetEnum.ALLY_ALL);

		// add each to appropriate list
		foreach(BattleEntity entity in entitySet) {
			if(entity.isPC) {
				allyTarget.entities.Add(entity);
			}
			else {
				enemyTarget.entities.Add(entity);
			}
		}

		// add to main list if we have count > 0
		if(enemyTarget.entities.Count > 0) {
			entityList.Add(enemyTarget);
		}

		if(allyTarget.entities.Count > 0) {
			entityList.Add(allyTarget);
		}
	}

	/// <summary>
	/// Populates the row targets.
	/// </summary>
	/// <param name="entityList">Entity list.</param>
	/// <param name="entitySet">Entity set.</param>
	private static void PopulateRowTargets(List<SelectableTarget> entityList, HashSet<BattleEntity> entitySet) {
		// create the bins for when we find the values
		SelectableTarget frontRowTarget = new SelectableTarget(ROW_FRONT_NAME, new List<BattleEntity>(), ResolvedTargetEnum.ALLY_ROW_FRONT);
		SelectableTarget middleRowTarget = new SelectableTarget(ROW_MIDDLE_NAME, new List<BattleEntity>(), ResolvedTargetEnum.ALLY_ROW_MIDDLE);
		SelectableTarget backRowTarget = new SelectableTarget(ROW_BACK_NAME, new List<BattleEntity>(), ResolvedTargetEnum.ALLY_ROW_BACK);

		// populate the bins
		foreach(BattleEntity entity in entitySet) {
			if(entity.isPC) {
				switch(((PCBattleEntity)entity).pcCharacter.rowPosition) {
				case PCCharacter.RowPosition.FRONT:
					frontRowTarget.entities.Add(entity);
					break;
				case PCCharacter.RowPosition.MIDDLE:
					middleRowTarget.entities.Add(entity);
					break;
				case PCCharacter.RowPosition.BACK:
					backRowTarget.entities.Add(entity);
					break;
				}
			}
		}

		// only add bins that have ones
		if(frontRowTarget.entities.Count > 0) {
			entityList.Add(frontRowTarget);
		}
		if(middleRowTarget.entities.Count > 0) {
			entityList.Add(middleRowTarget);
		}
		if(backRowTarget.entities.Count > 0) {
			entityList.Add(backRowTarget);
		}
	}

	/// <summary>
	/// Gets the skill.
	/// </summary>
	/// <value>The skill.</value>
	public ICombatSkill skill {
		private set;
		get;
	}

	public List<SelectableTarget> targetList {
		private set;
		get;
	}

	private Dictionary<BattleEntity, SelectableTarget> mTargetMap;

	public SelectableTarget GetSelectableTarget(BattleEntity entity) {
		return mTargetMap[entity];
	}
	

	private SelectableTargetManager(ICombatSkill skill, List<SelectableTarget> targetList, Dictionary<BattleEntity, SelectableTarget> targetMap ) {
		this.skill = skill;
		this.targetList = targetList;
		this.mTargetMap = targetMap;
	}
}
