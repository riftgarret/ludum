using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class ActionGUIComponent : MonoBehaviour {

	    
	private ICombatSkill mCombatSkill;
	private PCTurnManagerComponent mTurnManager;
	private Button m_Button;

	public GameObject m_ButtonGameObject;
	public Text m_Text;

	void Awake() {	
		mTurnManager = GameObject.FindGameObjectWithTag (Tags.BATTLE_CONTROLLER).GetComponent<PCTurnManagerComponent> ();
		m_Button = m_ButtonGameObject.GetComponent<Button> ();
	}

	void OnGUI() {
		m_Button.enabled = mTurnManager.decisionState == PCTurnManagerComponent.DecisionState.SKILL;
	}

    public ICombatSkill CombatSkill {
		get { return mCombatSkill;}
		set { 
			mCombatSkill = value;
			m_Text.text = mCombatSkill.DisplayName;
		}
	}

	public void OnButtonClick() {
		Debug.Log("Action Selected: " + mCombatSkill.DisplayName);
		mTurnManager.SelectSkill (mCombatSkill);
	}
}

