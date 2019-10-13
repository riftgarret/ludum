using System;
using Redninja.Components.Skills;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillItemUI : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] private RawImage icon = default;
	[SerializeField] private Text skillText = default;
	[SerializeField] private Image background = default;
	private Color defaultColor;
	[SerializeField] private Color SelectedBackgroundColor = default;

	private ISkill skill;

	public ISkill Skill
	{
		get => skill;
		set
		{
			this.skill = value;
			skillText.text = skill.Name;			
		}
	}

	public bool Selected
	{
		get => background.color == SelectedBackgroundColor;
		set => background.color = value ? SelectedBackgroundColor : defaultColor;
	}

	void Awake()
	{
		defaultColor = background.color;
	}

	public event Action<SkillItemUI> OnSkillSelected;

	public void OnPointerClick(PointerEventData eventData) => OnSkillSelected?.Invoke(this);	

}
