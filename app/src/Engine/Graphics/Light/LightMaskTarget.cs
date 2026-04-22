using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graphics
{
    /// <summary>
    /// Owns a RenderTarget2D that receives all light-blob draws (black background,
    /// additive white/colored blobs). The mask is then composited over the world
    /// using a multiply blend so unlit pixels go dark and lit pixels survive.
    /// </summary>
    public sealed class LightMaskTarget : IDisposable
    {
        private bool isDisposed;
        private Game game;

        private RenderTarget2D maskTarget;
        private bool isSet;

        /// <summary>Ambient darkness color. Pure black = fully dark; lighter = brighter ambient.</summary>
        public Color AmbientColor = Color.Black;

        /// <summary>
        /// Multiply blend: dest * src.
        /// MonoGame has no built-in Multiplicative, so we define it manually.
        ///   BlendFunction.Add with (Zero, SourceColor) = dest * src per channel.
        /// </summary>
        private static readonly BlendState MultiplyBlend = new BlendState
        {
            ColorBlendFunction = BlendFunction.Add,
            ColorSourceBlend = Blend.Zero,
            ColorDestinationBlend = Blend.SourceColor,

            AlphaBlendFunction = BlendFunction.Add,
            AlphaSourceBlend = Blend.Zero,
            AlphaDestinationBlend = Blend.SourceAlpha,
        };

        public LightMaskTarget(Game game, int width, int height)
        {
            this.game = game ?? throw new ArgumentNullException(nameof(game));
            maskTarget = new RenderTarget2D(game.GraphicsDevice, width, height);
            isSet = false;
        }

        // ── Render target control ─────────────────────────────────────────────

        /// <summary>
        /// Redirect all subsequent draw calls into the light mask.
        /// Call this before Sprites.Begin() for the light pass.
        /// </summary>
        public void BeginMask()
        {
            if (isSet)
                throw new InvalidOperationException("LightMaskTarget is already set as render target.");

            game.GraphicsDevice.SetRenderTarget(maskTarget);

            // Clear to the ambient color. Pure black = total darkness; anything brighter
            // raises the ambient light floor before any LightSource blobs are drawn.
            game.GraphicsDevice.Clear(AmbientColor);

            isSet = true;
        }

        /// <summary>
        /// Return rendering back to whatever target was active before (usually the Screen).
        /// Call this after Sprites.End() for the light pass.
        /// </summary>
        public void EndMask(RenderTarget2D previousTarget = null)
        {
            if (!isSet)
                throw new InvalidOperationException("EndMask called without a matching BeginMask.");

            game.GraphicsDevice.SetRenderTarget(previousTarget);
            isSet = false;
        }

        // ── Compositing ───────────────────────────────────────────────────────

        /// <summary>
        /// Composite the light mask over the world.
        /// Multiplicative blend: world_pixel * mask_pixel.
        ///   mask = black (0,0,0) → world pixel becomes black (fully dark)
        ///   mask = white (1,1,1) → world pixel unchanged (fully lit)
        ///   mask = colored       → world pixel tinted by light color
        ///
        /// <param name="sprites">The Sprites wrapper to draw through.</param>
        /// <param name="destinationRectangle">Same destination rect Screen.Present() uses.</param>
        /// </summary>
        public void Composite(Sprites sprites, Rectangle destinationRectangle)
        {
            if (isSet)
                throw new InvalidOperationException("EndMask must be called before Composite.");

            sprites.Begin(null, MultiplyBlend);
            sprites.Draw(maskTarget, destinationRectangle, Color.White);
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
                maskTarget?.Dispose();
                maskTarget = null;
            }
            isDisposed = true;
        }
    }
}