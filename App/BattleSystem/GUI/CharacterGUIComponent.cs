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

        private BattleEntity battleEntity;
        public BattleEntity BattleEntity
        {
            get { return battleEntity; }
            set
            {
                battleEntity = value;
                characterTitle.text = battleEntity.Character.displayName;
                hpSlider.maxValue = battleEntity.MaxHP;
                hpSlider.minValue = 0;
                hpSlider.value = battleEntity.CurrentHP;
            }
        }


        void OnGUI()
        {
            if (battleEntity == null)
            {
                return;
            }

            UpdateActionSlider();
            UpdateHPSlider();
            UpdateResourceSlider();
        }

        private void UpdateResourceSlider()
        {
            // TODO implement
        }

        /// <summary>
        /// Updates the HP slider.
        /// </summary>
        private void UpdateHPSlider()
        {
            // TODO provide yeilding animation, decelerating interporlators 
            hpSlider.value = battleEntity.CurrentHP;
        }

        /// <summary>
        /// Updates the action slider. to meet the four TurnState.Phase states
        /// </summary>
        private void UpdateActionSlider()
        {
            switch (battleEntity.TurnState.Phase)
            {
                // PREPARE -> animate 0 to 1 as completes
                case TurnState.PhaseState.PREPARE:
                    actionSlider.value = battleEntity.TurnState.TurnPercent;
                    break;
                // EXECUTING -> stays at 1
                case TurnState.PhaseState.EXECUTE:
                    actionSlider.value = 1f;
                    break;
                // RECOVER -> animate 1 to 0 as completes
                case TurnState.PhaseState.RECOVER:
                    actionSlider.value = 1f - battleEntity.TurnState.TurnPercent;
                    break;
                // awaiting input, at 0 
                case TurnState.PhaseState.REQUIRES_INPUT:
                    actionSlider.value = 0f;
                    break;
                default:
                    break;
            }
        }
    } 
}

