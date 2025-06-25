using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resources;
using System.Diagnostics;
using System.Drawing;
using static Resources.StaticSpriteFactory;
using Color = Microsoft.Xna.Framework.Color;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Graphics
{
    public class ParallaxBackground
    {

        public enum ParallaxBackgrounds
        {
            SEASIDE_EVENING,
        }

        public ParallaxBackgrounds Type;
        public ParallaxLayer ParallaxCanvasLayer;
        public ParallaxLayer[] ParallaxBackLayers;
        public ParallaxLayer[] ParallaxFrontLayers;

        public ParallaxBackground(ParallaxLayer parallaxCanvasLayer, ParallaxLayer[] parallaxBackLayers, ParallaxLayer[] parallaxFrontLayers)
        {
            ParallaxCanvasLayer = parallaxCanvasLayer;
            ParallaxBackLayers = parallaxBackLayers;
            ParallaxFrontLayers = parallaxFrontLayers;

            Init();
        }

        public ParallaxBackground(ParallaxBackgrounds type)
        {
            Type = type;

            SetLayers();
            Init();
        }


        public void SetLayers()
        {
            ParallaxCanvasLayer = new ParallaxLayer(backgroundCanvasLayerSprites[Type], 1.0f);

            ParallaxBackLayers = new ParallaxLayer[backgroundBackLayerSprites[Type].Length];
            for (int i = 0; i < ParallaxBackLayers.Length; i++)
            {
                ParallaxBackLayers[i] = new ParallaxLayer(backgroundBackLayerSprites[Type][i], 0.5f);
            }

            ParallaxFrontLayers = new ParallaxLayer[backgroundFrontLayerSprites[Type].Length];
            for (int i = 0; i < ParallaxFrontLayers.Length; i++)
            {
                ParallaxFrontLayers[i] = new ParallaxLayer(backgroundFrontLayerSprites[Type][i], 0.0f);
            }

        }

        public void Init()
        {
            ParallaxCanvasLayer.Init();
            foreach (var item in ParallaxBackLayers)
            {
                item.Init();
            }
            foreach (var item in ParallaxFrontLayers)
            {
                item.Init();
            }
        }

        public void Update()
        {
            ParallaxCanvasLayer.Update();
            foreach (var item in ParallaxBackLayers)
            {
                item.Update();
            }
            foreach (var item in ParallaxFrontLayers)
            {
                item.Update();
            }
        }


        public void DrawCanvas()
        {
            ParallaxCanvasLayer.Draw();
        }

        public void DrawParallaxBackLayers()
        {
            foreach (var item in ParallaxBackLayers)
            {
                item.Draw();
            }
        }

        public void DrawParallaxFrontLayers()
        {
            foreach(var item in ParallaxFrontLayers)
            {
                item.Draw();
            }
        }
    }
}
