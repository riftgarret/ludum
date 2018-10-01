﻿using Redninja.Core.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redninja.BattleSystem.Config
{
    public class BattleTestLoader : BattleConfig
    {

        public PCPartySO pcPartySO;
        public EnemyPartySO enemyPartySO;

        private PCCharacter[] mPcCharacters;
        private EnemyCharacter[] mEnemyCharacters;

        void Awake()
        {
            LoadCharacters();
        }

        private void LoadCharacters()
        {
            mPcCharacters = pcPartySO.CreateUniqueCharacters();
            mEnemyCharacters = enemyPartySO.CreateUniqueCharacters();
        }

        public override PCCharacter[] pcCharacters
        {
            get { return mPcCharacters; }
        }

        public override EnemyCharacter[] enemyChracters
        {
            get { return mEnemyCharacters; }
        }
    } 
}

