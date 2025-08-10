using Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Physics
{
    public sealed class FlatWorld
    {
        public static int TransformCount = 0;
        public static int NoTransformCount = 0;

        public static readonly float MinBodySize = 0.01f * 0.01f;
        public static readonly float MaxBodySize = 1000f * 1000f;

        public static readonly float MinDensity = 0.5f;     // g/cm^3
        public static readonly float MaxDensity = 21.4f;
         
        public static readonly int MinIterations = 1;
        public static readonly int MaxIterations = 128;

        public FlatVector gravity;
        public static readonly int ConstantGravityMultiplier = 50;
        public static readonly float GlobalGravityMultiplier = 1f;

        public List<FlatBody> bodyList;
        private List<(int, int)> contactPairs;

        private FlatVector[] contactList;
        private FlatVector[] impulseList;
        private FlatVector[] raList;
        private FlatVector[] rbList;
        private FlatVector[] frictionImpulseList;
        private float[] jList;

        public int BodyCount
        {
            get { return bodyList.Count; }
        }

        public FlatWorld()
        {
            gravity = new FlatVector(0f, -9.81f * GlobalGravityMultiplier * ConstantGravityMultiplier);
            bodyList = new List<FlatBody>();
            contactPairs = new List<(int, int)>();

            contactList = new FlatVector[2];
            impulseList = new FlatVector[2];
            raList = new FlatVector[2];
            rbList = new FlatVector[2];
            frictionImpulseList = new FlatVector[2];
            jList = new float[2];
        }

        public void AddBody(FlatBody body)
        {
            bodyList.Add(body);

            /*
            if (Body.Owner is GroupMember)
            {
                // Disable inertia and friction
                Body.InvInertia = 0f;
                Body.StaticFriction = 0f;
                Body.DynamicFriction = 0f;
            }*/
        }

        public bool RemoveBody(FlatBody body)
        {
            return bodyList.Remove(body);
        }


        public void RefreshList(List<Entity> newList)
        {
            bodyList.Clear();
            for (int i = 0; i < newList.Count; i++)
            {
                if (newList[i] is PhysicalEntity phent)
                {
                    AddBody(phent.Model.Body);
                }
            }
        }

        public bool GetBody(int index, out FlatBody body)
        {
            body = null;

            if (index < 0 || index >= bodyList.Count)
            {
                return false;
            }

            body = bodyList[index];
            return true;
        }

        public void Step(float time, int totalIterations)
        {
            totalIterations = FlatMath.Clamp(totalIterations, MinIterations, MaxIterations);

            for (int currentIteration = 0; currentIteration < totalIterations; currentIteration++)
            {
                contactPairs.Clear();
                StepBodies(time, totalIterations);
                BroadPhase();
                NarrowPhase(time);
            }
        }

        private void BroadPhase()
        {
            for (int i = 0; i < bodyList.Count - 1; i++)
            {
                FlatBody bodyA = bodyList[i];
                FlatAABB bodyA_aabb = bodyA.GetAABB();

                for (int j = i + 1; j < bodyList.Count; j++)
                {
                    FlatBody bodyB = bodyList[j];
                    FlatAABB bodyB_aabb = bodyB.GetAABB();

                    if (bodyA.IsStatic && bodyB.IsStatic)
                    {
                        continue;
                    }

                    if (!Collisions.IntersectAABBs(bodyA_aabb, bodyB_aabb))
                    {
                        continue;
                    }
                    else
                    {
                        if (CollisionHandler.IgnoreCollision(bodyA, bodyB))
                        {
                            continue;
                        }
                    }

                    contactPairs.Add((i, j));
                }
            }
        }

        private void NarrowPhase(float deltaTime)
        {
            for (int i = 0; i < contactPairs.Count; i++)
            {
                (int, int) pair = contactPairs[i];
                FlatBody bodyA = bodyList[pair.Item1];
                FlatBody bodyB = bodyList[pair.Item2];

                if (Collisions.Collide(bodyA, bodyB, out FlatVector normal, out float depth))
                {
                    bodyA.IsColliding = true;
                    bodyB.IsColliding = true;

                    {
                        SeparateBodies(bodyA, bodyB, normal * depth);
                        Collisions.FindContactPoints(bodyA, bodyB, out FlatVector contact1, out FlatVector contact2, out int contactCount);
                        FlatManifold contact = new FlatManifold(bodyA, bodyB, normal, depth, contact1, contact2, contactCount);
                        ResolveCollisionWithRotationAndFriction(in contact);

                        //Damp X-velocity for Player-related collisions
                        const float dampingFactor = 5.0f; //Adjust for desired decay rate
                        if (bodyA.Owner is Player || bodyB.Owner is Player)
                        {
                            bodyA.LinearVelocity = new FlatVector(
                                bodyA.LinearVelocity.X * (1f - dampingFactor * deltaTime),
                                bodyA.LinearVelocity.Y
                            );
                            bodyB.LinearVelocity = new FlatVector(
                                bodyB.LinearVelocity.X * (1f - dampingFactor * deltaTime),
                                bodyB.LinearVelocity.Y
                            );
                        }
                    }

                }
                else
                {
                    bodyA.IsColliding = false;
                    bodyB.IsColliding = false;
                }
            }
        }


        public void StepBodies(float time, int totalIterations)
        {
            for (int i = 0; i < bodyList.Count; i++)
            {
                bodyList[i].Step(time, gravity, totalIterations);
            }
        }

        private void SeparateBodies(FlatBody bodyA, FlatBody bodyB, FlatVector mtv)
        {
            if (bodyA.IsStatic)
            {
                bodyB.Move(mtv);
            }
            else if (bodyB.IsStatic)
            {
                bodyA.Move(-mtv);
            }
            else
            {
                bodyA.Move(-mtv / 2f);
                bodyB.Move(mtv / 2f);
            }
        }

        public void ResolveCollisionBasic(in FlatManifold contact, bool restrictFriction)
        {
            FlatBody bodyA = contact.BodyA;
            FlatBody bodyB = contact.BodyB;
            FlatVector normal = contact.Normal;
            float depth = contact.Depth;

            FlatVector relativeVelocity = bodyB.LinearVelocity - bodyA.LinearVelocity;

            if (FlatMath.Dot(relativeVelocity, normal) > 0f)
            {
                return;
            }

            float e = MathF.Min(bodyA.Restitution, bodyB.Restitution);

            
            float j = -(1f + e) * FlatMath.Dot(relativeVelocity, normal);
            j /= bodyA.InvMass + bodyB.InvMass;
            

            FlatVector impulse = j * normal;

            bodyA.LinearVelocity -= impulse * bodyA.InvMass;
            bodyB.LinearVelocity += impulse * bodyB.InvMass;
        }

        public void ResolveCollisionWithRotation(in FlatManifold contact)
        {
            FlatBody bodyA = contact.BodyA;
            FlatBody bodyB = contact.BodyB;

            
            FlatVector normal = contact.Normal;
            FlatVector contact1 = contact.Contact1;
            FlatVector contact2 = contact.Contact2;
            int contactCount = contact.ContactCount;

            float e = MathF.Min(bodyA.Restitution, bodyB.Restitution);

            contactList[0] = contact1;
            contactList[1] = contact2;

            for (int i = 0; i < contactCount; i++)
            {
                impulseList[i] = FlatVector.Zero;
                raList[i] = FlatVector.Zero;
                rbList[i] = FlatVector.Zero;
            }

            for (int i = 0; i < contactCount; i++)
            {
                FlatVector ra = contactList[i] - bodyA.Position;
                FlatVector rb = contactList[i] - bodyB.Position;

                raList[i] = ra;
                rbList[i] = rb;

                FlatVector raPerp = new FlatVector(-ra.Y, ra.X);
                FlatVector rbPerp = new FlatVector(-rb.Y, rb.X);

                FlatVector angularLinearVelocityA = raPerp * bodyA.AngularVelocity;
                FlatVector angularLinearVelocityB = rbPerp * bodyB.AngularVelocity;

                FlatVector relativeVelocity =
                    bodyB.LinearVelocity + angularLinearVelocityB -
                    (bodyA.LinearVelocity + angularLinearVelocityA);

                float contactVelocityMag = FlatMath.Dot(relativeVelocity, normal);

                if (contactVelocityMag > 0f)
                {
                    continue;
                }

                float raPerpDotN = FlatMath.Dot(raPerp, normal);
                float rbPerpDotN = FlatMath.Dot(rbPerp, normal);

                float denom = bodyA.InvMass + bodyB.InvMass +
                    raPerpDotN * raPerpDotN * bodyA.InvInertia +
                    rbPerpDotN * rbPerpDotN * bodyB.InvInertia;

                float j = -(1f + e) * contactVelocityMag;
                j /= denom;
                j /= contactCount;

                FlatVector impulse = j * normal;
                impulseList[i] = impulse;
            }

            for (int i = 0; i < contactCount; i++)
            {
                FlatVector impulse = impulseList[i];
                FlatVector ra = raList[i];
                FlatVector rb = rbList[i];

                bodyA.LinearVelocity += -impulse * bodyA.InvMass;
                bodyA.AngularVelocity += -FlatMath.Cross(ra, impulse) * bodyA.InvInertia;
                bodyB.LinearVelocity += impulse * bodyB.InvMass;
                bodyB.AngularVelocity += FlatMath.Cross(rb, impulse) * bodyB.InvInertia;
            }
        }

        public void ResolveCollisionWithRotationAndFriction(in FlatManifold contact)
        {
            FlatBody bodyA = contact.BodyA;
            FlatBody bodyB = contact.BodyB;

            PhysicalEntity bodyAOwner = bodyA.Owner;
            PhysicalEntity bodyBOwner = bodyB.Owner;

            //disable rotation
            bool restrictRotation = false;

            //TODO
            //lost poise = fall
            if(bodyBOwner is StatsEntity sEnt)
            {
                if (sEnt is EquipmentEntity || sEnt is AnimalMob)
                {
                    restrictRotation = true;

                    if (sEnt.Stats.LostPoise())
                    {
                        //if lost poise then dont restrict rotation
                        restrictRotation = false;
                    }
                }
                
            }

            FlatVector normal = contact.Normal;
            FlatVector contact1 = contact.Contact1;
            FlatVector contact2 = contact.Contact2;
            int contactCount = contact.ContactCount;

            float e = MathF.Min(bodyA.Restitution, bodyB.Restitution);

            float sf = (bodyA.StaticFriction + bodyB.StaticFriction) * 0.5f;
            float df = (bodyA.DynamicFriction + bodyB.DynamicFriction) * 0.5f;

            contactList[0] = contact1;
            contactList[1] = contact2;

            for (int i = 0; i < contactCount; i++)
            {
                impulseList[i] = FlatVector.Zero;
                raList[i] = FlatVector.Zero;
                rbList[i] = FlatVector.Zero;
                frictionImpulseList[i] = FlatVector.Zero;
                jList[i] = 0f;
            }

            for (int i = 0; i < contactCount; i++)
            {
                FlatVector ra = contactList[i] - bodyA.Position;
                FlatVector rb = contactList[i] - bodyB.Position;

                raList[i] = ra;
                rbList[i] = rb;

                FlatVector raPerp = new FlatVector(-ra.Y, ra.X);
                FlatVector rbPerp = new FlatVector(-rb.Y, rb.X);

                FlatVector angularLinearVelocityA = raPerp * bodyA.AngularVelocity;
                FlatVector angularLinearVelocityB = rbPerp * bodyB.AngularVelocity;

                if (restrictRotation)
                {
                    bodyA.AngularVelocity = 0f;
                    bodyB.AngularVelocity = 0f;
                }

                FlatVector relativeVelocity =
                    bodyB.LinearVelocity + angularLinearVelocityB -
                    (bodyA.LinearVelocity + angularLinearVelocityA);

                float contactVelocityMag = FlatMath.Dot(relativeVelocity, normal);

                if (contactVelocityMag > 0f)
                {
                    continue;
                }

                float raPerpDotN = FlatMath.Dot(raPerp, normal);
                float rbPerpDotN = FlatMath.Dot(rbPerp, normal);

                float denom = bodyA.InvMass + bodyB.InvMass +
                    raPerpDotN * raPerpDotN * bodyA.InvInertia +
                    rbPerpDotN * rbPerpDotN * bodyB.InvInertia;

                float j = -(1f + e) * contactVelocityMag;
                j /= denom;
                j /= contactCount;

                jList[i] = j;

                FlatVector impulse = j * normal;
                impulseList[i] = impulse;
            }

            for (int i = 0; i < contactCount; i++)
            {
                FlatVector impulse = impulseList[i];
                FlatVector ra = raList[i];
                FlatVector rb = rbList[i];

                bodyA.LinearVelocity += -impulse * bodyA.InvMass;
                bodyA.AngularVelocity += -FlatMath.Cross(ra, impulse) * bodyA.InvInertia;
                if (restrictRotation)
                {
                    bodyA.AngularVelocity = 0f;
                }

                bodyB.LinearVelocity += impulse * bodyB.InvMass;
                bodyB.AngularVelocity += FlatMath.Cross(rb, impulse) * bodyB.InvInertia;
                if (restrictRotation)
                {
                    bodyB.AngularVelocity = 0f;
                }
            }

            for (int i = 0; i < contactCount; i++)
            {
                FlatVector ra = contactList[i] - bodyA.Position;
                FlatVector rb = contactList[i] - bodyB.Position;

                raList[i] = ra;
                rbList[i] = rb;

                FlatVector raPerp = new FlatVector(-ra.Y, ra.X);
                FlatVector rbPerp = new FlatVector(-rb.Y, rb.X);

                FlatVector angularLinearVelocityA = raPerp * bodyA.AngularVelocity;
                FlatVector angularLinearVelocityB = rbPerp * bodyB.AngularVelocity;

                if (restrictRotation)
                {
                    angularLinearVelocityA = FlatVector.Zero;
                    angularLinearVelocityB = FlatVector.Zero;
                }

                FlatVector relativeVelocity =
                    bodyB.LinearVelocity + angularLinearVelocityB -
                    (bodyA.LinearVelocity + angularLinearVelocityA);

                FlatVector tangent = relativeVelocity - FlatMath.Dot(relativeVelocity, normal) * normal;

                if (FlatMath.NearlyEqual(tangent, FlatVector.Zero))
                {
                    continue;
                }
                else
                {
                    tangent = FlatMath.Normalize(tangent);
                }

                float raPerpDotT = FlatMath.Dot(raPerp, tangent);
                float rbPerpDotT = FlatMath.Dot(rbPerp, tangent);

                float denom = bodyA.InvMass + bodyB.InvMass +
                    raPerpDotT * raPerpDotT * bodyA.InvInertia +
                    rbPerpDotT * rbPerpDotT * bodyB.InvInertia;

                float jt = -FlatMath.Dot(relativeVelocity, tangent);
                jt /= denom;
                jt /= contactCount;

                FlatVector frictionImpulse;
                float j = jList[i];

                if (MathF.Abs(jt) <= j * sf)
                {
                    frictionImpulse = jt * tangent;
                }
                else
                {
                    frictionImpulse = -j * tangent * df;
                }

                frictionImpulseList[i] = frictionImpulse;
            }

            for (int i = 0; i < contactCount; i++)
            {

                FlatVector frictionImpulse = frictionImpulseList[i];
                FlatVector ra = raList[i];
                FlatVector rb = rbList[i];

                bodyA.LinearVelocity += -frictionImpulse * bodyA.InvMass;
                bodyA.AngularVelocity += -FlatMath.Cross(ra, frictionImpulse) * bodyA.InvInertia;

                bodyB.LinearVelocity += frictionImpulse * bodyB.InvMass;
                bodyB.AngularVelocity += FlatMath.Cross(rb, frictionImpulse) * bodyB.InvInertia;

                if (restrictRotation)
                {
                    bodyA.AngularVelocity = 0f;
                    bodyB.AngularVelocity = 0f;
                }
            }

        }
    }
}
