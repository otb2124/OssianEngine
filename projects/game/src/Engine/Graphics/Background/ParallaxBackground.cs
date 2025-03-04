using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resources;
using System.Linq;
using UI;
using Utils;

namespace Graphics
{
    public class ParallaxBackground
    {

        public AnimationManager[] aManagers;
        public StaticSprites[] layers;
        public float[] layerSpeeds;
        public Vector2[] positions;

        public Rectangle bounds;

        public ParallaxBackground()
        {
            SetLayers(new StaticSprites[] { 
                StaticSprites.GRAPHICS_PARALLAX_0_0, 
                StaticSprites.GRAPHICS_PARALLAX_0_1,
                StaticSprites.GRAPHICS_PARALLAX_0_2,
                StaticSprites.GRAPHICS_PARALLAX_0_3,
            });

            layerSpeeds = new float[]
            {
                1.0f,
                1.0f,
                1.0f/1.5f,
                1.0f/1.5f/1.5f,
            };

            positions = new Vector2[4];

            int lastIndex = layers.Length - 1;
            AnimationManager lastAnim = new AnimationManager();
            lastAnim.AddStaticAnimation(layers[lastIndex]);

            Rectangle lastFrame = lastAnim.GetCurrent().GetCurrentFrame();
            bounds = new Rectangle(-Graphics.screen.Width/2, 0, lastFrame.Width, lastFrame.Height);
        }

        public void SetLayers(StaticSprites[] sprites)
        {
            this.layers = sprites;
            this.aManagers = new AnimationManager[layers.Length];

            for (int i = 0; i < layers.Length; i++)
            {
                aManagers[i] = new AnimationManager();
                aManagers[i].AddStaticAnimation(layers[i]);
            }
        }

        public void Update()
        {
            float cameraposX = Graphics.camera.Position.X;

            for (int i = 0; i < layers.Length; i++)
            {
                positions[i].X = cameraposX * layerSpeeds[i];
            }
        }

        public void Draw()
        {
            for (int i = 1; i < aManagers.Length; i++)
            {
                Rectangle srcRect = aManagers[i].GetCurrent().GetCurrentFrame();

                Vector2 adjustedPos = positions[i];
                Vector2 adjustedOrigin = (srcRect.Size.ToVector2() / 2);
                Vector2 adjustedScale = Vector2.One;

                Graphics.sprites.Draw(
                     ResourceLoader.spriteSheets[aManagers[i].GetCurrent().spriteSheet].texture,
                adjustedPos,
                     aManagers[i].GetCurrent().GetCurrentFrame(),
                     Color.White,
                     0f,
                     adjustedOrigin,
                     adjustedScale,
                     SpriteEffects.FlipVertically, 0f);
            }
        }

        public void DrawCanvas()
        {
                Rectangle srcRect = aManagers[0].GetCurrent().GetCurrentFrame();

                Vector2 adjustedPos = positions[0];
                Vector2 adjustedOrigin = (srcRect.Size.ToVector2() / 2);
                Vector2 adjustedScale = Vector2.One;

                Graphics.sprites.Draw(
                     ResourceLoader.spriteSheets[aManagers[0].GetCurrent().spriteSheet].texture,
                adjustedPos,
                     aManagers[0].GetCurrent().GetCurrentFrame(),
                     Color.White,
                     0f,
                     adjustedOrigin,
                     adjustedScale,
                     SpriteEffects.FlipVertically, 0f);
        }
    }
}
