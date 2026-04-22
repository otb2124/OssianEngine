using Microsoft.Xna.Framework;
using System;

namespace Physics
{
    public readonly struct PhysicalVector
    {
        public readonly float X;
        public readonly float Y;

        public static readonly PhysicalVector Zero = new PhysicalVector(0f, 0f);

        public PhysicalVector(float x, float y)
        {
            X = x;
            Y = y;
        }

        public static PhysicalVector operator +(PhysicalVector a, PhysicalVector b)
        {
            return new PhysicalVector(a.X + b.X, a.Y + b.Y);
        }

        public static PhysicalVector operator -(PhysicalVector a, PhysicalVector b)
        {
            return new PhysicalVector(a.X - b.X, a.Y - b.Y);
        }

        public static PhysicalVector operator -(PhysicalVector v)
        {
            return new PhysicalVector(-v.X, -v.Y);
        }

        public static PhysicalVector operator *(PhysicalVector v, float s)
        {
            return new PhysicalVector(v.X * s, v.Y * s);
        }

        public static PhysicalVector operator *(float s, PhysicalVector v)
        {
            return new PhysicalVector(v.X * s, v.Y * s);
        }

        public static PhysicalVector operator /(PhysicalVector v, float s)
        {
            return new PhysicalVector(v.X / s, v.Y / s);
        }

        internal static PhysicalVector Transform(PhysicalVector v, PhysicalTransform transform)
        {
            return new PhysicalVector(
                transform.Cos * v.X - transform.Sin * v.Y + transform.PositionX,
                transform.Sin * v.X + transform.Cos * v.Y + transform.PositionY);
        }

        public static PhysicalVector Lerp(PhysicalVector a, PhysicalVector b, float t)
        {
            float clampedT = MathHelper.Clamp(t, 0f, 1f);
            return new PhysicalVector(
                MathHelper.Lerp(a.X, b.X, clampedT),
                MathHelper.Lerp(a.Y, b.Y, clampedT)
            );
        }

        public bool Equals(PhysicalVector other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            if (obj is PhysicalVector other)
            {
                return Equals(other);
            }

            return false;
        }


        public Vector2 ToVector2()
        {
            return PhysicalConverter.ToVector2(this);
        }

        public Point ToPoint()
        {
            return PhysicalConverter.ToVector2(this).ToPoint();
        }

        public override int GetHashCode()
        {
            return new { X, Y }.GetHashCode();
        }

        public override string ToString()
        {
            return $"X: {X}, Y: {Y}";
        }
    }
}
