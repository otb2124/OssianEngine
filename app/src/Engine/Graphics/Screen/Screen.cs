using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Physics;

namespace Graphics
{
    public sealed class Screen : IDisposable
    {
        public readonly static int MinDim = 64;
        public readonly static int MaxDim = 4096;

        private bool isDisposed;
        private Game game;

        public readonly int Width;
        public readonly int Height;

        private RenderTarget2D target;
        private bool isSet;

        public RenderTarget2D Target => target;      // expose so Graphics.cs can restore it

        public Screen(Game game, int width, int height)
        {
            isDisposed = false;
            this.game = game ?? throw new ArgumentNullException("game");

            Width = PhysicalMath.Clamp(width, MinDim, MaxDim);
            Height = PhysicalMath.Clamp(height, MinDim, MaxDim);

            target = new RenderTarget2D(this.game.GraphicsDevice, Width, Height);
            isSet = false;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (isDisposed) return;
            if (disposing)
            {
                target?.Dispose();
                target = null;
            }
            isDisposed = true;
            GC.SuppressFinalize(this);
        }

        public void Set()
        {
            if (isSet)
                throw new Exception("The Screen is already set as the rendering target.");

            game.GraphicsDevice.SetRenderTarget(target);
            isSet = true;
        }

        public void Unset()
        {
            if (!isSet)
                throw new Exception("Function \"SetGameProps\" must be called before \"UnSet\" as pairs.");

            game.GraphicsDevice.SetRenderTarget(null);
            isSet = false;
        }

        public void Present(Sprites sprites)
        {
            Present(sprites, Color.CornflowerBlue);
        }

        public void Present(Sprites sprites, Color backgroundColor, bool textureFiltering = true)
        {
            if (isSet)
                throw new Exception("The \"Screen\" is currently set as the render target. \"UnSet\" the \"Screen\" before presenting.");

            if (sprites is null)
                throw new ArgumentNullException("Sprites");

            game.GraphicsDevice.Clear(backgroundColor);

            Rectangle destinationRectangle = CalculateDestinationRectangle();

            sprites.Begin(null, BlendState.Opaque);
            sprites.Draw(target, destinationRectangle, Color.White);
            sprites.End();
        }

        /// <summary>
        /// Returns the letterboxed/pillarboxed destination rectangle used when blitting
        /// the Screen to the backbuffer. Use this in Graphics.cs when compositing the
        /// light mask so it lines up perfectly with the world blit.
        /// </summary>
        public Rectangle GetDestinationRectangle() => CalculateDestinationRectangle();

        internal Rectangle CalculateDestinationRectangle()
        {
            Rectangle backbufferRectangle = game.GraphicsDevice.PresentationParameters.Bounds;
            float backbuffer_aspect = backbufferRectangle.Width / (float)backbufferRectangle.Height;
            float screen_aspect = Width / (float)Height;

            float rx = 0;
            float ry = 0;
            float rw = backbufferRectangle.Width;
            float rh = backbufferRectangle.Height;

            if (screen_aspect > backbuffer_aspect)
            {
                rh = rw / screen_aspect;
                ry = (backbufferRectangle.Height - rh) / 2f;
            }
            else if (screen_aspect < backbuffer_aspect)
            {
                rw = rh * screen_aspect;
                rx = (backbufferRectangle.Width - rw) / 2f;
            }

            return new Rectangle((int)rx, (int)ry, (int)rw, (int)rh);
        }
    }
}