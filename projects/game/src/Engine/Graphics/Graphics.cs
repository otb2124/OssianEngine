using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
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

            //bg
            sprites.Begin(camera, BlendState.Additive);
            backgroundManager.DrawCanvas();  // Draw background with additive blending
            sprites.End();

            // Draw other background elements: Alpha blending (for transparency)
            sprites.Begin(camera, BlendState.NonPremultiplied); // Use NonPremultiplied for alpha blending
            backgroundManager.Draw();  // Draw foreground elements with alpha blending
            sprites.End();

            //colliders
            if (GameStateManager.gameMode == GameStateManager.GameModes.debugMode)
            {
                shapes.Begin(camera);
                Entities.Entities.entityManager.DrawColliders();
                shapes.End();
            }
            
            //entity sprites
            sprites.Begin(camera);
            Entities.Entities.entityManager.Draw();
            sprites.End();

            //hitboxes over models (fix to over entity sprites, but under weapon sprites)
            if (GameStateManager.gameMode == GameStateManager.GameModes.debugMode)
            {
                shapes.Begin(camera);
                Entities.Entities.entityManager.DrawHitboxes();
                shapes.End();
            }

            //ui
            sprites.Begin(camera);
            UI.UI.UIManager.Draw();
            sprites.End();


            screen.Unset();
            screen.Present(sprites, Color.Black, true);
        }

    }
}
