using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class CharacterLayoutComponent: MonoBehaviour {
	[SerializeField]
    private GameObject m_CharacterPortraitPrefab;
	[SerializeField]
	private bool m_IsPC = true;

    private BattleEntityManagerComponent mEntityManager;    
    private RectTransform mTransform;

    void Awake() {
        mEntityManager = GameObject.FindGameObjectWithTag(Tags.BATTLE_CONTROLLER).GetComponent<BattleEntityManagerComponent>();
		mTransform = GetComponent<RectTransform>();          
		EnsureLayout ();
    }

    void Start() {
		BattleEntity[] entities;
		if (m_IsPC) {
			entities = mEntityManager.pcEntities;
		}
		else {
			entities = mEntityManager.enemyEntities;
		}

        foreach (BattleEntity be in entities) {
            GameObject characterPortrait = (GameObject)Instantiate(m_CharacterPortraitPrefab);
            RectTransform rect = characterPortrait.GetComponent<RectTransform>();
            CharacterGUIComponent charGUI = characterPortrait.GetComponent<CharacterGUIComponent>();
            charGUI.BattleEntity = be;
			rect.SetParent(mTransform);
        }
    }

    void OnGUI() {
        
    }
	
	private void EnsureLayout() {
		LayoutGroup layout = GetComponent<LayoutGroup> ();
		if (layout == null) {
			VerticalLayoutGroup vlayout = gameObject.AddComponent<VerticalLayoutGroup>();
			vlayout.childForceExpandHeight = false;
			vlayout.spacing = 10f;
			vlayout.childAlignment = TextAnchor.MiddleCenter;
		}
	}
}

