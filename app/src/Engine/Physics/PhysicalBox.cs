using System;
using Microsoft.Xna.Framework;

namespace Physics
{
    public readonly struct PhysicalBox
    {
        public readonly Vector2 Min;
        public readonly Vector2 Max;

        public static readonly PhysicalBox Empty = new PhysicalBox(Vector2.Zero, Vector2.Zero);

        public PhysicalBox(Vector2 min, Vector2 max)
        {
            Min = min;
            Max = max;
        }

        public PhysicalBox(Vector2 center, float width, float height)
        {
            float left = center.X - width * 0.5f;
            float right = left + width;
            float bottom = center.Y - height * 0.5f;
            float top = bottom + height;

            Min = new Vector2(left, bottom);
            Max = new Vector2(right, top);
        }

        public PhysicalBox(float minX, float maxX, float minY, float maxY)
        {
            Min = new Vector2(minX, minY);
            Max = new Vector2(maxX, maxY);
        }

        public bool Equals(in PhysicalBox other)
        {
            return Min == other.Min && Max == other.Max;
        }

        public override bool Equals(object obj)
        {
            if (obj is PhysicalBox other)
            {
                return Equals(other);
            }

            return false;
        }

        public override int GetHashCode()
        {
            int result = new { Min, Max }.GetHashCode();
            return result;
        }

        public override string ToString()
        {
            string result = string.Format("Min: {0}, Max: {1}", Min, Max);
            return result;
        }
    }
}
