using Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Physics
{
    public sealed class PhysicalWorld
    {
        public static int TransformCount = 0;
        public static int NoTransformCount = 0;

        public static readonly float MinBodySize = 0.01f * 0.01f;
        public static readonly float MaxBodySize = 1000f * 1000f;

        public static readonly float MinDensity = 0.5f;     // g/cm^3
        public static readonly float MaxDensity = 21.4f;
         
        public static readonly int MinIterations = 1;
        public static readonly int MaxIterations = 128;

        public PhysicalVector gravity;
        public static readonly int ConstantGravityMultiplier = 50;
        public static readonly float GlobalGravityMultiplier = 1f;

        public List<PhysicalBody> bodyList;
        private List<(int, int)> contactPairs;

        private PhysicalVector[] contactList;
        private PhysicalVector[] impulseList;
        private PhysicalVector[] raList;
        private PhysicalVector[] rbList;
        private PhysicalVector[] frictionImpulseList;
        private float[] jList;

        public int BodyCount
        {
            get { return bodyList.Count; }
        }

        public PhysicalWorld()
        {
            gravity = new PhysicalVector(0f, -9.81f * GlobalGravityMultiplier * ConstantGravityMultiplier);
            bodyList = new List<PhysicalBody>();
            contactPairs = new List<(int, int)>();

            contactList = new PhysicalVector[2];
            impulseList = new PhysicalVector[2];
            raList = new PhysicalVector[2];
            rbList = new PhysicalVector[2];
            frictionImpulseList = new PhysicalVector[2];
            jList = new float[2];
        }

        public void AddBody(PhysicalBody body)
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

        public bool RemoveBody(PhysicalBody body)
        {
            return bodyList.Remove(body);
        }


        public void RefreshList(List<WorldEntity> newList)
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

        public bool GetBody(int index, out PhysicalBody body)
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
            totalIterations = PhysicalMath.Clamp(totalIterations, MinIterations, MaxIterations);

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
                PhysicalBody bodyA = bodyList[i];
                PhysicalAABB bodyA_aabb = bodyA.GetAABB();

                for (int j = i + 1; j < bodyList.Count; j++)
                {
                    PhysicalBody bodyB = bodyList[j];
                    PhysicalAABB bodyB_aabb = bodyB.GetAABB();

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
                        CollisionHandler.HandleUnrestrictedCollision(bodyA, bodyB);

                        if (CollisionHandler.IgnoreCollision(bodyA, bodyB) || ProjectilePhysicsHandler.CheckProjectileCollision(bodyA, bodyB))
                        {
                            //collision does not happen
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
                PhysicalBody bodyA = bodyList[pair.Item1];
                PhysicalBody bodyB = bodyList[pair.Item2];

                if (Collisions.Collide(bodyA, bodyB, out PhysicalVector normal, out float depth))
                {
                    bodyA.IsColliding = true;
                    bodyB.IsColliding = true;

                    {
                        SeparateBodies(bodyA, bodyB, normal * depth);
                        Collisions.FindContactPoints(bodyA, bodyB, out PhysicalVector contact1, out PhysicalVector contact2, out int contactCount);
                        PhysicalManifold contact = new PhysicalManifold(bodyA, bodyB, normal, depth, contact1, contact2, contactCount);
                        ResolveCollisionWithRotationAndFriction(in contact);

                        //Damp X-velocity for Player-related collisions
                        const float dampingFactor = 5.0f; //Adjust for desired decay rate
                        if (bodyA.Owner is Player || bodyB.Owner is Player)
                        {
                            bodyA.LinearVelocity = new PhysicalVector(
                                bodyA.LinearVelocity.X * (1f - dampingFactor * deltaTime),
                                bodyA.LinearVelocity.Y
                            );
                            bodyB.LinearVelocity = new PhysicalVector(
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

        private void SeparateBodies(PhysicalBody bodyA, PhysicalBody bodyB, PhysicalVector mtv)
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

        public void ResolveCollisionBasic(in PhysicalManifold contact, bool restrictFriction)
        {
            PhysicalBody bodyA = contact.BodyA;
            PhysicalBody bodyB = contact.BodyB;
            PhysicalVector normal = contact.Normal;
            float depth = contact.Depth;

            PhysicalVector relativeVelocity = bodyB.LinearVelocity - bodyA.LinearVelocity;

            if (PhysicalMath.Dot(relativeVelocity, normal) > 0f)
            {
                return;
            }

            float e = MathF.Min(bodyA.Restitution, bodyB.Restitution);

            
            float j = -(1f + e) * PhysicalMath.Dot(relativeVelocity, normal);
            j /= bodyA.InvMass + bodyB.InvMass;
            

            PhysicalVector impulse = j * normal;

            bodyA.LinearVelocity -= impulse * bodyA.InvMass;
            bodyB.LinearVelocity += impulse * bodyB.InvMass;
        }

        public void ResolveCollisionWithRotation(in PhysicalManifold contact)
        {
            PhysicalBody bodyA = contact.BodyA;
            PhysicalBody bodyB = contact.BodyB;

            
            PhysicalVector normal = contact.Normal;
            PhysicalVector contact1 = contact.Contact1;
            PhysicalVector contact2 = contact.Contact2;
            int contactCount = contact.ContactCount;

            float e = MathF.Min(bodyA.Restitution, bodyB.Restitution);

            contactList[0] = contact1;
            contactList[1] = contact2;

            for (int i = 0; i < contactCount; i++)
            {
                impulseList[i] = PhysicalVector.Zero;
                raList[i] = PhysicalVector.Zero;
                rbList[i] = PhysicalVector.Zero;
            }

            for (int i = 0; i < contactCount; i++)
            {
                PhysicalVector ra = contactList[i] - bodyA.Position;
                PhysicalVector rb = contactList[i] - bodyB.Position;

                raList[i] = ra;
                rbList[i] = rb;

                PhysicalVector raPerp = new PhysicalVector(-ra.Y, ra.X);
                PhysicalVector rbPerp = new PhysicalVector(-rb.Y, rb.X);

                PhysicalVector angularLinearVelocityA = raPerp * bodyA.AngularVelocity;
                PhysicalVector angularLinearVelocityB = rbPerp * bodyB.AngularVelocity;

                PhysicalVector relativeVelocity =
                    bodyB.LinearVelocity + angularLinearVelocityB -
                    (bodyA.LinearVelocity + angularLinearVelocityA);

                float contactVelocityMag = PhysicalMath.Dot(relativeVelocity, normal);

                if (contactVelocityMag > 0f)
                {
                    continue;
                }

                float raPerpDotN = PhysicalMath.Dot(raPerp, normal);
                float rbPerpDotN = PhysicalMath.Dot(rbPerp, normal);

                float denom = bodyA.InvMass + bodyB.InvMass +
                    raPerpDotN * raPerpDotN * bodyA.InvInertia +
                    rbPerpDotN * rbPerpDotN * bodyB.InvInertia;

                float j = -(1f + e) * contactVelocityMag;
                j /= denom;
                j /= contactCount;

                PhysicalVector impulse = j * normal;
                impulseList[i] = impulse;
            }

            for (int i = 0; i < contactCount; i++)
            {
                PhysicalVector impulse = impulseList[i];
                PhysicalVector ra = raList[i];
                PhysicalVector rb = rbList[i];

                bodyA.LinearVelocity += -impulse * bodyA.InvMass;
                bodyA.AngularVelocity += -PhysicalMath.Cross(ra, impulse) * bodyA.InvInertia;
                bodyB.LinearVelocity += impulse * bodyB.InvMass;
                bodyB.AngularVelocity += PhysicalMath.Cross(rb, impulse) * bodyB.InvInertia;
            }
        }

        public void ResolveCollisionWithRotationAndFriction(in PhysicalManifold contact)
        {
            PhysicalBody bodyA = contact.BodyA;
            PhysicalBody bodyB = contact.BodyB;

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

                    if (sEnt.StatsManager.LostPoise())
                    {
                        //if lost poise then dont restrict rotation
                        restrictRotation = false;
                    }
                }
                
            }

            PhysicalVector normal = contact.Normal;
            PhysicalVector contact1 = contact.Contact1;
            PhysicalVector contact2 = contact.Contact2;
            int contactCount = contact.ContactCount;

            float e = MathF.Min(bodyA.Restitution, bodyB.Restitution);

            float sf = (bodyA.StaticFriction + bodyB.StaticFriction) * 0.5f;
            float df = (bodyA.DynamicFriction + bodyB.DynamicFriction) * 0.5f;

            contactList[0] = contact1;
            contactList[1] = contact2;

            for (int i = 0; i < contactCount; i++)
            {
                impulseList[i] = PhysicalVector.Zero;
                raList[i] = PhysicalVector.Zero;
                rbList[i] = PhysicalVector.Zero;
                frictionImpulseList[i] = PhysicalVector.Zero;
                jList[i] = 0f;
            }

            for (int i = 0; i < contactCount; i++)
            {
                PhysicalVector ra = contactList[i] - bodyA.Position;
                PhysicalVector rb = contactList[i] - bodyB.Position;

                raList[i] = ra;
                rbList[i] = rb;

                PhysicalVector raPerp = new PhysicalVector(-ra.Y, ra.X);
                PhysicalVector rbPerp = new PhysicalVector(-rb.Y, rb.X);

                PhysicalVector angularLinearVelocityA = raPerp * bodyA.AngularVelocity;
                PhysicalVector angularLinearVelocityB = rbPerp * bodyB.AngularVelocity;

                if (restrictRotation)
                {
                    bodyA.AngularVelocity = 0f;
                    bodyB.AngularVelocity = 0f;
                }

                PhysicalVector relativeVelocity =
                    bodyB.LinearVelocity + angularLinearVelocityB -
                    (bodyA.LinearVelocity + angularLinearVelocityA);

                float contactVelocityMag = PhysicalMath.Dot(relativeVelocity, normal);

                if (contactVelocityMag > 0f)
                {
                    continue;
                }

                float raPerpDotN = PhysicalMath.Dot(raPerp, normal);
                float rbPerpDotN = PhysicalMath.Dot(rbPerp, normal);

                float denom = bodyA.InvMass + bodyB.InvMass +
                    raPerpDotN * raPerpDotN * bodyA.InvInertia +
                    rbPerpDotN * rbPerpDotN * bodyB.InvInertia;

                float j = -(1f + e) * contactVelocityMag;
                j /= denom;
                j /= contactCount;

                jList[i] = j;

                PhysicalVector impulse = j * normal;
                impulseList[i] = impulse;
            }

            for (int i = 0; i < contactCount; i++)
            {
                PhysicalVector impulse = impulseList[i];
                PhysicalVector ra = raList[i];
                PhysicalVector rb = rbList[i];

                bodyA.LinearVelocity += -impulse * bodyA.InvMass;
                bodyA.AngularVelocity += -PhysicalMath.Cross(ra, impulse) * bodyA.InvInertia;
                if (restrictRotation)
                {
                    bodyA.AngularVelocity = 0f;
                }

                bodyB.LinearVelocity += impulse * bodyB.InvMass;
                bodyB.AngularVelocity += PhysicalMath.Cross(rb, impulse) * bodyB.InvInertia;
                if (restrictRotation)
                {
                    bodyB.AngularVelocity = 0f;
                }
            }

            for (int i = 0; i < contactCount; i++)
            {
                PhysicalVector ra = contactList[i] - bodyA.Position;
                PhysicalVector rb = contactList[i] - bodyB.Position;

                raList[i] = ra;
                rbList[i] = rb;

                PhysicalVector raPerp = new PhysicalVector(-ra.Y, ra.X);
                PhysicalVector rbPerp = new PhysicalVector(-rb.Y, rb.X);

                PhysicalVector angularLinearVelocityA = raPerp * bodyA.AngularVelocity;
                PhysicalVector angularLinearVelocityB = rbPerp * bodyB.AngularVelocity;

                if (restrictRotation)
                {
                    angularLinearVelocityA = PhysicalVector.Zero;
                    angularLinearVelocityB = PhysicalVector.Zero;
                }

                PhysicalVector relativeVelocity =
                    bodyB.LinearVelocity + angularLinearVelocityB -
                    (bodyA.LinearVelocity + angularLinearVelocityA);

                PhysicalVector tangent = relativeVelocity - PhysicalMath.Dot(relativeVelocity, normal) * normal;

                if (PhysicalMath.NearlyEqual(tangent, PhysicalVector.Zero))
                {
                    continue;
                }
                else
                {
                    tangent = PhysicalMath.Normalize(tangent);
                }

                float raPerpDotT = PhysicalMath.Dot(raPerp, tangent);
                float rbPerpDotT = PhysicalMath.Dot(rbPerp, tangent);

                float denom = bodyA.InvMass + bodyB.InvMass +
                    raPerpDotT * raPerpDotT * bodyA.InvInertia +
                    rbPerpDotT * rbPerpDotT * bodyB.InvInertia;

                float jt = -PhysicalMath.Dot(relativeVelocity, tangent);
                jt /= denom;
                jt /= contactCount;

                PhysicalVector frictionImpulse;
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

                PhysicalVector frictionImpulse = frictionImpulseList[i];
                PhysicalVector ra = raList[i];
                PhysicalVector rb = rbList[i];

                bodyA.LinearVelocity += -frictionImpulse * bodyA.InvMass;
                bodyA.AngularVelocity += -PhysicalMath.Cross(ra, frictionImpulse) * bodyA.InvInertia;

                bodyB.LinearVelocity += frictionImpulse * bodyB.InvMass;
                bodyB.AngularVelocity += PhysicalMath.Cross(rb, frictionImpulse) * bodyB.InvInertia;

                if (restrictRotation)
                {
                    bodyA.AngularVelocity = 0f;
                    bodyB.AngularVelocity = 0f;
                }
            }

        }
    }
}
