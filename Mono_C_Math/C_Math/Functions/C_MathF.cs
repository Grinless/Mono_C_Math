using C_Math.Matricies;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace C_Math.Functions
{
    public sealed class C_MathF
    {
        public const double PI = 3.1415926535897931D;
        private const float DEG_TO_RAD = MathF.PI / 180;
        public const double E = 2.7182818284590451D;

        private const float RAD_TO_DEG = 180 / MathF.PI;
        public static float RadToDeg => RAD_TO_DEG;
        public static float DegToRad => DEG_TO_RAD;


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Cos(float value) => (float)Math.Cos(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sin(float value) => (float)Math.Sin(value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sec(float value) => 1 / ((float)Math.Cos(value));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Cot(float value) => (float)(Math.Cos(value) / Math.Sin(value));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Tan(float value) => (float)(Math.Tan(value));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Atan(float value) => (float)(Math.Atan(value));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sqrt(float value) => (float)(Math.Sqrt(value));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Pow(float value, int power) {
            float val = value;
            for (int i = 0; i < power; i++)
            {
                val *= value;
            }

            return val;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Pow(float value, float power)
        {
            return MathF.Pow(value, power);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Log(float value)
        {
            return MathF.Log(value);
        }

        /// <summary>
        /// Limit a value to the passed maximum.
        /// </summary>
        /// <param name="value"> The value to limit. </param>
        /// <param name="max"> The maximum that value can be.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Max(float value, float max) =>
            (value > max) ? max : value;

        /// <summary>
        /// Limit a value above a minimum threshold. 
        /// </summary>
        /// <param name="value"> The value to limit. </param>
        /// <param name="min"> The minimum vaue threshold. </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Min(float value, float min) =>
            (value < min) ? min : value;

        /// <summary>
        /// Return the absolute value of a number.
        /// </summary>
        /// <param name="value"> The value to absolute. </param>
        /// <returns> The value with the sign set to positive. </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Abs(float value) =>
            ((value < 0) ? -1 : 1) * value;

        /// <summary>
        /// Returns a value clamped within the range [Min, Max]. 
        /// </summary>
        /// <param name="value"> The inital value to clamp. </param>
        /// <param name="min">   The minimum range. </param>
        /// <param name="max">   The maximum range. </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Clamp(float value, float min, float max) =>
            (value < min) ? min : (value > max) ? max : value;

        /// <summary>
        /// Returns a value clamped within the range [Min, Max]. 
        /// </summary>
        /// <param name="value"> The inital value to clamp. </param>
        /// <param name="range">   The range [Min, Max]. </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Clamp(float value, Vector2 range) =>
            Clamp(value, range.X, range.Y);

        /// <summary>
        /// Convert a value within range A to a value within range B. 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="initalRange"></param>
        /// <param name="newRange"></param>
        /// <returns>value converted to the Range B scale. </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double ConvertToRange(float value, Vector2 initalRange, Vector2 newRange)
        {
            float numerator = (value - initalRange.X);
            float denominator = (initalRange.Y - initalRange.X);
            float newR = (newRange.Y - newRange.X);
            return numerator / denominator * newR;
        }

        /// <summary>
        /// Returns the vector perpendicular to the passed in vector.
        /// </summary>       
        public static Vector2 PerpendicularCW(Vector2 value)
        {
            return C_M2X2.RotateC90(value);
        }

        /// <summary>
        /// Returns the vector perpendicular to the passed in vector.
        /// </summary>
        public static Vector2 PerpendicularCCW(Vector2 v)
        {
            return C_M2X2.RotateCC90(v);
        }

        public static float GetLineMidpoint(Vector2 a, Vector2 b)
        {
            return (b - a).Length() / 2;
        }

        public static Vector2 Normal(Vector2 v)
        {
            Vector2 _v = v; 
            v.Normalize();
            return _v;
        }
    }
}
