using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;
/// <summary>
/// This class should represent the state of logs that can be eventually scrolled by the user.
/// The goal is not to just have text, but present metadata to allow hover over to see in depth usage
/// like a lot of paradox games (Pillars, EU4, etc)
/// </summary>
namespace Redninja.BattleSystem.Log
{        

    [RequireComponent(typeof(VerticalLayoutGroup))]
    [RequireComponent(typeof(ContentSizeFitter))]    
    public class BattleLogLayout : MonoBehaviour 
    {        
        [SerializeField]
        private DataBoundElement template;

        [SerializeField]
        private int maxRecords = 50;

        private LinkedList<DataBoundElement> managedElements = new LinkedList<DataBoundElement>();        

        void Awake()
        {
            // sanity check
            template.gameObject.SetActive(false);
        }

        public void Clear()
        {
            if (managedElements != null)
            {
                foreach (DataBoundElement e in managedElements)
                {
                    Destroy(e.gameObject);
                }
                managedElements.Clear();
            }
        }

        public void AddElement(object data)
        {
            if(template == null)
            {
                return;
            }

            DataBoundElement element = Instantiate(template, transform);
            element.gameObject.SetActive(true);
            element.Bind(data);
            element.Draw();

            managedElements.AddLast(element);

            TrimElementsToMax();
        }

        private void TrimElementsToMax()
        {
            while (managedElements.Count > maxRecords && managedElements.Count > 0)
            {
                DataBoundElement front = managedElements.First.Value;
                Destroy(front.gameObject);
                managedElements.RemoveFirst();
            }
        }
    } 
}
