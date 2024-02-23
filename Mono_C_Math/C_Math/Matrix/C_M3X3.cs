using C_Math.Functions;
using C_Math.Matricies;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Math.Matricies
{
    public sealed class C_M3X3
    {
        /// <summary>
        /// The float value of the first row first column. 
        /// </summary>
        public float E00;

        /// <summary>
        /// The float value of the first row second column. 
        /// </summary>
        public float E01;

        /// <summary>
        /// The float value of the first row third column. 
        /// </summary>
        public float E02;

        /// <summary>
        /// The float value of the second row first column. 
        /// </summary>
        public float E10;

        /// <summary>
        /// The float value of the second row second column. 
        /// </summary>
        public float E11;

        /// <summary>
        /// The float value of the second row third column. 
        /// </summary>
        public float E12;

        /// <summary>
        /// The float value of the third row first column. 
        /// </summary>
        public float E20;

        /// <summary>
        /// The float value of the third row second column. 
        /// </summary>
        public float E21;

        /// <summary>
        /// The float value of the third row third column. 
        /// </summary>
        public float E22;

        #region Shorthand Refs. 
        private static C_M3X3 identity =
            new C_M3X3(
                1, 0, 0,
                0, 1, 0,
                0, 0, 1
            );
        private static C_M3X3 zero =
            new C_M3X3(
                    0, 0, 0,
                    0, 0, 0,
                    0, 0, 0
                );
        public static C_M3X3 Identity => identity;
        public static C_M3X3 Zero => zero;
        #endregion

        #region Row/Column Access Properties. 
        public Vector3 R1
        {
            get => new Vector3(E00, E01, E02);
            set
            {
                E00 = value.X;
                E01 = value.Y;
                E02 = value.Z;
            }
        }

        public Vector3 R2
        {
            get => new Vector3(E10, E11, E12);
            set
            {
                E10 = value.X;
                E11 = value.Y;
                E12 = value.Z;
            }
        }

        public Vector3 R3
        {
            get => new Vector3(E20, E21, E22);
            set
            {
                E20 = value.X;
                E21 = value.Y;
                E22 = value.Z;
            }
        }

        public Vector3 C1
        {
            get => new Vector3(E00, E10, E20);
            set
            {
                E00 = value.X;
                E10 = value.Y;
                E20 = value.Z;
            }
        }

        public Vector3 C2
        {
            get => new Vector3(E01, E11, E21);
            set
            {
                E01 = value.X;
                E11 = value.Y;
                E21 = value.Z;
            }
        }

        public Vector3 C3
        {
            get => new Vector3(E02, E12, E22);
            set
            {
                E02 = value.X;
                E12 = value.Y;
                E22 = value.Z;
            }
        }
        #endregion

        /// <summary>
        /// Create a new C_M3X3. 
        /// </summary>
        /// <param name="E00"> The float value of the first row first column.   </param>
        /// <param name="E01"> The float value of the first row second column.  </param>
        /// <param name="E02"> The float value of the first row third column.   </param>
        /// <param name="E10"> The float value of the second row first column.  </param>
        /// <param name="E11"> The float value of the second row second column. </param>
        /// <param name="E12"> The float value of the second row third column.  </param>
        /// <param name="E20"> The float value of the third row first column.   </param>
        /// <param name="E21"> The float value of the third row second column.  </param>
        /// <param name="E22"> The float value of the third row third column.   </param>
        public C_M3X3(float E00, float E01, float E02, float E10,
            float E11, float E12, float E20, float E21, float E22)
        {
            this.E00 = E00;
            this.E01 = E01;
            this.E02 = E02;
            this.E10 = E10;
            this.E11 = E11;
            this.E12 = E12;
            this.E20 = E20;
            this.E21 = E21;
            this.E22 = E22;
        }

        /// <summary>
        /// Interchange C_M3X3 a's rows with columns.  
        /// </summary>
        /// <param name="a"> The C_M2X2 to transpose.</param>
        public static void Transpose(ref C_M3X3 a)
        {
            Vector3 r1 = a.R1;
            Vector3 r2 = a.R2;
            Vector3 r3 = a.R3;

            a.C1 = r1;
            a.C2 = r2;
            a.C3 = r3;
        }

        public float Determinant()
        {
            ///[E00 E01 E02]   [E00 E01 E02 E00 E01]
            ///[E10 E11 E12]   [E10 E11 E12 E10 E11]
            ///[E20 E21 E22]   [E20 E21 E22 E20 E21]
            ///
            ///E00 * E11 * E22 + E01 * E12 * E20 + E02 * E10 * E21 - E20 * E11 * E02 - 
            ///E21 * E12 * E00 - E22 * E10 * E01
            ///

            return
                  E00 * E11 * E22
                + E01 * E12 * E20
                + E02 * E10 * E21
                - E20 * E11 * E02
                - E21 * E12 * E00
                - E22 * E10 * E01;
        }

        #region Utility Functions. 
        /// <summary>
        /// Returns C_M3X3 b with copied elements from C_M3X3 a.
        /// </summary>
        /// <param name="a"> The C_M3X3 to copy. </param>
        /// <param name="b"> The recipient C_M3X3. </param>
        public static void Copy(C_M3X3 a, ref C_M3X3 b)
        {
            b.E00 = a.E00;
            b.E01 = a.E01;
            b.E10 = a.E10;
            b.E11 = a.E11;
            b.E00 = a.E00;
            b.E01 = a.E01;
            b.E02 = a.E02;
            b.E10 = a.E10;
            b.E11 = a.E11;
            b.E12 = a.E12;
            b.E20 = a.E20;
            b.E21 = a.E21;
            b.E22 = a.E22;
        }

        /// <summary>
        /// Print the C_M3X3 to using Unity Engines built in debugger. 
        /// </summary>
        public void PrintMatrix()
        {
            Console.WriteLine(this.ToString());
        }

        /// <summary>
        /// Retrive the C_M3X3 formatted as a string. 
        /// </summary>
        /// <returns> String formatted copy of the matrix. </returns>
        public override string ToString()
        {
            return "[" + E00 + " " + E01 + " " + E02 + "]" + "\n" +
                   "[" + E10 + " " + E11 + " " + E12 + "]" + "\n" +
                   "[" + E20 + " " + E21 + " " + E22 + "]" + "\n";
        }
        #endregion

        public static C_M3X3 TranslationMatrixX(float x) =>
            TranslationMatrix(x, 0);

        public static C_M3X3 TranslationMatrixY(float y) =>
            TranslationMatrix(0, y);

        public static C_M3X3 TranslationMatrix(Vector2 vec) =>
            TranslationMatrix(vec.X, vec.Y);

        public static C_M3X3 TranslationMatrix(float x, float y)
        {
            return new C_M3X3(
                1, 0, x,
                0, 1, y,
                0, 0, 1
                );
        }

        public static C_M3X3 GetScaleMatrixX(float x) =>
            GetScaleMatrix(x, 1);

        public static C_M3X3 GetScaleMatrixY(float y) =>
            GetScaleMatrix(1, y);

        public static C_M3X3 GetScaleMatrixY(Vector2 value) =>
            GetScaleMatrix(value.X, value.Y);

        public static C_M3X3 GetScaleMatrix(float x, float y)
        {
            return new C_M3X3(
                   x * 1, 0, 0,
                   0, y * 1, 0,
                   0, 0, 1
                );
        }

        public static C_M3X3 RotationMatrix(
            float theta1, float theta2, float theta3)
        {
            //Calculate repeated values. 
            float a = -MathF.Sin(theta1) * MathF.Sin(theta2);
            float b = MathF.Sin(theta1) * MathF.Sin(theta3);
            float c = MathF.Cos(theta1) * MathF.Cos(theta3);

            /////////////
            //Calculate Matrix. 
            ////////////

            return new(
                    MathF.Cos(theta2) * MathF.Cos(theta3),
                    (a * MathF.Cos(theta3)) + (MathF.Cos(theta1) * MathF.Sin(theta3)),
                    (c * MathF.Sin(theta1)) + b
                    ,
                    -MathF.Cos(theta2) * MathF.Sin(theta3),
                    a + c,
                    (-b * MathF.Cos(theta1)) - b
                    ,
                    -MathF.Sin(theta2),
                    -MathF.Sin(theta1) * MathF.Cos(theta2),
                    MathF.Cos(theta1) * MathF.Cos(theta2)
                );
        }

        public static C_M3X3 RotationMatrix(float angle)
        {
            float angleR = angle * C_MathF.DegToRad;

            return new(
                 MathF.Cos(angleR), MathF.Sin(angleR), 0,
                -MathF.Sin(angleR), MathF.Cos(angleR), 0,
                                 0, 0, 1
                );
        }

        public static C_M3X3 ReflectionMatrix(int signX, int signY)
        {
            return new(
                signX * 1, 0, 0,
                        0, signY * 1, 0,
                        0, 0, 1
                );
        }

        public static C_M3X3 ShearMatrix(float shearX, float shearY)
        {
            return new(
                     1, shearY, 0,
                shearX, 1, 0,
                     0, 0, 1
                );
        }

        public static C_M3X3 TRS_Matrix(Vector2 position, Vector2 scale, float orientation)
        {
            C_M3X3 scaleM = C_M3X3.GetScaleMatrix(scale.X, scale.Y);
            C_M3X3 rotM = C_M3X3.RotationMatrix(orientation);
            C_M3X3 transM = C_M3X3.TranslationMatrix(position.X, position.Y);
            return transM * rotM * scaleM;
        }

        public static C_M3X3 operator *(float lhs, C_M3X3 rhs)
        {
            return new C_M3X3(
                lhs * rhs.E00, lhs * rhs.E01, lhs * rhs.E02,
                lhs * rhs.E10, lhs * rhs.E11, lhs * rhs.E12,
                lhs * rhs.E20, lhs * rhs.E21, lhs * rhs.E22
                );
        }

        public static C_M3X3 operator *(C_M3X3 lhs, C_M3X3 rhs)
        {
            float E00 = lhs.E00 * rhs.E00 + lhs.E01 * rhs.E10 + lhs.E02 * rhs.E20;
            float E10 = lhs.E10 * rhs.E00 + lhs.E11 * rhs.E10 + lhs.E12 * rhs.E20;
            float E20 = lhs.E20 * rhs.E00 + lhs.E21 * rhs.E10 + lhs.E22 * rhs.E20;

            float E01 = lhs.E00 * rhs.E01 + lhs.E01 * rhs.E11 + lhs.E02 * rhs.E21;
            float E11 = lhs.E10 * rhs.E01 + lhs.E11 * rhs.E11 + lhs.E12 * rhs.E21;
            float E21 = lhs.E20 * rhs.E01 + lhs.E21 * rhs.E11 + lhs.E22 * rhs.E21;

            float E02 = lhs.E00 * rhs.E02 + lhs.E01 * rhs.E12 + lhs.E02 * rhs.E22;
            float E12 = lhs.E10 * rhs.E02 + lhs.E11 * rhs.E12 + lhs.E12 * rhs.E22;
            float E22 = lhs.E20 * rhs.E02 + lhs.E21 * rhs.E12 + lhs.E22 * rhs.E22;

            return new C_M3X3(
                E00, E01, E02,
                E10, E11, E12,
                E20, E21, E22
                );
        }

        public static Vector2 operator *(C_M3X3 m, Vector2 v)
        {
            Vector3 v3 = new Vector3(v.X, v.Y, 1);
            float x = m.E00 * v3.X + m.E01 * v3.Y + m.E02 * v3.Z;
            float y = m.E10 * v3.X + m.E11 * v3.Y + m.E12 * v3.Z;
            return new Vector2(x, y);
        }

        public static Vector3 operator *(C_M3X3 m, Vector3 v)
        {
            float x = m.E00 * v.X + m.E01 * v.Y + m.E02 * v.Z;
            float y = m.E10 * v.X + m.E11 * v.Y + m.E12 * v.Z;
            float z = m.E20 * v.X + m.E21 * v.Y + m.E22 * v.Z;
            return new Vector3(x, y, z);
        }

        public static Vector2[] operator *(C_M3X3 m, Vector2[] v)
        {
            Vector2[] vecs = new Vector2[v.Length];
            Vector3 v3;
            for (int i = 0; i < v.Length; i++)
            {
                v3 = new Vector3(v[i].X, v[i].Y, 1);
                v3 = m * v3;
                vecs[i] = new Vector2(v3.X, v3.Y);
            }
            return vecs;
        }

        public static C_M3X3 operator +(C_M3X3 lhs, C_M3X3 rhs) =>
            new C_M3X3(
                lhs.E00 + rhs.E00, lhs.E10 + rhs.E10, lhs.E02 + rhs.E02,
                lhs.E10 + rhs.E10, lhs.E11 + rhs.E11, lhs.E12 + rhs.E12,
                lhs.E20 + rhs.E20, lhs.E21 + rhs.E21, lhs.E22 + rhs.E22
                );

        public static C_M3X3 operator -(C_M3X3 lhs, C_M3X3 rhs) =>
            lhs + (-1.0F * rhs);

        public override bool Equals(object obj)
        {
            C_M3X3 objInst;

            if (obj is not C_M2X2)
                return false;

            objInst = (C_M3X3)obj;

            return (
                R1 == objInst.R1 &&
                R2 == objInst.R2 &&
                R3 == objInst.R3);
        }

        //TODO: Fix row and column access Properties. 

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + R1.GetHashCode();
            hash = hash * 23 + R2.GetHashCode();
            hash = hash * 23 + R3.GetHashCode();
            hash = hash * 23 + C1.GetHashCode();
            hash = hash * 23 + C2.GetHashCode();
            hash = hash * 23 + C3.GetHashCode();
            return hash;
        }

        public static bool operator ==(C_M3X3 x, C_M3X3 y)
        {
            return (
                x.E00 == y.E00 &&
                x.E01 == y.E01 &&
                x.E02 == y.E02 &&
                x.E10 == y.E10 &&
                x.E11 == y.E11 &&
                x.E12 == y.E12 &&
                x.E20 == y.E20 &&
                x.E21 == y.E21 &&
                x.E22 == y.E22
                );
        }

        public static bool operator !=(C_M3X3 x, C_M3X3 y)
        {
            return !(x == y);
        }
    }
}
