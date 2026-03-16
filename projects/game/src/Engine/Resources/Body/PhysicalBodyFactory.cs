using Microsoft.Xna.Framework;
using Physics;
using Resources;
using SharpDX.MediaFoundation;
using System;
using System.Collections.Generic;
using Utils;

namespace Resources
{

    public enum PhysicalBodies
    {
        CRATE_0,
        CRATE_1,
        CIRCLE,
        COIN,
        ITEM_DROP,
        HUMAN,
        SLIME,
        LEDGE,
        PROJECTILE,
        SPIKE_S, SPIKE_L, SPIKE_XL
    }

    public struct PhysicalBodyPreset
    {
        public BodyDynamics Dynamics;
        public BodyShapeType Shape;
        public Vector2 Size;   //width/height for Box, radius for Circle
        public float Density;
        public float Restitution;

        public PhysicalBodyPreset(BodyDynamics dynamics, BodyShapeType shape, Vector2 size, float density, float restitution)
        {
            Dynamics = dynamics;
            Shape = shape;
            Size = size;
            Density = density;
            Restitution = restitution;
        }
    }


    public static class PhysicalBodyFactory
    {
        public static PhysicalBody CreatePhysicalBody(PhysicalBodies preset, Vector2 offSet)
        {
            if (!PhysicalBodyPresetMap.TryGetValue(preset, out PhysicalBodyPreset config))
            {
                //fallback (same as your default case)
                config = new PhysicalBodyPreset(BodyDynamics.STATIC, BodyShapeType.Box, new Vector2(10, 10), 1f, 0f);
            }

            Vector2 adjustedSize = new Vector2(config.Size.X - offSet.X, config.Size.Y - offSet.Y);

            //prevent negative/zero sizes
            adjustedSize.X = Math.Max(1, adjustedSize.X);
            adjustedSize.Y = Math.Max(1, adjustedSize.Y);

            return CreatePhysicalBody(
                config.Dynamics,
                config.Shape,
                adjustedSize,
                config.Density,
                config.Restitution
            );
        }


        public static PhysicalBody CreatePhysicalBody(PhysicalBodies preset)
        {
            return CreatePhysicalBody(preset, Vector2.Zero);
        }


        public static PhysicalBody CreatePhysicalBody(BodyDynamics bodyDynamics, BodyShapeType bodyShapeType, Vector2 size, float density, float resitution)
        {
            PhysicalBody body;

            bool isStatic = bodyDynamics == BodyDynamics.STATIC;

            string errorMsg;
            bool success;
            if (bodyShapeType == BodyShapeType.Box)
            {
                success = PhysicalBody.CreateBoxBody(size.X, size.Y, density, isStatic, resitution, out body, out errorMsg);
            }
            else
            {
                success = PhysicalBody.CreateCircleBody(size.X, density, isStatic, resitution, out body, out errorMsg);
            }

            body.BodyShapeType = bodyShapeType;

            return body;
        }



        public static Dictionary<PhysicalBodies, PhysicalBodyPreset> PhysicalBodyPresetMap = new Dictionary<PhysicalBodies, PhysicalBodyPreset>
            {
                { PhysicalBodies.CRATE_0,   new PhysicalBodyPreset(BodyDynamics.STATIC,  BodyShapeType.Box,    new Vector2(50, 50), 0.5f, 0.5f) },
                { PhysicalBodies.CRATE_1,   new PhysicalBodyPreset(BodyDynamics.DYNAMIC, BodyShapeType.Box,    new Vector2(10, 10), 0.5f, 0.5f) },
                { PhysicalBodies.CIRCLE,    new PhysicalBodyPreset(BodyDynamics.DYNAMIC, BodyShapeType.Circle, new Vector2(10, 10), 0.5f, 2f)   },
                { PhysicalBodies.COIN,      new PhysicalBodyPreset(BodyDynamics.DYNAMIC, BodyShapeType.Circle, new Vector2(5, 5), 0.5f, 2f)   },
                { PhysicalBodies.ITEM_DROP, new PhysicalBodyPreset(BodyDynamics.DYNAMIC, BodyShapeType.Circle, new Vector2(10, 10), 0.5f, 2f)   },
                { PhysicalBodies.HUMAN,  new PhysicalBodyPreset(BodyDynamics.DYNAMIC, BodyShapeType.Box,       new Vector2(40, 40), 10f, 0f)   },
                { PhysicalBodies.SLIME,    new PhysicalBodyPreset(BodyDynamics.DYNAMIC, BodyShapeType.Box,     new Vector2(20, 20), 10f, 0f)   },
                { PhysicalBodies.LEDGE,     new PhysicalBodyPreset(BodyDynamics.STATIC,  BodyShapeType.Box,    new Vector2(20, 20), 1f, 0f)   },
                { PhysicalBodies.PROJECTILE,new PhysicalBodyPreset(BodyDynamics.DYNAMIC, BodyShapeType.Box,    new Vector2(20, 20), 1f, 0f)   },
                { PhysicalBodies.SPIKE_S,   new PhysicalBodyPreset(BodyDynamics.STATIC,  BodyShapeType.Box,    new Vector2(32, 32), 1f, 0f)   },
                { PhysicalBodies.SPIKE_L,   new PhysicalBodyPreset(BodyDynamics.STATIC,  BodyShapeType.Box,    new Vector2(32, 48), 1f, 0f)   },
                { PhysicalBodies.SPIKE_XL,   new PhysicalBodyPreset(BodyDynamics.STATIC,  BodyShapeType.Box,   new Vector2(32, 64), 1f, 0f)   }
            };
    }
}
