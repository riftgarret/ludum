﻿using UnityEngine;
using System.Collections;
using App.BattleSystem.Entity;

namespace App.BattleSystem.Actions
{
    public class BattleActionInitiative : BaseBattleAction
    {        
        private float recoverTime;

        public BattleActionInitiative(float initiativeTime)
        {
            recoverTime = initiativeTime;
            SetPhase(ActionPhase.RECOVER);
        }        

        protected override void ExecuteAction(float actionClock)
        {
            // does nothing
        }

        public override float TimePrepare => 0;

        public override float TimeAction => 0;

        public override float TimeRecover => recoverTime;
    } 
}
