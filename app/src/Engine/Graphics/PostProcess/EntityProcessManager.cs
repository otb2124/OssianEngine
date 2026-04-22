using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Graphics
{
    public class EntityProcessManager : IDisposable
    {
        private bool isDisposed = false;
        private readonly RenderTarget2D[] rt = new RenderTarget2D[3];

        public List<ProcessEffect> Effects { get; } = new();

        public bool HasEffects => Effects.Count > 0;

        public EntityProcessManager()
        {
            var gd = Graphics.GraphicsDeviceManager.GraphicsDevice;
            for (int i = 0; i < 3; i++)
                rt[i] = new RenderTarget2D(gd, Graphics.PreferredBackBufferSize.X, Graphics.PreferredBackBufferSize.Y, false,
                    SurfaceFormat.Color, DepthFormat.None, 0,
                    RenderTargetUsage.PreserveContents);
        }

        public void Add(ProcessEffect fx) => Effects.Add(fx);

        public ProcessEffect GetEffect(Type type)
        {
            return Effects.FirstOrDefault(fx => fx.GetType() == type);
        }

        public void SetShaders()
        {
            foreach (var effect in Effects)
            {
                effect.SetShader();
            }
        }

        public RenderTarget2D CaptureAndProcess(Action drawAction)
        {
            var gd = Graphics.GraphicsDeviceManager.GraphicsDevice;

            bool wasOpen = Graphics.Sprites.IsBatchOpen;

            if (wasOpen)
                Graphics.Sprites.End();

            // Capture entity into rt[0] with camera transform
            gd.SetRenderTarget(rt[0]);
            gd.Clear(new Color(0, 0, 0, 0));
            Graphics.Sprites.Begin(Graphics.Camera, BlendState.NonPremultiplied);
            drawAction();
            Graphics.Sprites.End();

            // Ping-pong
            int src = 0, dst = 1;
            Rectangle fullRect = new Rectangle(0, 0, rt[0].Width, rt[0].Height);

            foreach (var fx in Effects)
            {
                if (!fx.Enabled) continue;
                if (fx is BloomEffect || fx is BurningEffect)
                {
                    fx.ApplyMultiPass(Graphics.Sprites, rt[src], rt[dst], rt[2]);
                    (src, dst) = (dst, src);
                }
                else
                {
                    gd.SetRenderTarget(rt[dst]);
                    gd.Clear(new Color(0, 0, 0, 0));
                    fx.Apply(rt[src], Graphics._lastGameTime);
                    fx.Shader.Parameters["ScreenTexture"]?.SetValue(rt[src]);
                    Graphics.Sprites.Begin(null, BlendState.AlphaBlend, fx.Shader);
                    Graphics.Sprites.DrawRT(rt[src], fullRect, Color.White);
                    Graphics.Sprites.End();
                    (src, dst) = (dst, src);
                }
            }

            // Restore previous RT and reopen batch only if it was open before
            if (wasOpen)
                Graphics.Sprites.Begin(Graphics.Camera);

            return rt[src];
        }



        public void Dispose()
        {
            if (isDisposed) return;
            rt[0]?.Dispose();
            rt[1]?.Dispose();
            rt[2]?.Dispose();
            isDisposed = true;
        }
    }
}