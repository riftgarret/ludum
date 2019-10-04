using App.BattleSystem.Entity;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace App.BattleSystem.GUI
{
    public class CharacterLayoutComponent : MonoBehaviour
    {
        [SerializeField]
        private GameObject characterPortraitPrefab;

        private Dictionary<BattleEntity, CharacterGUIComponent> entityMap;

        void Awake()
        {
            entityMap = new Dictionary<BattleEntity, CharacterGUIComponent>();
            EnsureLayout();
        }

        public void SetEntities(IEnumerable<BattleEntity> entities)
        {
            RectTransform rect = GetComponent<RectTransform>();
            foreach (BattleEntity be in entities)
            {
                GameObject characterPortrait = (GameObject)Instantiate(characterPortraitPrefab);
                RectTransform childRect = characterPortrait.GetComponent<RectTransform>();
                CharacterGUIComponent charGUI = characterPortrait.GetComponent<CharacterGUIComponent>();
                charGUI.SetDisplayName(be.Character.displayName);                
                childRect.SetParent(rect);
                entityMap[be] = charGUI;
            }
        }

        public void SetEntityHp(BattleEntity battleEntity, float hpCurrent, float hpTotal)
        {
            entityMap[battleEntity].SetHp(hpCurrent, hpTotal);
        }

        public void SetEntityActionPercent(BattleEntity battleEntity, float percent)
        {
            entityMap[battleEntity].SetActionPercent(percent);
        }

        /// <summary>
        /// Check to make sure we have a vertical layout component that will handle adding items.
        /// </summary>
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

