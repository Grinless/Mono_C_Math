using C_Math.Collision.RadialLowLevel;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Math.Collision
{
    public static class C_RadialChecks
    {
        public static bool WithinRadius(Vector2 origin, Vector2 point, float radius)
        {
            return C_RadialCheck.Get(origin, point, radius).less;
        }

        public static bool WithinOrOnRadius(Vector2 origin, Vector2 point, float radius)
        {
            return C_RadialCheck.Get(origin, point, radius).LessEqual;
        }

        public static bool OutsideRadius(Vector2 origin, Vector2 point, float radius)
        {
            return C_RadialCheck.Get(origin, point, radius).greater;
        }

        public static bool OutsideOrOnRadius(Vector2 origin, Vector2 point, float radius)
        {
            return C_RadialCheck.Get(origin, point, radius).GreaterEqual;
        }
    }
}

namespace C_Math.Collision.RadialLowLevel
{
    public struct C_RadialCheck
    {
        public bool less;
        public bool equal;
        public bool greater;

        public bool GreaterEqual => equal || greater;
        public bool LessEqual => equal || greater;

        internal C_RadialCheck(bool less, bool equal, bool greater)
        {
            this.less = less;
            this.equal = equal;
            this.greater = greater;
        }

        public static C_RadialCheck Get(Vector2 origin, Vector2 point, float radius)
        {
            return C_Radial.PointIntersectRadius(origin, point, radius);
        }
    }

    public static class C_Radial
    {
        public static C_RadialCheck PointIntersectRadius(
            Vector2 origin, Vector2 point, float radius)
        {
            float magnitudeOP = (point - origin).Length();
            return new C_RadialCheck(
                (magnitudeOP < radius) ? true : false,
                (magnitudeOP == radius) ? true : false,
                (magnitudeOP > radius) ? true : false
                );
        }
    }
}
