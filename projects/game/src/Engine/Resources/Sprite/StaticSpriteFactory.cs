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
            public SpriteSheets SpriteSheet;
            public Rectangle SrcRect;
            public float Z;
            public float CurrentZ;
            public SpriteEffects Effect;
            public SpriteData(SpriteSheets sheet, Rectangle srcRect, float z = 0)
            {
                SpriteSheet = sheet;
                SrcRect = srcRect;
                CurrentZ = z;
                Z = CurrentZ;
                Effect = SpriteEffects.None;
            }

            public SpriteData(SpriteSheets sheet, Rectangle srcRect, float z, SpriteEffects neweffect)
            {
                SpriteSheet = sheet;
                SrcRect = srcRect;
                CurrentZ = z;
                Z = CurrentZ;
                Effect = neweffect;
            }
        }

        public static readonly Dictionary<StaticSprites, SpriteData> spriteMappings = new()
        {
            { StaticSprites.NONE, new SpriteData(SpriteSheets.NONE, new Rectangle(0,0,0,0), 0) },

            { StaticSprites.GRAPHICS_CLOUD_0, new SpriteData(SpriteSheets.GRAPHICS_CLOUDS, new Rectangle(0,0,360,128), 2) },
            { StaticSprites.GRAPHICS_SUN, new SpriteData(SpriteSheets.GRAPHICS_SUN, new Rectangle(0,0,64,64), 0) },

            { StaticSprites.GRAPHICS_MOON, new SpriteData(SpriteSheets.GRAPHICS_MOON, new Rectangle(0,0,64,64), 0) },
            { StaticSprites.GRAPHICS_STATIC_DRAGON, new SpriteData(SpriteSheets.GRAPHICS_STATIC, new Rectangle(0,0,128,64), 1) },

            { StaticSprites.ENTITIES_STATIC_BALL, new SpriteData(SpriteSheets.ENTITIES_STATIC, new Rectangle(0, 0, 64, 64), 98)},
            { StaticSprites.ENTITIES_STATIC_CRATE_0, new SpriteData(SpriteSheets.ENTITIES_STATIC, new Rectangle(0, 64, 64, 64), 98)},
            { StaticSprites.ENTITIES_STATIC_CRATE_1, new SpriteData(SpriteSheets.ENTITIES_STATIC, new Rectangle(64, 64, 16, 16), 98)},

            { StaticSprites.ENTITIES_LEDGE, new SpriteData(SpriteSheets.ENTITIES_LEDGES, new Rectangle(0, 0, 32, 32), 98)},

            { StaticSprites.ENTITIES_PLAYER, new SpriteData(SpriteSheets.ENTITIES_PLAYER, new Rectangle(0,0,64,128), 100)},
            { StaticSprites.ENTITIES_BANDIT, new SpriteData(SpriteSheets.ENTITIES_BANDIT, new Rectangle(0,0,48,96), 99)},
            { StaticSprites.ENTITIES_SLIME, new SpriteData(SpriteSheets.ENTITIES_SLIME, new Rectangle(0,0,64,64), 99)},
            { StaticSprites.ENTITIES_BAT, new SpriteData(SpriteSheets.ENTITIES_BAT, new Rectangle(0,64,64,64), 99)},

            { StaticSprites.ENTITIES_FIREBALL, new SpriteData(SpriteSheets.ENITIES_FIREBALL, new Rectangle(0,0,64,64), 99)},
            { StaticSprites.ENTITIES_ARROW, new SpriteData(SpriteSheets.ENITIES_ARROW, new Rectangle(0,0,64,64), 99)},

            { StaticSprites.LIGHT_DARKNESS_FULL, new SpriteData(SpriteSheets.LIGHT_DARKNESS_FULL, new Rectangle(0,0,80,64), 200) },
            { StaticSprites.LIGHT_DARKNESS_VIGNETTE, new SpriteData(SpriteSheets.LIGHT_DARKNESS_MIN, new Rectangle(0,0,320,180), 200) },

            { StaticSprites.UI_GAME_ICON, new SpriteData(SpriteSheets.UI_GAME_ICON, new Rectangle(0,0,64,64), 0) },
            { StaticSprites.UI_CURSOR, new SpriteData(SpriteSheets.UI_CURSOR, new Rectangle(0,0,32,32), 0) },

            { StaticSprites.ENTITIES_WEAPONS_TERRABLADE, new SpriteData(SpriteSheets.ENTITIES_WEAPONS_TERRABLADE, new Rectangle(0,0,32,64), 200) },
            { StaticSprites.ENTITIES_WEAPONS_TORCH, new SpriteData(SpriteSheets.ENTITIES_WEAPONS_TORCH, new Rectangle(0,0,32,64), 200) }
        };



        public static SpriteData[] TileSetCut(TileEntity.TileSets tileSet)
        {
            SpriteData[] data = new SpriteData[16];

            Vector2 pos = Vector2.Zero;
            int tileSize = 32;

            switch (tileSet)
            {
                case TileEntity.TileSets.SET0:
                    pos = Vector2.Zero;
                    break;
                case TileEntity.TileSets.SET1:
                    pos = new Vector2(0, tileSize*2*1);
                    break;
            }



            
            //outer corner left top
            data[0] = new SpriteData(SpriteSheets.ENTITIES_TILES, new Rectangle((int)pos.X + 0 * tileSize, (int)pos.Y, tileSize, tileSize), 0);
            //outer corner right top
            data[1] = new SpriteData(SpriteSheets.ENTITIES_TILES, new Rectangle((int)pos.X + 0 * tileSize, (int)pos.Y, tileSize, tileSize), 0, SpriteEffects.FlipHorizontally);
            //outer corner left bottom
            data[2] = new SpriteData(SpriteSheets.ENTITIES_TILES, new Rectangle((int)pos.X + 0 * tileSize, (int)pos.Y, tileSize, tileSize), 0, SpriteEffects.FlipVertically);
            //outer corner right bottom
            data[3] = new SpriteData(SpriteSheets.ENTITIES_TILES, new Rectangle((int)pos.X + 0 * tileSize, (int)pos.Y, tileSize, tileSize), 0, SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically);

            //top border
            data[4] = new SpriteData(SpriteSheets.ENTITIES_TILES, new Rectangle((int)pos.X + 1 * tileSize, (int)pos.Y, tileSize, tileSize), 0);
            //bottom border
            data[5] = new SpriteData(SpriteSheets.ENTITIES_TILES, new Rectangle((int)pos.X + 1 * tileSize, (int)pos.Y, tileSize, tileSize), 0, SpriteEffects.FlipVertically);


            //inner corner right top
            data[6] = new SpriteData(SpriteSheets.ENTITIES_TILES, new Rectangle((int)pos.X + 2 * tileSize, (int)pos.Y, tileSize, tileSize), 0);
            //inner corner left top
            data[7] = new SpriteData(SpriteSheets.ENTITIES_TILES, new Rectangle((int)pos.X + 2 * tileSize, (int)pos.Y, tileSize, tileSize), 0, SpriteEffects.FlipHorizontally);
            //inner corner right bottom
            data[8] = new SpriteData(SpriteSheets.ENTITIES_TILES, new Rectangle((int)pos.X + 2 * tileSize, (int)pos.Y, tileSize, tileSize), 0, SpriteEffects.FlipVertically);
            //inner corner left bottom
            data[9] = new SpriteData(SpriteSheets.ENTITIES_TILES, new Rectangle((int)pos.X + 2 * tileSize, (int)pos.Y, tileSize, tileSize), 0, SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically);

            //pillar top
            data[10] = new SpriteData(SpriteSheets.ENTITIES_TILES, new Rectangle((int)pos.X + 3 * tileSize, (int)pos.Y, tileSize, tileSize), 0);

            //left border
            data[11] = new SpriteData(SpriteSheets.ENTITIES_TILES, new Rectangle((int)pos.X + 0 * tileSize, (int)pos.Y + tileSize, tileSize, tileSize), 0);
            //right border
            data[12] = new SpriteData(SpriteSheets.ENTITIES_TILES, new Rectangle((int)pos.X + 0 * tileSize, (int)pos.Y + tileSize, tileSize, tileSize), 0, SpriteEffects.FlipHorizontally);

            //inner
            data[13] = new SpriteData(SpriteSheets.ENTITIES_TILES, new Rectangle((int)pos.X + 1 * tileSize, (int)pos.Y + tileSize, tileSize, tileSize), 0);
            //inner alt
            data[14] = new SpriteData(SpriteSheets.ENTITIES_TILES, new Rectangle((int)pos.X + 2 * tileSize, (int)pos.Y + tileSize, tileSize, tileSize), 0);
            //pillar bottom
            data[15] = new SpriteData(SpriteSheets.ENTITIES_TILES, new Rectangle((int)pos.X + 3 * tileSize, (int)pos.Y + tileSize, tileSize, tileSize), 0);


            return data;
        }

        public static SpriteData[] PlatformSetCut(Vector2 pos, int tileSize = 32)
        {
            SpriteData[] data = new SpriteData[3];
            
            //left
            data[0] = new SpriteData(SpriteSheets.ENTITIES_PLATFORMS, new Rectangle((int)pos.X + 0 * tileSize, (int)pos.Y, tileSize, tileSize), 0);

            //center
            data[1] = new SpriteData(SpriteSheets.ENTITIES_PLATFORMS, new Rectangle((int)pos.X + 1 * tileSize, (int)pos.Y, tileSize, tileSize), 0);

            //right
            data[2] = new SpriteData(SpriteSheets.ENTITIES_PLATFORMS, new Rectangle((int)pos.X + 0 * tileSize, (int)pos.Y, tileSize, tileSize), 0, SpriteEffects.FlipHorizontally);

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

        public static SpriteData GetEntityParticle(Vector2 pos)
        {
            return new SpriteData(SpriteSheets.ENTITIES_PARTICLES, new Rectangle((int)pos.X*16, (int)pos.Y*16, 16, 16), 0);
        }


        public static Dictionary<ItemKey, Point> itemUISpriteMappings = new()
        {
            { new ItemKey(ItemLib.Weapons.TERRABLADE), new Point(0,0) },
            { new ItemKey(ItemLib.Weapons.TORCH), new Point(1,0) },
            { new ItemKey(ItemLib.Weapons.BOW), new Point(2,0) },

            //TODO: change to utils/none
            { new ItemKey(ItemLib.Weapons.BARE_HAND), new Point(-1,-1) },

            { new ItemKey(ItemLib.Chestplates.IRON_CHESTPLATE), new Point(0,1) },
            { new ItemKey(ItemLib.Helmets.IRON_HELMET), new Point(0,2) },
            { new ItemKey(ItemLib.Boots.IRON_BOOTS), new Point(0,3) },
            { new ItemKey(ItemLib.Gloves.IRON_GLOVES), new Point(0,4) },
            { new ItemKey(ItemLib.Necklaces.IRON_NECKLACE), new Point(0,5) },
            { new ItemKey(ItemLib.Capes.LEATHER_CAPE), new Point(0,6) },
            { new ItemKey(ItemLib.Belts.IRON_BELT), new Point(0,7) },
            { new ItemKey(ItemLib.Rings.IRON_RING), new Point(0,8) },
            { new ItemKey(ItemLib.Pets.CALL_DOG), new Point(0,9) },
            { new ItemKey(ItemLib.LightPets.CALL_FIREFLY), new Point(0,10) },
            { new ItemKey(ItemLib.Containments.BACKPACK), new Point(0,11) },
            { new ItemKey(ItemLib.Consumables.HEALTH_POTION), new Point(0,12) },
            { new ItemKey(ItemLib.Materials.SWORD_HILT), new Point(0,13) },
            { new ItemKey(ItemLib.Keys.GOLDEN_KEY), new Point(0,14) },

            { new ItemKey(ItemLib.QuestItems.NOTE), new Point(0,15) },
            { new ItemKey(ItemLib.QuestItems.STONE), new Point(1,15) },
            { new ItemKey(ItemLib.QuestItems.HEAD), new Point(2,15) },

            { new ItemKey(ItemLib.Currencies.GOLD_COIN), new Point(0,16) },
        };

        public static SpriteData GetItemUISprite(Item item)
        {
            if(item == null)
            {
                return StaticSpriteFactory.spriteMappings[StaticSprites.NONE];
            }

            return GetItemUISpriteByItemKey(item.ItemKey);
        }

        public static SpriteData GetItemUISpriteByItemKey(ItemKey itemKey)
        {
            SpriteData data = new SpriteData();

            Point iconSize = new Point(64, 64);
            Point spriteSheetLocation = itemUISpriteMappings[itemKey];

            data = new SpriteData(SpriteSheets.UI_ITEMS, new Rectangle(spriteSheetLocation * iconSize, iconSize), 100);

            return data;
        }


        public static readonly Dictionary<Graphics.ParallaxBackground.ParallaxBackgrounds, SpriteData> backgroundCanvasLayerSprites = new()
        {
            { 
                Graphics.ParallaxBackground.ParallaxBackgrounds.SEASIDE_EVENING, new SpriteData(SpriteSheets.GRAPHICS_BG0_CANVAS, new Rectangle(0, 0, 1280, 720), -100)
            },
            {
                Graphics.ParallaxBackground.ParallaxBackgrounds.GREEN, new SpriteData(SpriteSheets.GRAPHICS_BG1_CANVAS, new Rectangle(0, 0, 1280, 720), -100)
            },
        };

        public static readonly Dictionary<Graphics.ParallaxBackground.ParallaxBackgrounds, SpriteData[]> backgroundBackLayerSprites = new()
        {
            { 
                Graphics.ParallaxBackground.ParallaxBackgrounds.SEASIDE_EVENING,
                new SpriteData[]
                {
                    new SpriteData(SpriteSheets.GRAPHICS_BG0_B0, new Rectangle(0, 0, 1600, 720), -99),
                    new SpriteData(SpriteSheets.GRAPHICS_BG0_B1, new Rectangle(0, 0, 1920, 720), -98),
                    new SpriteData(SpriteSheets.GRAPHICS_BG0_B2, new Rectangle(0, 0, 2240, 720), -97)
                }
            },
            {
                Graphics.ParallaxBackground.ParallaxBackgrounds.GREEN,
                new SpriteData[]
                {
                    new SpriteData(SpriteSheets.GRAPHICS_BG1_B0, new Rectangle(0, 0, 1462, 720), -99),
                    new SpriteData(SpriteSheets.GRAPHICS_BG1_B1, new Rectangle(0, 0, 1645, 720), -98),
                    new SpriteData(SpriteSheets.GRAPHICS_BG1_B2, new Rectangle(0, 0, 1828, 720), -97),
                    new SpriteData(SpriteSheets.GRAPHICS_BG1_B3, new Rectangle(0, 0, 2011, 720), -97),
                    new SpriteData(SpriteSheets.GRAPHICS_BG1_B4, new Rectangle(0, 0, 2195, 720), -97),
                    new SpriteData(SpriteSheets.GRAPHICS_BG1_B5, new Rectangle(0, 0, 2377, 720), -97)
                }
            }
        };

        public static readonly Dictionary<Graphics.ParallaxBackground.ParallaxBackgrounds, SpriteData[]> backgroundFrontLayerSprites = new()
        {
            { 
                Graphics.ParallaxBackground.ParallaxBackgrounds.SEASIDE_EVENING,
                new SpriteData[]
                {
                    new SpriteData(SpriteSheets.GRAPHICS_BG0_F0, new Rectangle(0, 0, 2560, 720), 1000)
                }
            },
            {
                Graphics.ParallaxBackground.ParallaxBackgrounds.GREEN,
                new SpriteData[]
                {
                    new SpriteData(SpriteSheets.GRAPHICS_BG1_F0, new Rectangle(0, 0, 2560, 720), 1000)
                }
            }
        };

        

    }
}
