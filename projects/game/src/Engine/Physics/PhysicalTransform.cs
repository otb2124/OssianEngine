using System;

namespace Physics
{
    public struct PhysicalTransform
    {
        public readonly static PhysicalTransform Zero = new PhysicalTransform(0f, 0f, 0f);

        public readonly float PositionX;
        public readonly float PositionY;
        public readonly float Sin;
        public readonly float Cos;


        public float CosScaleX => Cos;
        public float SinScaleY => -Sin;
        public float SinScaleX => Sin;
        public float CosScaleY => Cos;

        public PhysicalTransform(PhysicalVector position, float angle)
        {
            PositionX = position.X;
            PositionY = position.Y;
            Sin = MathF.Sin(angle);
            Cos = MathF.Cos(angle);
        }

        public PhysicalTransform(float x, float y, float angle)
        {
            PositionX = x;
            PositionY = y;
            Sin = MathF.Sin(angle);
            Cos = MathF.Cos(angle);
        }


    }
}
