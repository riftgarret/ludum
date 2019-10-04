using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Core.Characters
{
    public class PartyComponent : MonoBehaviour
    {
        protected List<Character> characters;

        public List<Character> Characters
        {
            get { return new List<Character>(characters); }
        }
    }

}

