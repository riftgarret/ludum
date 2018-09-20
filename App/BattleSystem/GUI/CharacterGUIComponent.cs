using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class CharacterGUIComponent : MonoBehaviour {

    public Slider actionSlider;
    public Slider hpSlider;
    public Slider resourceSlider;
    public Image portraitImage;
    public Text characterTitle;

    private BattleEntity mBattleEntity;
    public BattleEntity BattleEntity {
        get { return mBattleEntity; }
        set {
            mBattleEntity = value;
			characterTitle.text = mBattleEntity.character.displayName;
            hpSlider.maxValue = mBattleEntity.maxHP;
            hpSlider.minValue = 0;
			hpSlider.value = mBattleEntity.currentHP;
        }
    }            
    

    void OnGUI() {
        if (mBattleEntity == null) {
            return;
        }

		UpdateActionSlider ();
		UpdateHPSlider ();
		UpdateResourceSlider ();
    }

	private void UpdateResourceSlider() {
		// TODO implement
	}

	/// <summary>
	/// Updates the HP slider.
	/// </summary>
	private void UpdateHPSlider() {
		// TODO provide yeilding animation, decelerating interporlators 
		hpSlider.value = mBattleEntity.currentHP;
	}

	/// <summary>
	/// Updates the action slider. to meet the four TurnState.Phase states
	/// </summary>
	private void UpdateActionSlider() {
		switch (mBattleEntity.turnState.phase) {
			// PREPARE -> animate 0 to 1 as completes
		case TurnState.Phase.PREPARE:
			actionSlider.value = mBattleEntity.turnState.turnPercent;
			break;
			// EXECUTING -> stays at 1
		case TurnState.Phase.EXECUTE:
			actionSlider.value = 1f;
			break;
			// RECOVER -> animate 1 to 0 as completes
		case TurnState.Phase.RECOVER:
			actionSlider.value = 1f - mBattleEntity.turnState.turnPercent;
			break;
			// awaiting input, at 0 
		case TurnState.Phase.REQUIRES_INPUT:
			actionSlider.value = 0f;
			break;
		default:
			break;
		}
	}
}

