using System;
using System.Collections;
using System.Collections.Generic;
using Redninja;
using UnityEngine;

public class CharacterOverlayContainer : MonoBehaviour
{
	[SerializeField]
	private GameObject characterOverlayPrefab = default;

	public event Action<BattleCharacterOverlayUI> OnCharacterOverlaySelected;

	public void AddCharacter(IUnitModel unit)
	{
		var go = Instantiate(characterOverlayPrefab, transform);
		var characterUI = go.GetComponent<BattleCharacterOverlayUI>();
		characterUI.Unit = unit;
		characterUI.OnCharacterSelected += onClick;
	}

	private void onClick(BattleCharacterOverlayUI ui) => OnCharacterOverlaySelected?.Invoke(ui);
}
