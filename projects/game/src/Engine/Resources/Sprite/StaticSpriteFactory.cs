using Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using Utils;

namespace Resources
{

    public enum StaticSprites
    {
        NONE,
        //--------
        //GRAPHICS
        //parallax
        GRAPHICS_PARALLAX_0_0,
        GRAPHICS_PARALLAX_0_1,
        GRAPHICS_PARALLAX_0_2,
        GRAPHICS_PARALLAX_0_3,
        GRAPHICS_PARALLAX_0_N,

        //static
        GRAPHICS_STATIC_DRAGON,

        //sun
        GRAPHICS_SUN,
        GRAPHICS_MOON,

        //rain
        GRAPHICS_CLOUD_0,

        //--------
        //ENTITIES
        //livingentities
        ENTITIES_HUMAN_M,
        ENTITIES_HUMAN_M_ARMOR_CHESTPLATE_0,
        ENTITIES_HUMAN_M_ARMOR_HELMET_0,
        ENTITIES_HUMAN_M_ARMOR_BOOTS_0,
        ENTITIES_HUMAN_M_ARMOR_GLOVES_0,

        ENTITIES_SLIME, ENTITIES_BAT,

        ENTITIES_FIREBALL, ENTITIES_ARROW,

        //physicalentities
        ENTITIES_STATIC_BALL,

        //ledges
        ENTITIES_LEDGE,

        //crates
        ENTITIES_STATIC_CRATE_0,
        ENTITIES_STATIC_CRATE_1,

        //equipment
        ENTITIES_WEAPONS_TERRABLADE,
        ENTITIES_WEAPONS_TORCH,

        //light
        LIGHT_DARKNESS_FULL,
        LIGHT_DARKNESS_VIGNETTE,

        //--
        //UI
        //MISC
        UI_CURSOR,
        UI_GAME_ICON
    }


    public struct StaticSprite
    {
        public SpriteSheets SpriteSheet;
        public Rectangle SrcRect;
        public float MaxZ;
        public float CurrentZ;
        public SpriteEffects Effect;
        public StaticSprite(SpriteSheets sheet, Rectangle srcRect, float z = 0, SpriteEffects effect = SpriteEffects.None)
        {
            SpriteSheet = sheet;
            SrcRect = srcRect;
            CurrentZ = z;
            MaxZ = CurrentZ;
            Effect = effect;
        }
    }

    public static class StaticSpriteFactory
    {

        public static StaticSprite[] TileSetCut(TileEntity.TileSets tileSet)
        {
            StaticSprite[] data = new StaticSprite[16];

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
            data[0] = new StaticSprite(SpriteSheets.ENTITIES_TILES, new Rectangle((int)pos.X + 0 * tileSize, (int)pos.Y, tileSize, tileSize), 0);
            //outer corner right top
            data[1] = new StaticSprite(SpriteSheets.ENTITIES_TILES, new Rectangle((int)pos.X + 0 * tileSize, (int)pos.Y, tileSize, tileSize), 0, SpriteEffects.FlipHorizontally);
            //outer corner left bottom
            data[2] = new StaticSprite(SpriteSheets.ENTITIES_TILES, new Rectangle((int)pos.X + 0 * tileSize, (int)pos.Y, tileSize, tileSize), 0, SpriteEffects.FlipVertically);
            //outer corner right bottom
            data[3] = new StaticSprite(SpriteSheets.ENTITIES_TILES, new Rectangle((int)pos.X + 0 * tileSize, (int)pos.Y, tileSize, tileSize), 0, SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically);

            //top border
            data[4] = new StaticSprite(SpriteSheets.ENTITIES_TILES, new Rectangle((int)pos.X + 1 * tileSize, (int)pos.Y, tileSize, tileSize), 0);
            //bottom border
            data[5] = new StaticSprite(SpriteSheets.ENTITIES_TILES, new Rectangle((int)pos.X + 1 * tileSize, (int)pos.Y, tileSize, tileSize), 0, SpriteEffects.FlipVertically);


            //inner corner right top
            data[6] = new StaticSprite(SpriteSheets.ENTITIES_TILES, new Rectangle((int)pos.X + 2 * tileSize, (int)pos.Y, tileSize, tileSize), 0);
            //inner corner left top
            data[7] = new StaticSprite(SpriteSheets.ENTITIES_TILES, new Rectangle((int)pos.X + 2 * tileSize, (int)pos.Y, tileSize, tileSize), 0, SpriteEffects.FlipHorizontally);
            //inner corner right bottom
            data[8] = new StaticSprite(SpriteSheets.ENTITIES_TILES, new Rectangle((int)pos.X + 2 * tileSize, (int)pos.Y, tileSize, tileSize), 0, SpriteEffects.FlipVertically);
            //inner corner left bottom
            data[9] = new StaticSprite(SpriteSheets.ENTITIES_TILES, new Rectangle((int)pos.X + 2 * tileSize, (int)pos.Y, tileSize, tileSize), 0, SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically);

            //pillar top
            data[10] = new StaticSprite(SpriteSheets.ENTITIES_TILES, new Rectangle((int)pos.X + 3 * tileSize, (int)pos.Y, tileSize, tileSize), 0);

            //left border
            data[11] = new StaticSprite(SpriteSheets.ENTITIES_TILES, new Rectangle((int)pos.X + 0 * tileSize, (int)pos.Y + tileSize, tileSize, tileSize), 0);
            //right border
            data[12] = new StaticSprite(SpriteSheets.ENTITIES_TILES, new Rectangle((int)pos.X + 0 * tileSize, (int)pos.Y + tileSize, tileSize, tileSize), 0, SpriteEffects.FlipHorizontally);

            //inner
            data[13] = new StaticSprite(SpriteSheets.ENTITIES_TILES, new Rectangle((int)pos.X + 1 * tileSize, (int)pos.Y + tileSize, tileSize, tileSize), 0);
            //inner alt
            data[14] = new StaticSprite(SpriteSheets.ENTITIES_TILES, new Rectangle((int)pos.X + 2 * tileSize, (int)pos.Y + tileSize, tileSize, tileSize), 0);
            //pillar bottom
            data[15] = new StaticSprite(SpriteSheets.ENTITIES_TILES, new Rectangle((int)pos.X + 3 * tileSize, (int)pos.Y + tileSize, tileSize, tileSize), 0);


            return data;
        }

        public static StaticSprite[] PlatformSetCut(Vector2 pos, int tileSize = 32)
        {
            StaticSprite[] data = new StaticSprite[3];
            
            //left
            data[0] = new StaticSprite(SpriteSheets.ENTITIES_PLATFORMS, new Rectangle((int)pos.X + 0 * tileSize, (int)pos.Y, tileSize, tileSize), 0);

            //center
            data[1] = new StaticSprite(SpriteSheets.ENTITIES_PLATFORMS, new Rectangle((int)pos.X + 1 * tileSize, (int)pos.Y, tileSize, tileSize), 0);

            //right
            data[2] = new StaticSprite(SpriteSheets.ENTITIES_PLATFORMS, new Rectangle((int)pos.X + 0 * tileSize, (int)pos.Y, tileSize, tileSize), 0, SpriteEffects.FlipHorizontally);

            return data;
        }

        public static StaticSprite[] UIFrameCut(Vector2 pos, int tileSize)
        {
            StaticSprite[] data = new StaticSprite[9];

            data[0] = new StaticSprite(SpriteSheets.UI_FRAMES, new Rectangle((int)pos.X, (int)pos.Y, tileSize, tileSize), 0, SpriteEffects.FlipVertically); //left top corner
            data[1] = new StaticSprite(SpriteSheets.UI_FRAMES, new Rectangle((int)pos.X, (int)pos.Y, tileSize, tileSize), 0, SpriteEffects.FlipVertically | SpriteEffects.FlipHorizontally); //right top corner
            data[2] = new StaticSprite(SpriteSheets.UI_FRAMES, new Rectangle((int)pos.X, (int)pos.Y, tileSize, tileSize), 0); //left bottom corner
            data[3] = new StaticSprite(SpriteSheets.UI_FRAMES, new Rectangle((int)pos.X, (int)pos.Y, tileSize, tileSize), 0, SpriteEffects.FlipHorizontally); //right bottom corner

            data[4] = new StaticSprite(SpriteSheets.UI_FRAMES, new Rectangle((int)pos.X + tileSize, (int)pos.Y, tileSize, tileSize), 0, SpriteEffects.FlipVertically); //top border
            data[5] = new StaticSprite(SpriteSheets.UI_FRAMES, new Rectangle((int)pos.X + tileSize, (int)pos.Y, tileSize, tileSize), 0); //bottom border

            data[6] = new StaticSprite(SpriteSheets.UI_FRAMES, new Rectangle((int)pos.X, (int)pos.Y + tileSize, tileSize, tileSize), 0); //left border
            data[7] = new StaticSprite(SpriteSheets.UI_FRAMES, new Rectangle((int)pos.X, (int)pos.Y + tileSize, tileSize, tileSize), 0, SpriteEffects.FlipHorizontally); //right border

            data[8] = new StaticSprite(SpriteSheets.UI_FRAMES, new Rectangle((int)pos.X + tileSize, (int)pos.Y + tileSize, tileSize, tileSize), 0); //right border

            return data;
        }

        public static StaticSprite[] UIHUDStatBarCut(Vector2 pos, int tileSize)
        {
            StaticSprite[] data = new StaticSprite[4];

            data[0] = new StaticSprite(SpriteSheets.UI_HUD, new Rectangle((int)pos.X+tileSize*0, (int)pos.Y, tileSize, tileSize), 0);
            data[1] = new StaticSprite(SpriteSheets.UI_HUD, new Rectangle((int)pos.X + tileSize * 1, (int)pos.Y, tileSize, tileSize), 0);
            data[2] = new StaticSprite(SpriteSheets.UI_HUD, new Rectangle((int)pos.X + tileSize * 2, (int)pos.Y, tileSize, tileSize), 0);
            data[3] = new StaticSprite(SpriteSheets.UI_HUD, new Rectangle((int)pos.X + tileSize * 3, (int)pos.Y, tileSize, tileSize), 0);

            return data;
        }

        public static StaticSprite GetEntityParticle(Vector2 pos)
        {
            return new StaticSprite(SpriteSheets.ENTITIES_PARTICLES, new Rectangle((int)pos.X*16, (int)pos.Y*16, 16, 16), 0);
        }


        public static Dictionary<EquatableKey, Point> ItemUISpriteMappings = new()
        {
            { new EquatableKey(ItemLib.Weapons.TERRABLADE), new Point(0,0) },
            { new EquatableKey(ItemLib.Weapons.TORCH), new Point(1,0) },
            { new EquatableKey(ItemLib.Weapons.BOW), new Point(2,0) },

            //TODO: change to utils/none
            { new EquatableKey(ItemLib.Weapons.BARE_HAND), new Point(-1,-1) },

            { new EquatableKey(ItemLib.Chestplates.IRON_CHESTPLATE), new Point(0,1) },
            { new EquatableKey(ItemLib.Helmets.IRON_HELMET), new Point(0,2) },
            { new EquatableKey(ItemLib.Boots.IRON_BOOTS), new Point(0,3) },
            { new EquatableKey(ItemLib.Gloves.IRON_GLOVES), new Point(0,4) },
            { new EquatableKey(ItemLib.Necklaces.IRON_NECKLACE), new Point(0,5) },
            { new EquatableKey(ItemLib.Capes.LEATHER_CAPE), new Point(0,6) },
            { new EquatableKey(ItemLib.Belts.IRON_BELT), new Point(0,7) },
            { new EquatableKey(ItemLib.Rings.IRON_RING), new Point(0,8) },
            { new EquatableKey(ItemLib.Pets.CALL_DOG), new Point(0,9) },
            { new EquatableKey(ItemLib.LightPets.CALL_FIREFLY), new Point(0,10) },
            { new EquatableKey(ItemLib.Containments.BACKPACK), new Point(0,11) },
            { new EquatableKey(ItemLib.Consumables.HEALTH_POTION), new Point(0,12) },
            { new EquatableKey(ItemLib.Materials.SWORD_HILT), new Point(0,13) },
            { new EquatableKey(ItemLib.Keys.GOLDEN_KEY), new Point(0,14) },

            { new EquatableKey(ItemLib.QuestItems.NOTE), new Point(0,15) },
            { new EquatableKey(ItemLib.QuestItems.STONE), new Point(1,15) },
            { new EquatableKey(ItemLib.QuestItems.HEAD), new Point(2,15) },

            { new EquatableKey(ItemLib.Currencies.GOLD_COIN), new Point(0,16) },
        };

        public static StaticSprite GetItemUISprite(Item item)
        {
            if(item == null)
            {
                return StaticSpriteFactory.StaticSpriteMappings[StaticSprites.NONE];
            }

            return GetItemUISpriteByItemKey(item.ItemKey);
        }

        public static StaticSprite GetItemUISpriteByItemKey(EquatableKey itemKey)
        {
            StaticSprite data = new StaticSprite();

            Point iconSize = new Point(64, 64);
            Point spriteSheetLocation = ItemUISpriteMappings[itemKey];

            data = new StaticSprite(SpriteSheets.UI_ITEMS, new Rectangle(spriteSheetLocation * iconSize, iconSize), 100);

            return data;
        }


        public static readonly Dictionary<Graphics.ParallaxBackground.ParallaxBackgrounds, StaticSprite> BackgroundCanvasLayerSprites = new()
        {
            { 
                Graphics.ParallaxBackground.ParallaxBackgrounds.SEASIDE_EVENING, new StaticSprite(SpriteSheets.GRAPHICS_BG0_CANVAS, new Rectangle(0, 0, 1280, 720), -100)
            },
            {
                Graphics.ParallaxBackground.ParallaxBackgrounds.GREEN, new StaticSprite(SpriteSheets.GRAPHICS_BG1_CANVAS, new Rectangle(0, 0, 1280, 720), -100)
            },
        };

        public static readonly Dictionary<Graphics.ParallaxBackground.ParallaxBackgrounds, StaticSprite[]> BackgroundBackLayerSprites = new()
        {
            { 
                Graphics.ParallaxBackground.ParallaxBackgrounds.SEASIDE_EVENING,
                new StaticSprite[]
                {
                    new StaticSprite(SpriteSheets.GRAPHICS_BG0_B0, new Rectangle(0, 0, 1600, 720), -99),
                    new StaticSprite(SpriteSheets.GRAPHICS_BG0_B1, new Rectangle(0, 0, 1920, 720), -98),
                    new StaticSprite(SpriteSheets.GRAPHICS_BG0_B2, new Rectangle(0, 0, 2240, 720), -97)
                }
            },
            {
                Graphics.ParallaxBackground.ParallaxBackgrounds.GREEN,
                new StaticSprite[]
                {
                    new StaticSprite(SpriteSheets.GRAPHICS_BG1_B0, new Rectangle(0, 0, 1462, 720), -99),
                    new StaticSprite(SpriteSheets.GRAPHICS_BG1_B1, new Rectangle(0, 0, 1645, 720), -98),
                    new StaticSprite(SpriteSheets.GRAPHICS_BG1_B2, new Rectangle(0, 0, 1828, 720), -97),
                    new StaticSprite(SpriteSheets.GRAPHICS_BG1_B3, new Rectangle(0, 0, 2011, 720), -97),
                    new StaticSprite(SpriteSheets.GRAPHICS_BG1_B4, new Rectangle(0, 0, 2195, 720), -97),
                    new StaticSprite(SpriteSheets.GRAPHICS_BG1_B5, new Rectangle(0, 0, 2377, 720), -97)
                }
            }
        };

        public static readonly Dictionary<Graphics.ParallaxBackground.ParallaxBackgrounds, StaticSprite[]> BackgroundFrontLayerSprites = new()
        {
            { 
                Graphics.ParallaxBackground.ParallaxBackgrounds.SEASIDE_EVENING,
                new StaticSprite[]
                {
                    new StaticSprite(SpriteSheets.GRAPHICS_BG0_F0, new Rectangle(0, 0, 2560, 720), 1000)
                }
            },
            {
                Graphics.ParallaxBackground.ParallaxBackgrounds.GREEN,
                new StaticSprite[]
                {
                    new StaticSprite(SpriteSheets.GRAPHICS_BG1_F0, new Rectangle(0, 0, 2560, 720), 1000)
                }
            }
        };

        public static readonly Dictionary<StaticSprites, StaticSprite> StaticSpriteMappings = new()
        {
            { StaticSprites.NONE, new StaticSprite(SpriteSheets.NONE, new Rectangle(0,0,0,0), 0) },

            { StaticSprites.GRAPHICS_CLOUD_0, new StaticSprite(SpriteSheets.GRAPHICS_CLOUDS, new Rectangle(0,0,360,128), 2) },
            { StaticSprites.GRAPHICS_SUN, new StaticSprite(SpriteSheets.GRAPHICS_SUN, new Rectangle(0,0,64,64), 0) },

            { StaticSprites.GRAPHICS_MOON, new StaticSprite(SpriteSheets.GRAPHICS_MOON, new Rectangle(0,0,64,64), 0) },
            { StaticSprites.GRAPHICS_STATIC_DRAGON, new StaticSprite(SpriteSheets.GRAPHICS_STATIC, new Rectangle(0,0,128,64), 1) },

            { StaticSprites.ENTITIES_STATIC_BALL, new StaticSprite(SpriteSheets.ENTITIES_STATIC, new Rectangle(0, 0, 64, 64), 98)},
            { StaticSprites.ENTITIES_STATIC_CRATE_0, new StaticSprite(SpriteSheets.ENTITIES_STATIC, new Rectangle(0, 64, 64, 64), 98)},
            { StaticSprites.ENTITIES_STATIC_CRATE_1, new StaticSprite(SpriteSheets.ENTITIES_STATIC, new Rectangle(64, 64, 16, 16), 98)},

            { StaticSprites.ENTITIES_LEDGE, new StaticSprite(SpriteSheets.ENTITIES_LEDGES, new Rectangle(0, 0, 32, 32), 98)},

            { StaticSprites.ENTITIES_HUMAN_M, new StaticSprite(SpriteSheets.ENTITIES_HUMAN_M, new Rectangle(0,0,64,128), 100)},
            { StaticSprites.ENTITIES_SLIME, new StaticSprite(SpriteSheets.ENTITIES_SLIME, new Rectangle(0,0,64,64), 99)},
            { StaticSprites.ENTITIES_BAT, new StaticSprite(SpriteSheets.ENTITIES_BAT, new Rectangle(0,64,64,64), 99)},

            { StaticSprites.ENTITIES_FIREBALL, new StaticSprite(SpriteSheets.ENITIES_FIREBALL, new Rectangle(0,0,64,64), 99)},
            { StaticSprites.ENTITIES_ARROW, new StaticSprite(SpriteSheets.ENITIES_ARROW, new Rectangle(0,0,64,64), 99)},

            { StaticSprites.LIGHT_DARKNESS_FULL, new StaticSprite(SpriteSheets.LIGHT_DARKNESS_FULL, new Rectangle(0,0,80,64), 200) },
            { StaticSprites.LIGHT_DARKNESS_VIGNETTE, new StaticSprite(SpriteSheets.LIGHT_DARKNESS_MIN, new Rectangle(0,0,320,180), 200) },

            { StaticSprites.UI_GAME_ICON, new StaticSprite(SpriteSheets.UI_GAME_ICON, new Rectangle(0,0,64,64), 0) },
            { StaticSprites.UI_CURSOR, new StaticSprite(SpriteSheets.UI_CURSOR, new Rectangle(0,0,32,32), 0) },

            { StaticSprites.ENTITIES_WEAPONS_TERRABLADE, new StaticSprite(SpriteSheets.ENTITIES_WEAPONS_TERRABLADE, new Rectangle(0,0,32,64), 200) },
            { StaticSprites.ENTITIES_WEAPONS_TORCH, new StaticSprite(SpriteSheets.ENTITIES_WEAPONS_TORCH, new Rectangle(0,0,32,64), 200) }
        };

    }
}
