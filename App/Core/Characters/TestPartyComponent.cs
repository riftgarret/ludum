using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Core.Characters
{
    public class TestPartyComponent : PartyComponent
    {
        public PCPartySO pcParty;

        void Awake()
        {
            characters = new List<Character>(pcParty.CreateUniqueCharacters());
        }
    } 
}


