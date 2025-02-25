using Microsoft.Xna.Framework;

namespace Graphics
{
    public static class Graphics
    {
        public static int ResolutionX = 1280, ResolutionY = 768;

        public static Sprites sprites;
        public static Shapes shapes;
        public static GraphicsDeviceManager graphicsDeviceManager;
        public static Camera camera;
        public static CameraOperator cameraOperator;
        public static Screen screen;
        public static GameTime gameTime;

        public static void Draw()
        {
            screen.Set();
            shapes.Begin(camera);

            //ENTITIES
            sprites.Begin(camera);

            Entities.Entities.entityManager.Draw();
            //UIMANAGER
            UI.UIManager.Draw();
            //Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effect, float layerDepth

            shapes.End();
            sprites.End();

            screen.Unset();
            screen.Present(sprites, Color.Black, true);
        }
    }
}
