using App.Core.Skills;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace App.BattleSystem.GUI
{
    public class ActionGUIComponent : MonoBehaviour
    {

        public delegate void OnSkillSelected(ICombatSkill combatSkill);

        public OnSkillSelected OnSkillSelectedDelegate { get; set; }

        private ICombatSkill combatSkill;
        private Button button;

        public GameObject buttonGameObject;
        public Text text;

        public bool EnableButton
        {
            get => button.enabled;
            set => button.enabled = value;
        }

        void Awake()
        {
            button = buttonGameObject.GetComponent<Button>();
        }

        public ICombatSkill CombatSkill
        {
            get { return combatSkill; }
            set
            {
                combatSkill = value;
                text.text = combatSkill.DisplayName;
            }
        }

        public void OnButtonClick()
        {
            Debug.Log("Action Selected: " + combatSkill.DisplayName);
            OnSkillSelectedDelegate?.Invoke(combatSkill);
        }
    } 
}

