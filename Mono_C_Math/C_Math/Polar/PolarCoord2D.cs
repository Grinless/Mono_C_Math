﻿using C_Math.Functions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Math.Polar
{
    /// <summary>
    /// Class used to represent Polar Coordinate points. 
    /// </summary>
    public sealed class PolarCoord2D
    {
        /// <summary>
        /// Returns a PolarCoord2D with both r and theta set to zero.
        /// </summary>
        public static readonly PolarCoord2D None = new PolarCoord2D(0, 0);

        /// <summary>
        /// Returns a PolarCoord2D with r set to 1 and theta set to zero.
        /// </summary>
        public static readonly PolarCoord2D R1T0 = new PolarCoord2D(1, 0);

        /// <summary>
        /// The current radius set to the point. 
        /// </summary>
        private float _r;

        /// <summary>
        /// The current theta set to the point. 
        /// </summary>
        private float _theta;

        /// <summary>
        /// Returns the cartestian coordinate X position of the coordinate.  
        /// </summary>
        public float RectangularX => _r * C_MathF.Cos(_theta);

        /// <summary>
        /// Returns the cartesian Y position of the coordinate. 
        /// </summary>
        public float RectangularY => _r * C_MathF.Sin(_theta);

        /// <summary>
        /// CTOR:
        /// </summary>
        private PolarCoord2D()
        {
            this._r = 0;
            this._theta = 0;
        }

        /// <summary>
        /// CTOR: 
        /// </summary>
        /// <param name="r"> The radius of the point from the origin. </param>
        /// <param name="theta"> The angle of rotation applied to the point in radians. </param>
        public PolarCoord2D(float r, float theta)
        {
            _r = r;
            _theta = theta;
        }

        /// <summary>
        /// Returns the Polar Coordiante as a Cartisian coordinate Vector2. 
        /// </summary>
        public Vector2 ConvertPolarToVec() =>
            new Vector2(RectangularX, RectangularY);

        public static explicit operator Vector2(PolarCoord2D a) =>
            new Vector2(a.RectangularX, a.RectangularY);

        public static explicit operator PolarCoord2D(Vector2 a)
        {
            return new PolarCoord2D(
                C_MathF.Sqrt((a.X * a.X) + (a.Y * a.Y)), //Calculate R. 
                C_MathF.Atan(a.Y / a.X) //Calculate Theta. 
                );
        }
    }

    public enum PolarDirection
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    public struct PolarMultiOutput
    {
        public float a, b, c, d;
        public bool aSet, bSet, cSet, dSet;

        public PolarMultiOutput(float a, float b)
        {
            this.a = a;
            this.aSet = true;
            this.b = b;
            this.bSet = true;
            this.c = 0;
            this.cSet = false;
            this.d = 0;
            this.dSet = false;
        }

        public PolarMultiOutput(float a, float b, float c)
        {
            this.a = a;
            this.aSet = true;
            this.b = b;
            this.bSet = true;
            this.c = c;
            this.cSet = true;
            this.d = 0;
            this.dSet = false;
        }

        public PolarMultiOutput(float a, float b, float c, float d)
        {
            this.a = a;
            this.aSet = true;
            this.b = b;
            this.bSet = true;
            this.c = c;
            this.cSet = true;
            this.d = d;
            this.dSet = true;
        }
    }

    public class PolarCoordBuilder
    {
        #region Builder Functions.

        /// <summary>
        /// Returns an array of cartestian Vector points using the provided function. 
        /// </summary>
        /// <param name="function">  The function used for building the points.                      </param>
        /// <param name="divisions"> The number of divisions that should occur on the path.          </param>
        /// <param name="period">    The period overwhich the polar function should traverse.        </param>
        /// <returns>                A Vector2[] containing points alligned with the polar function. </returns>
        public static Vector2[] ArrayBuilder(System.Func<float, float> function, int divisions, PolarPeriod2D period)
        {
            List<PolarCoord2D> steps = new List<PolarCoord2D>();
            float theta = period.MinRadians;
            float thetaStep = period.Length / divisions;

            for (int i = 0; i < divisions; i++)
            {
                steps.Add(new PolarCoord2D(function(theta), theta));
                theta += thetaStep;
            }

            return ConvertToVectors(steps);
        }

        /// <summary>
        /// Returns an array of cartestian Vector points using the provided function. 
        /// </summary>
        /// <param name="function">  The function used for building the points.                      </param>
        /// <param name="amplitude"> The height of the function.                                     </param>
        /// <param name="divisions"> The number of divisions that should occur on the path.          </param>
        /// <param name="period">    The period overwhich the polar function should traverse.        </param>
        /// <returns>                A Vector2[] containing points alligned with the polar function. </returns>
        public static Vector2[] ArrayBuilder(System.Func<float, float, float> function, float amplitude, int divisions, PolarPeriod2D period)
        {
            List<PolarCoord2D> steps = new List<PolarCoord2D>();
            float theta = period.MinRadians;
            float thetaStep = period.Length / divisions;

            for (int i = 0; i < divisions; i++)
            {
                steps.Add(new PolarCoord2D(function(theta, amplitude), theta));
                theta += thetaStep;
            }

            return ConvertToVectors(steps);
        }

        /// <summary>
        /// Returns an array of cartestian Vector points using the provided function. 
        /// </summary>
        /// <param name="function">  The function used for building the points.                      </param>
        /// <param name="amplitude"> The height of the function.                                     </param>
        /// <param name="b"></param>
        /// <param name="divisions"> The number of divisions that should occur on the path.          </param>
        /// <param name="period">    The period overwhich the polar function should traverse.        </param>
        /// <returns>                A Vector2[] containing points alligned with the polar function. </returns>
        public static Vector2[] ArrayBuilder(System.Func<float, float, float, float> function, float amplitude, float b, int divisions, PolarPeriod2D period)
        {
            List<PolarCoord2D> steps = new List<PolarCoord2D>();
            float theta = period.MinRadians;
            float thetaStep = period.Length / divisions;

            for (int i = 0; i < divisions; i++)
            {
                steps.Add(new PolarCoord2D(function(theta, amplitude, b), theta));
                theta += thetaStep;
            }

            return ConvertToVectors(steps);
        }

        /// <summary>
        /// Returns an array of cartestian Vector points using the provided function. 
        /// </summary>
        /// <param name="function">  The function used for building the points.                      </param>
        /// <param name="amplitude"> The height of the function.                                     </param>
        /// <param name="b"></param>
        /// <param name="divisions"> The number of divisions that should occur on the path.          </param>
        /// <param name="period">    The period overwhich the polar function should traverse.        </param>
        /// <returns>                A Vector2[] containing points alligned with the polar function. </returns>
        public static Vector2[] ArrayBuilder(System.Func<float, float, int, float> function, float amplitude, int b, int divisions, PolarPeriod2D period)
        {
            List<PolarCoord2D> steps = new List<PolarCoord2D>();
            float theta = period.MinRadians;
            float thetaStep = period.Length / divisions;

            for (int i = 0; i < divisions; i++)
            {
                steps.Add(new PolarCoord2D(function(theta, amplitude, b), theta));
                theta += thetaStep;
            }

            return ConvertToVectors(steps);
        }

        /// <summary>
        /// Returns an array of cartestian Vector points using the provided function. 
        /// </summary>
        /// <param name="function">  The function used for building the points.                      </param>
        /// <param name="amplitude"> The height of the function.                                     </param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="divisions"> The number of divisions that should occur on the path.          </param>
        /// <param name="period">    The period overwhich the polar function should traverse.        </param>
        /// <returns>                A Vector2[] containing points alligned with the polar function. </returns>
        public static Vector2[] ArrayBuilder(System.Func<float, float, float, float, float> function, float amplitude, float b, float c, int divisons, PolarPeriod2D p)
        {
            List<PolarCoord2D> steps = new List<PolarCoord2D>();
            float theta = p.MinRadians;
            float thetaStep = p.Length / divisons;

            for (int i = 0; i < divisons; i++)
            {
                steps.Add(new PolarCoord2D(function(theta, amplitude, b, c), theta));
                theta += thetaStep;
            }

            return ConvertToVectors(steps);
        }

        /// <summary>
        /// Returns an array of cartestian Vector points using the provided function. 
        /// </summary>
        /// <param name="function">  The function used for building the points.                      </param>
        /// <param name="amplitude"> The height of the function.                                     </param>
        /// <param name="b"></param>
        /// <param name="divisions"> The number of divisions that should occur on the path.          </param>
        /// <param name="period">    The period overwhich the polar function should traverse.        </param>
        /// <returns>                A Vector2[] containing points alligned with the polar function. </returns>
        public static Vector2[] ArrayBuilder(System.Func<float, PolarMultiOutput> function, int divisions, PolarPeriod2D period)
        {
            List<PolarCoord2D> steps = new List<PolarCoord2D>();
            float theta = period.MinRadians;
            float thetaStep = period.Length / divisions;
            PolarMultiOutput output;
            for (int i = 0; i < divisions; i++)
            {
                output = function(theta);

                if (output.aSet)
                    steps.Add(new PolarCoord2D(output.a, theta));
                if (output.bSet)
                    steps.Add(new PolarCoord2D(output.b, theta));
                if (output.cSet)
                    steps.Add(new PolarCoord2D(output.c, theta));
                if (output.dSet)
                    steps.Add(new PolarCoord2D(output.d, theta));

                theta += thetaStep;
            }

            return ConvertToVectors(steps);
        }

        /// <summary>
        /// Returns an array of cartestian Vector points using the provided function. 
        /// </summary>
        /// <param name="function">  The function used for building the points.                      </param>
        /// <param name="amplitude"> The height of the function.                                     </param>
        /// <param name="b"></param>
        /// <param name="divisions"> The number of divisions that should occur on the path.          </param>
        /// <param name="period">    The period overwhich the polar function should traverse.        </param>
        /// <returns>                A Vector2[] containing points alligned with the polar function. </returns>
        public static Vector2[] ArrayBuilder(System.Func<float, float, PolarMultiOutput> function, float amplitude, int divisions, PolarPeriod2D period)
        {
            List<PolarCoord2D> steps = new List<PolarCoord2D>();
            float theta = period.MinRadians;
            float thetaStep = period.Length / divisions;
            PolarMultiOutput output;
            for (int i = 0; i < divisions; i++)
            {
                output = function(theta, amplitude);

                if (output.aSet)
                    steps.Add(new PolarCoord2D(output.a, theta));
                if (output.bSet)
                    steps.Add(new PolarCoord2D(output.b, theta));
                if (output.cSet)
                    steps.Add(new PolarCoord2D(output.c, theta));
                if (output.dSet)
                    steps.Add(new PolarCoord2D(output.d, theta));

                theta += thetaStep;
            }

            return ConvertToVectors(steps);
        }

        /// <summary>
        /// Returns an array of cartestian Vector points using the provided function. 
        /// </summary>
        /// <param name="function">  The function used for building the points.                      </param>
        /// <param name="amplitude"> The height of the function.                                     </param>
        /// <param name="b"></param>
        /// <param name="divisions"> The number of divisions that should occur on the path.          </param>
        /// <param name="period">    The period overwhich the polar function should traverse.        </param>
        /// <returns>                A Vector2[] containing points alligned with the polar function. </returns>
        public static Vector2[] ArrayBuilder(System.Func<float, float, float, PolarMultiOutput> function, float amplitude, float b, int divisions, PolarPeriod2D period)
        {
            List<PolarCoord2D> steps = new List<PolarCoord2D>();
            float theta = period.MinRadians;
            float thetaStep = period.Length / divisions;
            PolarMultiOutput output;
            for (int i = 0; i < divisions; i++)
            {
                output = function(theta, amplitude, b);

                if (output.aSet)
                    steps.Add(new PolarCoord2D(output.a, theta));
                if (output.bSet)
                    steps.Add(new PolarCoord2D(output.b, theta));
                if (output.cSet)
                    steps.Add(new PolarCoord2D(output.c, theta));
                if (output.dSet)
                    steps.Add(new PolarCoord2D(output.d, theta));

                theta += thetaStep;
            }

            return ConvertToVectors(steps);
        }

        /// <summary>
        /// Returns an array of cartestian Vector points using the provided function. 
        /// </summary>
        /// <param name="function">  The function used for building the points.                      </param>
        /// <param name="amplitude"> The height of the function.                                     </param>
        /// <param name="b"></param>
        /// <param name="divisions"> The number of divisions that should occur on the path.          </param>
        /// <param name="period">    The period overwhich the polar function should traverse.        </param>
        /// <returns>                A Vector2[] containing points alligned with the polar function. </returns>
        public static Vector2[] ArrayBuilder(System.Func<float, float, float, float, PolarMultiOutput> function, float amplitude, float b, float c, int divisions, PolarPeriod2D period)
        {
            List<PolarCoord2D> steps = new List<PolarCoord2D>();
            float theta = period.MinRadians;
            float thetaStep = period.Length / divisions;
            PolarMultiOutput output;
            for (int i = 0; i < divisions; i++)
            {
                output = function(theta, amplitude, b, c);

                if (output.aSet)
                    steps.Add(new PolarCoord2D(output.a, theta));
                if (output.bSet)
                    steps.Add(new PolarCoord2D(output.b, theta));
                if (output.cSet)
                    steps.Add(new PolarCoord2D(output.c, theta));
                if (output.dSet)
                    steps.Add(new PolarCoord2D(output.d, theta));

                theta += thetaStep;
            }

            return ConvertToVectors(steps);
        }

        #endregion

        /// <summary>
        /// Returns a Cartesian Vector2[] from a List of PolarCoor2D's. 
        /// Cleans out NAN vectors. 
        /// </summary>
        /// <param name="polarSteps"> The polar coords to convert. </param>
        private static Vector2[] ConvertToVectors(List<PolarCoord2D> polarSteps)
        {
            List<Vector2> points = new List<Vector2>();
            Vector2 temp;

            for (int i = 0; i < polarSteps.Count; i++)
            {
                temp = polarSteps[i].ConvertPolarToVec();

                if (!float.IsNaN(temp.X) && !float.IsNaN(temp.Y))
                {
                    points.Add(polarSteps[i].ConvertPolarToVec());
                }
            }

            return points.ToArray();
        }

        /// <summary>
        /// https://mathworld.wolfram.com/ArchimedeanSpiral.html
        /// </summary>
        /// <param name="amplitude"> The amplitude of the function.</param>
        /// <param name="wrapping"> The tightness of the spiral. </param>
        /// <param name="divisions"> The number of point divisions to create. </param>
        /// <param name="minPeriod"> The minuimum period of the function (raidans). </param>
        /// <param name="maxPeriod"> The maximum period of the function (radians).</param>
        /// <returns> A array of points in the form of the Archimedean spiral.</returns>
        public static Vector2[] ArchimedeanSpiral(float amplitude, float wrapping, int divisions, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.ArchimedeanSpiral, amplitude, wrapping, divisions, p);

        /// <summary>
        /// https://mathworld.wolfram.com/ArchimedesSpiral.html
        /// </summary>
        /// <param name="amplitude"> The amplitude of the function.</param>
        /// <param name="wrapping"> The tightness of the spiral. </param>
        /// <param name="divisions"> The number of point divisions to create. </param>
        /// <param name="minPeriod"> The minuimum period of the function (raidans). </param>
        /// <param name="maxPeriod"> The maximum period of the function (radians).</param>
        /// <returns> A array of points in the form of the Archimedean spiral.</returns>
        public static Vector2[] ArchimedesSpiral(float amplitude, int divisions, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.ArchimedesSpiral, amplitude, divisions, p);

        /// <summary>
        /// https://mathworld.wolfram.com/Atom-Spiral.html
        /// </summary>
        /// <param name="amplitude"> The amplitude of the function.</param>
        /// <param name="divisions"> The number of point divisions to create. </param>
        /// <param name="p"> The minuimum period of the function (raidans). </param>
        /// <returns> A array of points in the form of the Atom spiral.</returns>
        public static Vector2[] AtomSpiral(float amplitude, int divisions, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.AtomSpiral, amplitude, divisions, p);

        /// <summary>
        /// https://mathworld.wolfram.com/Bifoliate.html
        /// </summary>
        /// <param name="amplitude"> The amplitude of the function.</param>
        /// <param name="divisions"> The number of point divisions to create. </param>
        /// <param name="minPeriod"> The minuimum period of the function (raidans). </param>
        /// <param name="maxPeriod"> The maximum period of the function (radians).</param>
        /// <returns> A array of points in the form of the Atom spiral.</returns>
        public static Vector2[] Bifoliate(float amplitude, int divisions, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.Bifoliate, amplitude, divisions, p);

        /// <summary>
        /// https://mathworld.wolfram.com/Bifolium.html
        /// </summary>
        /// <param name="amplitude"> The amplitude of the function.</param>
        /// <param name="divisions"> The number of point divisions to create. </param>
        /// <param name="minPeriod"> The minuimum period of the function (raidans). </param>
        /// <param name="maxPeriod"> The maximum period of the function (radians).</param>
        /// <returns> A array of points in the form of the Atom spiral.</returns>
        public static Vector2[] Bifolium(float amplitude, int divisions, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.Bifolium, amplitude, divisions, p);

        /// <summary>
        /// https://en.wikipedia.org/wiki/Butterfly_curve_(transcendental)
        /// https://mathworld.wolfram.com/ButterflyCurve.html
        /// </summary>
        /// <param name="divisions"> The number of point divisions to create. </param>
        /// <param name="minPeriod"> The minuimum period of the function (raidans). </param>
        /// <param name="maxPeriod"> The maximum period of the function (radians).</param>
        /// <returns> A array of points in the form of the Atom spiral.</returns>
        public static Vector2[] ButterflyCurve(int divisions, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.ButterflyCurve, divisions, p);

        /// <summary>
        /// https://mathworld.wolfram.com/CissoidofDiocles.html
        /// </summary>
        /// <param name="amplitude"> The amplitude of the function.</param>
        /// <param name="divisions"> The number of point divisions to create. </param>
        /// <param name="p"> The minuimum period of the function (raidans). </param>
        /// <returns> A array of points in the form of the Atom spiral.</returns>
        public static Vector2[] CissoidOfDiocles(float amplitude, int divisions, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.CissoidOfDiocles, amplitude, divisions, p);

        /// <summary>
        /// https://mathworld.wolfram.com/CayleysSextic.html
        /// https://en.wikipedia.org/wiki/Cayley%27s_sextic#:~:text=Equations%20of%20the%20curve&text=4(x2%20%2B%20y2,2%20%2B%20y2)2%20.&text=y%20%3D%20cos3t%20sin%203t.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="numberOfPoints"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Vector2[] CayleysSextic(float a, int numberOfPoints, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.CayleysSextic, a, numberOfPoints, p);

        /// <summary>
        /// https://mathworld.wolfram.com/Cochleoid.html
        /// </summary>
        /// <param name="a"></param>
        /// <param name="divisions"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Vector2[] Cochleoid(float a, int divisions, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.Cochleoid, a, divisions, p);

        /// <summary>
        /// https://mathworld.wolfram.com/CycloidofCeva.html
        /// </summary>
        /// <param name="a"></param>
        /// <param name="divisions"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Vector2[] CycloidOfCeva(int divisions, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.CycloidOfCeva, divisions, p);

        /// <summary>
        /// https://mathworld.wolfram.com/ConchoidofdeSluze.html
        /// </summary>
        /// <param name="a"></param>
        /// <param name="divisions"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Vector2[] ConchoidOfDeSluze(float a, int divisions, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.ConchoidOfDeSluze, a, divisions, p);

        public static Vector2[] Cardioid(float a, int divisions, PolarPeriod2D p, PolarDirection dir)
        {
            switch (dir)
            {
                case PolarDirection.UP:
                    return ArrayBuilder(PolarFunctions2D.CardioidCuspUp, a, divisions, p);
                case PolarDirection.DOWN:
                    return ArrayBuilder(PolarFunctions2D.CardioidCuspDown, a, divisions, p);
                case PolarDirection.LEFT:
                    return ArrayBuilder(PolarFunctions2D.CardioidCuspLeft, a, divisions, p);
                case PolarDirection.RIGHT:
                    return ArrayBuilder(PolarFunctions2D.CardioidCuspRight, a, divisions, p);
            }
            return ArrayBuilder(PolarFunctions2D.CardioidCuspRight, a, divisions, p);
        }

        /// <summary>
        /// https://mathworld.wolfram.com/DevilsCurve.html
        /// </summary>
        /// <param name="a"></param>
        /// <param name="divisions"></param>
        /// <param name="maxPeriod"></param>
        /// <returns></returns>
        public static Vector2[] DevilsCurve(float a, float b, int divisions, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.DevilsCurve, a, b, divisions, p);

        /// <summary>
        /// https://mathworld.wolfram.com/DuererFolium.html
        /// </summary>
        /// <param name="a"></param>
        /// <param name="divisions"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Vector2[] DurerFolium(float a, int divisions, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.DurerFolium, a, divisions, p);

        /// <summary>
        /// https://mathworld.wolfram.com/FermatsSpiral.html
        /// </summary>
        /// <param name="a"></param>
        /// <param name="steps"></param>
        /// <param name="maxPeriod"></param>
        /// <returns></returns>
        public static Vector2[] FermatsSpiral(float a, int steps, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.FermatsSpiral, a, steps, p);

        /// <summary>
        /// https://mathworld.wolfram.com/DuererFolium.html
        /// </summary>
        /// <param name="a"></param>
        /// <param name="divisions"></param>
        /// <param name="maxPeriod"></param>
        /// <returns></returns>
        public static Vector2[] FreethsNeproid(float a, int divisions, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.FreethsNeproid, a, divisions, p);

        /// <summary>
        /// The Garfield Curve as defined by Wolfram. 
        /// https://mathworld.wolfram.com/GarfieldCurve.html
        /// </summary>
        /// <param name="steps"> The number of steps the range should be divided into. </param>
        /// <param name="p"> The maximum theta a curve should reach. </param>
        /// <returns></returns>
        public static Vector2[] GarfieldCurve(int steps, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.GarfieldCurves, steps, p);

        /// <summary>
        /// https://mathworld.wolfram.com/EightCurve.html
        /// </summary>
        /// <param name="a"></param>
        /// <param name="steps"></param>
        /// <param name="maxPeriod"></param>
        /// <returns></returns>
        public static Vector2[] EightCurve(float a, int steps, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.EightCurve, a, steps, p);

        /// <summary>
        /// https://mathworld.wolfram.com/FoliumofDescartes.html
        /// </summary>
        /// <param name="a"></param>
        /// <param name="steps"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Vector2[] FoliumOfDescartes(float a, int steps, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.FoliumofDescartes, a, steps, p);

        /// <summary>
        /// https://mathworld.wolfram.com/Epispiral.html
        /// </summary>
        /// <param name="a"></param>
        /// <param name="divisions"></param>
        /// <param name="maxPeriod"></param>
        /// <returns></returns>
        public static Vector2[] Epispiral(float a, int n, int divisions, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.Epispiral, a, n, divisions, p);

        /// <summary>
        /// https://mathworld.wolfram.com/Hyperbola.html
        /// </summary>
        /// <param name="amplitude"> The amplitude of the function. </param>
        /// <param name="divisions"> The number of points to be created. </param>
        /// <param name="p"> The minimum period for the output (Radians).</param>
        /// <returns></returns>
        public static Vector2[] Hyperbola(float amplitude, int divisions, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.Hyperbola, amplitude, divisions, p);

        /// <summary>
        /// Hyperbolic Spiral: https://mathworld.wolfram.com/HyperbolicSpiral.html
        /// </summary>
        /// <param name="amplitude"> The amplitude of the function. </param>
        /// <param name="divisions"> The number of points to be created. </param>
        /// <param name="p"> The minimum period for the output (Radians).</param>
        /// <returns></returns>
        public static Vector2[] HyperbolicSpiral(float amplitude, int divisions, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.HyperbolicSpiral, amplitude, divisions, p);

        /// <summary>
        /// Hippopede Curve: https://mathworld.wolfram.com/Hippopede.html
        /// </summary>
        /// <param name="a"> The amplitude of the curve. </param>
        /// <param name="b"> The length of the curve. </param>
        /// <param name="steps"> The steps per point across the curve. </param>
        /// <param name="p"> The maximum point of the curve in radians. </param>
        /// <returns></returns>
        public static Vector2[] HippopedeCurve(float a, float b, int steps, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.HippopedeCurve, a, b, steps, p);

        /// <summary>
        /// https://mathworld.wolfram.com/KampyleofEudoxus.html
        /// </summary>
        /// <param name="a"></param>
        /// <param name="divisions"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Vector2[] KampyleOfEudoxus(float a, int divisions, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.KaympleOfEudoxus, a, divisions, p);

        /// <summary>
        /// https://mathworld.wolfram.com/KappaCurve.html
        /// </summary>
        /// <param name="a"></param>
        /// <param name="divisions"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Vector2[] KappaCurve(float a, int divisions, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.KappaCurveFunc, a, divisions, p);

        /// <summary>
        /// https://mathworld.wolfram.com/KeplersFolium.html
        /// </summary>
        /// <param name="a"></param>
        /// <param name="divisions"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Vector2[] KeplersFolium(float a, float b, int divisions, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.KeplersFoliumFunc, a, b, divisions, p);

        /// <summary>
        /// https://mathworld.wolfram.com/KeplersFolium.html
        /// </summary>
        /// <param name="a"></param>
        /// <param name="divisions"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Vector2[] Lemniscate(float a, int divisions, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.Lemniscate, a, divisions, p);

        /// <summary>
        /// https://mathworld.wolfram.com/KeplersFolium.html
        /// </summary>
        /// <param name="a"></param>
        /// <param name="divisions"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Vector2[] Limacon(float a, float b, int divisions, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.Limacon, a, b, divisions, p);

        /// <summary>
        /// https://mathworld.wolfram.com/LimaconTrisectrix.html
        /// </summary>
        /// <param name="a"></param>
        /// <param name="divisions"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Vector2[] LimaconTrisectrix(float a, int divisions, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.LimaconTrisectrix, a, divisions, p);

        /// <summary>
        /// https://mathworld.wolfram.com/LogarithmicSpiral.html
        /// </summary>
        /// <param name="a"></param>
        /// <param name="numberOfPoints"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Vector2[] LogarithmicSpiral(float a, float b, int numberOfPoints, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.LogaritmicSpiral, a, b, numberOfPoints, p);

        /// <summary>
        /// https://mathworld.wolfram.com/MaclaurinTrisectrix.html
        /// </summary>
        /// <param name="a"></param>
        /// <param name="divisions"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Vector2[] MaclaurinTrisectrix(float a, int divisions, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.MaclaurinTrisectrix, a, divisions, p);

        /// <summary>
        /// https://mathworld.wolfram.com/MalteseCrossCurve.html
        /// </summary>
        /// <param name="divisions"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Vector2[] MalteseCross(int divisions, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.MalteseCross, divisions, p);

        /// <summary>
        /// https://mathworld.wolfram.com/Neoid.html
        /// </summary>
        /// <param name="a"></param>
        /// <param name="divisions"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Vector2[] Neoid(float a, float b, int divisions, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.Neoid, a, b, divisions, p);

        /// <summary>
        /// https://mathworld.wolfram.com/Ophiuride.html
        /// </summary>
        /// <param name="a"></param>
        /// <param name="divisions"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Vector2[] Ophiuride(float a, float b, int divisions, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.Ophiuride, a, b, divisions, p);

        /// <summary>
        /// https://mathworld.wolfram.com/Quadrifolium.html
        /// </summary>
        /// <param name="a"></param>
        /// <param name="divisions"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Vector2[] Quadrifolium(float a, int divisions, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.Quadrifolium, a, divisions, p);

        //public static Vector2[] Rose(float a, float k, int divisions, PolarPeriod2D p) =>
        //ArrayBuilder(PolarFunctions2D.Rose, a, k, divisions, p);

        /// <summary>
        /// https://mathworld.wolfram.com/ScarabaeusCurve.html
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="divisions"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Vector2[] ScarabaeusCurve(float a, float b, int divisions, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.ScarabaeusCurve, a, b, divisions, p);

        /// <summary>
        /// https://mathworld.wolfram.com/SemicubicalParabola.html
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="divisions"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Vector2[] SemicubicalParabola(float a, float b, int divisions, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.SemicubicalParabola, a, divisions, p);

        /// <summary>
        /// https://mathworld.wolfram.com/SemicubicalParabola.html
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="divisions"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Vector2[] Strophoid(float a, float b, int divisions, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.Strophoid, a, b, divisions, p);

        /// <summary>
        /// https://mathworld.wolfram.com/SwastikaCurve.html
        /// </summary>
        /// <param name="divisions"> The number of points to create. </param>
        /// <param name="p"> The minmum period of the output (radians). </param>
        /// <returns></returns>
        public static Vector2[] SwastikaCurve(int divisions, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.SwastikaCurve, divisions, p);

        /// <summary>
        /// https://mathworld.wolfram.com/Trifolium.html
        /// </summary>
        /// <param name="a"></param>
        /// <param name="divisions"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Vector2[] Trifolium(float a, int divisions, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.Trifolium, a, divisions, p);

        /// <summary>
        /// https://mathworld.wolfram.com/TschirnhausenCubic.html
        /// </summary>
        /// <param name="amplitude"></param>
        /// <param name="divisions"></param>
        /// <param name="p"></param>
        /// <returns></returns>
        public static Vector2[] TschirnhausenCubic(float amplitude, int divisions, PolarPeriod2D p) =>
            ArrayBuilder(PolarFunctions2D.TschirnhausenCubic, amplitude, divisions, p);

        /// <summary>
        /// https://mathworld.wolfram.com/WattsCurve.html
        /// </summary>
        /// <param name="amplitude"></param>
        /// <param name="b"></param>
        /// <param name="c"></param>
        /// <param name="divisions"></param>
        /// <param name="period"></param>
        /// <returns></returns>
        public static Vector2[] WattsCurve(float amplitude, float b, float c, int divisions, PolarPeriod2D period) =>
            ArrayBuilder(PolarFunctions2D.WattsCurve, amplitude, b, c, divisions, period);
    }
}
