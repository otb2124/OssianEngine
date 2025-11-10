using Graphics;
using Microsoft.Xna.Framework;

namespace Physics
{
    public static class PhysicalConverter
    {
        public static Vector2 ToScreenVector(PhysicalVector flatVector, Screen screen, Camera camera)
        {
            float x = (flatVector.X - camera.Position.X) * camera.Zoom + screen.Width / 2f;
            float y = screen.Height - ((flatVector.Y - camera.Position.Y) * camera.Zoom + screen.Height / 2f);
            return new Vector2(x, y);
        }

        public static PhysicalVector ToPhysicalVector(Vector2 vector2)
        {
            return new PhysicalVector(vector2.X, vector2.Y);
        }

        public static Vector2 ToVector2(PhysicalVector flatVector)
        {
            return new Vector2(flatVector.X, flatVector.Y);
        }
    }
}