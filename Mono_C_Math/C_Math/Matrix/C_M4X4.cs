using C_Math.Functions;
using Microsoft.Xna.Framework;
using System;

namespace C_Math.Matricies
{
    public struct C_M4X4
    {
        public float E00, E01, E02, E03;
        public float E10, E11, E12, E13;
        public float E20, E21, E22, E23;
        public float E30, E31, E32, E33;

        #region Shorthand Refs. 
        private static C_M4X4 identity = new C_M4X4(
            1, 0, 0, 0,
            0, 1, 0, 0,
            0, 0, 1, 0,
            0, 0, 0, 0
            );
        private static C_M4X4 zero = new C_M4X4(
            0, 0, 0, 0,
            0, 0, 0, 0,
            0, 0, 0, 0,
            0, 0, 0, 0
            );
        public static C_M4X4 Identity => identity;
        public static C_M4X4 Zero => zero;
        #endregion

        #region Row/Column Access Properties. 
        public Vector4 R1
        {
            get => new Vector4(E00, E01, E02, E03);
            set
            {
                E00 = value.X;
                E01 = value.Y;
                E02 = value.Z;
                E03 = value.W;
            }
        }

        public Vector4 R2
        {
            get => new Vector4(E10, E11, E12, E13);
            set
            {
                E10 = value.X;
                E11 = value.Y;
                E12 = value.Z;
                E13 = value.W;
            }
        }

        public Vector4 R3
        {
            get => new Vector4(E20, E21, E22, E23);
            set
            {
                E20 = value.X;
                E21 = value.Y;
                E22 = value.Z;
                E23 = value.W;
            }
        }

        public Vector4 R4
        {
            get => new Vector4(E30, E31, E32, E33);
            set
            {
                E30 = value.X;
                E31 = value.Y;
                E32 = value.Z;
                E33 = value.W;
            }
        }

        public Vector4 C1
        {
            get => new Vector4(E00, E10, E20, E30);
            set
            {
                E00 = value.X;
                E10 = value.Y;
                E20 = value.Z;
                E30 = value.W;
            }
        }

        public Vector4 C2
        {
            get => new Vector4(E01, E11, E21, E31);
            set
            {
                E01 = value.X;
                E11 = value.Y;
                E21 = value.Z;
                E21 = value.W;
            }
        }

        public Vector4 C3
        {
            get => new Vector4(E02, E12, E22, E32);
            set
            {
                E02 = value.X;
                E12 = value.Y;
                E22 = value.Z;
                E32 = value.W;
            }
        }

        public Vector4 C4
        {
            get => new Vector4(E03, E13, E23, E33);
            set
            {
                E03 = value.X;
                E13 = value.Y;
                E23 = value.Z;
                E33 = value.W;
            }
        }
        #endregion

        public C_M4X4(
            float E00, float E01, float E02, float E03,
            float E10, float E11, float E12, float E13,
            float E20, float E21, float E22, float E23,
            float E30, float E31, float E32, float E33)
        {
            this.E00 = E00; this.E01 = E01;
            this.E02 = E02; this.E03 = E03;
            this.E10 = E10; this.E11 = E11;
            this.E12 = E12; this.E13 = E13;
            this.E20 = E20; this.E21 = E21;
            this.E22 = E22; this.E23 = E23;
            this.E30 = E30; this.E31 = E31;
            this.E32 = E32; this.E33 = E33;
        }

        public static C_M4X4 Transpose(C_M4X4 a)
        {
            C_M4X4 newM = C_M4X4.zero;
            newM.C1 = a.R1;
            newM.C2 = a.R2;
            newM.C3 = a.R3;
            newM.C4 = a.R4;
            return newM;
        }

        public static C_M4X4 TranslationMatrixX(float x)
        {
            return TranslationMatrix(x, 0, 0);
        }

        public static C_M4X4 TranslationMatrixY(float y)
        {
            return TranslationMatrix(0, y, 0);
        }

        public static C_M4X4 TranslationMatrixZ(float z)
        {
            return TranslationMatrix(0, 0, z);
        }

        public static C_M4X4 TranslationMatrix(float x, float y, float z)
        {
            return new C_M4X4(
                1, 0, 0, x,
                0, 1, 0, y,
                0, 0, 1, z,
                0, 0, 0, 1
                );
        }

        public static C_M4X4 GetScaleMatrixX(float x)
        {
            return ScaleMatrix(x, 0, 0);
        }

        public static C_M4X4 GetScaleMatrixY(float y)
        {
            return ScaleMatrix(0, y, 0);
        }

        public static C_M4X4 GetScaleMatrixZ(float z)
        {
            return ScaleMatrix(0, 0, z);
        }

        public static C_M4X4 ScaleMatrix(float x, float y, float z)
        {
            return new C_M4X4(
                x * 1, 0, 0, 0,
                0, y * 1, 0, 0,
                0, 0, z * 1, 0,
                0, 0, 0, 1
                );
        }

        public static C_M4X4 GetRotXMatrix(float orientation)
        {
            float angleR = orientation * C_MathF.DegToRad;
            float cosT = C_MathF.Cos(angleR);
            float sinT = C_MathF.Sin(angleR);

            return new C_M4X4(
                1, 0, 0, 0,
                0, cosT, -sinT, 0,
                0, sinT, cosT, 0,
                0, 0, 0, 1
                );
        }

        public static C_M4X4 GetRotYMatrix(float orientation)
        {
            float angleR = orientation * C_MathF.DegToRad;
            float cosT = C_MathF.Cos(angleR);
            float sinT = C_MathF.Sin(angleR);

            return new C_M4X4(
                 cosT, 0, sinT, 0,
                    0, 1, 0, 0,
                -sinT, 0, cosT, 0,
                    0, 0, 0, 0
                );
        }

        public static C_M4X4 GetRotZMatrix(float orientation)
        {
            float angleR = orientation * C_MathF.DegToRad;
            float cosT = C_MathF.Cos(angleR);
            float sinT = C_MathF.Sin(angleR);

            return new C_M4X4(
                 cosT, -sinT, 0, 0,
                 sinT, cosT, 0, 0,
                    0, 0, 0, 0,
                    0, 0, 0, 0
                );
        }

        public static implicit operator C_M4X4(C_M3X3 a)
        {
            return new C_M4X4(
                a.E00, a.E01, a.E02, 0,
                a.E10, a.E11, a.E12, 0,
                a.E20, a.E21, a.E22, 0,
                    0, 0, 0, 0
                );
        }

        public static C_M4X4 RotationMatrix(
        float t1, float t2, float t3)
        {
            //Calculate repeated values. 
            float a = -MathF.Sin(t1) * MathF.Sin(t2);
            float b = MathF.Sin(t1) * MathF.Sin(t3);
            float c = MathF.Cos(t1) * MathF.Cos(t3);

            /////////////
            //Calculate individual elements. 
            ////////////

            //X column. 
            float x1 = MathF.Cos(t2) * MathF.Cos(t3);
            float x2 = (a * MathF.Cos(t3)) + (MathF.Cos(t1) * MathF.Sin(t3));
            float x3 = (c * MathF.Sin(t1)) + b;

            //Y column.
            float y1 = -MathF.Cos(t2) * MathF.Sin(t3);
            float y2 = a + c;
            float y3 = (-b * MathF.Cos(t1)) - b;

            //z Column.
            float z1 = -MathF.Sin(t2);
            float z2 = -MathF.Sin(t1) * MathF.Cos(t2);
            float z3 = MathF.Cos(t1) * MathF.Cos(t2);

            /////////////
            //Set and return matrix. 
            ////////////

            return new C_M4X4(
                x1, x2, x3, 0,
                y1, y2, y3, 0,
                z1, z2, z3, 0,
                 0, 0, 0, 0
                );
        }

        public static C_M4X4 ReflectionMatrix(float signX, float signY, float signZ)
        {
            return new(
                signX, 0, 0, 0,
                0, signY, 0, 0,
                0, 0, signZ, 0,
                0, 0, 0, 1
                );
        }

        ///TODO: TEST THIS FUNCTION CAUSE IT LOOKS WEIRD ASF. 
        public static C_M4X4 ShearMatrix(float shearX, float shearY)
        {
            return new(
                1, shearY, 0, 0,
                shearX, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 1
                );
        }
    }
}
