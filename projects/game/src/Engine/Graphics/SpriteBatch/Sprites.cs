using System;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.XAudio2;

namespace Graphics
{
    public enum SpriteBlendType
    {
        Additive, Alpha
    }

    public sealed class Sprites : IDisposable
    {
        private bool isDisposed;
        private Game game;
        private SpriteBatch sprites;
        private BasicEffect effect;

        public Sprites(Game game)
        {
            isDisposed = false;
            this.game = game ?? throw new ArgumentNullException("game");
            sprites = new SpriteBatch(this.game.GraphicsDevice);

            effect = new BasicEffect(this.game.GraphicsDevice);
            effect.FogEnabled = false;
            effect.LightingEnabled = false;
            effect.PreferPerPixelLighting = false;
            effect.VertexColorEnabled = true;
            effect.Texture = null;
            effect.TextureEnabled = true;
            effect.Projection = Matrix.Identity;
            effect.View = Matrix.Identity;
            effect.World = Matrix.Identity;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (isDisposed)
            {
                return;
            }

            if (disposing)
            {
                effect?.Dispose();
                sprites?.Dispose();
            }

            isDisposed = true;
            GC.SuppressFinalize(this);
        }

        public void Begin(Camera camera = null, BlendState blendState = null, bool anisotroph = false)
        {
            bool textureFiltering = false;

            SamplerState samplerState = textureFiltering ? SamplerState.AnisotropicClamp : SamplerState.PointClamp;

            if (camera is null)
            {
                Viewport viewport = game.GraphicsDevice.Viewport;
                effect.View = Matrix.Identity;
                effect.Projection = Matrix.CreateOrthographicOffCenter(0, viewport.Width, 0, viewport.Height, 0, 1);
            }
            else
            {
                camera.Update();
                effect.View = camera.View;
                effect.Projection = camera.Projection;

                if (camera.Z > camera.BaseZ && anisotroph)
                {
                    samplerState = SamplerState.AnisotropicClamp;
                }
            }

            // If no BlendState is passed, default to Alpha blending (NonPremultiplied)
            blendState = blendState ?? BlendState.NonPremultiplied;

            sprites.Begin(samplerState: samplerState, blendState: blendState, rasterizerState: RasterizerState.CullNone, effect: effect);
        }



        public void End()
        {
            sprites.End();
        }

        public void Draw(Texture2D texture, Rectangle destinationRectangle, Color color)
        {
            sprites.Draw(texture, destinationRectangle, null, color, 0f, Vector2.Zero, SpriteEffects.FlipVertically, 0f);
        }

        public void Draw(Texture2D texture, Rectangle? sourceRectangle, Vector2 origin, Vector2 position, float rotation, float scale, Color color)
        {
            sprites.Draw(texture, position, sourceRectangle, color, rotation, origin, new Vector2(scale), SpriteEffects.FlipVertically, 0f);
        }

        public void Draw(Texture2D texture, Rectangle? sourceRectangle, Vector2 origin, Vector2 position, float rotation, Vector2 scale, Color color)
        {
            sprites.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, SpriteEffects.FlipVertically, 0f);
        }

        public void Draw(Texture2D texture, Rectangle? sourceRectangle, Vector2 origin, Vector2 position, Color color)
        {
            sprites.Draw(texture, position, sourceRectangle, color, 0f, origin, 1f, SpriteEffects.FlipVertically, 0f);
        }

        public void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRectangle, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effect, float layerDepth)
        {
            sprites.Draw(texture, position, sourceRectangle, color, rotation, origin, scale, effect, layerDepth);
        }

        public void DrawString(SpriteFont font, string text, Vector2 position, Color color)
        {
            sprites.DrawString(font, text, position, color, 0f, Vector2.Zero, 1f, SpriteEffects.FlipVertically, 0f);
        }

        public void DrawString(SpriteFont font, StringBuilder text, Vector2 position, Color color)
        {
            sprites.DrawString(font, text, position, color, 0f, Vector2.Zero, 1f, SpriteEffects.FlipVertically, 0f);
        }

        public void DrawString(SpriteFont font, string text, Vector2 position, float rotation, Vector2 origin, float scale, Color color)
        {
            sprites.DrawString(font, text, position, color, rotation, origin, scale, SpriteEffects.FlipVertically, 0f);
        }
    }
}
