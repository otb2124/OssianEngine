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

        // ── Light mask ────────────────────────────────────────────────────────
        public static LightMaskTarget LightMask;

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

            // Create the light mask at the same resolution as the Screen so
            // every pixel lines up 1-to-1 when we blit both to the backbuffer.
            LightMask = new LightMaskTarget(game, ScreenResolution.X, ScreenResolution.Y);
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

        public static void UpdateGameTime(GameTime newGameTime)
        {
            CurrentLogicTime = (double)newGameTime.ElapsedGameTime.TotalSeconds * TimeScale;
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

                Sprites.Begin(Camera, BlendState.Additive);
                LightManager.Draw();
                Sprites.End();

                LightMask.EndMask(previousTarget: null);
            }

            // ── 3. Composite world + mask onto backbuffer ─────────────────────

            GraphicsDeviceManager.GraphicsDevice.Clear(Color.Black);
            Rectangle destRect = Screen.GetDestinationRectangle();

            // Blit the raw world
            Sprites.Begin(null, BlendState.Opaque);
            Sprites.Draw(Screen.Target, destRect, Color.White);
            Sprites.End();

            // Multiply the light mask over it
            // (dark ambient pixels stay dark; lit pixels survive)
            if (GameStateManager.gameMode == GameStateManager.GameModes.PLAY_MODE)
            {
                LightMask.Composite(Sprites, destRect);
            }

            // ── 4. Post-composite sprite overlays ─────────────────────────────
            //
            // Vignette and map-specific filter layers (fog, color tints) draw
            // here — after the mask — so they sit on top of the lit world.
            // The full-screen darkness layer is handled by the mask above and
            // is intentionally skipped inside FilterManager.Draw().

            if (GameStateManager.gameMode == GameStateManager.GameModes.PLAY_MODE)
            {
                Sprites.Begin(null, BlendState.AlphaBlend);
                FilterManager.Draw();
                Sprites.End();
            }

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