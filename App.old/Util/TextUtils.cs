using App.Core.Equipment;

namespace App.Util
{
    public class TextUtils
    {
        public static string DmgToString(DamageType dmg)
        {
            switch (dmg)
            {
                case DamageType.CRUSH:
                    return "crush";
                case DamageType.SLASH:
                    return "slash";
                case DamageType.PIERCE:
                    return "pierce";
                case DamageType.DARK:
                    return "dark";
                case DamageType.LIGHT:
                    return "light";
                case DamageType.WIND:
                    return "wind";
                case DamageType.EARTH:
                    return "earth";
                case DamageType.FIRE:
                    return "fire";
                case DamageType.WATER:
                    return "water";
            }
            return "";
        }


    } 
}

