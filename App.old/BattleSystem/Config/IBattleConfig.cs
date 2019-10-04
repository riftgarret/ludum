using App.Core.Characters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace App.BattleSystem.Config
{
    public interface IBattleConfig
    {
        PCCharacter[] pcCharacters { get; }
        EnemyCharacter[] enemyChracters { get; }
    } 
}

