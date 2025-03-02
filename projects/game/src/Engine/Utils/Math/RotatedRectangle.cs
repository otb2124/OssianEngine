using Microsoft.Xna.Framework;
using System;


namespace Utils
{
    public class RotatedRectangle
    {
        public Vector2 Center;
        public float Width;
        public float Height;
        public float Rotation;


        public RotatedRectangle()
        {
            Center = Vector2.Zero;
            Width = 0f;
            Height = 0f;
            Rotation = 0f;
        }

        public RotatedRectangle(Vector2 pos, Vector2 size, float rot = 0f)
        {
            Center = pos;
            Width = size.X;
            Height = size.Y;
            Rotation = rot;
        }

        public RotatedRectangle(Vector2 center, float width, float height, float rotation = 0f)
        {
            Center = center;
            Width = width;
            Height = height;
            Rotation = rotation;
        }

        public Vector2[] GetCorners()
        {
            Vector2[] corners = new Vector2[4];

            corners[0] = new Vector2(-Width / 2, -Height / 2);
            corners[1] = new Vector2(Width / 2, -Height / 2);
            corners[2] = new Vector2(Width / 2, Height / 2);
            corners[3] = new Vector2(-Width / 2, Height / 2);

            for (int i = 0; i < 4; i++)
            {
                corners[i] = Vector2.Transform(corners[i], Matrix.CreateRotationZ(Rotation)) + Center;
            }

            return corners;
        }

        public bool Contains(Vector2 point)
        {
            Vector2[] corners = GetCorners();
            int i, j = 3;
            bool inside = false;

            for (i = 0; i < 4; j = i++)
            {
                if (((corners[i].Y > point.Y) != (corners[j].Y > point.Y)) &&
                    (point.X < (corners[j].X - corners[i].X) * (point.Y - corners[i].Y) / (corners[j].Y - corners[i].Y) + corners[i].X))
                {
                    inside = !inside;
                }
            }

            return inside;
        }

        public Vector2 Size()
        {
            return new Vector2(Width, Height);
        }

        public bool Intersects(RotatedRectangle other)
        {
            Vector2[] thisCorners = GetCorners();
            Vector2[] otherCorners = other.GetCorners();

            Vector2[] axes = new Vector2[8];

            for (int i = 0; i < 4; i++)
            {
                Vector2 edge = thisCorners[(i + 1) % 4] - thisCorners[i];
                axes[i] = new Vector2(-edge.Y, edge.X);
            }

            for (int i = 0; i < 4; i++)
            {
                Vector2 edge = otherCorners[(i + 1) % 4] - otherCorners[i];
                axes[4 + i] = new Vector2(-edge.Y, edge.X);
            }

            foreach (Vector2 axis in axes)
            {
                float minA = float.MaxValue, maxA = float.MinValue;
                foreach (var corner in thisCorners)
                {
                    float projection = Vector2.Dot(corner, axis);
                    minA = Math.Min(minA, projection);
                    maxA = Math.Max(maxA, projection);
                }

                float minB = float.MaxValue, maxB = float.MinValue;
                foreach (var corner in otherCorners)
                {
                    float projection = Vector2.Dot(corner, axis);
                    minB = Math.Min(minB, projection);
                    maxB = Math.Max(maxB, projection);
                }

                if (maxA < minB || maxB < minA)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
