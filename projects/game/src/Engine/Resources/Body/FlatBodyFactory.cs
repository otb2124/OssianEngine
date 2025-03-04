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
                case FlatBodyPreset.PLATFORM:
                    body = createFlatBody(BodyDynamics.STATIC, BodyShapeType.Box, new Vector2(128-offSet.X, 32-offSet.Y), 0.5f, 0.5f);
                    break;
                case FlatBodyPreset.CRATE_0:
                    body = createFlatBody(BodyDynamics.STATIC, BodyShapeType.Box, new Vector2(50 - offSet.X, 50 - offSet.Y), 0.5f, 0.5f);
                    break;
                case FlatBodyPreset.CRATE_1:
                    body = createFlatBody(BodyDynamics.DYNAMIC, BodyShapeType.Box, new Vector2(10 - offSet.X, 10 - offSet.Y), 0.5f, 0.5f);
                    break;
                case FlatBodyPreset.CIRCLE:
                    body = createFlatBody(BodyDynamics.DYNAMIC, BodyShapeType.Circle, new Vector2(10 - offSet.X, 10 - offSet.Y), 0.5f, 2);
                    break;
                case FlatBodyPreset.HUMANOID:
                    body = createFlatBody(BodyDynamics.DYNAMIC, BodyShapeType.Box, new Vector2(20 - offSet.X, 40 - offSet.Y), 10, 0);
                    break;
                default:
                    body = createFlatBody(BodyDynamics.STATIC, BodyShapeType.Box, new Vector2(10 - offSet.X, 10 - offSet.Y), 1, 0);
                    break;
            }

            return body;
        }


        public static FlatBody createFlatBody(FlatBodyPreset preset)
        {
            return createFlatBody(preset, Vector2.Zero);
        }


        public static FlatBody createFlatBody(BodyDynamics bodyDynamics, BodyShapeType bodyShapeType, Vector2 size, float density, float resitution)
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
