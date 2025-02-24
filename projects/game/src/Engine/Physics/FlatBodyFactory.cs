using Microsoft.Xna.Framework;
using System.Diagnostics;

namespace Physics
{
    public static class FlatBodyFactory
    {


        public enum FlatBodyPreset
        {
            PLATFORM,
            CIRCLE,
            HUMANOID
        }


        public static FlatBody createFlatBody(FlatBodyPreset preset)
        {
            FlatBody body;

            switch(preset)
            {
                case FlatBodyPreset.PLATFORM:
                    body = createFlatBody(BodyDynamics.STATIC, BodyShapeType.Box, new Vector2(100, 10), 1, 0);
                    break;
                case FlatBodyPreset.CIRCLE:
                    body = createFlatBody(BodyDynamics.DYNAMIC, BodyShapeType.Circle, new Vector2(10, 10), 0.5f, 0.5f);
                    break;
                case FlatBodyPreset.HUMANOID:
                    body = createFlatBody(BodyDynamics.DYNAMIC, BodyShapeType.Box, new Vector2(10, 15), 1, 0);
                    break;
                default:
                    body = createFlatBody(BodyDynamics.STATIC, BodyShapeType.Box, new Vector2(10, 10), 1, 0);
                    break;
            }

            return body;
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
