using App.Core.Stats;
using System;
using UnityEngine;

namespace App.Core.Equipment
{
    public interface IEquipment
    {
        String DisplayName { get; }
        Texture2D Icon { get; }

        ElementVector OffensiveScalar { get; }
        ElementVector DefensiveScalar { get; }

        AttributeVector AttributeExtra { get; }
    } 
}

