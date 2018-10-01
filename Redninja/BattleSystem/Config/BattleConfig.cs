using Redninja.Core.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Redninja.BattleSystem.Config
{
    public class BattleConfig : MonoBehaviour, IBattleConfig
    {

        public virtual PCCharacter[] pcCharacters
        {
            get { throw new NotImplementedException(); }
        }

        public virtual EnemyCharacter[] enemyChracters
        {
            get { throw new NotImplementedException(); }
        }
    } 
}