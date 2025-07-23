using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphics
{
    public class LightManager : IDisposable
    {

        private RenderTarget2D lightMask;
        private Sprites sprites; 
        private GraphicsDevice graphicsDevice; 
        private List<LightSource> lightSources; 
        private Color darkOverlayColor; 
        private bool isDisposed;


        public LightManager()
        {
            if (Graphics.graphicsDeviceManager == null || Graphics.sprites == null)
            {
                throw new InvalidOperationException("Graphics must be initialized before LightManager");
            }
            graphicsDevice = Graphics.graphicsDeviceManager.GraphicsDevice;
            sprites = Graphics.sprites;
            lightMask = new RenderTarget2D(graphicsDevice, Graphics.ResolutionX, Graphics.ResolutionY);
            lightSources = new List<LightSource>();
            darkOverlayColor = new Color(0, 0, 0, 0.8f);
        }

        public void Init()
        {
            ClearLightSources();
            AddLightSource(new EntityEmissionLightSource(Entities.Entities.player.Id, new LightSource.LightSourceData(LightSource.LightSourceData.LightSourceForms.CIRCULAR, new Vector2(50f, 0f), Vector2.Zero, new Color(1f, 1f, 0.8f, 0.7f), 50f, 0f)));
            //AddLightSource(new EntityEmissionLightSource(20, new LightSource.LightSourceData(LightSource.LightSourceData.LightSourceForms.CIRCULAR, new Vector2(10f, 10f), Vector2.Zero, new Color(1f, 1f, 0.8f, 0.7f), 50f, 0f)));
        }

        public void AddLightSource(LightSource light)
        {
            lightSources.Add(light);
        }

        public void ClearLightSources()
        {
            lightSources.Clear();
        }

        public void Update()
        {
            foreach (var light in lightSources)
            {
                if (light != null)
                {
                    light.Update();
                }
            }
        }

        public void Draw()
        {
            foreach (var light in lightSources)
            {
                if(light != null)
                {
                    light.Draw();
                }
            }
        }

        public void ApplyLighting()
        {
            sprites.Draw(
                lightMask,
                new Rectangle(0, 0, Graphics.ResolutionX, Graphics.ResolutionY),
                darkOverlayColor // Use darkOverlayColor directly
            );
        }

        public void Dispose()
        {
            if (isDisposed)
                return;

            lightMask?.Dispose();
            isDisposed = true;
        }
    }
}
