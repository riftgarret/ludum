using App.BattleSystem.Entity;
using App.BattleSystem.Turn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


namespace App.BattleSystem.GUI
{
    public class CharacterGUIComponent : MonoBehaviour
    {

        public Slider actionSlider;
        public Slider hpSlider;
        public Slider resourceSlider;
        public Image portraitImage;
        public Text characterTitle;

        public void SetDisplayName(String text) => characterTitle.text = text;


        private void SetResource(float resourceCurrent, float resourceMax) 
        {
            resourceSlider.maxValue = resourceMax;
            resourceSlider.value = resourceCurrent;
        }

        /// <summary>
        /// Updates the HP slider.
        /// </summary>
        public void SetHp(float hpCurrent, float hpMax)
        {
            hpSlider.maxValue = hpMax;
            hpSlider.value = hpCurrent;
        }

        /// <summary>
        /// Updates the action slider. to meet the four TurnState.Phase states
        /// </summary>
        public void SetActionPercent(float percent) => actionSlider.value = percent;
    } 
}

