using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BattleConfig : MonoBehaviour, IBattleConfig {

    public virtual PCCharacter[] pcCharacters {
        get { throw new NotImplementedException(); }
    }

    public virtual EnemyCharacter[] enemyChracters {
        get { throw new NotImplementedException(); }
    }
}