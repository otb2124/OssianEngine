using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resources;

namespace Graphics
{
    /// <summary>
    /// Manages a chain of fullscreen post-process effects applied after the world,
    /// light mask, and vignette have been composited.
    ///
    /// How it works (ping-pong):
    ///   The composited frame is captured into RT[0].
    ///   Effect 1 reads RT[0], writes to RT[1].
    ///   Effect 2 reads RT[1], writes to RT[0].
    ///   ...
    ///   The last written RT is blitted to the backbuffer.
    ///
    /// This means N effects require only 2 render targets regardless of chain length.
    ///
    /// Usage in Graphics.Draw():
    ///   PostProcessManager.Capture();        // redirect backbuffer → RT[0]
    ///   ... draw world, masks, vignette ...
    ///   PostProcessManager.Process(gameTime); // run effect chain, blit final result
    /// </summary>
    public class PostProcessManager : IDisposable
    {
        private bool isDisposed;
        private Game game;

        // Ping-pong pair: [0] = capture target, [1] = scratch target.
        // Roles swap after each effect pass.
        private RenderTarget2D[] rt = new RenderTarget2D[2];

        private List<PostProcessEffect> effects;

        /// <summary>True while Capture() has been called and Process() has not yet.</summary>
        private bool isCapturing;

        public PostProcessManager(Game game, int width, int height)
        {
            this.game = game ?? throw new ArgumentNullException(nameof(game));
            effects = new List<PostProcessEffect>();

            rt[0] = new RenderTarget2D(game.GraphicsDevice, width, height);
            rt[1] = new RenderTarget2D(game.GraphicsDevice, width, height);

            isCapturing = false;
        }

        public void Init()
        {
            Effect saturationShader = ResourceLoader.shaders[Shaders.FX_SATURATION].Shader;
            var saturation = new SaturationEffect(saturationShader);
            saturation.Saturation = 0.75f;
            Add(saturation);

            Effect contrastShader = ResourceLoader.shaders[Shaders.FX_BRIGHTNESS_CONTRAST].Shader;
            var contrast = new BrightnessContrastEffect(contrastShader);
            contrast.Brightness = 1.08f;
            contrast.Contrast = 1.18f;
            Add(contrast);

            Effect colorGradeShader = ResourceLoader.shaders[Shaders.FX_COLOR_GRADE].Shader;
            var colorGrade = new ColorGradeEffect(colorGradeShader);
            colorGrade.TintColor = new Color(255, 240, 200);
            colorGrade.Intensity = 0.22f;
            //Add(colorGrade);

            Effect vignetteShader = ResourceLoader.shaders[Shaders.FX_VIGNETTE].Shader;
            var vignette = new VignetteEffect(vignetteShader);
            vignette.Intensity = 0.2f;
            vignette.Radius = 0.1f;
            //Add(vignette);

            var bloom = new BloomEffect(ResourceLoader.shaders[Shaders.FX_BLOOM].Shader);
            bloom.Threshold = 0.75f;
            bloom.Intensity = 0.5f;      // ← tweak this (higher = stronger glow)
            bloom.Radius = 4.0f;
            //Add(bloom);

            // 4. Rim light - Makes characters and bright objects stand out
            var rimLight = new RimLightEffect(ResourceLoader.shaders[Shaders.FX_RIM_LIGHT_COMPOSITE].Shader);
            rimLight.Intensity = 0.15f;
            rimLight.Power = 4.0f;
            rimLight.RimColor = new Color(255, 248, 220);
            //Add(rimLight);

            // Effect gammaShader = ResourceLoader.shaders[Shaders.FX_GAMMA].Shader;
            // var gamma = new GammaCorrectionEffect(gammaShader);
            // gamma.Gamma = 2.1f;     // slightly lower than 2.2 for softer feel
            // Add(gamma);
        }

        // ── Effect list management ─────────────────────────────────────────────

        public void Add(PostProcessEffect effect) => effects.Add(effect);
        public void Remove(PostProcessEffect effect) => effects.Remove(effect);
        public void Clear() => effects.Clear();

        // ── Pipeline control ──────────────────────────────────────────────────

        /// <summary>
        /// Redirect subsequent drawing into the capture render target.
        /// Call this in Graphics.Draw() before the world blit (step 3).
        /// The GraphicsDevice is NOT cleared here — that is the caller's responsibility.
        /// </summary>
        public void BeginCapture()
        {
            if (isCapturing)
                throw new InvalidOperationException("...");

            game.GraphicsDevice.SetRenderTarget(rt[0]);
            game.GraphicsDevice.Clear(Color.Black);   // ← ADD THIS
            isCapturing = true;
        }

        /// <summary>
        /// Stop capturing, run all enabled effects in sequence using ping-pong,
        /// then blit the final result to the backbuffer at the given destination.
        /// </summary>
        public void EndCaptureAndProcess(Sprites sprites, Rectangle destRect, GameTime gameTime)
        {
            if (!isCapturing)
                throw new InvalidOperationException("...");

            isCapturing = false;

            int src = 0;
            int dst = 1;

            Rectangle fullRect = new Rectangle(0, 0, rt[0].Width, rt[0].Height);

            foreach (PostProcessEffect fx in effects)
            {
                if (!fx.Enabled) continue;

                game.GraphicsDevice.SetRenderTarget(rt[dst]);
                game.GraphicsDevice.Clear(Color.Black);

                fx.Apply(rt[src], gameTime);

                // ← ADD THIS: bind the source RT to the shader's ScreenTexture sampler
                fx.Shader.Parameters["ScreenTexture"]?.SetValue(rt[src]);

                sprites.Begin(null, BlendState.Opaque, fx.Shader);
                sprites.DrawRT(rt[src], fullRect, Color.White);
                sprites.End();

                (src, dst) = (dst, src);
            }

            // Unbind all RTs → back to backbuffer for the final present
            game.GraphicsDevice.SetRenderTarget(null);

            sprites.Begin(null, BlendState.Opaque);
            sprites.DrawRT(rt[src], destRect, Color.White);   // ← DrawRT
            sprites.End();
        }

        // ── IDisposable ───────────────────────────────────────────────────────

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (isDisposed) return;
            if (disposing)
            {
                rt[0]?.Dispose();
                rt[1]?.Dispose();
            }
            isDisposed = true;
        }
    }
}