using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.BattleSystem.Entity
{
    // action phase
    public enum PhaseState
    {
        REQUIRES_INPUT,
        PREPARE,
        EXECUTE,
        RECOVER
    }
}
