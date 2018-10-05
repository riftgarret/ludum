using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redninja.BattleSystem.Targeting
{
    /// <summary>
    /// Target type represents the type of targetting that can happen.
    /// </summary>
    public enum TargetType
    {
        Target, // targets are not based on tiles
        Pattern, // targets are based on pattern on tiles
        TargetAndPattern // target is center of pattern
    }
}
