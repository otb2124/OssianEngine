using System;

namespace Physics
{
    public static class Collisions
    {
        public static void PointSegmentDistance(PhysicalVector p, PhysicalVector a, PhysicalVector b, out float distanceSquared, out PhysicalVector cp)
        {
            PhysicalVector ab = b - a;
            PhysicalVector ap = p - a;

            float proj = PhysicalMath.Dot(ap, ab);
            float abLenSq = PhysicalMath.LengthSquared(ab);
            float d = proj / abLenSq;

            if (d <= 0f)
            {
                cp = a;
            }
            else if (d >= 1f)
            {
                cp = b;
            }
            else
            {
                cp = a + ab * d;
            }

            distanceSquared = PhysicalMath.DistanceSquared(p, cp);
        }

        public static bool IntersectAABBs(PhysicalAABB a, PhysicalAABB b)
        {
            if (a.Max.X <= b.Min.X || b.Max.X <= a.Min.X ||
                a.Max.Y <= b.Min.Y || b.Max.Y <= a.Min.Y)
            {
                return false;
            }

            return true;
        }

        public static void FindContactPoints(
            PhysicalBody bodyA, PhysicalBody bodyB,
            out PhysicalVector contact1, out PhysicalVector contact2,
            out int contactCount)
        {
            contact1 = PhysicalVector.Zero;
            contact2 = PhysicalVector.Zero;
            contactCount = 0;

            BodyShapeType BodyShapeTypeA = bodyA.BodyShapeType;
            BodyShapeType BodyShapeTypeB = bodyB.BodyShapeType;

            if (BodyShapeTypeA is BodyShapeType.Box)
            {
                if (BodyShapeTypeB is BodyShapeType.Box)
                {
                    FindPolygonsContactPoints(bodyA.GetTransformedVertices(), bodyB.GetTransformedVertices(),
                        out contact1, out contact2, out contactCount);
                }
                else if (BodyShapeTypeB is BodyShapeType.Circle)
                {
                    FindCirclePolygonContactPoint(bodyB.Position, bodyB.Radius, bodyA.Position, bodyA.GetTransformedVertices(), out contact1);
                    contactCount = 1;
                }
            }
            else if (BodyShapeTypeA is BodyShapeType.Circle)
            {
                if (BodyShapeTypeB is BodyShapeType.Box)
                {
                    FindCirclePolygonContactPoint(bodyA.Position, bodyA.Radius, bodyB.Position, bodyB.GetTransformedVertices(), out contact1);
                    contactCount = 1;
                }
                else if (BodyShapeTypeB is BodyShapeType.Circle)
                {
                    FindCirclesContactPoint(bodyA.Position, bodyA.Radius, bodyB.Position, out contact1);
                    contactCount = 1;
                }
            }
        }

        private static void FindPolygonsContactPoints(
            PhysicalVector[] verticesA, PhysicalVector[] verticesB,
            out PhysicalVector contact1, out PhysicalVector contact2, out int contactCount)
        {
            contact1 = PhysicalVector.Zero;
            contact2 = PhysicalVector.Zero;
            contactCount = 0;

            float minDistSq = float.MaxValue;

            for (int i = 0; i < verticesA.Length; i++)
            {
                PhysicalVector p = verticesA[i];

                for (int j = 0; j < verticesB.Length; j++)
                {
                    PhysicalVector va = verticesB[j];
                    PhysicalVector vb = verticesB[(j + 1) % verticesB.Length];

                    PointSegmentDistance(p, va, vb, out float distSq, out PhysicalVector cp);

                    if (PhysicalMath.NearlyEqual(distSq, minDistSq))
                    {
                        if (!PhysicalMath.NearlyEqual(cp, contact1))
                        {
                            contact2 = cp;
                            contactCount = 2;
                        }
                    }
                    else if (distSq < minDistSq)
                    {
                        minDistSq = distSq;
                        contactCount = 1;
                        contact1 = cp;
                    }
                }
            }

            for (int i = 0; i < verticesB.Length; i++)
            {
                PhysicalVector p = verticesB[i];

                for (int j = 0; j < verticesA.Length; j++)
                {
                    PhysicalVector va = verticesA[j];
                    PhysicalVector vb = verticesA[(j + 1) % verticesA.Length];

                    PointSegmentDistance(p, va, vb, out float distSq, out PhysicalVector cp);

                    if (PhysicalMath.NearlyEqual(distSq, minDistSq))
                    {
                        if (!PhysicalMath.NearlyEqual(cp, contact1))
                        {
                            contact2 = cp;
                            contactCount = 2;
                        }
                    }
                    else if (distSq < minDistSq)
                    {
                        minDistSq = distSq;
                        contactCount = 1;
                        contact1 = cp;
                    }
                }
            }
        }

        private static void FindCirclePolygonContactPoint(
            PhysicalVector circleCenter, float circleRadius,
            PhysicalVector polygonCenter, PhysicalVector[] polygonVertices,
            out PhysicalVector cp)
        {
            cp = PhysicalVector.Zero;

            float minDistSq = float.MaxValue;

            for (int i = 0; i < polygonVertices.Length; i++)
            {
                PhysicalVector va = polygonVertices[i];
                PhysicalVector vb = polygonVertices[(i + 1) % polygonVertices.Length];

                PointSegmentDistance(circleCenter, va, vb, out float distSq, out PhysicalVector contact);

                if (distSq < minDistSq)
                {
                    minDistSq = distSq;
                    cp = contact;
                }
            }
        }

        private static void FindCirclesContactPoint(PhysicalVector centerA, float radiusA, PhysicalVector centerB, out PhysicalVector cp)
        {
            PhysicalVector ab = centerB - centerA;
            PhysicalVector dir = PhysicalMath.Normalize(ab);
            cp = centerA + dir * radiusA;
        }

        public static bool Collide(PhysicalBody bodyA, PhysicalBody bodyB, out PhysicalVector normal, out float depth)
        {
            normal = PhysicalVector.Zero;
            depth = 0f;

            BodyShapeType BodyShapeTypeA = bodyA.BodyShapeType;
            BodyShapeType BodyShapeTypeB = bodyB.BodyShapeType;

            if (BodyShapeTypeA is BodyShapeType.Box)
            {
                if (BodyShapeTypeB is BodyShapeType.Box)
                {
                    return IntersectPolygons(
                        bodyA.Position, bodyA.GetTransformedVertices(),
                        bodyB.Position, bodyB.GetTransformedVertices(),
                        out normal, out depth);
                }
                else if (BodyShapeTypeB is BodyShapeType.Circle)
                {
                    bool result = IntersectCirclePolygon(
                        bodyB.Position, bodyB.Radius,
                        bodyA.Position, bodyA.GetTransformedVertices(),
                        out normal, out depth);

                    normal = -normal;
                    return result;
                }
            }
            else if (BodyShapeTypeA is BodyShapeType.Circle)
            {
                if (BodyShapeTypeB is BodyShapeType.Box)
                {
                    return IntersectCirclePolygon(
                        bodyA.Position, bodyA.Radius,
                        bodyB.Position, bodyB.GetTransformedVertices(),
                        out normal, out depth);
                }
                else if (BodyShapeTypeB is BodyShapeType.Circle)
                {
                    return IntersectCircles(
                        bodyA.Position, bodyA.Radius,
                        bodyB.Position, bodyB.Radius,
                        out normal, out depth);
                }
            }

            return false;
        }

        public static bool IntersectCirclePolygon(PhysicalVector circleCenter, float circleRadius,
                                                    PhysicalVector polygonCenter, PhysicalVector[] vertices,
                                                    out PhysicalVector normal, out float depth)
        {
            normal = PhysicalVector.Zero;
            depth = float.MaxValue;

            PhysicalVector axis = PhysicalVector.Zero;
            float axisDepth = 0f;
            float minA, maxA, minB, maxB;

            for (int i = 0; i < vertices.Length; i++)
            {
                PhysicalVector va = vertices[i];
                PhysicalVector vb = vertices[(i + 1) % vertices.Length];

                PhysicalVector edge = vb - va;
                axis = new PhysicalVector(-edge.Y, edge.X);
                axis = PhysicalMath.Normalize(axis);

                ProjectVertices(vertices, axis, out minA, out maxA);
                ProjectCircle(circleCenter, circleRadius, axis, out minB, out maxB);

                if (minA >= maxB || minB >= maxA)
                {
                    return false;
                }

                axisDepth = MathF.Min(maxB - minA, maxA - minB);

                if (axisDepth < depth)
                {
                    depth = axisDepth;
                    normal = axis;
                }
            }

            int cpIndex = FindClosestPointOnPolygon(circleCenter, vertices);
            PhysicalVector cp = vertices[cpIndex];

            axis = cp - circleCenter;
            axis = PhysicalMath.Normalize(axis);

            ProjectVertices(vertices, axis, out minA, out maxA);
            ProjectCircle(circleCenter, circleRadius, axis, out minB, out maxB);

            if (minA >= maxB || minB >= maxA)
            {
                return false;
            }

            axisDepth = MathF.Min(maxB - minA, maxA - minB);

            if (axisDepth < depth)
            {
                depth = axisDepth;
                normal = axis;
            }

            PhysicalVector direction = polygonCenter - circleCenter;

            if (PhysicalMath.Dot(direction, normal) < 0f)
            {
                normal = -normal;
            }

            return true;
        }

        private static int FindClosestPointOnPolygon(PhysicalVector circleCenter, PhysicalVector[] vertices)
        {
            int result = -1;
            float minDistance = float.MaxValue;

            for (int i = 0; i < vertices.Length; i++)
            {
                PhysicalVector v = vertices[i];
                float distance = PhysicalMath.Distance(v, circleCenter);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    result = i;
                }
            }

            return result;
        }

        private static void ProjectCircle(PhysicalVector center, float radius, PhysicalVector axis, out float min, out float max)
        {
            PhysicalVector direction = PhysicalMath.Normalize(axis);
            PhysicalVector directionAndRadius = direction * radius;

            PhysicalVector p1 = center + directionAndRadius;
            PhysicalVector p2 = center - directionAndRadius;

            min = PhysicalMath.Dot(p1, axis);
            max = PhysicalMath.Dot(p2, axis);

            if (min > max)
            {
                // swap the min and max values.
                float t = min;
                min = max;
                max = t;
            }
        }

        public static bool IntersectPolygons(PhysicalVector centerA, PhysicalVector[] verticesA, PhysicalVector centerB, PhysicalVector[] verticesB, out PhysicalVector normal, out float depth)
        {
            normal = PhysicalVector.Zero;
            depth = float.MaxValue;

            for (int i = 0; i < verticesA.Length; i++)
            {
                PhysicalVector va = verticesA[i];
                PhysicalVector vb = verticesA[(i + 1) % verticesA.Length];

                PhysicalVector edge = vb - va;
                PhysicalVector axis = new PhysicalVector(-edge.Y, edge.X);
                axis = PhysicalMath.Normalize(axis);

                ProjectVertices(verticesA, axis, out float minA, out float maxA);
                ProjectVertices(verticesB, axis, out float minB, out float maxB);

                if (minA >= maxB || minB >= maxA)
                {
                    return false;
                }

                float axisDepth = MathF.Min(maxB - minA, maxA - minB);

                if (axisDepth < depth)
                {
                    depth = axisDepth;
                    normal = axis;
                }
            }

            for (int i = 0; i < verticesB.Length; i++)
            {
                PhysicalVector va = verticesB[i];
                PhysicalVector vb = verticesB[(i + 1) % verticesB.Length];

                PhysicalVector edge = vb - va;
                PhysicalVector axis = new PhysicalVector(-edge.Y, edge.X);
                axis = PhysicalMath.Normalize(axis);

                ProjectVertices(verticesA, axis, out float minA, out float maxA);
                ProjectVertices(verticesB, axis, out float minB, out float maxB);

                if (minA >= maxB || minB >= maxA)
                {
                    return false;
                }

                float axisDepth = MathF.Min(maxB - minA, maxA - minB);

                if (axisDepth < depth)
                {
                    depth = axisDepth;
                    normal = axis;
                }
            }

            PhysicalVector direction = centerB - centerA;

            if (PhysicalMath.Dot(direction, normal) < 0f)
            {
                normal = -normal;
            }

            return true;
        }

        private static void ProjectVertices(PhysicalVector[] vertices, PhysicalVector axis, out float min, out float max)
        {
            min = float.MaxValue;
            max = float.MinValue;

            for (int i = 0; i < vertices.Length; i++)
            {
                PhysicalVector v = vertices[i];
                float proj = PhysicalMath.Dot(v, axis);

                if (proj < min) { min = proj; }
                if (proj > max) { max = proj; }
            }
        }

        public static bool IntersectCircles(
            PhysicalVector centerA, float radiusA,
            PhysicalVector centerB, float radiusB,
            out PhysicalVector normal, out float depth)
        {
            normal = PhysicalVector.Zero;
            depth = 0f;

            float distance = PhysicalMath.Distance(centerA, centerB);
            float radii = radiusA + radiusB;

            if (distance >= radii)
            {
                return false;
            }

            normal = PhysicalMath.Normalize(centerB - centerA);
            depth = radii - distance;

            return true;
        }

    }
}
