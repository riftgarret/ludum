﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.BattleSystem.Entity
{
    // action phase
    public enum ActionPhase
    {
        REQUIRES_INPUT,
        PREPARE,
        EXECUTE,
        RECOVER
    }
}
