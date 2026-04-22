using System;

namespace Physics
{
    public readonly struct PhysicalManifold
    {
        public readonly PhysicalBody BodyA;
        public readonly PhysicalBody BodyB;
        public readonly PhysicalVector Normal;
        public readonly float Depth;
        public readonly PhysicalVector Contact1;
        public readonly PhysicalVector Contact2;
        public readonly int ContactCount;

        public PhysicalManifold(
            PhysicalBody bodyA, PhysicalBody bodyB,
            PhysicalVector normal, float depth,
            PhysicalVector contact1, PhysicalVector contact2, int contactCount)
        {
            BodyA = bodyA;
            BodyB = bodyB;
            Normal = normal;
            Depth = depth;
            Contact1 = contact1;
            Contact2 = contact2;
            ContactCount = contactCount;
        }
    }
}
