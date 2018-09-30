using App.BattleSystem.Log;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace App.Sandbox
{
    public class SandboxCombatLog : MonoBehaviour
    {
        [SerializeField]
        private BattleLogLayout view;
        private List<NestedText> nestedItems;

        void Start()
        {
            nestedItems = new List<NestedText>();

            //StartCoroutine(AddNewEntryEachSecond());
            for (int i = 0; i < 15; i++ )
            {
                IncremeentItems(i);
            }
            
            StartCoroutine(AddNewEntryEachSecond());
        }


        void OnGUI()
        {

        }

        private void IncremeentItems(int counter)
        {            
            view.AddElement(new NestedText("what the {0} happened here", counter));         
        }

        private IEnumerator AddNewEntryEachSecond()
        {
            for (int i = 0; i < 30; i++)
            {
                IncremeentItems(i);
                yield return new WaitForSeconds(1);
            }
        }
    }

    public class NestedText
    {
        public string RawText { get; private set; }
        public int Counter { get; private set; }
        public NestedText Inner { get; private set; }

        public NestedText(string raw, int counter)
        {
            RawText = raw;
            Counter = counter;
            if (counter > 0)
            {
                Inner = new NestedText(raw, counter - 1);
            }
        }
    }

    [RequireComponent(typeof(Text))]
    public class NestedTextView : DataBoundElement, IPointerEnterHandler, IPointerExitHandler
    {
        private Text _text;
        public Text text => this.SetFieldAndReturnComponent(ref _text);

        public override void Draw()
        {
            NestedText data = GetDataAs<NestedText>();
            string displayText = String.Format(data.RawText, data.Counter > 0 ?
                String.Format("<b><color=#00AA5566>{0}</color></b>", data.Counter)
                : "" + data.Counter);
            if (data.Counter > 0)
            {
                
            }

            text.text = displayText;
            base.Draw();            
            
        }

        private IEnumerator runningRoutine;

        private IEnumerator OpenPopupAfterDelay()
        {
            yield return new WaitForSeconds(1);
            //ShowPopup();
        }
  
        private void ShowPopup()
        {
            NestedTextView popup = Instantiate(this, transform.parent);
            NestedText data = GetDataAs<NestedText>();
            popup.Bind(new NestedText(data.RawText, data.Counter - 1));
            popup.Draw();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            runningRoutine = OpenPopupAfterDelay();
            eventData.Use();
            StartCoroutine(runningRoutine);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            StopCoroutine(runningRoutine);
        }
    }
}
