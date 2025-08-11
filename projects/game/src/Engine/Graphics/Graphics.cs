using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Physics;
using Resources;
using System;
using Utils;

namespace Graphics
{
    public static class Graphics
    {
        public static Sprites sprites;
        public static Shapes shapes;
        public static GraphicsDeviceManager graphicsDeviceManager;
        public static ContentManager contentManager;
        public static Camera camera;
        public static CameraOperator cameraOperator;
        public static Screen screen;

        public static BackgroundManager backgroundManager;
        public static ParticleManager particleManager;
        public static LightManager lightManager;

        public const double UpdatesPerSecond = 120d;
        public const double TargetLogicFrameRate = 60d;
        public const double TimeScale = UpdatesPerSecond / TargetLogicFrameRate;

        public static double CurrentLogicTime;

        public static readonly Point WindowPositon = new Point(400, 40);
        public static readonly Point ScreenResolution = new Point(1280, 720);
        public static readonly float BufferRatio = 0.85f;


        public static void OnGameObjectConstruction(Game game)
        {
            graphicsDeviceManager = new GraphicsDeviceManager(game);
            graphicsDeviceManager.SynchronizeWithVerticalRetrace = true;
            game.IsMouseVisible = false;
            game.IsFixedTimeStep = true;
            game.TargetElapsedTime = TimeSpan.FromTicks((long)Math.Round((double)TimeSpan.TicksPerSecond / UpdatesPerSecond));
        }

        public static void SetGameProps(Game game)
        {
            game.Window.Position = WindowPositon;
            FlatUtil.SetRelativeBackBufferSize(graphicsDeviceManager, BufferRatio);
            screen = new Screen(game, ScreenResolution.X, ScreenResolution.Y);
            sprites = new Sprites(game);
            shapes = new Shapes(game);
            contentManager = game.Content;
            contentManager.RootDirectory = ResourceLoader.ContentFolderPath;
            camera = new Camera(screen, game);
        }


        public static void Init()
        {
            cameraOperator = new CameraOperator(camera);
            backgroundManager = new BackgroundManager();
            backgroundManager.Init();

            particleManager = new ParticleManager();

            lightManager = new LightManager();
        }

        public static void Update()
        {
            cameraOperator.Update();
            camera.Update();

            particleManager.Update();
            backgroundManager.Update();

            lightManager.Update();
        }

        public static void UpdateGameTime(GameTime newGameTime)
        {
            CurrentLogicTime = (double)newGameTime.ElapsedGameTime.TotalSeconds * TimeScale;
        }

        public static void Draw()
        {
            graphicsDeviceManager.GraphicsDevice.Clear(Color.CornflowerBlue);

            screen.Set();

            //bg
            sprites.Begin(camera, BlendState.Additive);
            backgroundManager.DrawCanvas();
            sprites.End();

            //bg layers
            sprites.Begin(camera, BlendState.NonPremultiplied);
            backgroundManager.Draw();
            sprites.End();
            
            //entity sprites
            sprites.Begin(camera);
            Entities.Entities.entityManager.Draw();
            particleManager.Draw();
            backgroundManager.DrawParallaxFrontLayers();
            sprites.End();

            //lighting effect
            sprites.Begin(camera, BlendState.Additive);
            lightManager.Draw();
            sprites.End();

            sprites.Begin(camera, BlendState.AlphaBlend);
            //lightManager.ApplyLighting();
            sprites.End();

            //hitboxes over models (fix to over entity sprites, but under weapon sprites)
            if (GameStateManager.gameMode == GameStateManager.GameModes.COLLISION_DEBUG_MODE)
            {
                shapes.Begin(camera);
                Entities.Entities.entityManager.DrawColliders();
                shapes.End();
            }

            if (GameStateManager.gameMode == GameStateManager.GameModes.HITBOX_DEBUG_MODE)
            {
                shapes.Begin(camera);
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
