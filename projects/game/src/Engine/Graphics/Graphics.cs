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
        public static Sprites Sprites;
        public static Shapes Shapes;
        public static GraphicsDeviceManager GraphicsDeviceManager;
        public static ContentManager ContentManager;
        public static Camera Camera;
        public static CameraOperator CameraOperator;
        public static Screen Screen;

        public static BackgroundManager BackgroundManager;
        public static ParticleManager ParticleManager;
        public static LightManager LightManager;
        public static FilterManager FilterManager;
        public static VFXManager VFXManager;

        public const double UpdatesPerSecond = 120d;
        public const double TargetLogicFrameRate = 60d;
        public const double TimeScale = UpdatesPerSecond / TargetLogicFrameRate;

        public static double CurrentLogicTime;

        public static readonly Point WindowPositon = new Point(400, 40);
        public static readonly Point ScreenResolution = new Point(1280, 720);
        public static readonly float BufferRatio = 0.85f;


        public static void OnGameObjectConstruction(Game Game)
        {
            GraphicsDeviceManager = new GraphicsDeviceManager(Game);
            GraphicsDeviceManager.SynchronizeWithVerticalRetrace = true;
            Game.IsMouseVisible = false;
            Game.IsFixedTimeStep = true;
            Game.TargetElapsedTime = TimeSpan.FromTicks((long)Math.Round((double)TimeSpan.TicksPerSecond / UpdatesPerSecond));
        }

        public static void SetGameProps(Game game)
        {
            game.Window.Position = WindowPositon;
            PhysicalUtil.SetRelativeBackBufferSize(GraphicsDeviceManager, BufferRatio);
            Screen = new Screen(game, ScreenResolution.X, ScreenResolution.Y);
            Sprites = new Sprites(game);
            Shapes = new Shapes(game);
            ContentManager = game.Content;
            ContentManager.RootDirectory = ResourceLoader.ContentFolderPath;
            Camera = new Camera(Screen, game);
        }


        public static void Init()
        {
            CameraOperator = new CameraOperator();
            BackgroundManager = new BackgroundManager();
            BackgroundManager.Init();

            ParticleManager = new ParticleManager();

            LightManager = new LightManager();
            FilterManager = new FilterManager();
            FilterManager.Init();

            VFXManager = new VFXManager();
        }

        public static void Update()
        {
            CameraOperator.Update();
            Camera.Update();

            ParticleManager.Update();
            BackgroundManager.Update();

            LightManager.Update();
            FilterManager.Update();

            VFXManager.Update();
        }

        public static void UpdateGameTime(GameTime newGameTime)
        {
            CurrentLogicTime = (double)newGameTime.ElapsedGameTime.TotalSeconds * TimeScale;
        }

        public static void Draw()
        {
            GraphicsDeviceManager.GraphicsDevice.Clear(Color.CornflowerBlue);

            Screen.Set();

            //bg
            Sprites.Begin(Camera, BlendState.Additive);
            BackgroundManager.DrawCanvas();
            Sprites.End();

            //bg MapLayers
            Sprites.Begin(Camera, BlendState.NonPremultiplied);
            BackgroundManager.Draw();
            Sprites.End();

            //entity Sprites
            Sprites.Begin(Camera);
            Entities.Entities.EntityManager.Draw();
            ParticleManager.Draw();
            BackgroundManager.DrawParallaxFrontLayers();
            Sprites.End();

            if (GameStateManager.gameMode == GameStateManager.GameModes.PLAY_MODE)
            {
                //filters
                Sprites.Begin(Camera, BlendState.AlphaBlend);
                FilterManager.Draw();
                Sprites.End();

                //light
                Sprites.Begin(Camera, BlendState.Additive, false, true);
                LightManager.Draw();
                Sprites.End();
            }

            //hitboxes over models (fix to over entity Sprites, but under weapon Sprites)
            if (GameStateManager.gameMode == GameStateManager.GameModes.COLLISION_DEBUG_MODE)
            {
                Shapes.Begin(Camera);
                Entities.Entities.EntityManager.DrawColliders();
                Entities.Entities.EventManager.DrawColliders();
                Shapes.End();
            }

            if (GameStateManager.gameMode == GameStateManager.GameModes.HITBOX_DEBUG_MODE)
            {
                Shapes.Begin(Camera);
                Entities.Entities.EntityManager.DrawHitboxes();
                Shapes.End();
            }

            Sprites.Begin(Camera, BlendState.NonPremultiplied, false, false);


            //ui
            UI.UI.UIManager.Draw();
            Sprites.End();

            if (GameStateManager.gameMode == GameStateManager.GameModes.COLLISION_DEBUG_MODE || GameStateManager.gameMode == GameStateManager.GameModes.HITBOX_DEBUG_MODE)
            {
                Shapes.Begin(Camera);
                UI.UI.UIManager.DrawDebug();
                Shapes.End();
            }

            Screen.Unset();
            Screen.Present(Sprites, Color.Black, true);
        }

    }
}
