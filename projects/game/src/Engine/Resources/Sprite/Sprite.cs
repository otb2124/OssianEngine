using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using static Resources.SpriteSheet;

namespace Resources
{
    public class Sprite
    {

        public SpriteSheets spriteSheetId;
        public Rectangle srcRect;
        public int zIndex = 0;

        public Sprite(SpriteSheets spriteSheet, Rectangle srcRect)
        {
            this.spriteSheetId = spriteSheet;
            this.srcRect = srcRect;
        }

        public Sprite(SpriteSheets spriteSheet)
        {
            this.spriteSheetId = spriteSheet;
            this.srcRect = ResourceLoader.spriteSheets[spriteSheetId].texture.Bounds;
        }

        public void Draw(Vector2 pos, Color color, float rot, Vector2 origin, Vector2 scale, SpriteEffects effect, float layerDepth)
        {
            Graphics.Graphics.sprites.Draw(
                ResourceLoader.spriteSheets[spriteSheetId].texture,
                pos,
                srcRect,
                color,
                rot,
                origin,
                scale,
                effect,
                layerDepth
             );
        }


        public void Draw(Vector2 pos)
        {
            Draw(
                pos,
                Color.White,
                0f,
                new Vector2(ResourceLoader.spriteSheets[spriteSheetId].texture.Width/2, ResourceLoader.spriteSheets[spriteSheetId].texture.Height/2),
                Vector2.One,
                SpriteEffects.FlipVertically,
                0f
            );
        }
    }
}
