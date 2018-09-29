using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace App.DevNull
{
    public class DevNullCombatLog : MonoBehaviour 
    {
        private EnumerableView view;

        void Start()
        {
            view = GetComponent<EnumerableView>();
            view.Bind(new List<NestedText>()
            {
                new NestedText("what {0} is it", 3),
                new NestedText("Who could it be {0}.", 2)
            });
            view.Draw();
        }

        void Update()
        {
            
        }

        void OnGUI()
        {

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
    public class NestedTextView : DataBoundElement
    {
        private Text _text;
        public Text text => this.SetFieldAndReturnComponent(ref _text);

        public override void Draw()
        {
            NestedText data = GetDataAs<NestedText>();
            string displayText = String.Format(data.RawText, data.Counter > 0 ?
                String.Format("<b><color=#00ffffff>{0}</color></b>", data.Counter)
                : "" + data.Counter);
            if (data.Counter > 0)
            {
                
            }

            text.text = displayText;
            base.Draw();            
            
        }                
    }
}
