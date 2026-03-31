// Graphics/EntityFXRenderer.cs
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graphics
{
    public class EntityFXRenderer : IDisposable
    {
        private bool isDisposed = false;
        private readonly RenderTarget2D[] rt = new RenderTarget2D[3];

        public List<PostProcessEffect> Effects { get; } = new();

        public bool HasEffects => Effects.Count > 0;

        public EntityFXRenderer(int width, int height)
        {
            var gd = Graphics.GraphicsDeviceManager.GraphicsDevice;
            for (int i = 0; i < 3; i++)
                rt[i] = new RenderTarget2D(gd, width, height, false,
                    SurfaceFormat.Color, DepthFormat.None, 0,
                    RenderTargetUsage.PreserveContents);
        }

        public void Add(PostProcessEffect fx) => Effects.Add(fx);

        public RenderTarget2D CaptureAndProcess(
            Sprites sprites,
            Action drawAction,
            Action endOuterBatch,
            Action beginOuterBatch,
            GameTime gameTime)
        {
            var gd = Graphics.GraphicsDeviceManager.GraphicsDevice;

            endOuterBatch();

            // Capture entity into rt[0] with camera transform
            gd.SetRenderTarget(rt[0]);
            gd.Clear(new Color(0, 0, 0, 0));
            sprites.Begin(Graphics.Camera, BlendState.NonPremultiplied);
            drawAction();
            sprites.End();

            // Ping-pong
            int src = 0, dst = 1;
            Rectangle fullRect = new Rectangle(0, 0, rt[0].Width, rt[0].Height);

            foreach (var fx in Effects)
            {
                if (!fx.Enabled) continue;

                if (fx is BloomEffect bloom)
                {
                    // rt[2] is dedicated scratch, result always lands in rt[dst]
                    bloom.ApplyMultiPass(gd, sprites, rt[src], rt[dst], rt[2], gameTime);
                    (src, dst) = (dst, src);
                }
                else
                {
                    gd.SetRenderTarget(rt[dst]);
                    gd.Clear(new Color(0, 0, 0, 0));
                    fx.Apply(rt[src], gameTime);
                    fx.Shader.Parameters["ScreenTexture"]?.SetValue(rt[src]);
                    sprites.Begin(null, BlendState.AlphaBlend, fx.Shader);
                    sprites.DrawRT(rt[src], fullRect, Color.White, SpriteEffects.FlipVertically);
                    sprites.End();
                    (src, dst) = (dst, src);
                }
            }

            // DON'T blit back here — just return which RT has the result
            beginOuterBatch();

            return rt[src];
        }



        public void Dispose()
        {
            if (isDisposed) return;
            rt[0]?.Dispose();
            rt[1]?.Dispose();
            isDisposed = true;
        }
    }
}