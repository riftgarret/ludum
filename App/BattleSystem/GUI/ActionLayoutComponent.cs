using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class ActionLayoutComponent: MonoBehaviour {

	[SerializeField]
	private GameObject m_ActionPrefab;      

    private RectTransform m_Transform;
	private PCTurnManagerComponent m_TurnManager;

	// last state checking
	private PCBattleEntity m_CurrentEntity;
	private PCTurnManagerComponent.DecisionState m_CurrentDecisionState;

    void Awake() {
		m_TurnManager = GameObject.FindGameObjectWithTag(Tags.BATTLE_CONTROLLER).GetComponent<PCTurnManagerComponent>();
		m_Transform = GetComponent<RectTransform>();          
    }

    void Start() {
        
    }

	void OnGUI() {
		PCBattleEntity entity = m_TurnManager.currentEntity;		
		PCTurnManagerComponent.DecisionState decisionState = m_TurnManager.decisionState;
		if (m_CurrentEntity != entity || m_CurrentDecisionState != decisionState) {
			m_CurrentEntity = entity;
			m_CurrentDecisionState = decisionState;
			PopulateActions (entity, decisionState);
		}
	}
	
	
	void PopulateActions(PCBattleEntity entity, PCTurnManagerComponent.DecisionState state) {
		// destroy old buttons
		while (m_Transform.childCount > 0) {
			Transform transform = m_Transform.GetChild(0);
			transform.SetParent(null);
			GameObject.Destroy(transform.gameObject);
		}


		if (entity == null) {
			return;
		}


		foreach (ICombatSkill skill in entity.SkillSet.skills) {
			GameObject actionPrefabInstance = (GameObject)Instantiate(m_ActionPrefab);
			RectTransform rect = actionPrefabInstance.GetComponent<RectTransform>();
			ActionGUIComponent actionGUI = actionPrefabInstance.GetComponent<ActionGUIComponent>();
			actionGUI.CombatSkill = skill;
			rect.SetParent(m_Transform);
		}
	}


}

