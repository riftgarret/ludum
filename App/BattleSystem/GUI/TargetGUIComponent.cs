using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace App.BattleSystem.GUI
{
    public class TargetGUIComponent : MonoBehaviour
    {

        public delegate void OnTargetSelected(SelectableTarget selectableTarget);

        public OnTargetSelected OnTargetSelectedDelegate { get; set; }

        private SelectableTarget selectableTarget;
        private Button button;

        public GameObject buttonGameObject;
        public Text text;

        void Awake()
        {
            button = buttonGameObject.GetComponent<Button>();
        }

        public SelectableTarget SelectableTarget
        {
            get { return selectableTarget; }
            set
            {
                selectableTarget = value;
                text.text = selectableTarget.targetName;
            }
        }

        public void OnButtonClick()
        {
            Debug.Log("Target Selected: " + selectableTarget.targetName);
            OnTargetSelectedDelegate?.Invoke(selectableTarget);
        }
    } 
}

