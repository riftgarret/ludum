using App.BattleSystem.Effects;
using App.BattleSystem.Entity;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace App.Debugging
{
    public class DebugStatusEffectLayoutComponent : MonoBehaviour
    {

        [SerializeField]
        private GameObject m_StatusEffectPrefab;

        private BattleEntity m_Entity;

        private Dictionary<IStatusEffectRunner, GameObject> m_ChildMap;
        private RectTransform m_Transform;

        void Awake()
        {
            m_Transform = GetComponent<RectTransform>();
        }

        public BattleEntity entity
        {
            get { return m_Entity; }
            set
            {
                m_Entity = entity;
                if (m_ChildMap != null)
                {
                    DestroyAll();
                }
                m_ChildMap = new Dictionary<IStatusEffectRunner, GameObject>();
            }
        }

        void OnGUI()
        {
            if (m_Entity == null)
            {
                return;
            }

            StatusEffectClient client = entity.StatusEffectClient;
            List<IStatusEffectRunner> runners = new List<IStatusEffectRunner>();
            client.debugPopulateRunners(runners);

            HashSet<IStatusEffectRunner> oldRunners = new HashSet<IStatusEffectRunner>(m_ChildMap.Keys);

            foreach (IStatusEffectRunner runner in runners)
            {
                if (!m_ChildMap.ContainsKey(runner))
                {
                    m_ChildMap[runner] = CreateGo(runner);
                }
                else
                {
                    oldRunners.Remove(runner);
                }
            }

            foreach (IStatusEffectRunner runner in oldRunners)
            {
                GameObject go = m_ChildMap[runner];
                DestroyGo(go);
                m_ChildMap.Remove(runner);
            }
        }

        private GameObject CreateGo(IStatusEffectRunner runner)
        {
            GameObject view = (GameObject)Instantiate(m_StatusEffectPrefab);
            view.GetComponent<RectTransform>().SetParent(m_Transform);
            view.GetComponent<DebugStatusEffectGUIComponent>().runner = runner;
            return view;
        }

        private void DestroyAll()
        {
            foreach (GameObject go in m_ChildMap.Values)
            {
                DestroyGo(go);
            }

            m_ChildMap.Clear();
        }

        private void DestroyGo(GameObject go)
        {
            go.GetComponent<RectTransform>().SetParent(null);
            Destroy(go);
        }
    } 
}
