using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.Core.Characters
{
    public class TestEnemyComponent : EnemyComponent
    {
        public EnemyPartySO enemyPartySO;

        void Awake()
        {
            characters = new List<Character>();
            characters.AddRange(enemyPartySO.CreateUniqueCharacters());
        }

    }

}
