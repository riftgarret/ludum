using UnityEngine;
using System.Collections;

namespace App.BattleSystem.Action
{
    public class BattleActionInitiative : IBattleAction
    {

        private float recoverTime;

        public BattleActionInitiative(float initiativeTime)
        {
            recoverTime = initiativeTime;
        }

        public void OnExecuteAction(float actionClock)
        {
            throw new System.NotImplementedException();
        }

        public float TimePrepare
        {
            get
            {
                return 0;
            }
        }

        public float TimeAction
        {
            get
            {
                return 0;
            }
        }

        public float TimeRecover
        {
            get
            {
                return recoverTime;
            }
        }
    } 
}
