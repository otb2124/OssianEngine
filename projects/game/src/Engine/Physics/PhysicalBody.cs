using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resources;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Utils;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace Physics
{
    public enum BodyShapeType
    {
        Circle = 0,
        Box = 1,
    }

    public enum BodyDynamics
    {
        DYNAMIC = 0,
        STATIC = 1,
    }


    public sealed class PhysicalBody
    {
        private PhysicalVector position;
        public PhysicalVector linearVelocity;
        private float angle;
        private float angularVelocity;
        public PhysicalVector force;

        public BodyShapeType BodyShapeType;
        public float Density;
        public float Mass;
        public readonly float InvMass;
        public float Restitution;
        public readonly float Area;
        public float Inertia;
        public float InvInertia;
        public bool IsStatic;
        public float Radius;
        public float Width;
        public float Height;
        public float StaticFriction;
        public float DynamicFriction;

        private readonly PhysicalVector[] vertices;
        private PhysicalVector[] transformedVertices;
        private PhysicalAABB aabb;

        private bool transformUpdateRequired;
        public bool aabbUpdateRequired;


        //TODO: TURN INTO TYPE, NOT PHYSICALENTITY
        public Entities.PhysicalEntity Owner = null;

        public bool IsColliding = false;
        public bool IsFrozen = false;


        public PhysicalVector Position
        {
            get { return position; }
        }

        public PhysicalVector LinearVelocity
        {
            get { return linearVelocity; }
            set { linearVelocity = value; }
        }

        public float Angle
        {
            get { return angle; }
        }

        public float AngularVelocity
        {
            get { return angularVelocity; }
            internal set { angularVelocity = value; }
        }

        private PhysicalBody(float density, float mass, float inertia, float restitution, float area,
            bool isStatic, float radius, float width, float height, PhysicalVector[] vertices, BodyShapeType BodyShapeType)
        {
            position = PhysicalVector.Zero;
            linearVelocity = PhysicalVector.Zero;
            angle = 0f;
            angularVelocity = 0f;
            force = PhysicalVector.Zero;

            this.BodyShapeType = BodyShapeType;
            Density = density;
            Mass = mass;
            InvMass = mass > 0f ? 1f / mass : 0f;
            Inertia = inertia;
            InvInertia = inertia > 0f ? 1f / inertia : 0f;
            Restitution = restitution;
            Area = area;
            IsStatic = isStatic;
            Radius = radius;
            Width = width;
            Height = height;
            StaticFriction = 0.6f;
            DynamicFriction = 0.4f;

            if (BodyShapeType is BodyShapeType.Box)
            {
                this.vertices = vertices;
                transformedVertices = new PhysicalVector[this.vertices.Length];
            }
            else
            {
                this.vertices = null;
                transformedVertices = null;
            }

            transformUpdateRequired = true;
            aabbUpdateRequired = true;
        }

        private static PhysicalVector[] CreateBoxVertices(float width, float height)
        {
            float left = -width / 2f;
            float right = left + width;
            float bottom = -height / 2f;
            float top = bottom + height;

            PhysicalVector[] vertices = new PhysicalVector[4];
            vertices[0] = new PhysicalVector(left, top);
            vertices[1] = new PhysicalVector(right, top);
            vertices[2] = new PhysicalVector(right, bottom);
            vertices[3] = new PhysicalVector(left, bottom);

            return vertices;
        }

        private static int[] CreateBoxTriangles()
        {
            int[] triangles = new int[6];
            triangles[0] = 0;
            triangles[1] = 1;
            triangles[2] = 2;
            triangles[3] = 0;
            triangles[4] = 2;
            triangles[5] = 3;
            return triangles;
        }

        public PhysicalVector[] GetTransformedVertices()
        {
            if (transformUpdateRequired)
            {
                PhysicalTransform transform = new PhysicalTransform(position, angle);

                for (int i = 0; i < vertices.Length; i++)
                {
                    PhysicalVector v = vertices[i];
                    transformedVertices[i] = PhysicalVector.Transform(v, transform);
                }

                PhysicalWorld.TransformCount++;
            }
            else
            {
                PhysicalWorld.NoTransformCount++;
            }

            transformUpdateRequired = false;
            return transformedVertices;
        }

        public PhysicalAABB GetAABB()
        {
            if (aabbUpdateRequired)
            {
                float minX = float.MaxValue;
                float minY = float.MaxValue;
                float maxX = float.MinValue;
                float maxY = float.MinValue;

                if (BodyShapeType is BodyShapeType.Box)
                {
                    PhysicalVector[] vertices = GetTransformedVertices();

                    for (int i = 0; i < vertices.Length; i++)
                    {
                        PhysicalVector v = vertices[i];

                        if (v.X < minX) { minX = v.X; }
                        if (v.X > maxX) { maxX = v.X; }
                        if (v.Y < minY) { minY = v.Y; }
                        if (v.Y > maxY) { maxY = v.Y; }
                    }
                }
                else if (BodyShapeType is BodyShapeType.Circle)
                {
                    minX = position.X - Radius;
                    minY = position.Y - Radius;
                    maxX = position.X + Radius;
                    maxY = position.Y + Radius;
                }
                else
                {
                    throw new Exception("Unknown BodyShapeType.");
                }

                aabb = new PhysicalAABB(minX, minY, maxX, maxY);
            }

            aabbUpdateRequired = false;
            return aabb;
        }

        internal void Step(float time, PhysicalVector gravity, int iterations)
        {
            if (IsStatic || IsFrozen)
            {
                return;
            }

            time /= iterations;

            // force = mass * acc
            // acc = force / mass;

            //PhysicalVector acceleration = this.force / this.Mass;
            //this.linearVelocity += acceleration * time;


            linearVelocity += gravity * time;
            position += linearVelocity * time;

            angle += angularVelocity * time;

            force = PhysicalVector.Zero;
            transformUpdateRequired = true;
            aabbUpdateRequired = true;
        }

        public void Move(PhysicalVector amount)
        {
            position += amount;
            transformUpdateRequired = true;
            aabbUpdateRequired = true;
        }

        public void Jump(float amount)
        {
            position += new PhysicalVector(0, amount);
        }

        public void MoveTo(PhysicalVector position)
        {
            this.position = position;
            transformUpdateRequired = true;
            aabbUpdateRequired = true;
        }

        public void Rotate(float amount)
        {
            angle += amount;
            transformUpdateRequired = true;
            aabbUpdateRequired = true;
        }

        public void RotateTo(float angle)
        {
            this.angle = angle;
            transformUpdateRequired = true;
            aabbUpdateRequired = true;
        }

        public void AddForce(PhysicalVector amount)
        {
            force = amount;
        }

        public static bool CreateCircleBody(float radius, float density, bool isStatic, float restitution, out PhysicalBody body, out string errorMessage)
        {
            body = null;
            errorMessage = string.Empty;

            float area = radius * radius * MathF.PI;

            if (area < PhysicalWorld.MinBodySize)
            {
                errorMessage = $"Circle radius is too small. Min circle area is {PhysicalWorld.MinBodySize}.";
                return false;
            }

            if (area > PhysicalWorld.MaxBodySize)
            {
                errorMessage = $"Circle radius is too large. Max circle area is {PhysicalWorld.MaxBodySize}.";
                return false;
            }

            if (density < PhysicalWorld.MinDensity)
            {
                errorMessage = $"Density is too small. Min density is {PhysicalWorld.MinDensity}";
                return false;
            }

            if (density > PhysicalWorld.MaxDensity)
            {
                errorMessage = $"Density is too large. Max density is {PhysicalWorld.MaxDensity}";
                return false;
            }

            restitution = PhysicalMath.Clamp(restitution, 0f, 1f);

            float mass = 0f;
            float inertia = 0f;

            if (!isStatic)
            {
                // mass = area * depth * density
                mass = area * density;
                inertia = 1f / 2f * mass * radius * radius;

            }
            


            body = new PhysicalBody(density, mass, inertia, restitution, area, isStatic, radius, 0f, 0f, null, BodyShapeType.Circle);
            return true;
        }

        public static bool CreateBoxBody(float width, float height, float density, bool isStatic, float restitution, out PhysicalBody body, out string errorMessage)
        {
            body = null;
            errorMessage = string.Empty;

            float area = width * height;

            if (area < PhysicalWorld.MinBodySize)
            {
                errorMessage = $"Area is too small. Min area is {PhysicalWorld.MinBodySize}.";
                return false;
            }

            if (area > PhysicalWorld.MaxBodySize)
            {
                errorMessage = $"Area is too large. Max area is {PhysicalWorld.MaxBodySize}.";
                return false;
            }

            if (density < PhysicalWorld.MinDensity)
            {
                errorMessage = $"Density is too small. Min density is {PhysicalWorld.MinDensity}";
                return false;
            }

            if (density > PhysicalWorld.MaxDensity)
            {
                errorMessage = $"Density is too large. Max density is {PhysicalWorld.MaxDensity}";
                return false;
            }

            restitution = PhysicalMath.Clamp(restitution, 0f, 1f);

            float mass = 0f;
            float inertia = 0f;

            if (!isStatic)
            {
                // mass = area * depth * density
                mass = area * density;
                inertia = 1f / 12 * mass * (width * width + height * height);


                
            }

            PhysicalVector[] vertices = CreateBoxVertices(width, height);

            body = new PhysicalBody(density, mass, inertia, restitution, area, isStatic, 0f, width, height, vertices, BodyShapeType.Box);
            return true;
        }



        public void ApplyForce(PhysicalVector force)
        {
            if (!IsStatic)
            {
                LinearVelocity += force * 100;
            }
        }


        public RotatedRectangle ToRectangle()
        {
            return new RotatedRectangle(new Vector2(Position.X, Position.Y), new Vector2(Width, Height), Angle);
        }



        public PhysicalBody(PhysicalBody existingBody, float newHeight, float newWidth)
        {
            // Copy properties from the existing Body
            position = existingBody.Position;
            linearVelocity = existingBody.LinearVelocity;
            angle = existingBody.Angle;
            angularVelocity = existingBody.AngularVelocity;
            force = existingBody.force;

            BodyShapeType = existingBody.BodyShapeType;
            Density = existingBody.Density;
            Mass = existingBody.Mass;
            InvMass = existingBody.InvMass;
            Restitution = existingBody.Restitution;
            Area = existingBody.Area;
            Inertia = existingBody.Inertia;
            InvInertia = existingBody.InvInertia;
            IsStatic = existingBody.IsStatic;
            Radius = existingBody.Radius;
            Width = newWidth;
            Height = newHeight; // SetGameProps new height
            StaticFriction = existingBody.StaticFriction;
            DynamicFriction = existingBody.DynamicFriction;

            if (existingBody.BodyShapeType == BodyShapeType.Box)
            {
                // Recreate vertices with new height
                vertices = CreateBoxVertices(existingBody.Width, newHeight);
                transformedVertices = new PhysicalVector[vertices.Length];
            }
            else
            {
                vertices = null;
                transformedVertices = null;
            }

            transformUpdateRequired = true;
            aabbUpdateRequired = true;
        }


    }
}
