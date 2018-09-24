using System;

namespace App.Core.Stats
{
    [Serializable]
    public class ElementVector
    {
        // physical
        public float slash;
        public float pierce;
        public float crush;

        public float dark;
        public float light;
        public float wind;
        public float earth;
        public float fire;
        public float water;

        public ElementVector() { }
        public ElementVector(ElementVector copy)
        {
            slash = copy.slash;
            crush = copy.crush;
            pierce = copy.pierce;
            light = copy.light;
            dark = copy.dark;
            fire = copy.fire;
            water = copy.water;
            wind = copy.wind;
            earth = copy.earth;
        }

        public static ElementVector operator -(ElementVector e1, ElementVector e2)
        {
            ElementVector result = new ElementVector();
            result.crush = e1.crush - e2.crush;
            result.slash = e1.slash - e2.slash;
            result.pierce = e1.pierce - e2.pierce;
            result.light = e1.light - e2.light;
            result.dark = e1.dark - e2.dark;
            result.fire = e1.fire - e2.fire;
            result.water = e1.water - e2.water;
            result.earth = e1.earth - e2.earth;
            result.wind = e1.wind - e2.wind;
            return result;
        }

        public static ElementVector operator +(ElementVector e1, ElementVector e2)
        {
            ElementVector result = new ElementVector();
            result.crush = e1.crush + e2.crush;
            result.slash = e1.slash + e2.slash;
            result.pierce = e1.pierce + e2.pierce;
            result.light = e1.light + e2.light;
            result.dark = e1.dark + e2.dark;
            result.fire = e1.fire + e2.fire;
            result.water = e1.water + e2.water;
            result.earth = e1.earth + e2.earth;
            result.wind = e1.wind + e2.wind;
            return result;
        }

        public static ElementVector operator *(ElementVector e1, ElementVector e2)
        {
            ElementVector result = new ElementVector();
            result.crush = e1.crush * e2.crush;
            result.slash = e1.slash * e2.slash;
            result.pierce = e1.pierce * e2.pierce;
            result.light = e1.light * e2.light;
            result.dark = e1.dark * e2.dark;
            result.fire = e1.fire * e2.fire;
            result.water = e1.water * e2.water;
            result.earth = e1.earth * e2.earth;
            result.wind = e1.wind * e2.wind;
            return result;
        }

        public static ElementVector operator /(ElementVector e1, ElementVector e2)
        {
            ElementVector result = new ElementVector();
            result.crush = e1.crush / e2.crush;
            result.slash = e1.slash / e2.slash;
            result.pierce = e1.pierce / e2.pierce;
            result.light = e1.light / e2.light;
            result.dark = e1.dark / e2.dark;
            result.fire = e1.fire / e2.fire;
            result.water = e1.water / e2.water;
            result.earth = e1.earth / e2.earth;
            result.wind = e1.wind / e2.wind;
            return result;
        }

        public static ElementVector operator *(ElementVector e, float f)
        {
            ElementVector result = new ElementVector(e);
            result.crush *= f;
            result.pierce *= f;
            result.slash *= f;
            result.light *= f;
            result.dark *= f;
            result.fire *= f;
            result.water *= f;
            result.earth *= f;
            result.wind *= f;
            return result;
        }

        public ElementVector Max(float value)
        {
            ElementVector result = new ElementVector(this);
            result.crush = Math.Max(value, result.crush);
            result.slash = Math.Max(value, result.slash);
            result.pierce = Math.Max(value, result.pierce);
            result.light = Math.Max(value, result.light);
            result.dark = Math.Max(value, result.dark);
            result.fire = Math.Max(value, result.fire);
            result.water = Math.Max(value, result.water);
            result.earth = Math.Max(value, result.earth);
            result.wind = Math.Max(value, result.wind);
            return result;
        }

        public ElementVector Min(float value)
        {
            ElementVector result = new ElementVector(this);
            result.crush = Math.Min(value, result.crush);
            result.slash = Math.Min(value, result.slash);
            result.pierce = Math.Min(value, result.pierce);
            result.light = Math.Min(value, result.light);
            result.dark = Math.Min(value, result.dark);
            result.fire = Math.Min(value, result.fire);
            result.water = Math.Min(value, result.water);
            result.earth = Math.Min(value, result.earth);
            result.wind = Math.Min(value, result.wind);
            return result;
        }

        public float Sum
        {
            get
            {
                float result = 0;
                result += crush;
                result += slash;
                result += pierce;
                result += light;
                result += dark;
                result += fire;
                result += water;
                result += wind;
                result += earth;
                return result;
            }
        }

        public override string ToString()
        {
            return string.Format("[ElementVector: S={0}, P={1}, C={2}, D={3}, L={4}, W={5}, E={6}, F={7}, W={8}, Sum={9}]", slash, pierce, crush, dark, light, wind, earth, fire, water, Sum);
        }

    } 
}


