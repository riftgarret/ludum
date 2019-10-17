using System.Collections;
using System.Collections.Generic;
using Redninja;
using Redninja.Components.Actions;
using Redninja.Components.Decisions;
using Redninja.Components.Skills;
using Redninja.Entities;
using UnityEngine;

public class ActionModeBehavior : MonoBehaviour
{
	[SerializeField] private Canvas uiCanvas = default;
	[SerializeField] private GameObject skillOverlayContainerPrefab = default;
	[SerializeField] private GameObject proceedButton = default;

	private SkillSelectionContainerUI skillSelectionContainer;
	

	private ActionFlow actionFlow = ActionFlow.OPEN_SKILLS;

	private enum ActionFlow { OPEN_SKILLS, SELECT_SKILL, SELECT_TARGET  }

	private IBattleEntity currentUnit;
	private SkillItemUI currentSelectedSkill;
	private ITargetingContext currentTargetContext;

	public void OnCharacterSelected(IBattleEntity unit)
	{
		switch (actionFlow)
		{
			case ActionFlow.OPEN_SKILLS:
				if (unit.RequiresAction)
				{
					currentUnit = (IBattleEntity) unit;
					ShowSkills(unit);
					actionFlow = ActionFlow.SELECT_SKILL;
				}
				break;
			case ActionFlow.SELECT_TARGET:
				
				if (currentTargetContext.TargetSpecs.Count == 1 && currentTargetContext.TargetSpecs[0] is EntityTargetSpec)
				{
					AssignEntityTargetSpec((EntityTargetSpec)currentTargetContext.TargetSpecs[0], unit);					
				}

				if(currentTargetContext.IsReady)
				{
					var action = currentTargetContext.GetAction();
					currentUnit.SetAction(action);
					ResetState();
				}				
				break;
		}
	}

	private void AssignEntityTargetSpec(EntityTargetSpec entitySpec, IBattleEntity target)
	{
		if (entitySpec.IsValidTarget(target)) entitySpec.SelectTarget(target);
	}


	public void Cancel()
	{
		ResetState();
	}

	private void ShowSkills(IBattleEntity unit)
	{
		var go = GameObject.Instantiate(skillOverlayContainerPrefab, uiCanvas.transform);
		skillSelectionContainer = go.GetComponent<SkillSelectionContainerUI>();
		skillSelectionContainer.OnSkillSelected += HandleSkillSelected;
		skillSelectionContainer.LoadSkills(unit);		
	}

	private void ResetState()
	{
		if (skillSelectionContainer != null)
		{
			skillSelectionContainer.OnSkillSelected -= HandleSkillSelected;
			Destroy(skillSelectionContainer.gameObject);
		}
		currentUnit = null;
		currentTargetContext = null;
		currentSelectedSkill = null;
		actionFlow = ActionFlow.OPEN_SKILLS;
	}

	private void HandleSkillSelected(SkillItemUI ui, IBattleEntity unit)
	{
		if (currentSelectedSkill != null) currentSelectedSkill.Selected = false;
		currentSelectedSkill = ui;
		currentSelectedSkill.Selected = true;
		currentTargetContext = currentUnit.ActionContextProvider.GetTargetingContext(ui.Skill);
		actionFlow = ActionFlow.SELECT_TARGET;
	}
}
