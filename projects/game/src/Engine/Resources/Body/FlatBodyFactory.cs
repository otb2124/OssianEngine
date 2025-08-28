using Microsoft.Xna.Framework;
using Physics;
using SharpDX.MediaFoundation;
using Utils;

namespace Resources
{
    public static class FlatBodyFactory
    {


        


        public static FlatBody createFlatBody(FlatBodyPreset preset, Vector2 offSet)
        {
            FlatBody body;

            switch (preset)
            {
                case FlatBodyPreset.CRATE_0:
                    body = CreateFlatBody(BodyDynamics.STATIC, BodyShapeType.Box, new Vector2(50 - offSet.X, 50 - offSet.Y), 0.5f, 0.5f);
                    break;
                case FlatBodyPreset.CRATE_1:
                    body = CreateFlatBody(BodyDynamics.DYNAMIC, BodyShapeType.Box, new Vector2(10 - offSet.X, 10 - offSet.Y), 0.5f, 0.5f);
                    break;
                case FlatBodyPreset.CIRCLE:
                    body = CreateFlatBody(BodyDynamics.DYNAMIC, BodyShapeType.Circle, new Vector2(10 - offSet.X, 10 - offSet.Y), 0.5f, 2);
                    break;
                case FlatBodyPreset.COIN:
                    body = CreateFlatBody(BodyDynamics.DYNAMIC, BodyShapeType.Circle, new Vector2(5 - offSet.X, 5 - offSet.Y), 0.5f, 2);
                    break;
                case FlatBodyPreset.ITEM_DROP:
                    body = CreateFlatBody(BodyDynamics.DYNAMIC, BodyShapeType.Circle, new Vector2(10 - offSet.X, 10 - offSet.Y), 0.5f, 2);
                    break;
                case FlatBodyPreset.HUMANOID:
                    body = CreateFlatBody(BodyDynamics.DYNAMIC, BodyShapeType.Box, new Vector2(20 - offSet.X, 40 - offSet.Y), 10, 0);
                    break;
                case FlatBodyPreset.ANIMAL:
                    body = CreateFlatBody(BodyDynamics.DYNAMIC, BodyShapeType.Box, new Vector2(20 - offSet.X, 20 - offSet.Y), 10, 0);
                    break;
                case FlatBodyPreset.LEDGE:
                    body = CreateFlatBody(BodyDynamics.STATIC, BodyShapeType.Box, new Vector2(20 - offSet.X, 20 - offSet.Y), 1, 0);
                    break;
                case FlatBodyPreset.PROJECTILE:
                    body = CreateFlatBody(BodyDynamics.DYNAMIC, BodyShapeType.Box, new Vector2(20 - offSet.X, 20 - offSet.Y), 1, 0);
                    break;
                default:
                    body = CreateFlatBody(BodyDynamics.STATIC, BodyShapeType.Box, new Vector2(10 - offSet.X, 10 - offSet.Y), 1, 0);
                    break;
            }

            return body;
        }


        public static FlatBody createFlatBody(FlatBodyPreset preset)
        {
            return createFlatBody(preset, Vector2.Zero);
        }


        public static FlatBody CreateFlatBody(BodyDynamics bodyDynamics, BodyShapeType bodyShapeType, Vector2 size, float density, float resitution)
        {
            FlatBody body;

            bool isStatic = bodyDynamics == BodyDynamics.STATIC;

            string errorMsg;
            bool success;
            if (bodyShapeType == BodyShapeType.Box)
            {
                success = FlatBody.CreateBoxBody(size.X, size.Y, density, isStatic, resitution, out body, out errorMsg);
            }
            else
            {
                success = FlatBody.CreateCircleBody(size.X, density, isStatic, resitution, out body, out errorMsg);
            }

            body.BodyShapeType = bodyShapeType;

            return body;
        }
    }
}
