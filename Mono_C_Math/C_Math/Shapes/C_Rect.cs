using C_Math.Functions;
using C_Math.Matricies;
using Microsoft.Xna.Framework;

namespace C_Math.Shapes
{
    public struct C_Rect : IShape
    {
        private Vector2 _translation;
        private Vector2 _scale;
        private float _orientation;

        #region Interfaced Properties. 
        public Vector2[] GetShapeVerts
        {
            get => new Vector2[]
            {
                new Vector2(1, 1),
                new Vector2(-1, 1),
                new Vector2(-1, -1),
                new Vector2(1, -1),
            };
        }

        public Vector2[] GetTransformedVerts
        {
            get
            {
                C_M3X3 matrix = C_M3X3.TRS_Matrix(_translation, _scale, _orientation);
                Vector2[] initalVerts = GetShapeVerts;
                return matrix * initalVerts;
            }
        }

        public Vector2 Position
        {
            get { return _translation; }
            set { _translation = value; }
        }

        public Vector2 Scale
        {
            get { return _scale; }
            set { _scale = value; }
        }

        public float Orientation
        {
            get => _orientation;
            set
            {
                _orientation = value;
                if (_orientation > 360)
                    _orientation = 0;
                if (_orientation < 0)
                    _orientation = 360;
            }
        }

        public IShape GetShape => this;
        #endregion

        #region Properties. 
        public Vector2[] GetSides
        {
            get
            {
                Vector2[] verts = GetTransformedVerts;

                return new Vector2[]
                {
                (verts[1] - verts[0]),
                (verts[2] - verts[1]),
                (verts[3] - verts[2]),
                (verts[0] - verts[3])
                };
            }
        }

        public float[] GetLengths
        {
            get
            {
                Vector2[] verts = GetSides;

                return new float[]
                {
                verts[0].Length(),
                verts[1].Length(),
                verts[2].Length(),
                verts[3].Length()
                };
            }
        }

        public Vector2[] GetUnitizedSides
        {
            get
            {
                Vector2[] s = GetSides;
                Vector2[] norms = new Vector2[s.Length];

                for (int i = 0; i < s.Length; i++)
                {
                    norms[i] = C_MathF.Normal(s[i]);
                }
                return norms;
            }
        }

        public Vector2[] GetMidpoints
        {
            get
            {
                Vector2[] vert = GetTransformedVerts;
                Vector2[] sides = GetUnitizedSides;
                float[] lengths = GetLengths;

                return new Vector2[]
                {
                vert[0] + (sides[0] * (lengths[0] / 2)),
                vert[1] + (sides[1] * (lengths[1] / 2)),
                vert[2] + (sides[2] * (lengths[2] / 2)),
                vert[3] + (sides[3] * (lengths[3] / 2))
                };
            }
        }

        public Vector2[] GetNormals
        {
            get
            {
                Vector2[] sides = GetSides;
                float[] lengths = GetLengths;

                foreach (Vector2 v in sides)
                {
                    v.Normalize();
                }

                return new Vector2[]
                {
                    C_MathF.Normal(C_MathF.PerpendicularCW(sides[0] * (lengths[0]/2))),
                    C_MathF.Normal(C_MathF.PerpendicularCW(sides[1] * (lengths[1]/2))),
                    C_MathF.Normal(C_MathF.PerpendicularCW(sides[2] * (lengths[2]/2))),
                    C_MathF.Normal(C_MathF.PerpendicularCW(sides[3] * (lengths[3]/2))),
                };
            }
        }
        #endregion

        public Vector2 GetTransformedOrigin
        {
            get
            {
                C_M3X3 matrix = C_M3X3.TRS_Matrix(_translation, _scale, _orientation);
                return matrix * Vector2.Zero;
            }
        }

        public Vector2 Min
        {
            get
            {
                Vector2 tOrigin = GetTransformedOrigin;
                return new Vector2(tOrigin.X - Scale.X, tOrigin.Y - Scale.Y);
            }
        }

        public Vector2 Max
        {
            get
            {
                Vector2 tOrigin = GetTransformedOrigin;
                return new Vector2(tOrigin.X + Scale.X, tOrigin.Y + Scale.Y);
            }
        }

        public bool GetCollision(C_Rect a)
        {
            //If axis aligned get axis aligned collision.
            if (Orientation == 0)
            {
                return Collision.C_AABB.AABBCollisionCheck(this, a);
            }

            //TODO: Replace with a call to S.A.T
            return false;

        }

        public C_Rect(Vector2 position, Vector2 scale, float orientation)
        {
            this._translation = position;
            this._scale = scale;
            this._orientation = orientation;
        }
    }
}
