using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Utils;

namespace Resources
{
    public static class StaticSpriteFactory
    {
        

        public struct SpriteData
        {
            public SpriteSheets sheet;
            public Rectangle srcRect;
            public float z;
            public float currentZ;
            public SpriteEffects effect;
            public SpriteData(SpriteSheets sheet, Rectangle srcRect, float z)
            {
                this.sheet = sheet;
                this.srcRect = srcRect;
                this.currentZ = z;
                this.z = currentZ;
                this.effect = SpriteEffects.None;
            }

            public SpriteData(SpriteSheets sheet, Rectangle srcRect, float z, SpriteEffects neweffect)
            {
                this.sheet = sheet;
                this.srcRect = srcRect;
                this.currentZ = z;
                this.z = currentZ;
                this.effect = neweffect;
            }
        }

        public static readonly Dictionary<StaticSprites, SpriteData> spriteMappings = new()
        {
            { StaticSprites.GRAPHICS_PARALLAX_0_0, new SpriteData(SpriteSheets.GRAPHICS_PARALLAX_0, new Rectangle(0,0,1280,720), -100) },
            { StaticSprites.GRAPHICS_PARALLAX_0_1, new SpriteData(SpriteSheets.GRAPHICS_PARALLAX_0, new Rectangle(0,720,1280,720), -99) },
            { StaticSprites.GRAPHICS_PARALLAX_0_2, new SpriteData(SpriteSheets.GRAPHICS_PARALLAX_0, new Rectangle(0,720*2,1920,720), -98) },
            { StaticSprites.GRAPHICS_PARALLAX_0_3, new SpriteData(SpriteSheets.GRAPHICS_PARALLAX_0, new Rectangle(0,720*3,2344,720), -97) },
            { StaticSprites.GRAPHICS_PARALLAX_0_N, new SpriteData(SpriteSheets.GRAPHICS_PARALLAX_0, new Rectangle(0,720*4,2344,720), 1000) },

            { StaticSprites.GRAPHICS_CLOUD_0, new SpriteData(SpriteSheets.GRAPHICS_CLOUDS, new Rectangle(0,0,360,128), 2) },
            { StaticSprites.GRAPHICS_SUN, new SpriteData(SpriteSheets.GRAPHICS_SUN, new Rectangle(0,0,64,64), 0) },
            { StaticSprites.GRAPHICS_STATIC_DRAGON, new SpriteData(SpriteSheets.GRAPHICS_STATIC, new Rectangle(0,0,128,64), 1) },

            { StaticSprites.ENTITIES_STATIC_CIRCLE, new SpriteData(SpriteSheets.ENTITIES_STATIC, new Rectangle(64, 0, 32, 32), 98)},
            { StaticSprites.ENTITIES_STATIC_BALL, new SpriteData(SpriteSheets.ENTITIES_STATIC, new Rectangle(0, 0, 64, 64), 98)},
            { StaticSprites.ENTITIES_STATIC_CRATE_0, new SpriteData(SpriteSheets.ENTITIES_STATIC, new Rectangle(0, 64, 64, 64), 98)},
            { StaticSprites.ENTITIES_STATIC_CRATE_1, new SpriteData(SpriteSheets.ENTITIES_STATIC, new Rectangle(64, 64, 16, 16), 98)},

            { StaticSprites.ENTITIES_PLAYER, new SpriteData(SpriteSheets.ENTITIES_PLAYER, new Rectangle(0,0,48,96), 100)},
            { StaticSprites.ENTITIES_MOB0, new SpriteData(SpriteSheets.ENTITIES_MOB0, new Rectangle(0,0,48,96), 99)},





            { StaticSprites.UI_GAME_ICON, new SpriteData(SpriteSheets.UI_GAME_ICON, new Rectangle(0,0,64,64), 0) },
            { StaticSprites.UI_CURSOR, new SpriteData(SpriteSheets.UI_CURSOR, new Rectangle(0,0,32,32), 0) },

            { StaticSprites.ENTITIES_WEAPONS_SWORD0, new SpriteData(SpriteSheets.ENTITIES_WEAPONS, new Rectangle(0,0,32,64), 200) },
            { StaticSprites.ENTITIES_WEAPONS_SWORD1, new SpriteData(SpriteSheets.ENTITIES_WEAPONS, new Rectangle(32,0,32,64), 200) }
        };



        public static SpriteData[] PlatformCut(Vector2 pos, int tileSize = 32)
        {
            SpriteData[] data = new SpriteData[13];

            // Top row (Y = pos.Y)
            data[0] = new SpriteData(SpriteSheets.ENTITIES_PLATFORMS, new Rectangle((int)pos.X, (int)pos.Y, tileSize, tileSize), 0);
            data[1] = new SpriteData(SpriteSheets.ENTITIES_PLATFORMS, new Rectangle((int)pos.X + tileSize, (int)pos.Y, tileSize, tileSize), 0);
            data[2] = new SpriteData(SpriteSheets.ENTITIES_PLATFORMS, new Rectangle((int)pos.X + 2 * tileSize, (int)pos.Y, tileSize, tileSize), 0);
            data[3] = new SpriteData(SpriteSheets.ENTITIES_PLATFORMS, new Rectangle((int)pos.X + 3 * tileSize, (int)pos.Y, tileSize, tileSize), 0);

            // Middle row (Y = pos.Y + tileSize)
            data[4] = new SpriteData(SpriteSheets.ENTITIES_PLATFORMS, new Rectangle((int)pos.X, (int)pos.Y + tileSize, tileSize, tileSize), 0);
            data[5] = new SpriteData(SpriteSheets.ENTITIES_PLATFORMS, new Rectangle((int)pos.X + tileSize, (int)pos.Y + tileSize, tileSize, tileSize), 0);
            data[6] = new SpriteData(SpriteSheets.ENTITIES_PLATFORMS, new Rectangle((int)pos.X + 2 * tileSize, (int)pos.Y + tileSize, tileSize, tileSize), 0);
            data[7] = new SpriteData(SpriteSheets.ENTITIES_PLATFORMS, new Rectangle((int)pos.X + 3 * tileSize, (int)pos.Y + tileSize, tileSize, tileSize), 0);

            // Bottom row (Y = pos.Y + 2 * tileSize)
            data[8] = new SpriteData(SpriteSheets.ENTITIES_PLATFORMS, new Rectangle((int)pos.X, (int)pos.Y + 2 * tileSize, tileSize, tileSize), 0);
            data[9] = new SpriteData(SpriteSheets.ENTITIES_PLATFORMS, new Rectangle((int)pos.X + tileSize, (int)pos.Y + 2 * tileSize, tileSize, tileSize), 0);
            data[10] = new SpriteData(SpriteSheets.ENTITIES_PLATFORMS, new Rectangle((int)pos.X + 2 * tileSize, (int)pos.Y + 2 * tileSize, tileSize, tileSize), 0);
            data[11] = new SpriteData(SpriteSheets.ENTITIES_PLATFORMS, new Rectangle((int)pos.X + 3 * tileSize, (int)pos.Y + 2 * tileSize, tileSize, tileSize), 0);

            // Extra sprites (adjusted carefully)
            data[12] = new SpriteData(SpriteSheets.ENTITIES_PLATFORMS, new Rectangle((int)pos.X + tileSize / 2, (int)pos.Y + tileSize + tileSize / 3, tileSize, tileSize), 0);

            return data;
        }

        public static SpriteData[] UIFrameCut(Vector2 pos, int tileSize)
        {
            SpriteData[] data = new SpriteData[9];

            data[0] = new SpriteData(SpriteSheets.UI_FRAMES, new Rectangle((int)pos.X, (int)pos.Y, tileSize, tileSize), 0, SpriteEffects.FlipVertically); //left top corner
            data[1] = new SpriteData(SpriteSheets.UI_FRAMES, new Rectangle((int)pos.X, (int)pos.Y, tileSize, tileSize), 0, SpriteEffects.FlipVertically | SpriteEffects.FlipHorizontally); //right top corner
            data[2] = new SpriteData(SpriteSheets.UI_FRAMES, new Rectangle((int)pos.X, (int)pos.Y, tileSize, tileSize), 0); //left bottom corner
            data[3] = new SpriteData(SpriteSheets.UI_FRAMES, new Rectangle((int)pos.X, (int)pos.Y, tileSize, tileSize), 0, SpriteEffects.FlipHorizontally); //right bottom corner

            data[4] = new SpriteData(SpriteSheets.UI_FRAMES, new Rectangle((int)pos.X + tileSize, (int)pos.Y, tileSize, tileSize), 0, SpriteEffects.FlipVertically); //top border
            data[5] = new SpriteData(SpriteSheets.UI_FRAMES, new Rectangle((int)pos.X + tileSize, (int)pos.Y, tileSize, tileSize), 0); //bottom border

            data[6] = new SpriteData(SpriteSheets.UI_FRAMES, new Rectangle((int)pos.X, (int)pos.Y + tileSize, tileSize, tileSize), 0); //left border
            data[7] = new SpriteData(SpriteSheets.UI_FRAMES, new Rectangle((int)pos.X, (int)pos.Y + tileSize, tileSize, tileSize), 0, SpriteEffects.FlipHorizontally); //right border

            data[8] = new SpriteData(SpriteSheets.UI_FRAMES, new Rectangle((int)pos.X + tileSize, (int)pos.Y + tileSize, tileSize, tileSize), 0); //right border

            return data;
        }

        public static SpriteData[] UIHUDStatBarCut(Vector2 pos, int tileSize)
        {
            SpriteData[] data = new SpriteData[4];

            data[0] = new SpriteData(SpriteSheets.UI_HUD, new Rectangle((int)pos.X+tileSize*0, (int)pos.Y, tileSize, tileSize), 0);
            data[1] = new SpriteData(SpriteSheets.UI_HUD, new Rectangle((int)pos.X + tileSize * 1, (int)pos.Y, tileSize, tileSize), 0);
            data[2] = new SpriteData(SpriteSheets.UI_HUD, new Rectangle((int)pos.X + tileSize * 2, (int)pos.Y, tileSize, tileSize), 0);
            data[3] = new SpriteData(SpriteSheets.UI_HUD, new Rectangle((int)pos.X + tileSize * 3, (int)pos.Y, tileSize, tileSize), 0);

            return data;
        }


        public static Dictionary<Items, Point> itemUISpriteMappings = new()
        {
            { Items.SWORD,         new Point(0,0) },
            { Items.CHESTPLATE,    new Point(0,1) },
            { Items.HELMET,        new Point(0,2) },
            { Items.BOOTS,         new Point(0,3) },
            { Items.GLOVES,        new Point(0,4) },
            { Items.NECKLACE,      new Point(0,5) },
            { Items.BELT,          new Point(0,6) },
            { Items.RING,          new Point(0,7) },
            { Items.CALL_DOG,      new Point(0,8) },
            { Items.CALL_FIREFLY,  new Point(0,9) },
            { Items.BACKPACK,      new Point(0,10) },
            { Items.HEALTH_POTION, new Point(0,11) },
            { Items.SWORD_HILT,    new Point(0,12) },
            { Items.GOLDEN_KEY,    new Point(0,13) },
            { Items.NOTE,          new Point(0,14) },
            { Items.COIN,          new Point(0,15) },
        };

        public static SpriteData GetItemUISprite(Items item)
        {
            SpriteData data = new SpriteData();

            Point iconSize = new Point(64, 64);
            Point spriteSheetLocation = itemUISpriteMappings[item];

            data = new SpriteData(SpriteSheets.UI_ITEMS, new Rectangle(spriteSheetLocation*iconSize, iconSize), 0);

            return data;
        }

    }
}
