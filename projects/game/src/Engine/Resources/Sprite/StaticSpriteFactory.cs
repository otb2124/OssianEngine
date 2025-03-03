using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
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
            SWORD,
            SWORD_0
        }

        public struct SpriteData
        {
            public SpriteSheets sheet;
            public Rectangle srcRect;
            public float z;
            public SpriteData(SpriteSheets sheet, Rectangle srcRect, float z)
            {
                this.sheet = sheet;
                this.srcRect = srcRect;
                this.z = z;
            }
        }

        public static readonly Dictionary<StaticSprites, SpriteData> spriteMappings = new()
        {
            { StaticSprites.PLATFORM, new SpriteData(SpriteSheets.DECOR, new Rectangle(0, 128, 100, 10), 0)},
            { StaticSprites.CIRCLE, new SpriteData(SpriteSheets.DECOR, new Rectangle(0, 0, 64, 64), 0)},
            { StaticSprites.CRATE, new SpriteData(SpriteSheets.DECOR, new Rectangle(0, 64, 64, 64), 0)},

            { StaticSprites.HERO, new SpriteData(SpriteSheets.HERO, new Rectangle(0,0,48,96), 100)},
            { StaticSprites.MOB, new SpriteData(SpriteSheets.MOB, new Rectangle(0,0,48,96), 99)},

            { StaticSprites.BACKGROUND, new SpriteData(SpriteSheets.BACKGROUND, new Rectangle(0,0,1280,720), 0) },
            { StaticSprites.DRAGON, new SpriteData(SpriteSheets.DRAGON, new Rectangle(0,0,128,64), 10) },

            { StaticSprites.CURSOR, new SpriteData(SpriteSheets.CURSOR, new Rectangle(0,0,64,64), 0) },

            { StaticSprites.SWORD, new SpriteData(SpriteSheets.WEAPONS, new Rectangle(0,0,32,64), 200) },
            { StaticSprites.SWORD_0, new SpriteData(SpriteSheets.WEAPONS, new Rectangle(32,0,32,64), 200) }
        };
    }
}
