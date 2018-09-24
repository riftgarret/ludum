using App.BattleSystem.Entity;
using App.BattleSystem.Turn;
using App.Core.Skills;
using UnityEngine;


namespace App.BattleSystem.GUI
{
    public class ActionLayoutComponent : MonoBehaviour
    {

        [SerializeField]
        private GameObject actionPrefab;

        private RectTransform rectTransform;

        void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }

        /// <summary>
        /// clear out any buttons in layout.
        /// </summary>
        public void Clear()
        {
            // destroy old buttons
            while (rectTransform.childCount > 0)
            {
                Transform transform = rectTransform.GetChild(0);
                transform.SetParent(null);
                GameObject.Destroy(transform.gameObject);
            }
        }

        public void PopulateSkillLayout(PCBattleEntity entity, PCTurnManager.DecisionState state)
        {
            Clear();

            if (entity == null)
            {
                return;
            }


            foreach (ICombatSkill skill in entity.SkillSet.skills)
            {
                GameObject actionPrefabInstance = (GameObject)Instantiate(actionPrefab);
                RectTransform rect = actionPrefabInstance.GetComponent<RectTransform>();
                ActionGUIComponent actionGUI = actionPrefabInstance.GetComponent<ActionGUIComponent>();
                actionGUI.CombatSkill = skill;
                rect.SetParent(rectTransform);
            }
        }


    } 
}

