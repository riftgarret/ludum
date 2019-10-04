using System;
using UnityEngine;

namespace App.BattleSystem.Effects
{
    [Serializable]
    public class StatusEffectGroupGUIProperty
    {
        // TODO add GET accessors when we start using this
        [SerializeField]
        private string onStartTextFormat;

        [SerializeField]
        private string onEndTextFormat;

        [SerializeField]
        private Texture2D statusIcon;
    } 
}

