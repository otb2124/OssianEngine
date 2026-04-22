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
            GREEN,
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
            ParallaxCanvasLayer = new ParallaxLayer(BackgroundCanvasLayerSprites[Type], 1.0f, true);

            ParallaxBackLayers = new ParallaxLayer[BackgroundBackLayerSprites[Type].Length];
            for (int i = 0; i < ParallaxBackLayers.Length; i++)
            {
                float speed = (1 - (float)(i + 1) / (float)(ParallaxBackLayers.Length + 1));

                ParallaxBackLayers[i] = new ParallaxLayer(BackgroundBackLayerSprites[Type][i], speed);
            }

            ParallaxFrontLayers = new ParallaxLayer[BackgroundFrontLayerSprites[Type].Length];
            for (int i = 0; i < ParallaxFrontLayers.Length; i++)
            {
                ParallaxFrontLayers[i] = new ParallaxLayer(BackgroundFrontLayerSprites[Type][i], 0.0f);
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

        public void DrawParallaxBackLayer(int id)
        {
            ParallaxBackLayers[id].Draw();
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
