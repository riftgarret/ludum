using App.BattleSystem.Effects;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace App.Debugging
{
    public class DebugStatusEffectGUIComponent : MonoBehaviour
    {

        [SerializeField]
        private Slider m_TimeSlider;

        [SerializeField]
        private Text m_Text;

        private IStatusEffectRunner m_Runner;

        public IStatusEffectRunner runner
        {
            get { return m_Runner; }
            set
            {
                m_Runner = value;
                if (value != null)
                {
                    m_TimeSlider.minValue = 0;
                    m_TimeSlider.maxValue = value.Duration;
                }
            }
        }

        void OnGUI()
        {
            if (runner != null)
            {
                m_Text.text = string.Format("{0}: {1} ({2}/{3})",
                                       runner.Property.ToString(),
                                       runner.Strength,
                                       runner.Duration - runner.CurrentDurationLength,
                                       runner.Duration);

                m_TimeSlider.value = runner.Duration - runner.CurrentDurationLength;
            }
            else
            {
                m_Text.text = "Not connected";
                m_TimeSlider.value = 0;
            }
        }
    } 
}