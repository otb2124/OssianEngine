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
            Effect crtShader = ResourceLoader.shaders[Shaders.FX_CRT].Shader;
            var crt = new CRTEffect(crtShader);
            crt.ScanlineStrength = 0.2f;
            crt.Curvature = 0.9f;
            //Add(crt);
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