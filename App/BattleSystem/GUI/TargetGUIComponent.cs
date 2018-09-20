using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class TargetGUIComponent : MonoBehaviour {

	    
	private SelectableTarget m_SelectableTarget;
	private PCTurnManagerComponent m_TurnManager;
	private Button m_Button;

	public GameObject m_ButtonGameObject;
	public Text m_Text;

	void Awake() {	
		m_TurnManager = GameObject.FindGameObjectWithTag (Tags.BATTLE_CONTROLLER).GetComponent<PCTurnManagerComponent> ();
		m_Button = m_ButtonGameObject.GetComponent<Button> ();
	}

	void OnGUI() {
		m_Button.enabled = m_TurnManager.decisionState == PCTurnManagerComponent.DecisionState.TARGET;
	}

	public SelectableTarget SelectableTarget {
		get { return m_SelectableTarget;}
		set { 
			m_SelectableTarget = value;
			m_Text.text = m_SelectableTarget.targetName;
		}
	}

	public void OnButtonClick() {
		Debug.Log("Target Selected: " + m_SelectableTarget.targetName);
		m_TurnManager.SelectTarget (m_SelectableTarget);
	}
}

