using Microsoft.Xna.Framework;
using Physics;
using SharpDX.MediaFoundation;
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
        HUMANOID,
        ANIMAL,
        LEDGE,
        PROJECTILE
    }


    public static class PhysicalBodyFactory
    {


        public static PhysicalBody CreatePhysicalBody(PhysicalBodies preset, Vector2 offSet)
        {
            PhysicalBody body;

            switch (preset)
            {
                case PhysicalBodies.CRATE_0:
                    body = CreatePhysicalBody(BodyDynamics.STATIC, BodyShapeType.Box, new Vector2(50 - offSet.X, 50 - offSet.Y), 0.5f, 0.5f);
                    break;
                case PhysicalBodies.CRATE_1:
                    body = CreatePhysicalBody(BodyDynamics.DYNAMIC, BodyShapeType.Box, new Vector2(10 - offSet.X, 10 - offSet.Y), 0.5f, 0.5f);
                    break;
                case PhysicalBodies.CIRCLE:
                    body = CreatePhysicalBody(BodyDynamics.DYNAMIC, BodyShapeType.Circle, new Vector2(10 - offSet.X, 10 - offSet.Y), 0.5f, 2);
                    break;
                case PhysicalBodies.COIN:
                    body = CreatePhysicalBody(BodyDynamics.DYNAMIC, BodyShapeType.Circle, new Vector2(5 - offSet.X, 5 - offSet.Y), 0.5f, 2);
                    break;
                case PhysicalBodies.ITEM_DROP:
                    body = CreatePhysicalBody(BodyDynamics.DYNAMIC, BodyShapeType.Circle, new Vector2(10 - offSet.X, 10 - offSet.Y), 0.5f, 2);
                    break;
                case PhysicalBodies.HUMANOID:
                    body = CreatePhysicalBody(BodyDynamics.DYNAMIC, BodyShapeType.Box, new Vector2(20 - offSet.X, 40 - offSet.Y), 10, 0);
                    break;
                case PhysicalBodies.ANIMAL:
                    body = CreatePhysicalBody(BodyDynamics.DYNAMIC, BodyShapeType.Box, new Vector2(20 - offSet.X, 20 - offSet.Y), 10, 0);
                    break;
                case PhysicalBodies.LEDGE:
                    body = CreatePhysicalBody(BodyDynamics.STATIC, BodyShapeType.Box, new Vector2(20 - offSet.X, 20 - offSet.Y), 1, 0);
                    break;
                case PhysicalBodies.PROJECTILE:
                    body = CreatePhysicalBody(BodyDynamics.DYNAMIC, BodyShapeType.Box, new Vector2(20 - offSet.X, 20 - offSet.Y), 1, 0);
                    break;
                default:
                    body = CreatePhysicalBody(BodyDynamics.STATIC, BodyShapeType.Box, new Vector2(10 - offSet.X, 10 - offSet.Y), 1, 0);
                    break;
            }
            return body;
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
    }
}
