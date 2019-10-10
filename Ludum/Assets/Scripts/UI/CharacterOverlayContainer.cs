using System.Collections;
using System.Collections.Generic;
using Redninja;
using UnityEngine;

public class CharacterOverlayContainer : MonoBehaviour
{
	[SerializeField]
	private GameObject CharacterOverlayPrefab = default;

    public void AddCharacter(IUnitModel unit)
	{
		var go = Instantiate(CharacterOverlayPrefab, transform);
		var characterUI = go.GetComponent<BattleCharacterOverlayUI>();
		characterUI.Unit = unit;
	}
}
