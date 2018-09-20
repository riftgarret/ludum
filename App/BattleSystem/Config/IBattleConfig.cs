using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public interface IBattleConfig {
    PCCharacter[] pcCharacters { get; }
    EnemyCharacter[] enemyChracters { get; }    
}

