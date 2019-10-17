using System;
using System.Collections;
using System.Collections.Generic;
using Redninja;
using Redninja.Components.Skills;
using UnityEngine;

public class SkillSelectionContainerUI : MonoBehaviour
{
	[SerializeField] private GameObject skillItemPrefab = default;

	public event Action<SkillItemUI, IBattleEntity> OnSkillSelected;

	private IBattleEntity unit;

	public void LoadSkills(IBattleEntity unit)
	{
		this.unit = unit;
		foreach(ISkill skill in unit.ActionContextProvider.GetActionContext().Skills)
		{
			var go = Instantiate(skillItemPrefab, transform);
			var skillUI = go.GetComponent<SkillItemUI>();
			skillUI.Skill = skill;
			skillUI.OnSkillSelected += HandleSkillSelected;
		}
	}

	private void HandleSkillSelected(SkillItemUI ui) => OnSkillSelected?.Invoke(ui, unit);
}
