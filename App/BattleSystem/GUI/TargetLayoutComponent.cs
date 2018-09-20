using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class TargetLayoutComponent: MonoBehaviour {

	[SerializeField]
	private GameObject m_TargetPrefab;      

    private RectTransform m_Transform;
	private PCTurnManagerComponent m_TurnManager;

	// last state checking
	private PCBattleEntity m_CurrentEntity;
	private PCTurnManagerComponent.DecisionState m_CurrentDecisionState;

    void Awake() {
		m_TurnManager = GameObject.FindGameObjectWithTag(Tags.BATTLE_CONTROLLER).GetComponent<PCTurnManagerComponent>();
		m_Transform = GetComponent<RectTransform>();          
		m_CurrentEntity = null;
		m_CurrentDecisionState = PCTurnManagerComponent.DecisionState.IDLE;
    }

    void Start() {
        
    }

    void OnGUI() {
		PCBattleEntity entity = m_TurnManager.currentEntity;		
		PCTurnManagerComponent.DecisionState decisionState = m_TurnManager.decisionState;
		if (m_CurrentEntity != entity || m_CurrentDecisionState != decisionState) {
			m_CurrentEntity = entity;
			m_CurrentDecisionState = decisionState;
			PopulateTargets (entity, decisionState);
		}
    }
					

	void PopulateTargets(PCBattleEntity entity, PCTurnManagerComponent.DecisionState state) {
		// destroy old buttons
		while (m_Transform.childCount > 0) {
			Transform transform = m_Transform.GetChild(0);
			transform.SetParent(null);
			GameObject.Destroy(transform.gameObject);
		}


		if (entity == null || state != PCTurnManagerComponent.DecisionState.TARGET) {
			return;
		}


		List<SelectableTarget> selectableTargets = m_TurnManager.currentTargetManager.targetList;
		foreach (SelectableTarget selectableTarget in selectableTargets) {
			GameObject actionPrefabInstance = (GameObject)Instantiate(m_TargetPrefab);
			RectTransform rect = actionPrefabInstance.GetComponent<RectTransform>();
			TargetGUIComponent targetGUI = actionPrefabInstance.GetComponent<TargetGUIComponent>();
			targetGUI.SelectableTarget = selectableTarget;
			rect.SetParent(m_Transform);
		}
	}


}

