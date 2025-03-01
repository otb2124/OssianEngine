using Microsoft.Xna.Framework;
using System.Runtime.CompilerServices;
using Utils;

namespace Graphics
{
    public static class Graphics
    {
        public static int ResolutionX = 1280, ResolutionY = 720;

        public static Sprites sprites;
        public static Shapes shapes;
        public static GraphicsDeviceManager graphicsDeviceManager;
        public static Camera camera;
        public static CameraOperator cameraOperator;
        public static Screen screen;
        public static GameTime gameTime;
        public static BackgroundManager backgroundManager;

        public static void Init()
        {
            cameraOperator = new CameraOperator(camera);
            backgroundManager = new BackgroundManager();
            backgroundManager.Init();
        }

        public static void Update()
        {
            cameraOperator.Update();
            camera.Update();
            backgroundManager.Update();
        }

        public static void Draw()
        {
            screen.Set();

            sprites.Begin(camera);
            backgroundManager.Draw();
            sprites.End();

            if(GameStateManager.gameMode == GameStateManager.GameModes.debugMode)
            {
                shapes.Begin(camera);
                Entities.Entities.entityManager.DrawShapes();
                shapes.End();
            }
            

            sprites.Begin(camera);
            Entities.Entities.entityManager.Draw();
            UI.UI.UIManager.Draw();
            sprites.End();


            screen.Unset();
            screen.Present(sprites, Color.Black, true);
        }

    }
}
