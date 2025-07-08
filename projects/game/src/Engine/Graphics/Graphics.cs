using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Utils;

namespace Graphics
{
    public static class Graphics
    {
        public static int ResolutionX = 1280, ResolutionY = 720;

        public static Sprites sprites;
        public static Shapes shapes;
        public static GraphicsDeviceManager graphicsDeviceManager;
        public static ContentManager contentManager;
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

            //bg
            sprites.Begin(camera, BlendState.Additive);
            backgroundManager.DrawCanvas();
            sprites.End();

            // DrawParallaxBackLayers other background elements: Alpha blending (for transparency)
            sprites.Begin(camera, BlendState.NonPremultiplied);
            backgroundManager.Draw();
            sprites.End();
            
            //entity sprites
            sprites.Begin(camera);
            Entities.Entities.entityManager.Draw();
            backgroundManager.DrawParallaxFrontLayers();
            sprites.End();

            //hitboxes over models (fix to over entity sprites, but under weapon sprites)
            if (GameStateManager.gameMode == GameStateManager.GameModes.debugMode)
            {
                shapes.Begin(camera);
                Entities.Entities.entityManager.DrawColliders();
                Entities.Entities.entityManager.DrawHitboxes();
                shapes.End();
            }

            sprites.Begin(camera, BlendState.NonPremultiplied, false, false);
            //ui
            UI.UI.UIManager.Draw();
            sprites.End();


            screen.Unset();
            screen.Present(sprites, Color.Black, true);
        }

    }
}
