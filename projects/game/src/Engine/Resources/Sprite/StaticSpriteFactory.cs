using Microsoft.Xna.Framework;
using System;
using static Resources.SpriteSheet;

namespace Resources
{
    public static class StaticSpriteFactory
    {

        public enum StaticSprites
        {
            PLATFORM,
            CRATE,
            CIRCLE,
            HERO,
            CURSOR,
            BACKGROUND,
            MOB,
            DRAGON,
        }

        public static Sprite GetSprite(StaticSprites sprite)
        {
            switch (sprite)
            {
                case StaticSprites.PLATFORM:
                    return new Sprite(SpriteSheets.DECOR, new Rectangle(0,128,100,10));
                case StaticSprites.CIRCLE:
                    return new Sprite(SpriteSheets.DECOR, new Rectangle(0,0,64,64));
                case StaticSprites.CRATE:
                    return new Sprite(SpriteSheets.DECOR, new Rectangle(0,64,64,64));


                case StaticSprites.HERO:
                    return new Sprite(SpriteSheets.HERO);
                case StaticSprites.MOB:
                    return new Sprite(SpriteSheets.MOB);
                case StaticSprites.BACKGROUND:
                    return new Sprite(SpriteSheets.BACKGROUND);
                case StaticSprites.DRAGON:
                    return new Sprite(SpriteSheets.DRAGON);
                case StaticSprites.CURSOR:
                    return new Sprite(SpriteSheets.CURSOR);
                default:
                    throw new ArgumentOutOfRangeException(nameof(sprite), sprite, null);
            }
        
        }
    }
    
}
