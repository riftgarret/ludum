using App.BattleSystem.Entity;
using App.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace App.BattleSystem.GUI
{
    public class CharacterLayoutComponent : MonoBehaviour
    {
        [SerializeField]
        private GameObject characterPortraitPrefab;
        [SerializeField]
        private bool isPC = true;

        private BattleEntityManager mEntityManager;
        private RectTransform mTransform;

        void Awake()
        {
            mEntityManager = GameObject.FindGameObjectWithTag(Tags.BATTLE_CONTROLLER).GetComponent<BattleEntityManager>();
            mTransform = GetComponent<RectTransform>();
            EnsureLayout();
        }

        void Start()
        {
            BattleEntity[] entities;
            if (isPC)
            {
                entities = mEntityManager.pcEntities;
            }
            else
            {
                entities = mEntityManager.enemyEntities;
            }

            foreach (BattleEntity be in entities)
            {
                GameObject characterPortrait = (GameObject)Instantiate(characterPortraitPrefab);
                RectTransform rect = characterPortrait.GetComponent<RectTransform>();
                CharacterGUIComponent charGUI = characterPortrait.GetComponent<CharacterGUIComponent>();
                charGUI.BattleEntity = be;
                rect.SetParent(mTransform);
            }
        }

        void OnGUI()
        {

        }

        private void EnsureLayout()
        {
            LayoutGroup layout = GetComponent<LayoutGroup>();
            if (layout == null)
            {
                VerticalLayoutGroup vlayout = gameObject.AddComponent<VerticalLayoutGroup>();
                vlayout.childForceExpandHeight = false;
                vlayout.spacing = 10f;
                vlayout.childAlignment = TextAnchor.MiddleCenter;
            }
        }
    } 
}

