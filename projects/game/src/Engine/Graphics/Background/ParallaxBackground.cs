using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Resources;
using UI;
using Utils;

namespace Graphics
{
    public class ParallaxBackground
    {

        public Vector2 pos;
        public Vector2 origin;

        public AnimationManager[] aManagers;
        public StaticSprites[] layers;

        public ParallaxBackground()
        {
            this.origin = Vector2.Zero;

            SetLayers(new StaticSprites[] { 
                StaticSprites.GRAPHICS_PARALLAX_0_0, 
                StaticSprites.GRAPHICS_PARALLAX_0_1 
            });
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

        }

        public void Draw()
        {
            for (int i = 1; i < aManagers.Length; i++)
            {
                Rectangle srcRect = aManagers[i].GetCurrent().GetCurrentFrame();

                Vector2 adjustedPos = pos;
                Vector2 adjustedOrigin = (srcRect.Size.ToVector2() / 2);
                Vector2 adjustedScale = Vector2.One;

                adjustedPos += Graphics.camera.position;
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

                Vector2 adjustedPos = pos;
                Vector2 adjustedOrigin = (srcRect.Size.ToVector2() / 2);
                Vector2 adjustedScale = Vector2.One;

                adjustedPos += Graphics.camera.position;
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
