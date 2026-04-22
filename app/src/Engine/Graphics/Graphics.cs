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

        //from ini config
        public static Point WindowPosition;
        public static Point PreferredBackBufferSize;
        public static float BufferRatio;
        public static bool IsFullscreen;
        public static bool SynchronizeWithVerticalRetrace;
        public static bool IsBorderLess;
        public static double GraphicsFrameRate = 120d;
        public static double LogicUpdateRate = 60d;
        public static bool IsMouseVisible;
        public static bool ConsoleVisible;
        

        // ── Light mask ────────────────────────────────────────────────────────
        public static LightMaskTarget LightMask;

        // ── Post-process ──────────────────────────────────────────────────────
        public static PostProcessManager PostProcess;

        public static double TimeScale = GraphicsFrameRate / LogicUpdateRate;

        public static double CurrentLogicTime;

        


        public static readonly BlendState OverwriteBlend = new BlendState
        {
            ColorSourceBlend = Blend.One,
            ColorDestinationBlend = Blend.InverseSourceAlpha,
            AlphaSourceBlend = Blend.One,
            AlphaDestinationBlend = Blend.InverseSourceAlpha,
        };


        public static void OnGameObjectConstruction(Game Game)
        {
            ConsoleHelper.SetConsoleVisibility(ConsoleVisible);
            GraphicsDeviceManager = new GraphicsDeviceManager(Game);
            GraphicsDeviceManager.SynchronizeWithVerticalRetrace = SynchronizeWithVerticalRetrace;
            GraphicsDeviceManager.GraphicsProfile = GraphicsProfile.HiDef;
            GraphicsDeviceManager.PreferredBackBufferWidth = PreferredBackBufferSize.X;
            GraphicsDeviceManager.PreferredBackBufferHeight = PreferredBackBufferSize.Y;
            GraphicsDeviceManager.IsFullScreen = IsFullscreen;
            Game.IsMouseVisible = IsMouseVisible;
            Game.IsFixedTimeStep = true;
            Game.TargetElapsedTime = TimeSpan.FromTicks((long)Math.Round((double)TimeSpan.TicksPerSecond / GraphicsFrameRate));
            GraphicsDeviceManager.ApplyChanges();
        }

        public static void SetGameProps(Game game)
        {
            game.Window.Position = WindowPosition;
            PhysicalUtil.SetRelativeBackBufferSize(GraphicsDeviceManager, BufferRatio);
            Screen = new Screen(game, PreferredBackBufferSize.X, PreferredBackBufferSize.Y);
            Sprites = new Sprites(game);
            Shapes = new Shapes(game);
            ContentManager = game.Content;
            ContentManager.RootDirectory = ResourceLoader.ContentFolderPath;
            Camera = new Camera(Screen, game);

            // Create the light mask at the same resolution as the Screen so
            // every pixel lines up 1-to-1 when we blit both to the backbuffer.
            LightMask = new LightMaskTarget(game, PreferredBackBufferSize.X, PreferredBackBufferSize.Y);

            // Post-process ping-pong buffers at Screen resolution.
            PostProcess = new PostProcessManager(game, PreferredBackBufferSize.X, PreferredBackBufferSize.Y);
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
            VFXManager.Init();
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

        // Store GameTime so Draw() can pass it to PostProcessManager.
        public static GameTime _lastGameTime;

        public static void UpdateGameTime(GameTime newGameTime)
        {
            CurrentLogicTime = (double)newGameTime.ElapsedGameTime.TotalSeconds * TimeScale;
            _lastGameTime = newGameTime;
        }

        public static void Draw()
        {
            GraphicsDeviceManager.GraphicsDevice.Clear(Color.CornflowerBlue);

            // ── 1. World pass → Screen RenderTarget ───────────────────────────

            Screen.Set();

            Sprites.Begin(Camera, BlendState.Additive);
            BackgroundManager.DrawCanvas();
            Sprites.End();

            Sprites.Begin(Camera, BlendState.NonPremultiplied);
            BackgroundManager.Draw();
            Sprites.End();

            Sprites.Begin(Camera);
            Entities.Entities.EntityManager.Draw();
            ParticleManager.Draw();
            VFXManager.Draw();
            BackgroundManager.DrawParallaxFrontLayers();
            Sprites.End();

            Screen.Unset();

            // ── 2. Light mask pass → LightMask RenderTarget ───────────────────
            //
            // AmbientColor is set from the day/night darkness layers so the mask
            // automatically tracks the time of day:
            //   noon    → FilterManager returns White  → no darkening
            //   sunset  → returns dark orange-ish      → warm dim ambient
            //   midnight→ returns near-Black            → only lit spots visible
            //
            // Light blobs are drawn additively on top of that ambient base,
            // then multiplied over the world in step 3.

            if (GameStateManager.gameMode == GameStateManager.GameModes.PLAY_MODE)
            {
                LightMask.AmbientColor = FilterManager.GetDayTimeAmbient();
                LightMask.BeginMask();

                // IMPORTANT: Draw lights in SCREEN SPACE (no camera)
                Sprites.Begin(null, BlendState.Additive);        // ← null = screen space
                LightManager.DrawInScreenSpace();                // ← New method
                Sprites.End();

                LightMask.EndMask(previousTarget: null);
            }

            // ── 3. Composite world + mask → captured into PostProcess RT ─────────
            //
            // BeginCapture() redirects rendering into the post-process ping-pong buffer.
            // Everything drawn between BeginCapture and EndCaptureAndProcess becomes
            // the input texture for the first post-process effect in the chain.

            GraphicsDeviceManager.GraphicsDevice.Clear(Color.Black);
            Rectangle destRect = Screen.GetDestinationRectangle();

            //GraphicsDeviceManager.GraphicsDevice.Clear(Color.Black);

            PostProcess.BeginCapture();

            // Use full RT dimensions — destRect letterboxing only applies on the final backbuffer blit
            Rectangle fullRect = new Rectangle(0, 0, Screen.Width, Screen.Height);

            Sprites.Begin(null, BlendState.Opaque);
            Sprites.DrawRT(Screen.Target, fullRect, Color.White);
            Sprites.End();

            //Entities.Entities.EntityManager.DrawFXEntities();

            Sprites.Begin(null, OverwriteBlend);
            Entities.Entities.EntityManager.BlitEntityFXResults(Sprites, fullRect);
            Sprites.End();

            if (GameStateManager.gameMode == GameStateManager.GameModes.PLAY_MODE)
            {
                LightMask.Composite(Sprites, fullRect);   // ← fullRect here too
            }

            if (GameStateManager.gameMode == GameStateManager.GameModes.PLAY_MODE)
            {
                Sprites.Begin(null, BlendState.AlphaBlend);
                FilterManager.Draw();
                Sprites.End();
            }

            PostProcess.EndCaptureAndProcess(Sprites, destRect);

            // ── 5. Debug overlays ─────────────────────────────────────────────

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

            // ── 6. UI ─────────────────────────────────────────────────────────
            UI.UI.Draw();
        }

    }
}