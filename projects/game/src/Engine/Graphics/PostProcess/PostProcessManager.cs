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
            Effect fadeShader = ResourceLoader.shaders[Shaders.FX_CRT].Shader;

            // Pass it to the effect class
            var fade = new CRTEffect(fadeShader);
            //Add(fade);
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
                throw new InvalidOperationException("PostProcessManager.BeginCapture() called twice without Process().");

            game.GraphicsDevice.SetRenderTarget(rt[0]);
            isCapturing = true;
        }

        /// <summary>
        /// Stop capturing, run all enabled effects in sequence using ping-pong,
        /// then blit the final result to the backbuffer at the given destination.
        /// </summary>
        public void EndCaptureAndProcess(Sprites sprites, Rectangle destRect, GameTime gameTime)
        {
            if (!isCapturing)
                throw new InvalidOperationException("PostProcessManager.EndCaptureAndProcess() called without BeginCapture().");

            // Return to backbuffer before the loop so each pass can write to the other RT.
            game.GraphicsDevice.SetRenderTarget(null);
            isCapturing = false;

            int src = 0; // RT index that contains the current frame
            int dst = 1; // RT index to write the next effect into

            foreach (PostProcessEffect fx in effects)
            {
                if (!fx.Enabled) continue;

                // Write this effect's output into rt[dst].
                game.GraphicsDevice.SetRenderTarget(rt[dst]);
                game.GraphicsDevice.Clear(Color.Transparent);

                fx.Apply(rt[src], gameTime);

                sprites.Begin(null, BlendState.Opaque, fx.Shader);
                sprites.Draw(rt[src], destRect, Color.White);
                sprites.End();

                game.GraphicsDevice.SetRenderTarget(null);

                // Swap roles.
                (src, dst) = (dst, src);
            }

            // Blit the final processed frame to the backbuffer.
            sprites.Begin(null, BlendState.Opaque);
            sprites.Draw(rt[src], destRect, Color.White);
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