using System;
using Redninja;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleCharacterOverlayUI : MonoBehaviour, IPointerClickHandler
{
	[SerializeField] private BarUI hpBar = default;
	[SerializeField] private Text hpText = default;
	[SerializeField] private BarUI resBar = default;
	[SerializeField] private Text resText = default;
	[SerializeField] private Text nameText = default;
	[SerializeField] private BarUI actionBar = default;
	[SerializeField] private RawImage actionIcon = default;
	[SerializeField] private StatusEffectContainerUI statusEffectContainer = default;

	public event Action<BattleCharacterOverlayUI> OnCharacterSelected;

	public IBattleEntity Unit {
		get => unit;
		set
		{
			unit = value;
			InitCharacterUI(value);
		}
	}
	private IBattleEntity unit;

	private void InitCharacterUI(IBattleEntity unit)
	{
		nameText.text = Unit.Name;
		switch (Unit.Actions.Phase)
		{
			case ActionPhase.Waiting:
			case ActionPhase.Done:
				actionBar.PercentFill = 0;
				break;
			case ActionPhase.Preparing:
				actionBar.PercentFill = Unit.Actions.PhaseProgress;
				break;
			case ActionPhase.Executing:
				actionBar.PercentFill = 1;
				break;
			case ActionPhase.Recovering:
				actionBar.PercentFill = 1f - Unit.Actions.PhaseProgress;
				break;
		}

		// TODO set icon and status effects.
	}		
    
    void Update()
    {
		if(Unit == null)
		{
			return;
		}

		hpBar.PercentFill = unit.HP.Percent;
		hpText.text = "" + unit.HP.Current;
		resBar.PercentFill = unit.Resource.Percent;
		resText.text = "" + unit.HP.Current;
		nameText.text = Unit.Name;
		switch (Unit.Actions.Phase)
		{
			case ActionPhase.Waiting:
			case ActionPhase.Done:
				actionBar.PercentFill = 0;
				break;
			case ActionPhase.Preparing:
				actionBar.PercentFill = Mathf.Lerp(actionBar.PercentFill, Unit.Actions.PhaseProgress, 0.5f);
				break;
			case ActionPhase.Executing:
				actionBar.PercentFill = 1;
				break;
			case ActionPhase.Recovering:
				actionBar.PercentFill = Mathf.Lerp(actionBar.PercentFill, 1f - Unit.Actions.PhaseProgress, 0.5f);
				break;
		}		
	}

	public void OnPointerClick(PointerEventData eventData) => OnCharacterSelected?.Invoke(this);
}
