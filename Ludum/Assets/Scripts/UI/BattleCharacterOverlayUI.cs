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

	public IUnitModel Unit {
		get => unit;
		set
		{
			unit = value;
			InitCharacterUI(value);
		}
	}
	private IUnitModel unit;

	private void InitCharacterUI(IUnitModel unit)
	{
		nameText.text = Unit.Name;
		switch (Unit.Phase)
		{
			case ActionPhase.Waiting:
			case ActionPhase.Done:
				actionBar.PercentFill = 0;
				break;
			case ActionPhase.Preparing:
				actionBar.PercentFill = Unit.PhaseProgress;
				break;
			case ActionPhase.Executing:
				actionBar.PercentFill = 1;
				break;
			case ActionPhase.Recovering:
				actionBar.PercentFill = 1f - Unit.PhaseProgress;
				break;
		}

		// TODO set icon and status effects.
	}

	private float GetPercentFill(Stat stat)
	{
		if (!Unit.VolatileStats.ContainsKey(stat)) return 1f;
		return (float)Unit.VolatileStats[stat] / (float)Unit.Stats[stat];
	}

	private int TempGetVolatile(Stat stat) => Unit.VolatileStats.ContainsKey(stat)? Unit.VolatileStats[stat] : 90;
    
    void Update()
    {
		if(Unit == null)
		{
			return;
		}

		hpBar.PercentFill = GetPercentFill(Stat.HP);
		hpText.text = "" + TempGetVolatile(Stat.HP);
		resBar.PercentFill = GetPercentFill(Stat.HP);
		resText.text = "" + TempGetVolatile(Stat.Resource);
		nameText.text = Unit.Name;
		switch (Unit.Phase)
		{
			case ActionPhase.Waiting:
			case ActionPhase.Done:
				actionBar.PercentFill = 0;
				break;
			case ActionPhase.Preparing:
				actionBar.PercentFill = Mathf.Lerp(actionBar.PercentFill, Unit.PhaseProgress, 0.5f);
				break;
			case ActionPhase.Executing:
				actionBar.PercentFill = 1;
				break;
			case ActionPhase.Recovering:
				actionBar.PercentFill = Mathf.Lerp(actionBar.PercentFill, 1f - Unit.PhaseProgress, 0.5f);
				break;
		}		
	}

	public void OnPointerClick(PointerEventData eventData) => OnCharacterSelected?.Invoke(this);
}
