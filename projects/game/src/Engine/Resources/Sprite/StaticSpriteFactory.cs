using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using Utils;
using static Resources.SpriteSheet;

namespace Resources
{
    public static class StaticSpriteFactory
    {
        

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
            { StaticSprites.GRAPHICS_PARALLAX_0_0, new SpriteData(SpriteSheets.GRAPHICS_PARALLAX_0, new Rectangle(0,0,1280,720), -100) },
            { StaticSprites.GRAPHICS_PARALLAX_0_1, new SpriteData(SpriteSheets.GRAPHICS_PARALLAX_0, new Rectangle(0,720,1280,720), -99) },
            { StaticSprites.GRAPHICS_PARALLAX_0_2, new SpriteData(SpriteSheets.GRAPHICS_PARALLAX_0, new Rectangle(0,720*2,1920,720), -98) },
            { StaticSprites.GRAPHICS_PARALLAX_0_3, new SpriteData(SpriteSheets.GRAPHICS_PARALLAX_0, new Rectangle(0,720*3,2344,720), -97) },

            { StaticSprites.GRAPHICS_CLOUD_0, new SpriteData(SpriteSheets.GRAPHICS_CLOUDS, new Rectangle(0,0,360,128), 2) },
            { StaticSprites.GRAPHICS_SUN, new SpriteData(SpriteSheets.GRAPHICS_SUN, new Rectangle(0,0,64,64), 0) },
            { StaticSprites.GRAPHICS_STATIC_DRAGON, new SpriteData(SpriteSheets.GRAPHICS_STATIC, new Rectangle(0,0,128,64), 1) },


            { StaticSprites.ENTITIES_STATIC_PLATFORM, new SpriteData(SpriteSheets.ENTITIES_STATIC, new Rectangle(0, 128, 128, 16), 0)},
            { StaticSprites.ENTITIES_STATIC_BALL, new SpriteData(SpriteSheets.ENTITIES_STATIC, new Rectangle(0, 0, 64, 64), 0)},
            { StaticSprites.ENTITIES_STATIC_CRATE_0, new SpriteData(SpriteSheets.ENTITIES_STATIC, new Rectangle(0, 64, 64, 64), 0)},
            { StaticSprites.ENTITIES_STATIC_CRATE_1, new SpriteData(SpriteSheets.ENTITIES_STATIC, new Rectangle(64, 64, 16, 16), 0)},

            { StaticSprites.ENTITIES_PLAYER, new SpriteData(SpriteSheets.ENTITIES_PLAYER, new Rectangle(0,0,48,96), 100)},
            { StaticSprites.ENTITIES_MOB0, new SpriteData(SpriteSheets.ENTITIES_MOB0, new Rectangle(0,0,48,96), 99)},



            { StaticSprites.UI_GAME_ICON, new SpriteData(SpriteSheets.UI_GAME_ICON, new Rectangle(0,0,64,64), 0) },
            { StaticSprites.UI_CURSOR, new SpriteData(SpriteSheets.UI_CURSOR, new Rectangle(0,0,32,32), 0) },

            { StaticSprites.ENTITIES_WEAPONS_SWORD0, new SpriteData(SpriteSheets.ENTITIES_WEAPONS, new Rectangle(0,0,32,64), 200) },
            { StaticSprites.ENTITIES_WEAPONS_SWORD1, new SpriteData(SpriteSheets.ENTITIES_WEAPONS, new Rectangle(32,0,32,64), 200) }
        };
    }
}
