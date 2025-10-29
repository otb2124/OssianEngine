using System;

namespace Physics
{
    public readonly struct PhysicalAABB
    {
        public readonly PhysicalVector Min;
        public readonly PhysicalVector Max;

        public PhysicalAABB(PhysicalVector min, PhysicalVector max)
        {
            Min = min;
            Max = max;
        }

        public PhysicalAABB(float minX, float minY, float maxX, float maxY)
        {
            Min = new PhysicalVector(minX, minY);
            Max = new PhysicalVector(maxX, maxY);
        }
    }
}
