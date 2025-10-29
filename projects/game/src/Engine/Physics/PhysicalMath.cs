using System;

namespace Physics
{
    public static class PhysicalMath
    {
        /// <summary>
        /// Equal to 1/2 of a millimeter.
        /// </summary>
        public static readonly float VerySmallAmount = 0.0005f;

        public static float Clamp(float value, float min, float max)
        {
            if (min == max)
            {
                return min;
            }

            if (min > max)
            {
                throw new ArgumentOutOfRangeException("min is greater than the max.");
            }

            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }

        public static int Clamp(int value, int min, int max)
        {
            if (min == max)
            {
                return min;
            }

            if (min > max)
            {
                throw new ArgumentOutOfRangeException("min is greater than the max.");
            }

            if (value < min)
            {
                return min;
            }

            if (value > max)
            {
                return max;
            }

            return value;
        }

        public static float LengthSquared(PhysicalVector v)
        {
            return v.X * v.X + v.Y * v.Y;
        }

        public static float Length(PhysicalVector v)
        {
            return MathF.Sqrt(v.X * v.X + v.Y * v.Y);
        }

        public static float DistanceSquared(PhysicalVector a, PhysicalVector b)
        {
            float dx = a.X - b.X;
            float dy = a.Y - b.Y;
            return dx * dx + dy * dy;
        }

        public static float Distance(PhysicalVector a, PhysicalVector b)
        {
            float dx = a.X - b.X;
            float dy = a.Y - b.Y;
            return MathF.Sqrt(dx * dx + dy * dy);
        }

        public static PhysicalVector Normalize(PhysicalVector v)
        {
            float len = Length(v);
            return new PhysicalVector(v.X / len, v.Y / len);
        }

        public static float Dot(PhysicalVector a, PhysicalVector b)
        {
            // a · b = ax * bx + ay * by
            return a.X * b.X + a.Y * b.Y;
        }

        public static float Cross(PhysicalVector a, PhysicalVector b)
        {
            // cz = ax * by − ay * bx
            return a.X * b.Y - a.Y * b.X;
        }

        public static bool NearlyEqual(float a, float b)
        {
            return MathF.Abs(a - b) < VerySmallAmount;
        }

        public static bool NearlyEqual(PhysicalVector a, PhysicalVector b)
        {
            return DistanceSquared(a, b) < VerySmallAmount * VerySmallAmount;
        }


        public static PhysicalVector RotateVector(PhysicalVector vector, float angle)
        {
            float cos = MathF.Cos(angle);
            float sin = MathF.Sin(angle);

            float newX = vector.X * cos - vector.Y * sin;
            float newY = vector.X * sin + vector.Y * cos;

            return new PhysicalVector(newX, newY);
        }


        public static PhysicalVector ClampMagnitude(PhysicalVector vector, float maxLength)
        {
            float lengthSquared = LengthSquared(vector);

            if (lengthSquared > maxLength * maxLength)
            {
                float scaleFactor = maxLength / MathF.Sqrt(lengthSquared);
                return new PhysicalVector(vector.X * scaleFactor, vector.Y * scaleFactor);
            }

            return vector;
        }


        public static float AngleBetween(PhysicalVector a, PhysicalVector b)
        {
            // Calculate the dot product
            float dotProduct = Dot(a, b);

            // Calculate the magnitudes of the vectors
            float magnitudeA = Length(a);
            float magnitudeB = Length(b);

            // Calculate the angle in radians using the arccosine function
            float angle = (float)Math.Acos(dotProduct / (magnitudeA * magnitudeB));

            // Convert the angle to degrees
            float angleInDegrees = angle * (180f / (float)Math.PI);

            return angleInDegrees;
        }


    }
}
