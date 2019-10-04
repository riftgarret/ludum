using App.BattleSystem.Entity;
using App.BattleSystem.Targeting;
using App.BattleSystem.Turn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace App.BattleSystem.GUI
{
    public class TargetLayoutComponent : MonoBehaviour
    {

        [SerializeField]
        private GameObject targetPrefab;

        private RectTransform rectTransform;

        void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        public void PopulateTargets(PCBattleEntity entity, PCDecisionManager.DecisionState state, SelectableTargetManager selectableTargetManager)
        {
            // destroy old buttons
            while (rectTransform.childCount > 0)
            {
                Transform transform = rectTransform.GetChild(0);
                transform.SetParent(null);
                GameObject.Destroy(transform.gameObject);
            }


            if (entity == null || state != PCDecisionManager.DecisionState.TARGET)
            {
                return;
            }


            List<SelectableTarget> selectableTargets = selectableTargetManager.TargetList;
            foreach (SelectableTarget selectableTarget in selectableTargets)
            {
                GameObject actionPrefabInstance = (GameObject)Instantiate(targetPrefab);
                RectTransform rect = actionPrefabInstance.GetComponent<RectTransform>();
                TargetGUIComponent targetGUI = actionPrefabInstance.GetComponent<TargetGUIComponent>();
                targetGUI.SelectableTarget = selectableTarget;
                rect.SetParent(rectTransform);
            }
        }


    } 
}

