using C_Math.Shapes;
using Microsoft.Xna.Framework;
using System.Linq;

namespace C_Math.Collision
{
    public static class C_AABB
    {
        //TODO: TEST THIS FUNCTION. 
        public static bool AABBCollisionCheck(C_Rect a, C_Rect b)
        {
            Vector2 minA = a.Min;
            Vector2 maxA = a.Max;
            Vector2 minB = b.Min;
            Vector2 maxB = b.Max;

            if ((minA.X < minB.X && maxA.X > maxB.X) &&
                (minA.Y < minB.Y && maxA.Y > minB.Y))
            {
                return true;
            }
            return false;
        }
    }
}
